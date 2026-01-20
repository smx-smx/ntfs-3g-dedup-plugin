using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Ntfs3gInterop
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public partial struct INDEX_HEADER
    {
        [NativeTypeName("le32")]
        public uint entries_offset;

        [NativeTypeName("le32")]
        public uint index_length;

        [NativeTypeName("le32")]
        public uint allocated_size;

        public INDEX_HEADER_FLAGS ih_flags;

        [NativeTypeName("u8[3]")]
        public _reserved_e__FixedBuffer reserved;

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        [InlineArray(3)]
        public partial struct _reserved_e__FixedBuffer
        {
            public byte e0;
        }
    }
}
