namespace Ntfs3gInterop
{
    public enum MFT_RECORD_FLAGS
    {
        MFT_RECORD_IN_USE = unchecked((int)((ushort)(0x0001))),
        MFT_RECORD_IS_DIRECTORY = unchecked((int)((ushort)(0x0002))),
        MFT_RECORD_IS_4 = unchecked((int)((ushort)(0x0004))),
        MFT_RECORD_IS_VIEW_INDEX = unchecked((int)((ushort)(0x0008))),
        MFT_REC_SPACE_FILLER = 0xffff,
    }
}
