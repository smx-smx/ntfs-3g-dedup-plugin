using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace Ntfs3gInterop
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public partial struct INDEX_ENTRY_HEADER
    {
        [NativeTypeName("__AnonymousRecord_layout_L2303_C9")]
        public _Anonymous_e__Union Anonymous;

        [NativeTypeName("le16")]
        public ushort length;

        [NativeTypeName("le16")]
        public ushort key_length;

        public INDEX_ENTRY_FLAGS flags;

        [NativeTypeName("le16")]
        public ushort reserved;

        [UnscopedRef]
        public ref ulong indexed_file
        {
            get
            {
                return ref Anonymous.indexed_file;
            }
        }

        [UnscopedRef]
        public ref ushort data_offset
        {
            get
            {
                return ref Anonymous.Anonymous.data_offset;
            }
        }

        [UnscopedRef]
        public ref ushort data_length
        {
            get
            {
                return ref Anonymous.Anonymous.data_length;
            }
        }

        [UnscopedRef]
        public ref uint reservedV
        {
            get
            {
                return ref Anonymous.Anonymous.reservedV;
            }
        }

        [StructLayout(LayoutKind.Explicit, Pack = 1)]
        public partial struct _Anonymous_e__Union
        {
            [FieldOffset(0)]
            [NativeTypeName("leMFT_REF")]
            public ulong indexed_file;

            [FieldOffset(0)]
            [NativeTypeName("__AnonymousRecord_layout_L2305_C3")]
            public _Anonymous_e__Struct Anonymous;

            [StructLayout(LayoutKind.Sequential, Pack = 1)]
            public partial struct _Anonymous_e__Struct
            {
                [NativeTypeName("le16")]
                public ushort data_offset;

                [NativeTypeName("le16")]
                public ushort data_length;

                [NativeTypeName("le32")]
                public uint reservedV;
            }
        }
    }
}
