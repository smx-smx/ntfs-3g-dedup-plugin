using System.Runtime.InteropServices;

namespace Ntfs3gInterop
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public partial struct SDS_ENTRY
    {
        [NativeTypeName("le32")]
        public uint hash;

        [NativeTypeName("le32")]
        public uint security_id;

        [NativeTypeName("le64")]
        public ulong offset;

        [NativeTypeName("le32")]
        public uint length;

        public SECURITY_DESCRIPTOR_RELATIVE sid;
    }
}
