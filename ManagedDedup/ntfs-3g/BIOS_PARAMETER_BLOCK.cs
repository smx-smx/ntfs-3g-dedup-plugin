using System.Runtime.InteropServices;

namespace Ntfs3gInterop
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public partial struct BIOS_PARAMETER_BLOCK
    {
        [NativeTypeName("le16")]
        public ushort bytes_per_sector;

        [NativeTypeName("u8")]
        public byte sectors_per_cluster;

        [NativeTypeName("le16")]
        public ushort reserved_sectors;

        [NativeTypeName("u8")]
        public byte fats;

        [NativeTypeName("le16")]
        public ushort root_entries;

        [NativeTypeName("le16")]
        public ushort sectors;

        [NativeTypeName("u8")]
        public byte media_type;

        [NativeTypeName("le16")]
        public ushort sectors_per_fat;

        [NativeTypeName("le16")]
        public ushort sectors_per_track;

        [NativeTypeName("le16")]
        public ushort heads;

        [NativeTypeName("le32")]
        public uint hidden_sectors;

        [NativeTypeName("le32")]
        public uint large_sectors;
    }
}
