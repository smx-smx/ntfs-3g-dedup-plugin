using System.Runtime.InteropServices;

namespace Ntfs3gInterop
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public partial struct SDH_INDEX_DATA
    {
        [NativeTypeName("le32")]
        public uint hash;

        [NativeTypeName("le32")]
        public uint security_id;

        [NativeTypeName("le64")]
        public ulong offset;

        [NativeTypeName("le32")]
        public uint length;

        [NativeTypeName("le32")]
        public uint reserved_II;
    }
}
