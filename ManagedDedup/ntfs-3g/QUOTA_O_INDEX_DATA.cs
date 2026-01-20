using System.Runtime.InteropServices;

namespace Ntfs3gInterop
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public partial struct QUOTA_O_INDEX_DATA
    {
        [NativeTypeName("le32")]
        public uint owner_id;

        [NativeTypeName("le32")]
        public uint unknown;
    }
}
