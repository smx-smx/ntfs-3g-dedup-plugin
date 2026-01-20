using System.Runtime.InteropServices;

namespace Ntfs3gInterop
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public partial struct EFS_DF_ARRAY_HEADER
    {
        [NativeTypeName("le32")]
        public uint df_count;
    }
}
