using System.Runtime.InteropServices;

namespace Ntfs3gInterop
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public partial struct GENERIC_MAPPING
    {
        public ACCESS_MASK generic_read;

        public ACCESS_MASK generic_write;

        public ACCESS_MASK generic_execute;

        public ACCESS_MASK generic_all;
    }
}
