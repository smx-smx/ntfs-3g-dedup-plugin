namespace Ntfs3gInterop
{
    public enum ntfs_time_update_flags
    {
        NTFS_UPDATE_ATIME = 1 << 0,
        NTFS_UPDATE_MTIME = 1 << 1,
        NTFS_UPDATE_CTIME = 1 << 2,
    }
}
