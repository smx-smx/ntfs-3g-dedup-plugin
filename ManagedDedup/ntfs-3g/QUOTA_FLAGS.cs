namespace Ntfs3gInterop
{
    public enum QUOTA_FLAGS
    {
        QUOTA_FLAG_DEFAULT_LIMITS = unchecked((int)((uint)(0x00000001))),
        QUOTA_FLAG_LIMIT_REACHED = unchecked((int)((uint)(0x00000002))),
        QUOTA_FLAG_ID_DELETED = unchecked((int)((uint)(0x00000004))),
        QUOTA_FLAG_USER_MASK = unchecked((int)((uint)(0x00000007))),
        QUOTA_FLAG_TRACKING_ENABLED = unchecked((int)((uint)(0x00000010))),
        QUOTA_FLAG_ENFORCEMENT_ENABLED = unchecked((int)((uint)(0x00000020))),
        QUOTA_FLAG_TRACKING_REQUESTED = unchecked((int)((uint)(0x00000040))),
        QUOTA_FLAG_LOG_THRESHOLD = unchecked((int)((uint)(0x00000080))),
        QUOTA_FLAG_LOG_LIMIT = unchecked((int)((uint)(0x00000100))),
        QUOTA_FLAG_OUT_OF_DATE = unchecked((int)((uint)(0x00000200))),
        QUOTA_FLAG_CORRUPT = unchecked((int)((uint)(0x00000400))),
        QUOTA_FLAG_PENDING_DELETES = unchecked((int)((uint)(0x00000800))),
    }
}
