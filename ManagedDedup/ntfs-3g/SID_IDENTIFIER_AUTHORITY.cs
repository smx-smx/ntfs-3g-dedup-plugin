using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Ntfs3gInterop
{
    [StructLayout(LayoutKind.Explicit)]
    public partial struct SID_IDENTIFIER_AUTHORITY
    {
        [FieldOffset(0)]
        [NativeTypeName("__AnonymousRecord_layout_L1361_C2")]
        public _Anonymous_e__Struct Anonymous;

        [FieldOffset(0)]
        [NativeTypeName("u8[6]")]
        public _value_e__FixedBuffer value;

        [UnscopedRef]
        public ref ushort high_part
        {
            get
            {
                return ref Anonymous.high_part;
            }
        }

        [UnscopedRef]
        public ref uint low_part
        {
            get
            {
                return ref Anonymous.low_part;
            }
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public partial struct _Anonymous_e__Struct
        {
            [NativeTypeName("be16")]
            public ushort high_part;

            [NativeTypeName("be32")]
            public uint low_part;
        }

        [InlineArray(6)]
        public partial struct _value_e__FixedBuffer
        {
            public byte e0;
        }
    }
}
