using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Ntfs3gInterop
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public partial struct INTX_FILE
    {
        public INTX_FILE_TYPES magic;

        [NativeTypeName("__AnonymousRecord_layout_L2696_C2")]
        public _Anonymous_e__Union Anonymous;

        [UnscopedRef]
        public ref ulong major
        {
            get
            {
                return ref Anonymous.Anonymous.major;
            }
        }

        [UnscopedRef]
        public ref ulong minor
        {
            get
            {
                return ref Anonymous.Anonymous.minor;
            }
        }

        [StructLayout(LayoutKind.Explicit, Pack = 1)]
        public partial struct _Anonymous_e__Union
        {
            [FieldOffset(0)]
            [NativeTypeName("__AnonymousRecord_layout_L2698_C3")]
            public _Anonymous_e__Struct Anonymous;

            [FieldOffset(0)]
            [NativeTypeName("ntfschar[0]")]
            public _target_e__FixedBuffer target;

            [StructLayout(LayoutKind.Sequential, Pack = 1)]
            public partial struct _Anonymous_e__Struct
            {
                [NativeTypeName("le64")]
                public ulong major;

                [NativeTypeName("le64")]
                public ulong minor;

                [NativeTypeName("void *[0]")]
                public _device_end_e__FixedBuffer device_end;

                [StructLayout(LayoutKind.Sequential, Pack = 1)]
                public unsafe partial struct _device_end_e__FixedBuffer
                {
                    public void* e0;

                    public ref void* this[int index]
                    {
                        get
                        {
                            fixed (void** pThis = &e0)
                            {
                                return ref pThis[index];
                            }
                        }
                    }
                }
            }

            [StructLayout(LayoutKind.Sequential, Pack = 1)]
            public partial struct _target_e__FixedBuffer
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
}
