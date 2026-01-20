#region License
/*
 * Copyright (C) 2026 Stefano Moioli <smxdev4@gmail.com>
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */
#endregion
namespace ManagedDedup;

using System;

/// <summary>
/// CsCrc64
/// </summary>
/// <remarks>https://github.com/microsoft/service-fabric/blob/36f7531df0fd990f8af1792ae2cd5cf811521ab3/src/prod/ktl/src/src/kchecksum.cpp</remarks>
public static class ManagedCrc64
{
    private const ulong Poly = 0x9a6c9329ac4bc9b5;

    private static readonly ulong[] Table;

    static ManagedCrc64()
    {
        Table = new ulong[256];
        for (uint i = 0; i < 256; i++)
        {
            ulong crc = i;
            for (int j = 0; j < 8; j++)
            {
                if ((crc & 1) == 1)
                    crc = (crc >> 1) ^ Poly;
                else
                    crc >>= 1;
            }
            Table[i] = crc;
        }
    }

    public static ulong Compute(ReadOnlySpan<byte> data, ulong initialCrc = unchecked((ulong)-1))
    {
        ulong crc = ~initialCrc;

        foreach (byte b in data)
        {
            byte index = (byte)(crc ^ b);
            crc = (crc >> 8) ^ Table[index];
        }

        return ~crc;
    }
}