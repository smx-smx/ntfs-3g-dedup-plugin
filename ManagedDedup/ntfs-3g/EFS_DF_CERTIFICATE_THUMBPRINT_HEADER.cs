using System.Runtime.InteropServices;

namespace Ntfs3gInterop
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public partial struct EFS_DF_CERTIFICATE_THUMBPRINT_HEADER
    {
        [NativeTypeName("le32")]
        public uint thumbprint_offset;

        [NativeTypeName("le32")]
        public uint thumbprint_size;

        [NativeTypeName("le32")]
        public uint container_name_offset;

        [NativeTypeName("le32")]
        public uint provider_name_offset;

        [NativeTypeName("le32")]
        public uint user_name_offset;
    }
}
