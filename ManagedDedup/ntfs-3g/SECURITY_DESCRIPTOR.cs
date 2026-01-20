using System.Runtime.InteropServices;

namespace Ntfs3gInterop
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe partial struct SECURITY_DESCRIPTOR
    {
        [NativeTypeName("u8")]
        public byte revision;

        [NativeTypeName("u8")]
        public byte alignment;

        public SECURITY_DESCRIPTOR_CONTROL control;

        public SID* owner;

        public SID* group;

        public ACL* sacl;

        public ACL* dacl;
    }
}
