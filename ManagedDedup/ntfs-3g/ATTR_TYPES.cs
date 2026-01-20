namespace Ntfs3gInterop
{
    public enum ATTR_TYPES
    {
        AT_UNUSED = unchecked((int)((uint)(0))),
        AT_STANDARD_INFORMATION = unchecked((int)((uint)(0x10))),
        AT_ATTRIBUTE_LIST = unchecked((int)((uint)(0x20))),
        AT_FILE_NAME = unchecked((int)((uint)(0x30))),
        AT_OBJECT_ID = unchecked((int)((uint)(0x40))),
        AT_SECURITY_DESCRIPTOR = unchecked((int)((uint)(0x50))),
        AT_VOLUME_NAME = unchecked((int)((uint)(0x60))),
        AT_VOLUME_INFORMATION = unchecked((int)((uint)(0x70))),
        AT_DATA = unchecked((int)((uint)(0x80))),
        AT_INDEX_ROOT = unchecked((int)((uint)(0x90))),
        AT_INDEX_ALLOCATION = unchecked((int)((uint)(0xa0))),
        AT_BITMAP = unchecked((int)((uint)(0xb0))),
        AT_REPARSE_POINT = unchecked((int)((uint)(0xc0))),
        AT_EA_INFORMATION = unchecked((int)((uint)(0xd0))),
        AT_EA = unchecked((int)((uint)(0xe0))),
        AT_PROPERTY_SET = unchecked((int)((uint)(0xf0))),
        AT_LOGGED_UTILITY_STREAM = unchecked((int)((uint)(0x100))),
        AT_FIRST_USER_DEFINED_ATTRIBUTE = unchecked((int)((uint)(0x1000))),
        AT_END = unchecked((int)((uint)(0xffffffff))),
    }
}
