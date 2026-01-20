using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Ntfs3gInterop
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public partial struct NTFS_BOOT_SECTOR
    {
        [NativeTypeName("u8[3]")]
        public _jump_e__FixedBuffer jump;

        [NativeTypeName("le64")]
        public ulong oem_id;

        public BIOS_PARAMETER_BLOCK bpb;

        [NativeTypeName("u8")]
        public byte physical_drive;

        [NativeTypeName("u8")]
        public byte current_head;

        [NativeTypeName("u8")]
        public byte extended_boot_signature;

        [NativeTypeName("u8")]
        public byte reserved2;

        [NativeTypeName("sle64")]
        public ulong number_of_sectors;

        [NativeTypeName("sle64")]
        public ulong mft_lcn;

        [NativeTypeName("sle64")]
        public ulong mftmirr_lcn;

        [NativeTypeName("s8")]
        public sbyte clusters_per_mft_record;

        [NativeTypeName("u8[3]")]
        public _reserved0_e__FixedBuffer reserved0;

        [NativeTypeName("s8")]
        public sbyte clusters_per_index_record;

        [NativeTypeName("u8[3]")]
        public _reserved1_e__FixedBuffer reserved1;

        [NativeTypeName("le64")]
        public ulong volume_serial_number;

        [NativeTypeName("le32")]
        public uint checksum;

        [NativeTypeName("u8[426]")]
        public _bootstrap_e__FixedBuffer bootstrap;

        [NativeTypeName("le16")]
        public ushort end_of_sector_marker;

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        [InlineArray(3)]
        public partial struct _jump_e__FixedBuffer
        {
            public byte e0;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        [InlineArray(3)]
        public partial struct _reserved0_e__FixedBuffer
        {
            public byte e0;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        [InlineArray(3)]
        public partial struct _reserved1_e__FixedBuffer
        {
            public byte e0;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        [InlineArray(426)]
        public partial struct _bootstrap_e__FixedBuffer
        {
            public byte e0;
        }
    }
}
