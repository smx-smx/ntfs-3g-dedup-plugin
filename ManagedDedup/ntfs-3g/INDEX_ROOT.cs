using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Ntfs3gInterop
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public partial struct INDEX_ROOT
    {
        public ATTR_TYPES type;

        public COLLATION_RULES collation_rule;

        [NativeTypeName("le32")]
        public uint index_block_size;

        [NativeTypeName("s8")]
        public sbyte clusters_per_index_block;

        [NativeTypeName("u8[3]")]
        public _reserved_e__FixedBuffer reserved;

        public INDEX_HEADER index;

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        [InlineArray(3)]
        public partial struct _reserved_e__FixedBuffer
        {
            public byte e0;
        }
    }
}
