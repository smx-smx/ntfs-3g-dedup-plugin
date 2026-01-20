using System.Runtime.CompilerServices;

namespace Ntfs3gInterop
{
    public unsafe partial struct ntfs_index_context
    {
        [NativeTypeName("ntfs_inode *")]
        public _ntfs_inode* ni;

        [NativeTypeName("ntfschar *")]
        public ushort* name;

        [NativeTypeName("u32")]
        public uint name_len;

        public INDEX_ENTRY* entry;

        public void* data;

        [NativeTypeName("u16")]
        public ushort data_len;

        [NativeTypeName("COLLATE")]
        public delegate* unmanaged[Cdecl]<_ntfs_volume*, void*, int, void*, int, int> collate;

        [NativeTypeName("BOOL")]
        public int is_in_root;

        public INDEX_ROOT* ir;

        [NativeTypeName("ntfs_attr_search_ctx *")]
        public _ntfs_attr_search_ctx* actx;

        public INDEX_BLOCK* ib;

        [NativeTypeName("ntfs_attr *")]
        public _ntfs_attr* ia_na;

        [NativeTypeName("int[32]")]
        public _parent_pos_e__FixedBuffer parent_pos;

        [NativeTypeName("VCN[32]")]
        public _parent_vcn_e__FixedBuffer parent_vcn;

        public int pindex;

        [NativeTypeName("BOOL")]
        public int ib_dirty;

        [NativeTypeName("BOOL")]
        public int bad_index;

        [NativeTypeName("u32")]
        public uint block_size;

        [NativeTypeName("u8")]
        public byte vcn_size_bits;

        [InlineArray(32)]
        public partial struct _parent_pos_e__FixedBuffer
        {
            public int e0;
        }

        [InlineArray(32)]
        public partial struct _parent_vcn_e__FixedBuffer
        {
            public long e0;
        }
    }
}
