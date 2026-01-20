using System.Runtime.InteropServices;

namespace Ntfs3gInterop
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public partial struct EA_INFORMATION
    {
        [NativeTypeName("le16")]
        public ushort ea_length;

        [NativeTypeName("le16")]
        public ushort need_ea_count;

        [NativeTypeName("le32")]
        public uint ea_query_length;
    }
}
