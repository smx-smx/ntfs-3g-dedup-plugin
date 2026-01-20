/*
 * dotnet.c - NTFS-3G deduplication plugin
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
#define _GNU_SOURCE
#include <stdio.h>
#include <stdlib.h>
#include <stdbool.h>
#include <stdint.h>
#include <dlfcn.h>
#include <errno.h>

#if defined(_WIN32)
#include <processthreadsapi>
#include <handleapi.h>
#else
#include <pthread.h>
#endif

#include <ntfs-3g/plugin.h>
#include "dotnet.h"

void set_errno(int arg_errno){
	errno = arg_errno;
}

struct dotnet_args dotnet_args = {
	.pfnSetErrno = &set_errno
};

#define ARRAY_SIZE(arr) (sizeof(arr) / sizeof((arr)[0]))

typedef int (*pfnEzDotNetMain)(int argc, const char *argv[]);

int start_dotnet() {
	const char *helper_path = getenv("EZ_HELPER_PATH");
	const char *loader_path = getenv("EZ_LOADER_PATH");
	const char *asm_path = getenv("EZ_ASM_PATH");
	const char *asm_class_name = getenv("EZ_CLASS_NAME");
	const char *asm_method_name = getenv("EZ_CLASS_METHOD");

	bool fail=false;
	if(!helper_path){
		fputs("FATAL: EZ_HELPER_PATH not specified\n", stderr);
		fail = true;
	}
	if(!loader_path){
		fputs("FATAL: EZ_LOADER_PATH not specified\n", stderr);
		fail=true;
	}
	if(!asm_path){
		fputs("FATAL: EZ_ASM_PATH not specified\n", stderr);
		fail=true;
	}
	if(!asm_class_name){
		fputs("FATAL: EZ_CLASS_NAME not specified\n", stderr);
		fail=true;
	}
	if(!asm_method_name){
		fputs("FATAL: EZ_CLASS_METHOD not specified\n", stderr);
		fail=true;
	}

	if(fail){
		return EXIT_FAILURE;
	}

	void *ezDotNet = dlopen(helper_path, RTLD_GLOBAL | RTLD_NOW);
	if(ezDotNet == NULL){
		fprintf(stdout, "LoadLibraryA failed\n");
		return EXIT_FAILURE;
	}
	const pfnEzDotNetMain pfnMain = (pfnEzDotNetMain)(void *)dlsym(ezDotNet, "main");
	if (!pfnMain) {
		fputs("GetProcAddress failed\n", stderr);
		dlclose(ezDotNet);
		return EXIT_FAILURE;
	}

	char *ptr_args = NULL;
	asprintf(&ptr_args, "0x%llx", &dotnet_args);
	if(!ptr_args){
		fputs("asprintf() failed\n", stderr);
		dlclose(ezDotNet);
		return EXIT_FAILURE;
	}

	const char *argv[] = {
		"ezdotnet",
		loader_path,
		asm_path,
		asm_class_name,
		asm_method_name,
		"--ezdotnet",
		ptr_args
	};
	const int result = pfnMain(ARRAY_SIZE(argv), argv);
	dlclose(ezDotNet);
	return result;
}

void *start_dotnet_thread(void* arg){
	return (void *)(uintptr_t)start_dotnet();
}

void dotnet_init(){
	puts("...init");
#if defined(_WIN32) && !defined(__CYGWIN__)
	HANDLE hThread = CreateThread(NULL, 0, (LPTHREAD_START_ROUTINE)start_dotnet_thread, NULL, 0, NULL);
	if (hThread == NULL) {
		fputs("CreateThread failed\n", stderr);
		return;
	}
	CloseHandle(hThread);
	puts("...done");
#else
	pthread_t tid;
	if (pthread_create(&tid, NULL, &start_dotnet_thread, NULL) != 0) {
		fputs("pthread_create() failed\n", stderr);
		return;
	}
	if (pthread_detach(tid) != 0) {
		fputs("pthread_detach() failed\n", stderr);
	}
#endif
}
