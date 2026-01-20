using System.Runtime.InteropServices;

namespace Ntfs3gInterop
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public partial struct NTFS_RECORD
    {
        public NTFS_RECORD_TYPES magic;

        [NativeTypeName("le16")]
        public ushort usa_ofs;

        [NativeTypeName("le16")]
        public ushort usa_count;
    }
}
