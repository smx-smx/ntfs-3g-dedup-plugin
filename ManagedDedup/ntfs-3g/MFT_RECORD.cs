using System.Runtime.InteropServices;

namespace Ntfs3gInterop
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public partial struct MFT_RECORD
    {
        public NTFS_RECORD_TYPES magic;

        [NativeTypeName("le16")]
        public ushort usa_ofs;

        [NativeTypeName("le16")]
        public ushort usa_count;

        [NativeTypeName("leLSN")]
        public ulong lsn;

        [NativeTypeName("le16")]
        public ushort sequence_number;

        [NativeTypeName("le16")]
        public ushort link_count;

        [NativeTypeName("le16")]
        public ushort attrs_offset;

        public MFT_RECORD_FLAGS flags;

        [NativeTypeName("le32")]
        public uint bytes_in_use;

        [NativeTypeName("le32")]
        public uint bytes_allocated;

        [NativeTypeName("leMFT_REF")]
        public ulong base_mft_record;

        [NativeTypeName("le16")]
        public ushort next_attr_instance;

        [NativeTypeName("le16")]
        public ushort reserved;

        [NativeTypeName("le32")]
        public uint mft_record_number;
    }
}
