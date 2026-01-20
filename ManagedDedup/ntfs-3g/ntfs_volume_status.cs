namespace Ntfs3gInterop
{
    public enum ntfs_volume_status
    {
        NTFS_VOLUME_OK = 0,
        NTFS_VOLUME_SYNTAX_ERROR = 11,
        NTFS_VOLUME_NOT_NTFS = 12,
        NTFS_VOLUME_CORRUPT = 13,
        NTFS_VOLUME_HIBERNATED = 14,
        NTFS_VOLUME_UNCLEAN_UNMOUNT = 15,
        NTFS_VOLUME_LOCKED = 16,
        NTFS_VOLUME_RAID = 17,
        NTFS_VOLUME_UNKNOWN_REASON = 18,
        NTFS_VOLUME_NO_PRIVILEGE = 19,
        NTFS_VOLUME_OUT_OF_MEMORY = 20,
        NTFS_VOLUME_FUSE_ERROR = 21,
        NTFS_VOLUME_INSECURE = 22,
    }
}
