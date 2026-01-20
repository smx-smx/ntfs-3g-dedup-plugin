using System.Runtime.InteropServices;

namespace Ntfs3gInterop
{
    [StructLayout(LayoutKind.Explicit)]
    public partial struct attr_val
    {
        [FieldOffset(0)]
        [NativeTypeName("u8")]
        public byte _default;

        [FieldOffset(0)]
        public STANDARD_INFORMATION std_inf;

        [FieldOffset(0)]
        public ATTR_LIST_ENTRY al_entry;

        [FieldOffset(0)]
        public FILE_NAME_ATTR filename;

        [FieldOffset(0)]
        public OBJECT_ID_ATTR obj_id;

        [FieldOffset(0)]
        [NativeTypeName("SECURITY_DESCRIPTOR_ATTR")]
        public SECURITY_DESCRIPTOR_RELATIVE sec_desc;

        [FieldOffset(0)]
        public VOLUME_NAME vol_name;

        [FieldOffset(0)]
        public VOLUME_INFORMATION vol_inf;

        [FieldOffset(0)]
        public DATA_ATTR data;

        [FieldOffset(0)]
        public INDEX_ROOT index_root;

        [FieldOffset(0)]
        public INDEX_BLOCK index_blk;

        [FieldOffset(0)]
        public BITMAP_ATTR bmp;

        [FieldOffset(0)]
        public REPARSE_POINT reparse;

        [FieldOffset(0)]
        public EA_INFORMATION ea_inf;

        [FieldOffset(0)]
        public EA_ATTR ea;

        [FieldOffset(0)]
        public PROPERTY_SET property_set;

        [FieldOffset(0)]
        public LOGGED_UTILITY_STREAM logged_util_stream;

        [FieldOffset(0)]
        public EFS_ATTR_HEADER efs;
    }
}
