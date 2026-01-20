using System.Runtime.InteropServices;

namespace Ntfs3gInterop
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public partial struct SII_INDEX_KEY
    {
        [NativeTypeName("le32")]
        public uint security_id;
    }
}
