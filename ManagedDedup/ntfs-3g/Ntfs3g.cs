using System.Runtime.InteropServices;

namespace Ntfs3gInterop
{
    public static unsafe partial class Ntfs3g
    {
        [DllImport("ntfs-3g", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int stat([NativeTypeName("const char *restrict")] sbyte* __path, [NativeTypeName("struct stat *restrict")] stat* __sbuf);
    }
}
