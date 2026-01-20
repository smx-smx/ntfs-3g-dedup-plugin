using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Ntfs3gInterop
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public partial struct EA_ATTR
    {
        [NativeTypeName("le32")]
        public uint next_entry_offset;

        public EA_FLAGS flags;

        [NativeTypeName("u8")]
        public byte name_length;

        [NativeTypeName("le16")]
        public ushort value_length;

        [NativeTypeName("u8[0]")]
        public _name_e__FixedBuffer name;

        [NativeTypeName("u8[0]")]
        public _value_e__FixedBuffer value;

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public partial struct _name_e__FixedBuffer
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

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public partial struct _value_e__FixedBuffer
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
