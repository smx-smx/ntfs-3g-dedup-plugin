#region License
/*
 * Copyright (C) 2026 Stefano Moioli <smxdev4@gmail.com>
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */
#endregion
using Microsoft.Win32.SafeHandles;
using Smx.SharpIO;
using Smx.SharpIO.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.IO.Hashing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Win32;
using Windows.Win32.Storage.FileSystem;

namespace ManagedDedup;

public class PInvokeConstants
{
    public const uint GENERIC_READ = 0x80000000;
}

public record ChunkHeaderMetaData(
    ChunkHeaderType ChunkType,
    int Index,
    ChunkEntryType Type,
    object Data);

public static class Debug
{
    public static void WriteFile(string filePath, byte[] data)
    {
        File.WriteAllBytes(filePath, data);
    }
}

public class ChunkEntryObj
{
    public ChunkHeaderType Type { get; private set; }
    public readonly List<ChunkHeaderMetaData> Data = new List<ChunkHeaderMetaData>();

    public ChunkEntryObj(ChunkHeaderType type, List<ChunkHeaderMetaData> data)
    {
        Type = type;
        Data = data;
    }

    public T GetEntry<T>(int i, ChunkEntryType expectedType)
    {
        var item = Data.ElementAtOrDefault(i);
        if (item == null)
        {
            throw new ArgumentException($"element {i} not found in metadata");
        }
        if (item.Type != expectedType)
        {
            throw new ArgumentException($"element type {item.Type} != expected {expectedType}");
        }
        if (item.Data is not T res)
        {
            throw new ArgumentException($"cannot cast {item.Data.GetType()} to {typeof(T)}");
        }
        return res;
    }
}

public record SmapItem(long Offset, StreamMapRecord Record);

public struct ReparsePointHeader
{
    public uint ReparseTag;
    public ushort ReparseLength;
    private ushort _padding;
}

public struct DedupReparseHeader
{
    public byte FormatMajor;
    public byte FormatMinor;
    public ushort BodyLength;
}

public enum ChunkHeaderType : uint
{
    // reparse point chunks
    /// <summary>
    /// "File Entry" Reparse Point
    /// </summary>
    FERP = 0x70526546,
    /// <summary>
    /// "Reparse Buffer" Reparse Point
    /// </summary>
    RBRP = 0x70526252,
    /// <summary>
    /// "Dedup Data" Reparse Point
    /// </summary>
    DDRP = 0x70526444,
    // stream chunks
    /// <summary>
    /// Chunk Header Record
    /// </summary>
    CKHR = 0x72686B43,
    /// <summary>
    /// Redirection Table
    /// </summary>
    RRTL = 0x6C747252,
    /// <summary>
    /// Stream Map Properties 
    /// </summary>
    SMAP = 0x70616D53,
    /// <summary>
    /// Chunk Container Header
    /// </summary>
    CTHR = 0x72687443
}

public struct ChunkOuterHeader
{
    public ChunkHeaderType Type;
    public uint Checksum;
}

public struct ChunkHeader
{
    public uint Size;
    public ushort _padding;
    public ushort NumEntries;
}

public struct CommonHeader
{
    public ChunkHeaderType Magic;
    // [01 03 03]
    public byte Version1;
    public byte Version2;
    public byte Version3;
    public byte Unknown;
}

public struct ChunkContainerHeader
{
    public CommonHeader CommonHeader;
    public ulong SequenceNumber;
    public uint Id;
    public uint Generation;

    public uint _Unknown1;
    public uint _Unknown2;
    public ulong FileSize; // Valid Data Length

    public ulong _Unknown3;
    public uint RedirectionTableOffset; // 0x4000
    public uint _Unknown4;

    public ulong Checksum;
}

/// <summary>
/// RRTL
/// </summary>
public struct RedirectionTableHeader
{
    public const int SIZE = 0x20;
    public const int OFFSET = 0x3000;

    public CommonHeader CommonHeader;
    public uint _Unknown0;
    public uint ItemSize;
    public uint _Unknown1;
    public uint _Unknown2;
    public ulong Checksum;
}

public struct RedirectionTableData
{
    public const int SIZE = 0x20;

    public uint NumEntries;

    /*
    public uint _Unknown1;
    public ulong _Unknown2;
    public uint BackupRedirectionTableOffset; // 0x4000
    */
}

public struct RedirectionTableItem
{
    public const int SIZE = 8;

    public uint SequenceNumber;
    public uint Offset;
}

public enum ChunkType : ushort
{
    Data = 1,
    Stream = 2
}

public enum ChunkCompressionFormat : uint
{
    NONE = 0,
    LZNT1 = 1,
    XPRESS = 2,
    XPRESS_HUFFMAN = 3
}

public struct CommonChunkHeader
{
    public uint Id;
    public uint DataSize;
    public ChunkType Type; // 0x1
    public ushort HeaderSize; // 0x38
    public uint _Unknown1; // 0x8
    public uint _Unknown2; // 0x8
    public uint _Unknown3; // 0x8
}

/// <summary>
/// CKHR, in Data file
/// </summary>
public unsafe struct ChunkDataHeader
{
    public const int SIZE = 0x28;

    public CommonHeader CommonHeader;
    public CommonChunkHeader ChunkHeader;

    public ChunkCompressionFormat CompressionFormat; // 0x2
    public uint _Unknown1; // 0x2

    private DataDigestBuffer _digest;

    public ulong _Unknown2;
    public ulong Checksum;

    public byte[] Digest => ((ReadOnlySpan<byte>)_digest).ToArray();
}

/// <summary>
/// CKHR, in Stream file
/// </summary>
public unsafe struct ChunkRecordHeader
{
    public const int SIZE = 0x38;

    public CommonHeader CommonHeader;
    public CommonChunkHeader ChunkHeader;

    public fixed byte _Unknown4[0x18];
    private ChunkDigestBuffer _digest;
    public fixed byte _Unknown5[16];
    public ulong _Unknown6;

    public ulong Checksum;

    public byte[] Digest => ((ReadOnlySpan<byte>)_digest).ToArray();
}

public enum ChunkEntryType : ushort
{
    None = 0,
    Id = 5,
    // seen in PRBR
    QWord = 6,
    BitmapFlag = 9,
    VersionFlag = 10,
    CreationTime = 11,
    Guid = 12,
    ChunkBlob = 13,
    // seen in PRDD
    IdRefData = 14
}

public enum ChunkEntryIndex_FERP
{
    Revision = 0,
    Flags = 1,
    StoreGuid = 2,
    CreationTime = 3,
    FileSize = 4,
    BitmapType = 6,
    ReparseBuffer = 7,
    DedupReparse = 8
}

public enum ChunkEntryIndex_RBRP
{
    Revision = 0,
    RecallSizeShift = 1,
    RecallSmapDepth = 2,
    IdRef = 4,
    FileGuid = 5
}

public enum ChunkEntryIndex_DDRP
{
    Revision = 0,
    /// <summary>
    /// [SequenceNumber,StreamId] of the first data chunk
    /// </summary>
    IdRef = 1,
    ReparseInfo = 2
}

public enum RecallBitmapType : int
{
    Normal = 0,
    Snapshot = 1,
    Filesystem = 2
}

public struct ChunkEntry
{
    public ChunkEntryType Type;
    public ushort Size;
    public uint Offset;
}

[InlineArray(16)]
public struct ChunkDigestBuffer
{
    private byte _element0;
}

[InlineArray(32)]
public struct DataDigestBuffer
{
    private byte _element0;
}

/// <summary>
/// DDRP in Reparse Point
/// </summary>
public struct DedupReparseEntry
{
    // id
    public uint SequenceNumber;
    public uint StreamId;
    // addr
    public uint Offset;
    public uint _Unknown0;
    // info
    public uint NumProperties;
    public uint Size;
    public uint DataSize;
    public uint ItemSize;

    private ChunkDigestBuffer _digest;

    public ulong PartEnd;
    public ulong PartBegin;

    public byte[] Digest => ((ReadOnlySpan<byte>)_digest).ToArray();
}

/// <summary>
/// SMAP data
/// </summary>
public struct StreamMapRecord
{
    public uint SequenceNumber;
    public uint Id1;
    public uint DataOffset;
    public uint Id2;
    public ulong UncompressedFileOffsetEnd;
    private DataDigestBuffer _digest;
    public uint CompressedSize;
    public int _Unknown0;

    public byte[] Digest => ((ReadOnlySpan<byte>)_digest).ToArray();
}

public struct StreamMapHeader
{
    public CommonHeader CommonHeader;
}

public class DedupReparsePoint
{
    public required Guid DedupStoreId { get; set; }
    public required uint StreamId { get; set; }
}

public class DedupStreamInfo
{
    public string Path { get; private set; }
    public uint StreamId { get; private set; }
    public uint Generation { get; private set; }

    public DedupStreamInfo(string fullPath)
    {
        Path = fullPath;
        var ext = System.IO.Path.GetExtension(fullPath);
        if (ext != ".ccc")
        {
            throw new ArgumentException("not a dedup stream");
        }
        var baseName = System.IO.Path.GetFileNameWithoutExtension(fullPath);
        var parts = baseName.Split('.');
        if (parts.Length != 2)
        {
            throw new ArgumentException("invalid stream name");
        }
        StreamId = Convert.ToUInt32(parts[0], 16);
        Generation = Convert.ToUInt32(parts[1], 16);
    }
}

public class DedupDataStream : IDisposable
{
    private readonly MFile _mf;
    private readonly SpanStream _st;

    private readonly SafeFileHandle _handle;
    private readonly FileStream _fs;

    public DedupDataStream(string filePath)
    {

        _handle = PInvoke.CreateFile(
            filePath, PInvokeConstants.GENERIC_READ, FILE_SHARE_MODE.FILE_SHARE_READ,
            null, FILE_CREATION_DISPOSITION.OPEN_EXISTING,
            FILE_FLAGS_AND_ATTRIBUTES.FILE_FLAG_BACKUP_SEMANTICS,
            null);

        _fs = new FileStream(_handle, FileAccess.Read);

        _mf = new MFile(_fs);
        _st = new SpanStream(_mf);
    }

    public void Dispose()
    {
        _handle.Dispose();
        _fs.Dispose();
        _st.Dispose();
        _mf.Dispose();
    }

    private (ChunkDataHeader, Memory<byte>) ReadDataChunkHeader(SpanStream dataSt, StreamMapRecord smap)
    {
        Debug.WriteFile("data.bin", dataSt.Memory.ToArray());

        var ckhr = dataSt.SliceHere(Marshal.SizeOf<ChunkDataHeader>());
        Debug.WriteFile("ckhr_data.bin", ckhr.Memory.ToArray());

        var hdr = ckhr.ReadStruct<ChunkDataHeader>();
        if (hdr.CommonHeader.Magic != ChunkHeaderType.CKHR)
        {
            throw new InvalidDataException($"Expected magic type {ChunkHeaderType.CKHR}, got {hdr.CommonHeader.Magic}");
        }
        if (hdr.ChunkHeader.Type != ChunkType.Data)
        {
            throw new NotSupportedException("Unsupported struct type");
        }
        if (hdr.ChunkHeader.HeaderSize != ChunkDataHeader.SIZE)
        {
            throw new NotSupportedException("Unsupported struct size");
        }

        if (hdr.ChunkHeader.Id != smap.SequenceNumber)
        {
            throw new InvalidDataException("Sequence number mismatch");
        }

        if (hdr.ChunkHeader.DataSize != smap.CompressedSize)
        {
            throw new InvalidDataException("Body size mismatch");
        }

        if (!hdr.Digest.SequenceEqual(smap.Digest))
        {
            throw new InvalidDataException("Digest mismatch");
        }

        var computed = ManagedCrc64.Compute(ckhr.Span.Slice(0, ckhr.Span.Length - 8));
        if (computed != hdr.Checksum)
        {
            throw new InvalidDataException("Crc64 mismatch");
        }

        var chunkData = dataSt.Memory.Slice(ckhr.Memory.Length, (int)smap.CompressedSize);
        return (hdr, chunkData);

    }

    public Memory<byte> ReadEntry(StreamMapRecord smap, ref ulong lastFileOffset)
    {
        if (smap.UncompressedFileOffsetEnd <= lastFileOffset)
        {
            throw new InvalidDataException("data is not sequential");
        }

        // $FIXME: add new Smx.SharpIO API
        var subStream = _st.SliceHere();
        subStream.Position = smap.DataOffset;
        var dataSt = subStream.SliceHere((int)smap.CompressedSize + Marshal.SizeOf<ChunkDataHeader>());

        var (hdr, inputData) = ReadDataChunkHeader(dataSt, smap);

        var uncompressedSize = smap.UncompressedFileOffsetEnd - lastFileOffset;
        lastFileOffset = smap.UncompressedFileOffsetEnd;

        if (smap.CompressedSize != uncompressedSize)
        {
            var rtlFormat = hdr.CompressionFormat switch
            {
                ChunkCompressionFormat.LZNT1 => RtlCompressionFormat.COMPRESSION_FORMAT_LZNT1,
                ChunkCompressionFormat.XPRESS => RtlCompressionFormat.COMPRESSION_FORMAT_XPRESS,
                ChunkCompressionFormat.XPRESS_HUFFMAN => RtlCompressionFormat.COMPRESSION_FORMAT_XPRESS_HUFF,
                _ => throw new ArgumentException($"Invalid compression format 0x{hdr.CompressionFormat:X}")
            };

            var decompressionBuffer = new byte[uncompressedSize];
            var decompressedSize = NativeApis.RtlDecompressBuffer(rtlFormat, inputData.ToArray(), decompressionBuffer);
            if (decompressedSize != uncompressedSize)
            {
                throw new InvalidDataException($"Decompression failed: expected 0x{uncompressedSize:X} bytes, got 0x{decompressedSize:X}");
            }
            return decompressionBuffer;
        } else
        {
            return inputData;
        }
    }
}

public class DedupStream : IDisposable
{
    private readonly DedupChunkStore _store;
    private readonly MFile _mf;
    private readonly SpanStream _st;

    private SafeFileHandle _handle;
    private FileStream _fs;

    public DedupStream(DedupChunkStore store, string streamPath)
    {
        _store = store;

        _handle = PInvoke.CreateFile(
            streamPath, PInvokeConstants.GENERIC_READ, FILE_SHARE_MODE.FILE_SHARE_READ,
            null, FILE_CREATION_DISPOSITION.OPEN_EXISTING,
            FILE_FLAGS_AND_ATTRIBUTES.FILE_FLAG_BACKUP_SEMANTICS,
            null);

        _fs = new FileStream(_handle, FileAccess.Read);

        _mf = new MFile(_fs);
        _st = new SpanStream(_mf);
        _st.PerformAt(RedirectionTableHeader.OFFSET, () =>
        {
            ReadRedirectionTable();
        });
    }

    private void ReadRedirectionTable()
    {
        var rrtl = _st.SliceHere(RedirectionTableHeader.SIZE);
        Debug.WriteFile("rrtl.bin", rrtl.Memory.ToArray());

        var hdr = rrtl.ReadStruct<RedirectionTableHeader>();
        if (hdr.CommonHeader.Magic != ChunkHeaderType.RRTL)
        {
            throw new InvalidDataException($"Expected magic type {ChunkHeaderType.RRTL}, got {hdr.CommonHeader.Magic}");
        }
        if (hdr.ItemSize != RedirectionTableItem.SIZE)
        {
            throw new NotSupportedException("Unsupported item struct size");
        }

        var checksum = rrtl.PerformAt(RedirectionTableHeader.SIZE - 8, () => rrtl.ReadUInt64());
        var computed = ManagedCrc64.Compute(rrtl.Span.Slice(0, RedirectionTableHeader.SIZE - 8));
        if (computed != checksum)
        {
            throw new InvalidDataException("Crc64 mismatch");
        }
    }

    public void Dispose()
    {
        _handle.Dispose();
        _fs.Dispose();
        _st.Dispose();
        _mf.Dispose();
    }

    private void ReadData(SpanStream smapSt, Stream outStream)
    {
        var numSmaps = (int)(smapSt.Length - Marshal.SizeOf<StreamMapHeader>()) / Marshal.SizeOf<StreamMapRecord>();

        var hdr = smapSt.ReadStruct<StreamMapHeader>();
        if (hdr.CommonHeader.Magic != ChunkHeaderType.SMAP)
        {
            throw new InvalidDataException($"Expected magic type {ChunkHeaderType.SMAP}, got {hdr.CommonHeader.Magic}");
        }

        outStream.SetLength(0);

        var lastFileOffset = 0UL;
        for (var i = 0; i < numSmaps; i++)
        {
            var rec = smapSt.ReadStruct<StreamMapRecord>();
            using var dataChunk = _store.GetDataStream(rec.Id1, rec.Id2);

            var chunkData = dataChunk.ReadEntry(rec, ref lastFileOffset);

            var fileOffsetBegin = (long)rec.UncompressedFileOffsetEnd - chunkData.Length;
            outStream.Seek(fileOffsetBegin, SeekOrigin.Begin);
            outStream.Write(chunkData.Span);
        }
    }

    public void ReadEntry(ChunkIdRef idref, DedupReparseEntry entry)
    {
        var subStream = _st.SliceHere();
        subStream.Position = entry.Offset;

        var ckhr = subStream.SliceHere(Marshal.SizeOf<ChunkRecordHeader>());
        Debug.WriteFile("ckhr_stream.bin", ckhr.Memory.ToArray());

        var hdr = ckhr.ReadStruct<ChunkRecordHeader>();
        if (hdr.CommonHeader.Magic != ChunkHeaderType.CKHR)
        {
            throw new InvalidDataException($"Expected magic type {ChunkHeaderType.CKHR}, got {hdr.CommonHeader.Magic}");
        }
        if (hdr.ChunkHeader.Type != ChunkType.Stream)
        {
            throw new NotSupportedException("Unsupported struct type");
        }
        if (hdr.ChunkHeader.HeaderSize != ChunkRecordHeader.SIZE)
        {
            throw new NotSupportedException("Unsupported struct size");
        }

        if (hdr.ChunkHeader.Id != entry.SequenceNumber
            || hdr.ChunkHeader.Id != idref.SequenceNumber)
        {
            throw new InvalidDataException("Sequence number mismatch");
        }

        if (hdr.ChunkHeader.DataSize != entry.DataSize)
        {
            throw new InvalidDataException("Body size mismatch");
        }

        if (!hdr.Digest.SequenceEqual(entry.Digest))
        {
            throw new InvalidDataException("Digest mismatch");
        }

        var computed = ManagedCrc64.Compute(ckhr.Span.Slice(0, ckhr.Span.Length - 8));
        if (computed != hdr.Checksum)
        {
            throw new InvalidDataException("Crc64 mismatch");
        }

        if (entry.DataSize < Marshal.SizeOf<StreamMapHeader>())
        {
            throw new InvalidDataException("Invalid Data size");
        }
        var smapListSize = entry.DataSize - Marshal.SizeOf<StreamMapHeader>();
        if ((smapListSize % Marshal.SizeOf<StreamMapRecord>()) != 0)
        {
            throw new InvalidDataException("Invalid Data size");
        }

        var numSmaps = smapListSize / Marshal.SizeOf<StreamMapHeader>();

        var smapSt = _st.Slice((int)entry.Offset + ckhr.Span.Length, (int)hdr.ChunkHeader.DataSize);

        using var ms = new MemoryStream();
        ReadData(smapSt, ms);
    }
}

public class DedupChunkStore
{
    private readonly string _drivePath;
    private readonly Guid _storeGuid;
    private readonly string _storeDir;

    public DedupChunkStore(string drivePath, Guid guid)
    {
        _drivePath = drivePath;
        _storeGuid = guid;
        _storeDir = GetChunkStorePath(drivePath, guid);
    }

    private string GetPath(params string[] path)
    {
        return Path.Combine([_storeDir, .. path]);
    }

    private static string GetChunkStorePath(string drivePath, Guid guid)
    {
        var path = Path.Combine(drivePath, "System Volume Information", "Dedup", "ChunkStore", $"{{{guid}}}.ddp");
        if (!Path.Exists(path))
        {
            throw new InvalidOperationException($"Invalid path: {path}");
        }
        return path;
    }

    public DedupDataStream GetDataStream(uint id1, uint id2)
    {
        var dataDir = GetPath("Data");
        var fileName = $"{id1:x8}.{id2:x8}.ccc";
        var path = Path.Combine(dataDir, fileName);
        if (!File.Exists(path))
        {
            throw new InvalidOperationException($"data stream {fileName} not found in store");
        }
        return new DedupDataStream(path);
    }


    public DedupStream GetStream(uint streamId)
    {
        var streamDir = GetPath("Stream");
        var item = Directory.EnumerateFiles(streamDir, @$"{streamId:x8}.*.ccc")
            .Select(x => new DedupStreamInfo(x))
            .Where(x => x.StreamId == streamId)
            .OrderByDescending(x => x.Generation)
            .FirstOrDefault();

        if (item == null)
        {
            throw new InvalidOperationException($"stream id {streamId:x} not found in store");
        }

        return new DedupStream(this, item.Path);
    }
}

public record ChunkIdRef(uint SequenceNumber, uint StreamId);


public class DedupReparsePointParser
{
    private static readonly ImmutableDictionary<ChunkHeaderType, Type> CHUNK_HEADER_ITEMS = ImmutableDictionary.ToImmutableDictionary(
        new KeyValuePair<ChunkHeaderType, Type>[]
        {
            KeyValuePair.Create(ChunkHeaderType.FERP, typeof(ChunkEntryIndex_FERP)),
            KeyValuePair.Create(ChunkHeaderType.RBRP, typeof(ChunkEntryIndex_RBRP)),
            KeyValuePair.Create(ChunkHeaderType.DDRP, typeof(ChunkEntryIndex_DDRP))
        });

    private static readonly int ITEM_NAME_PADDING = CHUNK_HEADER_ITEMS.Values.SelectMany(Enum.GetNames).Max(x => x.Length) + 5;
    private static readonly int CHUNK_TYPE_PADDING = Enum.GetNames<ChunkEntryType>().Max(x => x.Length);

    public const uint DEDUP_REPARSE_TAG = 0x80000013;

    private readonly string _drivePath;

    public DedupReparsePointParser(string drivePath)
    {
        _drivePath = drivePath;
    }

    private SpanStream ValidateAndConsumeReparseHeader(SpanStream st)
    {
        // read 8 bytes reparse header
        var header = st.ReadStruct<ReparsePointHeader>();
        if (header.ReparseTag != DEDUP_REPARSE_TAG)
        {
            throw new InvalidDataException("Not a Dedup reparse point");
        }

        // fork with the correct data length
        var reparseSt = st.SliceHere(header.ReparseLength);
        Debug.WriteFile("rp.bin", reparseSt.Memory.ToArray());

        var actualCrc32 = reparseSt.PerformAt(reparseSt.Length - 4, reparseSt.ReadUInt32);
        var computedCrc32 = Crc32.HashToUInt32(reparseSt.Memory.Slice(0, header.ReparseLength - 4).Span);

        if (actualCrc32 != computedCrc32)
        {
            throw new InvalidDataException("Reparse point CRC mismatch");
        }

        // remove the CRC from the stream
        return reparseSt.Slice(0, (int)reparseSt.Length - 4);
    }

    private ChunkEntryObj ReadChunkEntry(SpanStream st)
    {
        var items = new List<ChunkHeaderMetaData>();

        var chunkOuterHeader = st.ReadStruct<ChunkOuterHeader>();
        Console.WriteLine(chunkOuterHeader.Type);


        var knownIndices = CHUNK_HEADER_ITEMS.TryGetValue(chunkOuterHeader.Type, out var itemIndices);
        var printItem = (ChunkHeaderMetaData itm) =>
        {
            var chunkTypeName = (((knownIndices && itemIndices != null)
                ? Enum.GetName(itemIndices, itm.Index)
                : null) ?? "<unknown>").PadLeft(ITEM_NAME_PADDING, ' ');

            var itemTypeName = (Enum.GetName(itm.Type) ?? "<unknown>").PadRight(CHUNK_TYPE_PADDING);

            Console.WriteLine($"{chunkTypeName} [{itm.ChunkType}.{itm.Index}:{itemTypeName}]: {itm.Data}");
        };

        var chunkHeaderAndBody = st.SliceHere();
        var crc32 = Crc32.HashToUInt32(chunkHeaderAndBody.Memory.Span);
        if (crc32 != chunkOuterHeader.Checksum)
        {
            throw new InvalidDataException("Chunk header CRC mismatch");
        }

        var chunkHeader = st.ReadStruct<ChunkHeader>();

        for (var i = 0; i < chunkHeader.NumEntries; i++)
        {
            var entry = st.ReadStruct<ChunkEntry>();

            // end marker
            if (entry.Type == ChunkEntryType.None && entry.Size == 0)
            {
                break;
            }
            var data = st.Memory.Slice((int)entry.Offset, entry.Size);

            ChunkHeaderMetaData? itm = default;

            switch (entry.Type)
            {
                case ChunkEntryType.VersionFlag:
                case ChunkEntryType.Id:
                case ChunkEntryType.BitmapFlag:
                    var dw = data.Span.Cast<uint>()[0];
                    itm = new ChunkHeaderMetaData(chunkOuterHeader.Type, i, entry.Type, dw);
                    printItem(itm);
                    items.Add(itm);
                    break;
                case ChunkEntryType.QWord:
                    var qw = data.Span.Cast<ulong>()[0];

                    if (chunkOuterHeader.Type == ChunkHeaderType.RBRP && i == (int)ChunkEntryIndex_RBRP.IdRef)
                    {
                        var idref = new ChunkIdRef(
                            (uint)qw,
                            (uint)(qw >> 32)
                        );
                        itm = new ChunkHeaderMetaData(chunkOuterHeader.Type, i, entry.Type, idref);
                        printItem(itm);
                        items.Add(itm);
                    } else
                    {
                        itm = new ChunkHeaderMetaData(chunkOuterHeader.Type, i, entry.Type, qw);
                        printItem(itm);
                        items.Add(itm);
                    }
                    break;
                case ChunkEntryType.IdRefData:
                    var sequenceNumber = data.Span.Cast<uint>()[0];
                    var streamId = data.Span.Cast<uint>()[1];
                    itm = new ChunkHeaderMetaData(chunkOuterHeader.Type, i, entry.Type, new ChunkIdRef(sequenceNumber, streamId));
                    printItem(itm);
                    items.Add(itm);
                    break;
                case ChunkEntryType.Guid:
                    var guid = data.Span.Cast<Guid>()[0];
                    itm = new ChunkHeaderMetaData(chunkOuterHeader.Type, i, entry.Type, guid);
                    printItem(itm);
                    items.Add(itm);
                    break;
                case ChunkEntryType.ChunkBlob:
                    var entrySt = new SpanStream(data, st.Endianness);
                    if (chunkOuterHeader.Type == ChunkHeaderType.DDRP)
                    {
                        var dedupReparse = entrySt.ReadStruct<DedupReparseEntry>();
                        itm = new ChunkHeaderMetaData(chunkOuterHeader.Type, i, entry.Type, dedupReparse);
                        printItem(itm);
                        items.Add(itm);
                    } else
                    {
                        var subChunk = ReadChunkEntry(entrySt);
                        itm = new ChunkHeaderMetaData(chunkOuterHeader.Type, i, entry.Type, subChunk);
                        printItem(itm);
                        items.Add(itm);
                    }
                    break;
                case ChunkEntryType.CreationTime:
                    var filetimeRaw = data.Span.Cast<ulong>()[0];
                    var filetime = DateTime.FromFileTimeUtc((long)filetimeRaw);
                    itm = new ChunkHeaderMetaData(chunkOuterHeader.Type, i, entry.Type, filetime);
                    printItem(itm);
                    items.Add(itm);
                    break;
                default:
                    itm = new ChunkHeaderMetaData(chunkOuterHeader.Type, i, entry.Type, data.Span.ToArray());
                    printItem(itm);
                    items.Add(itm);
                    break;
            }
        }

        return new ChunkEntryObj(chunkOuterHeader.Type, items);
    }

    private ChunkEntryObj ReadDedupChunkHeaders(SpanStream st)
    {
        // read 4 bytes header dedup header and fork
        // $TODO: validate major/minor
        var header = st.ReadStruct<DedupReparseHeader>();
        var bodySt = st.SliceHere();
        return ReadChunkEntry(bodySt);
    }

    public enum ReparsePropertyCommon
    {
        Version = 0
    }

    public enum ReparsePropertyFile
    {
        ChunkStoreGuid = 2
    }

    public enum ReparsePropertyDedup
    {
        IdRef = 1,
        DedupInfo = 2
    }

    public void Parse(Memory<byte> data)
    {
        using var st = new SpanStream(data);

        var reparseSt = ValidateAndConsumeReparseHeader(st);

        var chunkHeaders = ReadDedupChunkHeaders(reparseSt);
        if (chunkHeaders.Type != ChunkHeaderType.FERP)
        {
            throw new InvalidDataException($"expected {ChunkHeaderType.FERP}, but found {chunkHeaders.Type}");
        }
        var version = chunkHeaders.GetEntry<uint>(
            (int)ReparsePropertyCommon.Version, ChunkEntryType.VersionFlag);
        if (version != 1)
        {
            throw new NotSupportedException($"version {version} is not supported");
        }

        var chunkStoreGuid = chunkHeaders.GetEntry<Guid>(
            (int)ReparsePropertyFile.ChunkStoreGuid, ChunkEntryType.Guid);

        var prdd = chunkHeaders.Data.FirstOrDefault(x => x.Type == ChunkEntryType.ChunkBlob
            && x.Data is ChunkEntryObj prddInfo
            && prddInfo.Type == ChunkHeaderType.DDRP);

        if (prdd == null || prdd.Data is not ChunkEntryObj prddInfo)
        {
            throw new InvalidDataException("Missing PRDD chunk");
        }

        var dedupInfo = prddInfo.GetEntry<DedupReparseEntry>(
            (int)ReparsePropertyDedup.DedupInfo, ChunkEntryType.ChunkBlob);

        var finalDrivePath = _drivePath;

        var store = new DedupChunkStore(finalDrivePath, chunkStoreGuid);
        using var stream = store.GetStream(dedupInfo.StreamId);

        var idRef = prddInfo.GetEntry<ChunkIdRef>(
            (int)ReparsePropertyDedup.IdRef, ChunkEntryType.IdRefData);

        if (dedupInfo.StreamId != idRef.StreamId)
        {
            throw new InvalidDataException("streamID mismatch");
        }
    }
}
