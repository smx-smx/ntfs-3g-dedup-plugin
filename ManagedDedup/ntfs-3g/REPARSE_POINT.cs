using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Ntfs3gInterop
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public partial struct REPARSE_POINT
    {
        [NativeTypeName("le32")]
        public uint reparse_tag;

        [NativeTypeName("le16")]
        public ushort reparse_data_length;

        [NativeTypeName("le16")]
        public ushort reserved;

        [NativeTypeName("u8[0]")]
        public _reparse_data_e__FixedBuffer reparse_data;

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public partial struct _reparse_data_e__FixedBuffer
        {
            public byte e0;

            [UnscopedRef]
            public ref byte this[int index]
            {
                get
                {
                    return ref Unsafe.Add(ref e0, index);
                }
            }

            [UnscopedRef]
            public Span<byte> AsSpan(int length) => MemoryMarshal.CreateSpan(ref e0, length);
        }
    }
}
