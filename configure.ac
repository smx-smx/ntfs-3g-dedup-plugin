AC_INIT([ntfs-3g-deduplication], [1.2.4], [jean-pierre.andre@wanadoo.fr])

AC_CONFIG_SRCDIR([src/dedup.c])
AC_CONFIG_MACRO_DIR([m4])
AC_CONFIG_AUX_DIR([build-aux])
AM_INIT_AUTOMAKE([-Wall -Werror subdir-objects foreign])
AM_SILENT_RULES([yes])
m4_ifdef([AM_PROG_AR], [AM_PROG_AR])

AC_PROG_CC
AC_C_BIGENDIAN

LT_INIT([dlopen])

AC_CONFIG_HEADERS([config.h])
AC_CONFIG_FILES([Makefile])

AC_CHECK_HEADERS([errno.h \
		  limits.h \
		  stdarg.h \
		  stddef.h \
		  stdlib.h \
		  string.h \
		  sys/types.h \
		  ctype.h \
		  malloc.h])

PKG_CHECK_MODULES([LIBNTFS_3G], [libntfs-3g >= 2016.2.22AR], [],
		  [AC_MSG_ERROR(["Unable to find libntfs-3g"])])

if test -z "$PKG_CONFIG"; then
    AC_PATH_PROG(PKG_CONFIG, pkg-config, no)
fi

test "x${PKG_CONFIG}" = "xno" && AC_MSG_ERROR([pkg-config wasn't found! Please install from your vendor, or see http://pkg-config.freedesktop.org/wiki/])
# Libraries often install their metadata .pc files in directories
# not searched by pkg-config. Let's workaround this.
export PKG_CONFIG_PATH=${PKG_CONFIG_PATH}:/lib/pkgconfig:/usr/lib/pkgconfig:/opt/gnome/lib/pkgconfig:/usr/share/pkgconfig:/usr/local/lib/pkgconfig:$prefix/lib/pkgconfig:/opt/gnome/share/pkgconfig:/usr/local/share/pkgconfig
PKG_CHECK_MODULES(
    [FUSE_MODULE],
    [fuse >= 2.6.0],
    ,
    [
        AC_MSG_ERROR([FUSE >= 2.6.0 was not found. Either older FUSE is still present, or FUSE is not fully installed (e.g. fuse, libfuse, libfuse2, libfuse-dev, etc packages). Source code: http://fuse.sf.net])
    ]
)
FUSE_LIB_PATH=`$PKG_CONFIG --libs-only-L fuse | sed -e 's,/[/]*,/,g' -e 's,[ ]*$,,'`

AC_OUTPUT
