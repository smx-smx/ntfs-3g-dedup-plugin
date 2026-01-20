namespace Ntfs3gInterop
{
    public unsafe partial struct _ntfs_attr_search_ctx
    {
        public MFT_RECORD* mrec;

        public ATTR_RECORD* attr;

        [NativeTypeName("BOOL")]
        public int is_first;

        [NativeTypeName("ntfs_inode *")]
        public _ntfs_inode* ntfs_ino;

        public ATTR_LIST_ENTRY* al_entry;

        [NativeTypeName("ntfs_inode *")]
        public _ntfs_inode* base_ntfs_ino;

        public MFT_RECORD* base_mrec;

        public ATTR_RECORD* base_attr;
    }
}
