namespace Ultima.Extensions.Currency;

using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;

/// <summary>
/// Represents a currency code in ISO 4217 Alphabetic format.
/// </summary>
[DebuggerDisplay($"{nameof(Value)}")]
[JsonConverter(typeof(CurrencyCodeJsonConverter))]
[StructLayout(LayoutKind.Sequential, Pack = 1)]
[TypeConverter(typeof(CurrencyCodeConverter))]
public unsafe struct CurrencyCode : IEquatable<CurrencyCode>, IComparable<CurrencyCode>, IComparable
{
    /// <summary>
    /// Represents a null value for <see cref="CurrencyCode"/>.
    /// </summary>
    public static readonly CurrencyCode Null = default;

    private static readonly IReadOnlySet<string> Valids = typeof(CurrencyInfo).Assembly.GetTypes()
        .Where(t => !t.IsAbstract && typeof(CurrencyInfo).IsAssignableFrom(t))
        .Select(t => t.GetField("StaticCode") ?? throw new Exception($"{t} does not have StaticCode constant."))
        .Select(f => (string?)f.GetRawConstantValue() ?? throw new Exception($"{f} contains invalid value."))
        .ToHashSet();

    private fixed byte data[4]; // Null-terminated code.

    /// <summary>
    /// Gets the country which this currency belong to.
    /// </summary>
    /// <value>
    /// A <see cref="RegionInfo"/> represents the country or <c>null</c> if this currency is a supranational.
    /// </value>
    /// <exception cref="InvalidOperationException">
    /// The current value is <see cref="Null"/>.
    /// </exception>
    public RegionInfo? Country
    {
        get
        {
            if (this.data[0] == 0)
            {
                throw new InvalidOperationException("The current value is a null currency.");
            }
            else if (this.data[0] == 0x58 || (this.data[0] == 0x45 && this.data[1] == 0x55 && this.data[2] == 0x52))
            {
                // The code begin with X or is Euro.
                return null;
            }

            Span<char> code = stackalloc char[2];

            code[0] = (char)this.data[0];
            code[1] = (char)this.data[1];

            return new(new string(code));
        }
    }

    /// <summary>
    /// Gets a <see cref="string"/> represents the code for this currency.
    /// </summary>
    /// <value>
    /// The code for this currency, in ISO 4217 Alphabetic format.
    /// </value>
    /// <exception cref="InvalidOperationException">
    /// The current value is <see cref="Null"/>.
    /// </exception>
    public string Value
    {
        get
        {
            if (this.data[0] == 0)
            {
                throw new InvalidOperationException("The current value is a null currency.");
            }

            Span<char> value = stackalloc char[3];

            value[0] = (char)this.data[0];
            value[1] = (char)this.data[1];
            value[2] = (char)this.data[2];

            return new(value);
        }
    }

    public static bool operator ==(CurrencyCode left, CurrencyCode right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(CurrencyCode left, CurrencyCode right)
    {
        return !(left == right);
    }

    /// <summary>
    /// Convert an ISO 4217 Alphabetic code to a <see cref="CurrencyCode"/>.
    /// </summary>
    /// <param name="s">
    /// An ISO 4217 Alphabetic code to convert.
    /// </param>
    /// <returns>
    /// A <see cref="CurrencyCode"/> represents the value of <paramref name="s"/>.
    /// </returns>
    /// <exception cref="FormatException">
    /// <paramref name="s"/> is not a valid currency code.
    /// </exception>
    /// <remarks>
    /// This method ACCEPT any valid currency code, including a test currency (XTS). Use <see cref="CurrencyInfo.IsOfficial"/> or
    /// <see cref="CurrencyInfo.GetOfficial(string)"/> to check if the currency is an official currency.
    /// </remarks>
    public static CurrencyCode Parse(string s)
    {
        if (!Valids.Contains(s))
        {
            throw new FormatException($"{s} is not a valid currency code.");
        }

        CurrencyCode code;

        code.data[0] = (byte)s[0];
        code.data[1] = (byte)s[1];
        code.data[2] = (byte)s[2];

        return code;
    }

    public int CompareTo(CurrencyCode other)
    {
        for (var i = 0; i < 3; i++)
        {
            var l = this.data[i];
            var r = other.data[i];

            if (l < r)
            {
                return -1;
            }
            else if (l > r)
            {
                return 1;
            }
        }

        return 0;
    }

    public int CompareTo(object? obj)
    {
        if (obj == null)
        {
            return 1;
        }
        else if (obj is CurrencyCode other)
        {
            return this.CompareTo(other);
        }
        else
        {
            throw new ArgumentException($"The value is not an instance of {typeof(CurrencyCode)}.", nameof(obj));
        }
    }

    public bool Equals(CurrencyCode other)
    {
        for (var i = 0; i < 3; i++)
        {
            if (other.data[i] != this.data[i])
            {
                return false;
            }
        }

        return true;
    }

    public override bool Equals(object? obj) => obj is CurrencyCode other && this.Equals(other);

    public override int GetHashCode()
    {
        fixed (byte* p = this.data)
        {
            return Unsafe.AsRef<int>(p);
        }
    }

    public override string ToString() => this.Value;
}
