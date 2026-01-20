using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace Ntfs3gInterop
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public partial struct EFS_DF_CREDENTIAL_HEADER
    {
        [NativeTypeName("le32")]
        public uint cred_length;

        [NativeTypeName("le32")]
        public uint sid_offset;

        [NativeTypeName("le32")]
        public uint type;

        [NativeTypeName("__AnonymousRecord_layout_L2633_C2")]
        public _Anonymous_e__Union Anonymous;

        [UnscopedRef]
        public ref uint container_name_offset
        {
            get
            {
                return ref Anonymous.Anonymous1.container_name_offset;
            }
        }

        [UnscopedRef]
        public ref uint provider_name_offset
        {
            get
            {
                return ref Anonymous.Anonymous1.provider_name_offset;
            }
        }

        [UnscopedRef]
        public ref uint public_key_blob_offset
        {
            get
            {
                return ref Anonymous.Anonymous1.public_key_blob_offset;
            }
        }

        [UnscopedRef]
        public ref uint public_key_blob_size
        {
            get
            {
                return ref Anonymous.Anonymous1.public_key_blob_size;
            }
        }

        [UnscopedRef]
        public ref uint cert_thumbprint_header_size
        {
            get
            {
                return ref Anonymous.Anonymous2.cert_thumbprint_header_size;
            }
        }

        [UnscopedRef]
        public ref uint cert_thumbprint_header_offset
        {
            get
            {
                return ref Anonymous.Anonymous2.cert_thumbprint_header_offset;
            }
        }

        [UnscopedRef]
        public ref uint unknown1
        {
            get
            {
                return ref Anonymous.Anonymous2.unknown1;
            }
        }

        [UnscopedRef]
        public ref uint unknown2
        {
            get
            {
                return ref Anonymous.Anonymous2.unknown2;
            }
        }

        [StructLayout(LayoutKind.Explicit)]
        public partial struct _Anonymous_e__Union
        {
            [FieldOffset(0)]
            [NativeTypeName("__AnonymousRecord_layout_L2635_C3")]
            public _Anonymous1_e__Struct Anonymous1;

            [FieldOffset(0)]
            [NativeTypeName("__AnonymousRecord_layout_L2649_C3")]
            public _Anonymous2_e__Struct Anonymous2;

            [StructLayout(LayoutKind.Sequential, Pack = 1)]
            public partial struct _Anonymous1_e__Struct
            {
                [NativeTypeName("le32")]
                public uint container_name_offset;

                [NativeTypeName("le32")]
                public uint provider_name_offset;

                [NativeTypeName("le32")]
                public uint public_key_blob_offset;

                [NativeTypeName("le32")]
                public uint public_key_blob_size;
            }

            [StructLayout(LayoutKind.Sequential, Pack = 1)]
            public partial struct _Anonymous2_e__Struct
            {
                [NativeTypeName("le32")]
                public uint cert_thumbprint_header_size;

                [NativeTypeName("le32")]
                public uint cert_thumbprint_header_offset;

                [NativeTypeName("le32")]
                public uint unknown1;

                [NativeTypeName("le32")]
                public uint unknown2;
            }
        }
    }
}
