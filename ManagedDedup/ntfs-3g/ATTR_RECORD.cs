using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Ntfs3gInterop
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public partial struct ATTR_RECORD
    {
        public ATTR_TYPES type;

        [NativeTypeName("le32")]
        public uint length;

        [NativeTypeName("u8")]
        public byte non_resident;

        [NativeTypeName("u8")]
        public byte name_length;

        [NativeTypeName("le16")]
        public ushort name_offset;

        public ATTR_FLAGS flags;

        [NativeTypeName("le16")]
        public ushort instance;

        [NativeTypeName("__AnonymousRecord_layout_L746_C9")]
        public _Anonymous_e__Union Anonymous;

        [UnscopedRef]
        public ref uint value_length
        {
            get
            {
                return ref Anonymous.Anonymous1.value_length;
            }
        }

        [UnscopedRef]
        public ref ushort value_offset
        {
            get
            {
                return ref Anonymous.Anonymous1.value_offset;
            }
        }

        [UnscopedRef]
        public ref RESIDENT_ATTR_FLAGS resident_flags
        {
            get
            {
                return ref Anonymous.Anonymous1.resident_flags;
            }
        }

        [UnscopedRef]
        public ref sbyte reservedR
        {
            get
            {
                return ref Anonymous.Anonymous1.reservedR;
            }
        }

        [UnscopedRef]
        public ref ulong lowest_vcn
        {
            get
            {
                return ref Anonymous.Anonymous2.lowest_vcn;
            }
        }

        [UnscopedRef]
        public ref ulong highest_vcn
        {
            get
            {
                return ref Anonymous.Anonymous2.highest_vcn;
            }
        }

        [UnscopedRef]
        public ref ushort mapping_pairs_offset
        {
            get
            {
                return ref Anonymous.Anonymous2.mapping_pairs_offset;
            }
        }

        [UnscopedRef]
        public ref byte compression_unit
        {
            get
            {
                return ref Anonymous.Anonymous2.compression_unit;
            }
        }

        [UnscopedRef]
        public Span<byte> reserved1
        {
            get
            {
                return Anonymous.Anonymous2.reserved1;
            }
        }

        [UnscopedRef]
        public ref ulong allocated_size
        {
            get
            {
                return ref Anonymous.Anonymous2.allocated_size;
            }
        }

        [UnscopedRef]
        public ref ulong data_size
        {
            get
            {
                return ref Anonymous.Anonymous2.data_size;
            }
        }

        [UnscopedRef]
        public ref ulong initialized_size
        {
            get
            {
                return ref Anonymous.Anonymous2.initialized_size;
            }
        }

        [UnscopedRef]
        public ref ulong compressed_size
        {
            get
            {
                return ref Anonymous.Anonymous2.compressed_size;
            }
        }

        [StructLayout(LayoutKind.Explicit)]
        public partial struct _Anonymous_e__Union
        {
            [FieldOffset(0)]
            [NativeTypeName("__AnonymousRecord_layout_L748_C3")]
            public _Anonymous1_e__Struct Anonymous1;

            [FieldOffset(0)]
            [NativeTypeName("__AnonymousRecord_layout_L765_C3")]
            public _Anonymous2_e__Struct Anonymous2;

            [StructLayout(LayoutKind.Sequential, Pack = 1)]
            public partial struct _Anonymous1_e__Struct
            {
                [NativeTypeName("le32")]
                public uint value_length;

                [NativeTypeName("le16")]
                public ushort value_offset;

                public RESIDENT_ATTR_FLAGS resident_flags;

                [NativeTypeName("s8")]
                public sbyte reservedR;

                [NativeTypeName("void *[0]")]
                public _resident_end_e__FixedBuffer resident_end;

                [StructLayout(LayoutKind.Sequential, Pack = 1)]
                public unsafe partial struct _resident_end_e__FixedBuffer
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
            public partial struct _Anonymous2_e__Struct
            {
                [NativeTypeName("leVCN")]
                public ulong lowest_vcn;

                [NativeTypeName("leVCN")]
                public ulong highest_vcn;

                [NativeTypeName("le16")]
                public ushort mapping_pairs_offset;

                [NativeTypeName("u8")]
                public byte compression_unit;

                [NativeTypeName("u8[5]")]
                public _reserved1_e__FixedBuffer reserved1;

                [NativeTypeName("sle64")]
                public ulong allocated_size;

                [NativeTypeName("sle64")]
                public ulong data_size;

                [NativeTypeName("sle64")]
                public ulong initialized_size;

                [NativeTypeName("void *[0]")]
                public _non_resident_end_e__FixedBuffer non_resident_end;

                [NativeTypeName("sle64")]
                public ulong compressed_size;

                [NativeTypeName("void *[0]")]
                public _compressed_end_e__FixedBuffer compressed_end;

                [StructLayout(LayoutKind.Sequential, Pack = 1)]
                [InlineArray(5)]
                public partial struct _reserved1_e__FixedBuffer
                {
                    public byte e0;
                }

                [StructLayout(LayoutKind.Sequential, Pack = 1)]
                public unsafe partial struct _non_resident_end_e__FixedBuffer
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

                [StructLayout(LayoutKind.Sequential, Pack = 1)]
                public unsafe partial struct _compressed_end_e__FixedBuffer
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
        }
    }
}
