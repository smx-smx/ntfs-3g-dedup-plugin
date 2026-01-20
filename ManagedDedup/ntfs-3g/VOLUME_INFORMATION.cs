using System.Runtime.InteropServices;

namespace Ntfs3gInterop
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public partial struct VOLUME_INFORMATION
    {
        [NativeTypeName("le64")]
        public ulong reserved;

        [NativeTypeName("u8")]
        public byte major_ver;

        [NativeTypeName("u8")]
        public byte minor_ver;

        public VOLUME_FLAGS flags;
    }
}
