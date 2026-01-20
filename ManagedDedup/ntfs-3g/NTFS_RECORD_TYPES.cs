namespace Ntfs3gInterop
{
    public enum NTFS_RECORD_TYPES
    {
        magic_FILE = unchecked((int)((uint)(0x454c4946))),
        magic_INDX = unchecked((int)((uint)(0x58444e49))),
        magic_HOLE = unchecked((int)((uint)(0x454c4f48))),
        magic_RSTR = unchecked((int)((uint)(0x52545352))),
        magic_RCRD = unchecked((int)((uint)(0x44524352))),
        magic_CHKD = unchecked((int)((uint)(0x444b4843))),
        magic_BAAD = unchecked((int)((uint)(0x44414142))),
        magic_empty = unchecked((int)((uint)(0xffffffff))),
    }
}
