using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Ntfs3gInterop
{
    public partial struct OBJECT_ID_ATTR
    {
        public GUID object_id;

        [NativeTypeName("__AnonymousRecord_layout_L1207_C2")]
        public _Anonymous_e__Union Anonymous;

        [UnscopedRef]
        public ref GUID birth_volume_id
        {
            get
            {
                return ref Anonymous.Anonymous.birth_volume_id;
            }
        }

        [UnscopedRef]
        public ref GUID birth_object_id
        {
            get
            {
                return ref Anonymous.Anonymous.birth_object_id;
            }
        }

        [UnscopedRef]
        public ref GUID domain_id
        {
            get
            {
                return ref Anonymous.Anonymous.domain_id;
            }
        }

        [UnscopedRef]
        public Span<byte> extended_info
        {
            get
            {
                return Anonymous.extended_info;
            }
        }

        [StructLayout(LayoutKind.Explicit)]
        public partial struct _Anonymous_e__Union
        {
            [FieldOffset(0)]
            [NativeTypeName("__AnonymousRecord_layout_L1208_C3")]
            public _Anonymous_e__Struct Anonymous;

            [FieldOffset(0)]
            [NativeTypeName("u8[48]")]
            public _extended_info_e__FixedBuffer extended_info;

            public partial struct _Anonymous_e__Struct
            {
                public GUID birth_volume_id;

                public GUID birth_object_id;

                public GUID domain_id;
            }

            [InlineArray(48)]
            public partial struct _extended_info_e__FixedBuffer
            {
                public byte e0;
            }
        }
    }
}
