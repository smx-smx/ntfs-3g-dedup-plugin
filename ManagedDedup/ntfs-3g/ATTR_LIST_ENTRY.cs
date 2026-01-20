using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Ntfs3gInterop
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public partial struct ATTR_LIST_ENTRY
    {
        public ATTR_TYPES type;

        [NativeTypeName("le16")]
        public ushort length;

        [NativeTypeName("u8")]
        public byte name_length;

        [NativeTypeName("u8")]
        public byte name_offset;

        [NativeTypeName("leVCN")]
        public ulong lowest_vcn;

        [NativeTypeName("leMFT_REF")]
        public ulong mft_reference;

        [NativeTypeName("le16")]
        public ushort instance;

        [NativeTypeName("ntfschar[0]")]
        public _name_e__FixedBuffer name;

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public partial struct _name_e__FixedBuffer
        {
            public ushort e0;

            [UnscopedRef]
            public ref ushort this[int index]
            {
                get
                {
                    return ref Unsafe.Add(ref e0, index);
                }
            }

            [UnscopedRef]
            public Span<ushort> AsSpan(int length) => MemoryMarshal.CreateSpan(ref e0, length);
        }
    }
}
