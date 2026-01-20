namespace Ntfs3gInterop
{
    public unsafe partial struct ntfs_device
    {
        [NativeTypeName("struct ntfs_device_operations *")]
        public ntfs_device_operations* d_ops;

        [NativeTypeName("unsigned long")]
        public nuint d_state;

        [NativeTypeName("char *")]
        public sbyte* d_name;

        public void* d_private;

        public int d_heads;

        public int d_sectors_per_track;
    }
}
