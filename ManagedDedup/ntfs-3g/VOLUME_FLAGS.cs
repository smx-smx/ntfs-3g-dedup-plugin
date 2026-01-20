namespace Ntfs3gInterop
{
    public enum VOLUME_FLAGS
    {
        VOLUME_IS_DIRTY = unchecked((int)((ushort)(0x0001))),
        VOLUME_RESIZE_LOG_FILE = unchecked((int)((ushort)(0x0002))),
        VOLUME_UPGRADE_ON_MOUNT = unchecked((int)((ushort)(0x0004))),
        VOLUME_MOUNTED_ON_NT4 = unchecked((int)((ushort)(0x0008))),
        VOLUME_DELETE_USN_UNDERWAY = unchecked((int)((ushort)(0x0010))),
        VOLUME_REPAIR_OBJECT_ID = unchecked((int)((ushort)(0x0020))),
        VOLUME_CHKDSK_UNDERWAY = unchecked((int)((ushort)(0x4000))),
        VOLUME_MODIFIED_BY_CHKDSK = unchecked((int)((ushort)(0x8000))),
        VOLUME_FLAGS_MASK = unchecked((int)((ushort)(0xc03f))),
    }
}
