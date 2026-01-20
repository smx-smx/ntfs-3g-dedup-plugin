#!/usr/bin/env bash
cd "$(dirname "$(readlink -f "$0")")"

rm src/* || true


(cat <<EOF
// stat
#include <sys/stat.h>
// timespec
#include <time.h>
typedef int BOOL;
EOF
) | gcc -x c -E - | sed '/ size_t;$/d' > sys.gch

clangsharp_args(){
	echo \
	cmd /C ClangSharpPInvokeGenerator \
	-c unix-types \
	-c log-potential-typedef-remappings \
	-c multi-file \
	-c generate-helper-types \
	-c exclude-funcs-with-body \
	-c log-exclusions \
	-c default-codegen \
	-D __CYGWIN__ \
	-D HAVE_CONFIG_H \
	-D __time_t_defined \
	-x c \
	-I C:/cygwin64/usr/include \
	-I ../ \
	-n Ntfs3gInterop \
	-m Ntfs3g \
	-l ntfs-3g \
	-om CSharp \
	-o src
}

hide_fn(){
	local fn="$1"
	echo "-D${fn}=__no_${fn}"
}

$(clangsharp_args) \
	-f include/ntfs-3g/device.h \
	-f include/ntfs-3g/plugin.h \
	-f include/ntfs-3g/index.h \
	-f include/ntfs-3g/layout.h \
	-f include/ntfs-3g/inode.h \
	-f include/ntfs-3g/attrib.h \
	-f include/ntfs-3g/runlist.h \
	-f include/ntfs-3g/volume.h \
	-f C:/cygwin64/usr/include/time.h \
	-f C:/cygwin64/usr/include/sys/signal.h \
	-e ATTR_RECORD::compressed_end \
	-e ATTR_RECORD::resident_end \
	-e ATTR_RECORD::non_resident_end \
	-e INTX_FILE::device_end \
	-e INTX_FILE::target \
	-e STANDARD_INFORMATION::v1_end \
	-e STANDARD_INFORMATION::v3_end \
	-e SECURITY_DESCRIPTOR_CONSTANTS::SECURITY_DESCRIPTOR_MIN_LENGTH \
	-r BOOL=int

$(clangsharp_args) \
	-f sys.gch \
	-i stat \
	-i timespec \
