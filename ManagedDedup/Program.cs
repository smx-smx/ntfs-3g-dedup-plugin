#region License
/*
 * Copyright (C) 2026 Stefano Moioli <smxdev4@gmail.com>
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */
#endregion
using ManagedDedup;
using ManagedDedup.Cygwin;
using Microsoft.Win32.SafeHandles;
using Ntfs3gInterop;
using Smx.SharpIO.Memory;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using Windows.Win32;
using Windows.Win32.Security;
using Windows.Win32.Storage.FileSystem;

namespace Smx.ManagedDedup;

public unsafe struct ProgramArgv
{
    public delegate* unmanaged[Cdecl]<uint, plugin_operations*> InitFn;
    public delegate* unmanaged[Cdecl]<int, void> SetErrno;
}

public class Program
{
    private string[] _args;
    private readonly TypedPointer<ProgramArgv> _argsNative;

    private delegate void MainDelegate(string[] args);

    private static bool IsRunningInCygwin()
    {
        return !PInvoke.GetModuleHandle("cygwin1").IsInvalid;
    }

    private static string[] ReadArgv(IntPtr args, int sizeBytes)
    {
        int nargs = sizeBytes / IntPtr.Size;
        string[] argv = new string[nargs];

        for (int i = 0; i < nargs; i++, args += IntPtr.Size)
        {
            IntPtr charPtr = Marshal.ReadIntPtr(args);
            argv[i] = Marshal.PtrToStringAnsi(charPtr);
        }
        return argv;
    }

    public static int Entry(IntPtr args, int sizeBytes)
    {
        var argv = ReadArgv(args, sizeBytes);

        Action<MainDelegate> initializer;

        if (
            RuntimeInformation.IsOSPlatform(OSPlatform.Windows) &&
            IsRunningInCygwin()
        )
        {
            initializer = (main) => {
                var stdin = new StreamReader(new CygwinInputStream(0));
                var stdout = new StreamWriter(new CygwinOutputStream(1));
                var stderr = new StreamWriter(new CygwinOutputStream(2));

                stdout.AutoFlush = true;
                stderr.AutoFlush = true;

                _pinnedObjects.Add(stdin);
                _pinnedObjects.Add(stdout);
                _pinnedObjects.Add(stderr);

                Console.SetIn(stdin);
                Console.SetOut(stdout);
                Console.SetError(stderr);

                main(argv);

            };
        } else
        {
            initializer = (main) => {
                main(argv);
            };
        }

        try
        {
            initializer(Main);
        } catch (Exception e)
        {
            Console.Error.WriteLine("Unhandled Exception");
            Console.Error.WriteLine(e.ToString());
        }
        return 0;
    }

    private static SafeFileHandle OpenProcessToken(SafeFileHandle hProc, TOKEN_ACCESS_MASK flags)
    {
        PInvoke.OpenProcessToken(hProc, flags, out var hToken);
        return hToken;
    }

    private static void EnablePrivilege(string privilegeName)
    {
        using var hProc = PInvoke.GetCurrentProcess_SafeHandle();
        if (hProc == null) throw new InvalidOperationException();
        using var hToken = OpenProcessToken(hProc, TOKEN_ACCESS_MASK.TOKEN_QUERY | TOKEN_ACCESS_MASK.TOKEN_ADJUST_PRIVILEGES);
        if (hToken == null) throw new InvalidOperationException();

        if (!PInvoke.LookupPrivilegeValue(null, privilegeName, out var luid))
        {
            throw new Win32Exception();
        }

        var tp = new TOKEN_PRIVILEGES
        {
            PrivilegeCount = 1,
            Privileges = new VariableLengthInlineArray<LUID_AND_ATTRIBUTES>
            {
                e0 = new LUID_AND_ATTRIBUTES
                {
                    Luid = luid,
                    Attributes = TOKEN_PRIVILEGES_ATTRIBUTES.SE_PRIVILEGE_ENABLED
                }
            }
        };

        unsafe
        {
            if(!PInvoke.AdjustTokenPrivileges(
                hToken, false, &tp,null))
            {
                throw new Win32Exception();
            }
        }
    }

    /// <summary>
    /// stores references to objects we don't want to be GC'd
    /// </summary>
    private static List<object> _pinnedObjects = new List<object>();

    public Program(string[] args)
    {
        _args = args;
        Console.WriteLine("args:");
        for(var i=0; i<args.Length; i++)
        {
            Console.WriteLine($"[{i}]: {args[i]}");
        }

        if (args[0] == "--ezdotnet")
        {
            _pinnedObjects.Add(this);

            if (Environment.GetEnvironmentVariable("EZ_DEBUG")?.Equals("1") ?? false)
            {
                if (!Debugger.IsAttached && Debugger.Launch())
                {
                    while (!Debugger.IsAttached)
                    {
                        Thread.Sleep(200);
                    }
                }
            }

            var argsPtr = new TypedPointer<ProgramArgv>(nint.Parse(args[1].Replace("0x", ""), System.Globalization.NumberStyles.HexNumber));
            _argsNative = argsPtr;

            var plugin = new Ntfs3gDedupPlugin(argsPtr);
            _pinnedObjects.Add(plugin);
        }
    }

    public static void Main(string[] args)
    {
       new Program(args).Run();
    }

    private void ReadReparsePoint(string filePath)
    {
        Console.WriteLine($"=> {filePath}");
        using var hFile = PInvoke.CreateFile(
            filePath, 0, FILE_SHARE_MODE.FILE_SHARE_NONE,
            null, FILE_CREATION_DISPOSITION.OPEN_EXISTING,
            FILE_FLAGS_AND_ATTRIBUTES.FILE_FLAG_OPEN_REPARSE_POINT | FILE_FLAGS_AND_ATTRIBUTES.FILE_FLAG_BACKUP_SEMANTICS,
            null);

        if (hFile.IsInvalid)
        {
            throw new Win32Exception("Failed to open handle to reparse point.");
        }

        var backingStore = new byte[(int)PInvoke.MAXIMUM_REPARSE_DATA_BUFFER_SIZE];
        var buf = new Memory<byte>(backingStore);
        unsafe
        {
            if (!PInvoke.DeviceIoControl(
                hFile,
                PInvoke.FSCTL_GET_REPARSE_POINT,
                null,
                buf.Span,
                null))
            {
                //throw new Win32Exception("Failed to read Reparse Point");
                return;
            }
        }

        int dataLength = 0;
        using (var rdr = new BinaryReader(new MemoryStream(backingStore)))
        {
            rdr.BaseStream.Position = 4;
            dataLength = rdr.ReadInt32();
        }
        var sliceSize = Math.Min(backingStore.Length, dataLength + 8);


        var drivePath = Path.GetPathRoot(filePath);
        if (drivePath == null)
        {
            throw new InvalidOperationException("GetPathRoot failed");
        }

        var parser = new DedupReparsePointParser(drivePath);
        parser.Parse(buf.Slice(0, sliceSize));
    }

    private void Run()
    {
        // Enable SE_BACKUP_NAME to access "System Volume Information"
        EnablePrivilege(PInvoke.SE_BACKUP_NAME);

        if(_argsNative.Address != 0)
        {
            // running in hosted mode, abort
            return;
        }

        using var fh = new FileStream("log.txt", FileMode.OpenOrCreate, FileAccess.Write, FileShare.Read);
        fh.SetLength(0);

        using var wr = new StreamWriter(fh, new UTF8Encoding(false));
        wr.AutoFlush = true;
        Console.SetOut(wr);

        var filePath = _args[0];

        if (Directory.Exists(filePath))
        {
            foreach(var file in Directory.EnumerateFiles(filePath, "*.*", new EnumerationOptions
            {
                RecurseSubdirectories = true,
                ReturnSpecialDirectories = false,
                IgnoreInaccessible = true
            }))
            {
                ReadReparsePoint(file);
            }
        } else
        {
            ReadReparsePoint(filePath);
        }
        
    }
}