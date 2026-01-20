namespace Ntfs3gInterop
{
    public unsafe partial struct ntfs_device_operations
    {
        [NativeTypeName("int (*)(struct ntfs_device *, int)")]
        public delegate* unmanaged[Cdecl]<ntfs_device*, int, int> open;

        [NativeTypeName("int (*)(struct ntfs_device *)")]
        public delegate* unmanaged[Cdecl]<ntfs_device*, int> close;

        [NativeTypeName("s64 (*)(struct ntfs_device *, s64, int)")]
        public delegate* unmanaged[Cdecl]<ntfs_device*, long, int, long> seek;

        [NativeTypeName("s64 (*)(struct ntfs_device *, void *, s64)")]
        public delegate* unmanaged[Cdecl]<ntfs_device*, void*, long, long> read;

        [NativeTypeName("s64 (*)(struct ntfs_device *, void *, s64)")]
        public delegate* unmanaged[Cdecl]<ntfs_device*, void*, long, long> write;

        [NativeTypeName("s64 (*)(struct ntfs_device *, void *, s64, s64)")]
        public delegate* unmanaged[Cdecl]<ntfs_device*, void*, long, long, long> pread;

        [NativeTypeName("s64 (*)(struct ntfs_device *, void *, s64, s64)")]
        public delegate* unmanaged[Cdecl]<ntfs_device*, void*, long, long, long> pwrite;

        [NativeTypeName("int (*)(struct ntfs_device *)")]
        public delegate* unmanaged[Cdecl]<ntfs_device*, int> sync;

        [NativeTypeName("int (*)(struct ntfs_device *, struct stat *)")]
        public delegate* unmanaged[Cdecl]<ntfs_device*, stat*, int> stat;

        [NativeTypeName("int (*)(struct ntfs_device *, unsigned long, void *)")]
        public delegate* unmanaged[Cdecl]<ntfs_device*, nuint, void*, int> ioctl;
    }
}
