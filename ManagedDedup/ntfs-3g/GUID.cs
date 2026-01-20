using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Ntfs3gInterop
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public partial struct GUID
    {
        [NativeTypeName("le32")]
        public uint data1;

        [NativeTypeName("le16")]
        public ushort data2;

        [NativeTypeName("le16")]
        public ushort data3;

        [NativeTypeName("u8[8]")]
        public _data4_e__FixedBuffer data4;

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        [InlineArray(8)]
        public partial struct _data4_e__FixedBuffer
        {
            public byte e0;
        }
    }
}
