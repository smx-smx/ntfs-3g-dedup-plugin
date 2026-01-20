/*
 * dotnet.h - NTFS-3G deduplication plugin
 *
 * Copyright (C) 2026 Stefano Moioli
 *
 * This program is free software: you can redistribute it and/or modify it under
 * the terms of the GNU General Public License as published by the Free Software
 * Foundation, either version 2 of the License, or (at your option) any later
 * version.
 *
 * This program is distributed in the hope that it will be useful, but WITHOUT
 * ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS
 * FOR A PARTICULAR PURPOSE.  See the GNU General Public License for more
 * details.
 *
 * You should have received a copy of the GNU General Public License along with
 * this program.  If not, see <http://www.gnu.org/licenses/>.
 */
#ifndef __DEDUP_DOTNET_H
#define __DEDUP_DOTNET_H

#include <stdint.h>
#include <ntfs-3g/plugin.h>

struct dotnet_args {
	const struct plugin_operations *(*pfnInit)(uint32_t tag);
	void (*pfnSetErrno)(int arg_errno);
};

extern struct dotnet_args dotnet_args;

#endif