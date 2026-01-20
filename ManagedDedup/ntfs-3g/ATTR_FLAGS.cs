namespace Ntfs3gInterop
{
    public enum ATTR_FLAGS
    {
        ATTR_IS_COMPRESSED = unchecked((int)((ushort)(0x0001))),
        ATTR_COMPRESSION_MASK = unchecked((int)((ushort)(0x00ff))),
        ATTR_IS_ENCRYPTED = unchecked((int)((ushort)(0x4000))),
        ATTR_IS_SPARSE = unchecked((int)((ushort)(0x8000))),
    }
}
