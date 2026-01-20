using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Ntfs3gInterop
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public partial struct EFS_ATTR_HEADER
    {
        [NativeTypeName("le32")]
        public uint length;

        [NativeTypeName("le32")]
        public uint state;

        [NativeTypeName("le32")]
        public uint version;

        [NativeTypeName("le32")]
        public uint crypto_api_version;

        [NativeTypeName("u8[16]")]
        public _unknown4_e__FixedBuffer unknown4;

        [NativeTypeName("u8[16]")]
        public _unknown5_e__FixedBuffer unknown5;

        [NativeTypeName("u8[16]")]
        public _unknown6_e__FixedBuffer unknown6;

        [NativeTypeName("le32")]
        public uint offset_to_ddf_array;

        [NativeTypeName("le32")]
        public uint offset_to_drf_array;

        [NativeTypeName("le32")]
        public uint reserved;

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        [InlineArray(16)]
        public partial struct _unknown4_e__FixedBuffer
        {
            public byte e0;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        [InlineArray(16)]
        public partial struct _unknown5_e__FixedBuffer
        {
            public byte e0;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        [InlineArray(16)]
        public partial struct _unknown6_e__FixedBuffer
        {
            public byte e0;
        }
    }
}
