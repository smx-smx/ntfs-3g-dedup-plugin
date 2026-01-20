using System.Runtime.InteropServices;

namespace Ntfs3gInterop
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public partial struct REPARSE_INDEX_KEY
    {
        [NativeTypeName("le32")]
        public uint reparse_tag;

        [NativeTypeName("leMFT_REF")]
        public ulong file_id;
    }
}
