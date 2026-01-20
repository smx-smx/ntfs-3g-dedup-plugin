using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Ntfs3gInterop
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public partial struct SID
    {
        [NativeTypeName("u8")]
        public byte revision;

        [NativeTypeName("u8")]
        public byte sub_authority_count;

        public SID_IDENTIFIER_AUTHORITY identifier_authority;

        [NativeTypeName("le32[1]")]
        public _sub_authority_e__FixedBuffer sub_authority;

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public partial struct _sub_authority_e__FixedBuffer
        {
            public uint e0;

            [UnscopedRef]
            public ref uint this[int index]
            {
                get
                {
                    return ref Unsafe.Add(ref e0, index);
                }
            }

            [UnscopedRef]
            public Span<uint> AsSpan(int length) => MemoryMarshal.CreateSpan(ref e0, length);
        }
    }
}
