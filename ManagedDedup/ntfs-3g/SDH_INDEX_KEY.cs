using System.Runtime.InteropServices;

namespace Ntfs3gInterop
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public partial struct SDH_INDEX_KEY
    {
        [NativeTypeName("le32")]
        public uint hash;

        [NativeTypeName("le32")]
        public uint security_id;
    }
}
