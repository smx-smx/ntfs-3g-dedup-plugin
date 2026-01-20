using System.Runtime.InteropServices;

namespace Ntfs3gInterop
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public partial struct ACCESS_ALLOWED_OBJECT_ACE
    {
        public ACE_TYPES type;

        public ACE_FLAGS flags;

        [NativeTypeName("le16")]
        public ushort size;

        public ACCESS_MASK mask;

        public OBJECT_ACE_FLAGS object_flags;

        public GUID object_type;

        public GUID inherited_object_type;

        public SID sid;
    }
}
