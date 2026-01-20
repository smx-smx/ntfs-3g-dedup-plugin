#region License
/*
 * Copyright (C) 2026 Stefano Moioli <smxdev4@gmail.com>
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */
#endregion
using ManagedDedup;
using Microsoft.Win32.SafeHandles;
using System;
using System.ComponentModel;
using System.IO;
using System.Text;
using Windows.Win32;
using Windows.Win32.Security;
using Windows.Win32.Storage.FileSystem;

namespace Smx.ManagedDedup;

public class Program
{
    private string[] _args;

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

    public Program(string[] args)
    {
        _args = args;
        Console.WriteLine("args:");
        for(var i=0; i<args.Length; i++)
        {
            Console.WriteLine($"[{i}]: {args[i]}");
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