#region License
/*
 * Copyright (C) 2026 Stefano Moioli <smxdev4@gmail.com>
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */
#endregion
using System;
using System.Runtime.InteropServices;

namespace ManagedDedup;

public enum RtlCompressionFormat : ushort
{
    COMPRESSION_FORMAT_NONE = 0x0000,
    COMPRESSION_FORMAT_DEFAULT = 0x0001,
    COMPRESSION_FORMAT_LZNT1 = 0x0002,
    COMPRESSION_FORMAT_XPRESS = 0x0003,
    COMPRESSION_FORMAT_XPRESS_HUFF = 0x0004,
}

public class NativeApis : IDisposable
{
    private static readonly nint _libHandle;

    private delegate uint pfnRtlDecompressBuffer(
        RtlCompressionFormat CompressionFormat,
        byte[] UncompressBuffer,
        uint UncompressBufferSize,
        byte[] CompressBuffer,
        uint CompressBufferSize,
        out uint FinalUncompressedSize);

    private static readonly pfnRtlDecompressBuffer _RtlDecompressBufferFunc;

    public static unsafe uint RtlDecompressBuffer(RtlCompressionFormat CompressionFormat, byte[] input, byte[] output)
    {
        var hres = _RtlDecompressBufferFunc(CompressionFormat,
            output, (uint)output.Length,
            input, (uint)input.Length,
            out var uncompressedSize);
        
        if(hres != 0)
        {
            throw new InvalidOperationException("Decompression failed");
        }
        return uncompressedSize;
    }


    static NativeApis()
    {
        _libHandle = NativeLibrary.Load("ntdll");
        _RtlDecompressBufferFunc = Marshal.GetDelegateForFunctionPointer<pfnRtlDecompressBuffer>(
            NativeLibrary.GetExport(_libHandle, "RtlDecompressBuffer"));
    }

    public void Dispose()
    {
        NativeLibrary.Free(_libHandle);
    }
}
