namespace Ntfs3gInterop
{
    public partial struct _runlist_element
    {
        [NativeTypeName("VCN")]
        public long vcn;

        [NativeTypeName("LCN")]
        public long lcn;

        [NativeTypeName("s64")]
        public long length;
    }
}
