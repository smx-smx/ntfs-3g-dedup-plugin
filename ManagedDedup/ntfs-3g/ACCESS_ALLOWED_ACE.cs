using System.Runtime.InteropServices;

namespace Ntfs3gInterop
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public partial struct ACCESS_ALLOWED_ACE
    {
        public ACE_TYPES type;

        public ACE_FLAGS flags;

        [NativeTypeName("le16")]
        public ushort size;

        public ACCESS_MASK mask;

        public SID sid;
    }
}
