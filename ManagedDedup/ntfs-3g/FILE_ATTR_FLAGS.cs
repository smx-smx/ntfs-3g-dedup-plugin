namespace Ntfs3gInterop
{
    public enum FILE_ATTR_FLAGS
    {
        FILE_ATTR_READONLY = unchecked((int)((uint)(0x00000001))),
        FILE_ATTR_HIDDEN = unchecked((int)((uint)(0x00000002))),
        FILE_ATTR_SYSTEM = unchecked((int)((uint)(0x00000004))),
        FILE_ATTR_DIRECTORY = unchecked((int)((uint)(0x00000010))),
        FILE_ATTR_ARCHIVE = unchecked((int)((uint)(0x00000020))),
        FILE_ATTR_DEVICE = unchecked((int)((uint)(0x00000040))),
        FILE_ATTR_NORMAL = unchecked((int)((uint)(0x00000080))),
        FILE_ATTR_TEMPORARY = unchecked((int)((uint)(0x00000100))),
        FILE_ATTR_SPARSE_FILE = unchecked((int)((uint)(0x00000200))),
        FILE_ATTR_REPARSE_POINT = unchecked((int)((uint)(0x00000400))),
        FILE_ATTR_COMPRESSED = unchecked((int)((uint)(0x00000800))),
        FILE_ATTR_OFFLINE = unchecked((int)((uint)(0x00001000))),
        FILE_ATTR_NOT_CONTENT_INDEXED = unchecked((int)((uint)(0x00002000))),
        FILE_ATTR_ENCRYPTED = unchecked((int)((uint)(0x00004000))),
        FILE_ATTRIBUTE_RECALL_ON_OPEN = unchecked((int)((uint)(0x00040000))),
        FILE_ATTR_VALID_FLAGS = unchecked((int)((uint)(0x00047fb7))),
        FILE_ATTR_VALID_SET_FLAGS = unchecked((int)((uint)(0x000031a7))),
        FILE_ATTR_I30_INDEX_PRESENT = unchecked((int)((uint)(0x10000000))),
        FILE_ATTR_VIEW_INDEX_PRESENT = unchecked((int)((uint)(0x20000000))),
    }
}
