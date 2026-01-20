using Ntfs3gInterop;
using Smx.ManagedDedup;
using Smx.SharpIO;
using Smx.SharpIO.Memory;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Win32;

namespace ManagedDedup
{
    public unsafe class Ntfs3gDedupPlugin
    {
        private static Ntfs3gDedupPlugin? _instance;

        private static Ntfs3gDedupPlugin Instance
        {
            get
            {
                if(_instance == null)
                {
                    throw new InvalidOperationException(nameof(_instance));
                }
                return _instance;
            }
        }

        public plugin_operations* Operations => (plugin_operations *)_opsHandle.AddrOfPinnedObject().ToPointer();

        private plugin_operations _ops;

        [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvCdecl) })]
        private static unsafe int DedupGetAttr(_ntfs_inode* inode, REPARSE_POINT* rp, stat* st)
        {
            return Instance.DedupGetAttr(
                new TypedPointer<_ntfs_inode>(new nint(inode)),
                new TypedPointer<REPARSE_POINT>(new nint(rp)),
                new TypedPointer<stat>(new nint(st))
            );
        }

        [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvCdecl) })]
        private static unsafe int DedupRead(
            _ntfs_inode* inode, REPARSE_POINT* rp, sbyte* buf,
            nuint size, long offset, fuse_file_info* fi)
        {
            return Instance.DedupRead(
                new TypedPointer<_ntfs_inode>(new nint(inode)),
                new TypedPointer<REPARSE_POINT>(new nint(rp)),
                new Span<byte>(buf, (int)size),
                offset,
                new TypedPointer<fuse_file_info>(new nint(fi))
            );
        }

        [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvCdecl) })]
        private static unsafe int DedupOpen(
            _ntfs_inode* inode, REPARSE_POINT*rp, fuse_file_info* fi
        )
        {
            return Instance.DedupOpen(
                new TypedPointer<_ntfs_inode>(new nint(inode)),
                new TypedPointer<REPARSE_POINT>(new nint(rp)),
                new TypedPointer<fuse_file_info>(new nint(fi)));
        }

        [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvCdecl) })]
        private static unsafe int DedupRelease(
            _ntfs_inode* inode, REPARSE_POINT* rp, fuse_file_info* fi
        )
        {
            Console.WriteLine($"[DedupRelease]");
            return 0;
        }

        private readonly DisposableGCHandle _opsHandle;
        private static delegate* unmanaged[Cdecl]<int, void> _setErrno;

        public Ntfs3gDedupPlugin(TypedPointer<ProgramArgv> args)
        {
            if(_instance != null)
            {
                throw new InvalidOperationException();
            }
            _instance = this;
            _ops = new plugin_operations
            {
                getattr = &DedupGetAttr,
                read = &DedupRead,
                open = &DedupOpen,
                release = &DedupRelease
            };
            _opsHandle = DisposableGCHandle.Pin(_ops);
            args.Value.InitFn = &Initialize;

            _setErrno = args.Value.SetErrno;
        }

        private static void SetErrno(int errno)
        {
            _setErrno(errno);
        }

        [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvCdecl) })]
        private static unsafe plugin_operations* Initialize(uint tag)
        {
            if(tag != DedupReparsePointParser.DEDUP_REPARSE_TAG)
            {
                SetErrno(-(int)Errno.EINVAL);
                return null;
            }
            return Instance.Operations;
        }

        private int DedupRead(
            TypedPointer<_ntfs_inode> ni,
            TypedPointer<REPARSE_POINT> reparse,
            Span<byte> buf,
            long offset,
            TypedPointer<fuse_file_info> fi
        )
        {
            Console.WriteLine($"[DedupRead]");
            return -(int)Errno.EINVAL;
        }

        private string? _volumeMountPoint = null;

        private int DedupGetAttr(
            TypedPointer<_ntfs_inode> ni,
            TypedPointer<REPARSE_POINT> reparse,
            TypedPointer<stat> stbuf
        )
        {
            Console.WriteLine($"[DedupGetAttr]");
            return -(int)Errno.EINVAL;
        }

        private int DedupOpen(
            TypedPointer<_ntfs_inode> ni,
            TypedPointer<REPARSE_POINT> reparse,
            TypedPointer<fuse_file_info> fi
        )
        {
            Console.WriteLine($"[DedupOpen]");
            var reparseBytes = new Span<byte>(reparse.ToPointer(), (int)PInvoke.MAXIMUM_REPARSE_DATA_BUFFER_SIZE).ToArray();
            return -(int)Errno.EINVAL;
        }
    }
}
