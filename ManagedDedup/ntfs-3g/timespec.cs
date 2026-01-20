namespace Ntfs3gInterop
{
    public partial struct timespec
    {
        [NativeTypeName("time_t")]
        public nint tv_sec;

        [NativeTypeName("long")]
        public nint tv_nsec;
    }
}
