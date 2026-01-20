using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Ntfs3gInterop
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public partial struct VOLUME_NAME
    {
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
