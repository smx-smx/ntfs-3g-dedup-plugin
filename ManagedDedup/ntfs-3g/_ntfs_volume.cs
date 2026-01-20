using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace Ntfs3gInterop
{
    public unsafe partial struct _ntfs_volume
    {
        [NativeTypeName("__AnonymousRecord_volume_L182_C2")]
        public _Anonymous_e__Union Anonymous;

        [NativeTypeName("char *")]
        public sbyte* vol_name;

        [NativeTypeName("unsigned long")]
        public nuint state;

        [NativeTypeName("ntfs_inode *")]
        public _ntfs_inode* vol_ni;

        [NativeTypeName("u8")]
        public byte major_ver;

        [NativeTypeName("u8")]
        public byte minor_ver;

        [NativeTypeName("le16")]
        public ushort flags;

        [NativeTypeName("u16")]
        public ushort sector_size;

        [NativeTypeName("u8")]
        public byte sector_size_bits;

        [NativeTypeName("u32")]
        public uint cluster_size;

        [NativeTypeName("u32")]
        public uint mft_record_size;

        [NativeTypeName("u32")]
        public uint indx_record_size;

        [NativeTypeName("u8")]
        public byte cluster_size_bits;

        [NativeTypeName("u8")]
        public byte mft_record_size_bits;

        [NativeTypeName("u8")]
        public byte indx_record_size_bits;

        [NativeTypeName("u8")]
        public byte mft_zone_multiplier;

        [NativeTypeName("u8")]
        public byte full_zones;

        [NativeTypeName("s64")]
        public long mft_data_pos;

        [NativeTypeName("LCN")]
        public long mft_zone_start;

        [NativeTypeName("LCN")]
        public long mft_zone_end;

        [NativeTypeName("LCN")]
        public long mft_zone_pos;

        [NativeTypeName("LCN")]
        public long data1_zone_pos;

        [NativeTypeName("LCN")]
        public long data2_zone_pos;

        [NativeTypeName("s64")]
        public long nr_clusters;

        [NativeTypeName("ntfs_inode *")]
        public _ntfs_inode* lcnbmp_ni;

        [NativeTypeName("ntfs_attr *")]
        public _ntfs_attr* lcnbmp_na;

        [NativeTypeName("LCN")]
        public long mft_lcn;

        [NativeTypeName("ntfs_inode *")]
        public _ntfs_inode* mft_ni;

        [NativeTypeName("ntfs_attr *")]
        public _ntfs_attr* mft_na;

        [NativeTypeName("ntfs_attr *")]
        public _ntfs_attr* mftbmp_na;

        [NativeTypeName("ntfs_inode *")]
        public _ntfs_inode* secure_ni;

        [NativeTypeName("struct ntfs_index_context *")]
        public ntfs_index_context* secure_xsii;

        [NativeTypeName("struct ntfs_index_context *")]
        public ntfs_index_context* secure_xsdh;

        public int secure_reentry;

        [NativeTypeName("unsigned int")]
        public uint secure_flags;

        public int mftmirr_size;

        [NativeTypeName("LCN")]
        public long mftmirr_lcn;

        [NativeTypeName("ntfs_inode *")]
        public _ntfs_inode* mftmirr_ni;

        [NativeTypeName("ntfs_attr *")]
        public _ntfs_attr* mftmirr_na;

        [NativeTypeName("ntfschar *")]
        public ushort* upcase;

        [NativeTypeName("u32")]
        public uint upcase_len;

        [NativeTypeName("ntfschar *")]
        public ushort* locase;

        public ATTR_DEF* attrdef;

        [NativeTypeName("s32")]
        public int attrdef_len;

        [NativeTypeName("s64")]
        public long free_clusters;

        [NativeTypeName("s64")]
        public long free_mft_records;

        [NativeTypeName("BOOL")]
        public int efs_raw;

        public ntfs_volume_special_files special_files;

        [NativeTypeName("char *")]
        public sbyte* abs_mnt_point;

        [NativeTypeName("struct CACHE_HEADER *")]
        public CACHE_HEADER* xinode_cache;

        [NativeTypeName("struct CACHE_HEADER *")]
        public CACHE_HEADER* nidata_cache;

        [NativeTypeName("struct CACHE_HEADER *")]
        public CACHE_HEADER* lookup_cache;

        [NativeTypeName("struct CACHE_HEADER *")]
        public CACHE_HEADER* securid_cache;

        [NativeTypeName("struct CACHE_HEADER *")]
        public CACHE_HEADER* legacy_cache;

        [UnscopedRef]
        public ref ntfs_device* dev
        {
            get
            {
                return ref Anonymous.dev;
            }
        }

        [UnscopedRef]
        public ref void* sb
        {
            get
            {
                return ref Anonymous.sb;
            }
        }

        [StructLayout(LayoutKind.Explicit)]
        public unsafe partial struct _Anonymous_e__Union
        {
            [FieldOffset(0)]
            [NativeTypeName("struct ntfs_device *")]
            public ntfs_device* dev;

            [FieldOffset(0)]
            public void* sb;
        }

        public partial struct CACHE_HEADER
        {
        }
    }
}
