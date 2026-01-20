using System.Runtime.InteropServices;

namespace Ntfs3gInterop
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public partial struct ACE_HEADER
    {
        public ACE_TYPES type;

        public ACE_FLAGS flags;

        [NativeTypeName("le16")]
        public ushort size;
    }
}
