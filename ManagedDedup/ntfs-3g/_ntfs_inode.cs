using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace Ntfs3gInterop
{
    public unsafe partial struct _ntfs_inode
    {
        [NativeTypeName("u64")]
        public ulong mft_no;

        public MFT_RECORD* mrec;

        [NativeTypeName("ntfs_volume *")]
        public _ntfs_volume* vol;

        [NativeTypeName("unsigned long")]
        public nuint state;

        public FILE_ATTR_FLAGS flags;

        [NativeTypeName("u32")]
        public uint attr_list_size;

        [NativeTypeName("u8 *")]
        public byte* attr_list;

        [NativeTypeName("s32")]
        public int nr_extents;

        [NativeTypeName("__AnonymousRecord_inode_L125_C2")]
        public _Anonymous_e__Union Anonymous;

        [NativeTypeName("s64")]
        public long data_size;

        [NativeTypeName("s64")]
        public long allocated_size;

        [NativeTypeName("ntfs_time")]
        public ulong creation_time;

        [NativeTypeName("ntfs_time")]
        public ulong last_data_change_time;

        [NativeTypeName("ntfs_time")]
        public ulong last_mft_change_time;

        [NativeTypeName("ntfs_time")]
        public ulong last_access_time;

        [NativeTypeName("le32")]
        public uint owner_id;

        [NativeTypeName("le32")]
        public uint security_id;

        [NativeTypeName("le64")]
        public ulong quota_charged;

        [NativeTypeName("le64")]
        public ulong usn;

        [UnscopedRef]
        public ref _ntfs_inode** extent_nis
        {
            get
            {
                return ref Anonymous.extent_nis;
            }
        }

        [UnscopedRef]
        public ref _ntfs_inode* base_ni
        {
            get
            {
                return ref Anonymous.base_ni;
            }
        }

        [StructLayout(LayoutKind.Explicit)]
        public unsafe partial struct _Anonymous_e__Union
        {
            [FieldOffset(0)]
            [NativeTypeName("ntfs_inode **")]
            public _ntfs_inode** extent_nis;

            [FieldOffset(0)]
            [NativeTypeName("ntfs_inode *")]
            public _ntfs_inode* base_ni;
        }
    }
}
