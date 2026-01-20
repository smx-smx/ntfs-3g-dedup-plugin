using System.Runtime.InteropServices;

namespace Ntfs3gInterop
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public partial struct EFS_DF_HEADER
    {
        [NativeTypeName("le32")]
        public uint df_length;

        [NativeTypeName("le32")]
        public uint cred_header_offset;

        [NativeTypeName("le32")]
        public uint fek_size;

        [NativeTypeName("le32")]
        public uint fek_offset;

        [NativeTypeName("le32")]
        public uint unknown1;
    }
}
