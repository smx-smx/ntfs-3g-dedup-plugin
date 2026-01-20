namespace Ntfs3gInterop
{
    public unsafe partial struct _ntfs_attr
    {
        [NativeTypeName("runlist_element *")]
        public _runlist_element* rl;

        [NativeTypeName("ntfs_inode *")]
        public _ntfs_inode* ni;

        public ATTR_TYPES type;

        public ATTR_FLAGS data_flags;

        [NativeTypeName("ntfschar *")]
        public ushort* name;

        [NativeTypeName("u32")]
        public uint name_len;

        [NativeTypeName("unsigned long")]
        public nuint state;

        [NativeTypeName("s64")]
        public long allocated_size;

        [NativeTypeName("s64")]
        public long data_size;

        [NativeTypeName("s64")]
        public long initialized_size;

        [NativeTypeName("s64")]
        public long compressed_size;

        [NativeTypeName("u32")]
        public uint compression_block_size;

        [NativeTypeName("u8")]
        public byte compression_block_size_bits;

        [NativeTypeName("u8")]
        public byte compression_block_clusters;

        [NativeTypeName("s8")]
        public sbyte unused_runs;
    }
}
