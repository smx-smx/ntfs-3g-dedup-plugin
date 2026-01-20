namespace Ntfs3gInterop
{
    public enum ATTR_DEF_FLAGS
    {
        ATTR_DEF_INDEXABLE = unchecked((int)((uint)(0x02))),
        ATTR_DEF_MULTIPLE = unchecked((int)((uint)(0x04))),
        ATTR_DEF_NOT_ZERO = unchecked((int)((uint)(0x08))),
        ATTR_DEF_INDEXED_UNIQUE = unchecked((int)((uint)(0x10))),
        ATTR_DEF_NAMED_UNIQUE = unchecked((int)((uint)(0x20))),
        ATTR_DEF_RESIDENT = unchecked((int)((uint)(0x40))),
        ATTR_DEF_ALWAYS_LOG = unchecked((int)((uint)(0x80))),
    }
}
