using System.Runtime.InteropServices;

namespace Ntfs3gInterop
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public partial struct ACL
    {
        [NativeTypeName("u8")]
        public byte revision;

        [NativeTypeName("u8")]
        public byte alignment1;

        [NativeTypeName("le16")]
        public ushort size;

        [NativeTypeName("le16")]
        public ushort ace_count;

        [NativeTypeName("le16")]
        public ushort alignment2;
    }
}
