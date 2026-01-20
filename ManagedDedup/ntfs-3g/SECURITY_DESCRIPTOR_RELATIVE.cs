using System.Runtime.InteropServices;

namespace Ntfs3gInterop
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public partial struct SECURITY_DESCRIPTOR_RELATIVE
    {
        [NativeTypeName("u8")]
        public byte revision;

        [NativeTypeName("u8")]
        public byte alignment;

        public SECURITY_DESCRIPTOR_CONTROL control;

        [NativeTypeName("le32")]
        public uint owner;

        [NativeTypeName("le32")]
        public uint group;

        [NativeTypeName("le32")]
        public uint sacl;

        [NativeTypeName("le32")]
        public uint dacl;
    }
}
