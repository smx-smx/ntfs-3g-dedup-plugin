using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Ntfs3gInterop
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public partial struct FILE_NAME_ATTR
    {
        [NativeTypeName("leMFT_REF")]
        public ulong parent_directory;

        [NativeTypeName("sle64")]
        public ulong creation_time;

        [NativeTypeName("sle64")]
        public ulong last_data_change_time;

        [NativeTypeName("sle64")]
        public ulong last_mft_change_time;

        [NativeTypeName("sle64")]
        public ulong last_access_time;

        [NativeTypeName("sle64")]
        public ulong allocated_size;

        [NativeTypeName("sle64")]
        public ulong data_size;

        public FILE_ATTR_FLAGS file_attributes;

        [NativeTypeName("__AnonymousRecord_layout_L1127_C9")]
        public _Anonymous_e__Union Anonymous;

        [NativeTypeName("u8")]
        public byte file_name_length;

        public FILE_NAME_TYPE_FLAGS file_name_type;

        [NativeTypeName("ntfschar[0]")]
        public _file_name_e__FixedBuffer file_name;

        [UnscopedRef]
        public ref ushort packed_ea_size
        {
            get
            {
                return ref Anonymous.Anonymous.packed_ea_size;
            }
        }

        [UnscopedRef]
        public ref ushort reserved
        {
            get
            {
                return ref Anonymous.Anonymous.reserved;
            }
        }

        [UnscopedRef]
        public ref uint reparse_point_tag
        {
            get
            {
                return ref Anonymous.reparse_point_tag;
            }
        }

        [StructLayout(LayoutKind.Explicit, Pack = 1)]
        public partial struct _Anonymous_e__Union
        {
            [FieldOffset(0)]
            [NativeTypeName("__AnonymousRecord_layout_L1128_C10")]
            public _Anonymous_e__Struct Anonymous;

            [FieldOffset(0)]
            [NativeTypeName("le32")]
            public uint reparse_point_tag;

            [StructLayout(LayoutKind.Sequential, Pack = 1)]
            public partial struct _Anonymous_e__Struct
            {
                [NativeTypeName("le16")]
                public ushort packed_ea_size;

                [NativeTypeName("le16")]
                public ushort reserved;
            }
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public partial struct _file_name_e__FixedBuffer
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
