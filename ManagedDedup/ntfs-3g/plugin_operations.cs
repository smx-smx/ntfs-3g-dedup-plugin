namespace Ntfs3gInterop
{
    public unsafe partial struct plugin_operations
    {
        [NativeTypeName("int (*)(ntfs_inode *, REPARSE_POINT *, struct stat *)")]
        public delegate* unmanaged[Cdecl]<_ntfs_inode*, REPARSE_POINT*, stat*, int> getattr;

        [NativeTypeName("int (*)(ntfs_inode *, REPARSE_POINT *, struct fuse_file_info *)")]
        public delegate* unmanaged[Cdecl]<_ntfs_inode*, REPARSE_POINT*, fuse_file_info*, int> open;

        [NativeTypeName("int (*)(ntfs_inode *, REPARSE_POINT *, struct fuse_file_info *)")]
        public delegate* unmanaged[Cdecl]<_ntfs_inode*, REPARSE_POINT*, fuse_file_info*, int> release;

        [NativeTypeName("int (*)(ntfs_inode *, REPARSE_POINT *, char *, size_t, off_t, struct fuse_file_info *)")]
        public delegate* unmanaged[Cdecl]<_ntfs_inode*, REPARSE_POINT*, sbyte*, nuint, long, fuse_file_info*, int> read;

        [NativeTypeName("int (*)(ntfs_inode *, REPARSE_POINT *, char *, size_t, off_t, struct fuse_file_info *)")]
        public delegate* unmanaged[Cdecl]<_ntfs_inode*, REPARSE_POINT*, sbyte*, nuint, long, fuse_file_info*, int> write;

        [NativeTypeName("int (*)(ntfs_inode *, REPARSE_POINT *, char **)")]
        public delegate* unmanaged[Cdecl]<_ntfs_inode*, REPARSE_POINT*, sbyte**, int> readlink;

        [NativeTypeName("int (*)(ntfs_inode *, REPARSE_POINT *, off_t)")]
        public delegate* unmanaged[Cdecl]<_ntfs_inode*, REPARSE_POINT*, long, int> truncate;

        [NativeTypeName("int (*)(ntfs_inode *, REPARSE_POINT *, struct fuse_file_info *)")]
        public delegate* unmanaged[Cdecl]<_ntfs_inode*, REPARSE_POINT*, fuse_file_info*, int> opendir;

        [NativeTypeName("int (*)(ntfs_inode *, REPARSE_POINT *, s64 *, void *, ntfs_filldir_t, struct fuse_file_info *)")]
        public delegate* unmanaged[Cdecl]<_ntfs_inode*, REPARSE_POINT*, long*, void*, delegate* unmanaged[Cdecl]<void*, ushort*, int, int, long, ulong, uint, int>, fuse_file_info*, int> readdir;

        [NativeTypeName("ntfs_inode *(*)(ntfs_inode *, REPARSE_POINT *, le32, ntfschar *, int, mode_t)")]
        public delegate* unmanaged[Cdecl]<_ntfs_inode*, REPARSE_POINT*, uint, ushort*, int, uint, _ntfs_inode*> create;

        [NativeTypeName("int (*)(ntfs_inode *, REPARSE_POINT *, ntfs_inode *, ntfschar *, int)")]
        public delegate* unmanaged[Cdecl]<_ntfs_inode*, REPARSE_POINT*, _ntfs_inode*, ushort*, int, int> link;

        [NativeTypeName("int (*)(ntfs_inode *, REPARSE_POINT *, char *, ntfs_inode *, ntfschar *, int)")]
        public delegate* unmanaged[Cdecl]<_ntfs_inode*, REPARSE_POINT*, sbyte*, _ntfs_inode*, ushort*, int, int> unlink;
    }
}
