namespace Ultima.Extensions.Currency;

using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Text.Json.Serialization;

/// <summary>
/// Represents a currency code in ISO 4217 Alphabetic format.
/// </summary>
[DebuggerDisplay($"{nameof(Value)}")]
[JsonConverter(typeof(CurrencyCodeJsonConverter))]
[TypeConverter(typeof(CurrencyCodeConverter))]
public sealed class CurrencyCode : IEquatable<CurrencyCode?>, IComparable<CurrencyCode?>, IComparable
{
    private static readonly IReadOnlyDictionary<string, CurrencyCode> Available = typeof(CurrencyInfo).Assembly.GetTypes()
        .Where(t => !t.IsAbstract && typeof(CurrencyInfo).IsAssignableFrom(t))
        .Select(t => t.GetField("StaticCode") ?? throw new Exception($"{t} does not have StaticCode constant."))
        .Select(f => (string?)f.GetRawConstantValue() ?? throw new Exception($"{f} contains invalid value."))
        .ToDictionary(c => c, c => new CurrencyCode(c));

    private CurrencyCode(string value)
    {
        this.Value = value;
        this.Country = (value[0] == 'X' || value == "EUR") ? null : new(value[..2]);
    }

    /// <summary>
    /// Gets the country which this currency belong to.
    /// </summary>
    /// <value>
    /// A <see cref="RegionInfo"/> represents the country or <see langword="null"/> if this currency is a supranational.
    /// </value>
    public RegionInfo? Country { get; }

    /// <summary>
    /// Gets a <see cref="string"/> represents the code for this currency.
    /// </summary>
    /// <value>
    /// The code for this currency, in ISO 4217 Alphabetic format.
    /// </value>
    public string Value { get; }

    public static bool operator ==(CurrencyCode? left, CurrencyCode? right)
    {
        if (ReferenceEquals(left, right))
        {
            return true;
        }
        else if (left is null)
        {
            return false;
        }
        else
        {
            return left.Equals(right);
        }
    }

    public static bool operator !=(CurrencyCode? left, CurrencyCode? right) => !(left == right);

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
        if (!Available.TryGetValue(s, out var code))
        {
            throw new FormatException($"{s} is not a valid currency code.");
        }

        return code;
    }

    public int CompareTo(CurrencyCode? other) => this.Value.CompareTo(other?.Value);

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

    public bool Equals(CurrencyCode? other) => this.Value.Equals(other?.Value);

    public override bool Equals(object? obj) => obj is CurrencyCode other && this.Equals(other);

    public override int GetHashCode() => this.Value.GetHashCode();

    public override string ToString() => this.Value;
}
