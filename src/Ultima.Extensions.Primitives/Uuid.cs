namespace Ultima.Extensions.Primitives;

using System;
using System.Buffers.Binary;

/// <summary>
/// Contains some methods to works with UUID.
/// </summary>
/// <remarks>
/// <see cref="Guid"/> is variant 1 UUID with time_low, time_mid and time_hi_and_version in little endian, which is incompatible with UUID on the other
/// platforms. So this class provides the methods to convert between <see cref="Guid"/> and UUID.
/// </remarks>
public static class Uuid
{
    public static byte[] FromGuid(Guid guid)
    {
        var data = guid.ToByteArray();

        if (!IsVariant1(data))
        {
            throw new ArgumentException("The value must be varian 1 UUID.", nameof(guid));
        }

        // Swap endianess for the first 3 fields.
        data.AsSpan(0, 4).Reverse();
        data.AsSpan(4, 2).Reverse();
        data.AsSpan(6, 2).Reverse();

        return data;
    }

    /// <summary>
    /// Convert a variant 1 UUID to a <see cref="Guid"/>.
    /// </summary>
    /// <param name="uuid">
    /// The UUID to convert.
    /// </param>
    /// <returns>
    /// A corresponding <see cref="Guid"/>.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// <see cref="ReadOnlySpan{T}.Length"/> of <paramref name="uuid"/> is not 16 or it is not a variant 1 UUID.
    /// </exception>
    public static Guid ToGuid(ReadOnlySpan<byte> uuid)
    {
        if (uuid.Length != 16)
        {
            throw new ArgumentException("The value must be exactly 16 bytes.", nameof(uuid));
        }
        else if (!IsVariant1(uuid))
        {
            throw new ArgumentException("The value must be variant 1 UUID.", nameof(uuid));
        }

        // Convert some fields from big-endian.
        var a = BinaryPrimitives.ReadInt32BigEndian(uuid);
        var b = BinaryPrimitives.ReadInt16BigEndian(uuid[4..]);
        var c = BinaryPrimitives.ReadInt16BigEndian(uuid[6..]);
        var d = uuid[8];
        var e = uuid[9];
        var f = uuid[10];
        var g = uuid[11];
        var h = uuid[12];
        var i = uuid[13];
        var j = uuid[14];
        var k = uuid[15];

        return new(a, b, c, d, e, f, g, h, i, j, k);
    }

    private static bool IsVariant1(ReadOnlySpan<byte> uuid) => (uuid[8] & 0xc0) == 0x80;
}
