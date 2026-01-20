namespace Ntfs3gInterop
{
    public partial struct stat
    {
        [NativeTypeName("dev_t")]
        public uint st_dev;

        [NativeTypeName("ino_t")]
        public nuint st_ino;

        [NativeTypeName("mode_t")]
        public uint st_mode;

        [NativeTypeName("nlink_t")]
        public ushort st_nlink;

        [NativeTypeName("uid_t")]
        public uint st_uid;

        [NativeTypeName("gid_t")]
        public uint st_gid;

        [NativeTypeName("dev_t")]
        public uint st_rdev;

        [NativeTypeName("off_t")]
        public nint st_size;

        [NativeTypeName("timestruc_t")]
        public timespec st_atim;

        [NativeTypeName("timestruc_t")]
        public timespec st_mtim;

        [NativeTypeName("timestruc_t")]
        public timespec st_ctim;

        [NativeTypeName("blksize_t")]
        public int st_blksize;

        [NativeTypeName("blkcnt_t")]
        public nint st_blocks;

        [NativeTypeName("timestruc_t")]
        public timespec st_birthtim;
    }
}
