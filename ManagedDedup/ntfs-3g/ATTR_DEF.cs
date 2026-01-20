using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Ntfs3gInterop
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public partial struct ATTR_DEF
    {
        [NativeTypeName("ntfschar[64]")]
        public _name_e__FixedBuffer name;

        public ATTR_TYPES type;

        [NativeTypeName("le32")]
        public uint display_rule;

        public COLLATION_RULES collation_rule;

        public ATTR_DEF_FLAGS flags;

        [NativeTypeName("sle64")]
        public ulong min_size;

        [NativeTypeName("sle64")]
        public ulong max_size;

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        [InlineArray(64)]
        public partial struct _name_e__FixedBuffer
        {
            public ushort e0;
        }
    }
}
