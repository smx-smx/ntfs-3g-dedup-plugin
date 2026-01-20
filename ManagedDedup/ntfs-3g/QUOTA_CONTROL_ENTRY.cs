using System.Runtime.InteropServices;

namespace Ntfs3gInterop
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public partial struct QUOTA_CONTROL_ENTRY
    {
        [NativeTypeName("le32")]
        public uint version;

        public QUOTA_FLAGS flags;

        [NativeTypeName("le64")]
        public ulong bytes_used;

        [NativeTypeName("sle64")]
        public ulong change_time;

        [NativeTypeName("sle64")]
        public ulong threshold;

        [NativeTypeName("sle64")]
        public ulong limit;

        [NativeTypeName("sle64")]
        public ulong exceeded_time;

        public SID sid;
    }
}
