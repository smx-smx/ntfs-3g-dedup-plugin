using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace Ntfs3gInterop
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public partial struct INDEX_ENTRY
    {
        [NativeTypeName("__AnonymousRecord_layout_L2329_C2")]
        public _Anonymous_e__Union Anonymous;

        [NativeTypeName("le16")]
        public ushort length;

        [NativeTypeName("le16")]
        public ushort key_length;

        public INDEX_ENTRY_FLAGS ie_flags;

        [NativeTypeName("le16")]
        public ushort reserved;

        [NativeTypeName("__AnonymousRecord_layout_L2351_C9")]
        public _key_e__Union key;

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
            [NativeTypeName("__AnonymousRecord_layout_L2334_C3")]
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

        [StructLayout(LayoutKind.Explicit, Pack = 1)]
        public partial struct _key_e__Union
        {
            [FieldOffset(0)]
            public FILE_NAME_ATTR file_name;

            [FieldOffset(0)]
            public SII_INDEX_KEY sii;

            [FieldOffset(0)]
            public SDH_INDEX_KEY sdh;

            [FieldOffset(0)]
            public GUID object_id;

            [FieldOffset(0)]
            public REPARSE_INDEX_KEY reparse;

            [FieldOffset(0)]
            public SID sid;

            [FieldOffset(0)]
            [NativeTypeName("le32")]
            public uint owner_id;
        }
    }
}
