namespace Ntfs3gInterop
{
    public unsafe partial struct tm
    {
        public int tm_sec;

        public int tm_min;

        public int tm_hour;

        public int tm_mday;

        public int tm_mon;

        public int tm_year;

        public int tm_wday;

        public int tm_yday;

        public int tm_isdst;

        [NativeTypeName("long")]
        public nint tm_gmtoff;

        [NativeTypeName("char *")]
        public sbyte* tm_zone;
    }
}
