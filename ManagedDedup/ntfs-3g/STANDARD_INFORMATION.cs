using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Ntfs3gInterop
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public partial struct STANDARD_INFORMATION
    {
        [NativeTypeName("sle64")]
        public ulong creation_time;

        [NativeTypeName("sle64")]
        public ulong last_data_change_time;

        [NativeTypeName("sle64")]
        public ulong last_mft_change_time;

        [NativeTypeName("sle64")]
        public ulong last_access_time;

        public FILE_ATTR_FLAGS file_attributes;

        [NativeTypeName("__AnonymousRecord_layout_L928_C9")]
        public _Anonymous_e__Union Anonymous;

        [UnscopedRef]
        public Span<byte> reserved12
        {
            get
            {
                return Anonymous.Anonymous1.reserved12;
            }
        }

        [UnscopedRef]
        public ref uint maximum_versions
        {
            get
            {
                return ref Anonymous.Anonymous2.maximum_versions;
            }
        }

        [UnscopedRef]
        public ref uint version_number
        {
            get
            {
                return ref Anonymous.Anonymous2.version_number;
            }
        }

        [UnscopedRef]
        public ref uint class_id
        {
            get
            {
                return ref Anonymous.Anonymous2.class_id;
            }
        }

        [UnscopedRef]
        public ref uint owner_id
        {
            get
            {
                return ref Anonymous.Anonymous2.owner_id;
            }
        }

        [UnscopedRef]
        public ref uint security_id
        {
            get
            {
                return ref Anonymous.Anonymous2.security_id;
            }
        }

        [UnscopedRef]
        public ref ulong quota_charged
        {
            get
            {
                return ref Anonymous.Anonymous2.quota_charged;
            }
        }

        [UnscopedRef]
        public ref ulong usn
        {
            get
            {
                return ref Anonymous.Anonymous2.usn;
            }
        }

        [StructLayout(LayoutKind.Explicit)]
        public partial struct _Anonymous_e__Union
        {
            [FieldOffset(0)]
            [NativeTypeName("__AnonymousRecord_layout_L930_C3")]
            public _Anonymous1_e__Struct Anonymous1;

            [FieldOffset(0)]
            [NativeTypeName("__AnonymousRecord_layout_L937_C3")]
            public _Anonymous2_e__Struct Anonymous2;

            [StructLayout(LayoutKind.Sequential, Pack = 1)]
            public partial struct _Anonymous1_e__Struct
            {
                [NativeTypeName("u8[12]")]
                public _reserved12_e__FixedBuffer reserved12;

                [NativeTypeName("void *[0]")]
                public _v1_end_e__FixedBuffer v1_end;

                [StructLayout(LayoutKind.Sequential, Pack = 1)]
                [InlineArray(12)]
                public partial struct _reserved12_e__FixedBuffer
                {
                    public byte e0;
                }

                [StructLayout(LayoutKind.Sequential, Pack = 1)]
                public unsafe partial struct _v1_end_e__FixedBuffer
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
                [NativeTypeName("le32")]
                public uint maximum_versions;

                [NativeTypeName("le32")]
                public uint version_number;

                [NativeTypeName("le32")]
                public uint class_id;

                [NativeTypeName("le32")]
                public uint owner_id;

                [NativeTypeName("le32")]
                public uint security_id;

                [NativeTypeName("le64")]
                public ulong quota_charged;

                [NativeTypeName("le64")]
                public ulong usn;

                [NativeTypeName("void *[0]")]
                public _v3_end_e__FixedBuffer v3_end;

                [StructLayout(LayoutKind.Sequential, Pack = 1)]
                public unsafe partial struct _v3_end_e__FixedBuffer
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
