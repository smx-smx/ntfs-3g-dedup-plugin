namespace Ntfs3gInterop
{
    public unsafe partial struct sigaltstack
    {
        public void* ss_sp;

        public int ss_flags;

        [NativeTypeName("size_t")]
        public nuint ss_size;
    }
}
