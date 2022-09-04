namespace Ultima.Extensions.Currency;

/// <summary>
/// Provides a details for a specific currency.
/// </summary>
public abstract class CurrencyInfo
{
    private static readonly IReadOnlyDictionary<string, CurrencyInfo> Available = typeof(CurrencyInfo).Assembly.GetTypes()
        .Where(t => !t.IsAbstract && typeof(CurrencyInfo).IsAssignableFrom(t))
        .Select(t => t.GetProperty("Instance") ?? throw new Exception($"{t} does not have Instance property."))
        .Select(p => (CurrencyInfo?)p.GetValue(null) ?? throw new Exception($"{p} contains invalid value."))
        .ToDictionary(i => i.Code.Value);

    protected CurrencyInfo()
    {
    }

    /// <summary>
    /// Gets a <see cref="CurrencyCode"/> for this currency.
    /// </summary>
    public abstract CurrencyCode Code { get; }

    /// <summary>
    /// Gets a number of fractional digits for this currency (e.g. JPY is 0 and USD is 2).
    /// </summary>
    public abstract int FractionalDigits { get; }

    /// <summary>
    /// Gets a value indicating whether this currency is an official currency for at least one country.
    /// </summary>
    public abstract bool IsOfficial { get; }

    /// <summary>
    /// Gets a <see cref="CurrencyInfo"/> for a specified <see cref="CurrencyCode"/>.
    /// </summary>
    /// <param name="code">
    /// The currency to get.
    /// </param>
    /// <returns>
    /// A <see cref="CurrencyInfo"/> for <paramref name="code"/>.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// <paramref name="code"/> is <see cref="CurrencyCode.Null"/>.
    /// </exception>
    /// <remarks>
    /// This method return ALL available currencies, including a test currency (XTS). You MUST use <see cref="GetOfficial(string)"/> or checking
    /// <see cref="IsOfficial"/> when accepting a currency from the user.
    /// </remarks>
    public static CurrencyInfo Get(CurrencyCode code)
    {
        if (code == CurrencyCode.Null)
        {
            throw new ArgumentException("The value is a null currency.", nameof(code));
        }

        // We will never get KeyNotFoundException here due to CurrencyCode can only be constructed from the same currency set.
        return Available[code.Value];
    }

    /// <summary>
    /// Gets a <see cref="CurrencyInfo"/> for an official currency specified by ISO 4217 Alphabetic code.
    /// </summary>
    /// <param name="code">
    /// The currency to get.
    /// </param>
    /// <returns>
    /// A <see cref="CurrencyInfo"/> for <paramref name="code"/> or <c>null</c> if <paramref name="code"/> is not a valid currency or not an official currency.
    /// </returns>
    public static CurrencyInfo? GetOfficial(string code)
    {
        if (!Available.TryGetValue(code, out var info) || !info.IsOfficial)
        {
            return null;
        }

        return info;
    }

    public override sealed bool Equals(object? obj) => obj is CurrencyInfo other && other.Code == this.Code;

    public override sealed int GetHashCode() => this.Code.GetHashCode();

    /// <summary>
    /// Round a <see cref="decimal"/> to a number of fractional digits specified by <see cref="FractionalDigits"/>.
    /// </summary>
    /// <param name="amount">
    /// The value to round.
    /// </param>
    /// <returns>
    /// The rounded value.
    /// </returns>
    /// <exception cref="OverflowException">
    /// The result is outside the range of a <see cref="decimal"/>.
    /// </exception>
    /// <remarks>
    /// This method is just a shorthand for <see cref="Math.Round(decimal, int)"/>.
    /// </remarks>
    public decimal Round(decimal amount) => Math.Round(amount, this.FractionalDigits);

    /// <summary>
    /// Checks if a specified <see cref="decimal"/> is a valid amount for this currency.
    /// </summary>
    /// <param name="amount">
    /// The value to check.
    /// </param>
    /// <returns>
    /// <c>true</c> if <paramref name="amount"/> is valid for this currency; otherwise <c>false</c>.
    /// </returns>
    /// <remarks>
    /// The valid amount is an amount that have fractional digits equal or less than <see cref="FractionalDigits"/>.
    /// </remarks>
    public bool IsValidAmount(decimal amount)
    {
        var digits = this.FractionalDigits;
        var mod = 1m;

        for (var i = 0; i < digits; i++)
        {
            mod /= 10m;
        }

        return (amount % mod) == 0m;
    }

    /// <summary>
    /// Convert a fractional amount to a minor unit amount.
    /// </summary>
    /// <param name="amount">
    /// The amount to convert.
    /// </param>
    /// <returns>
    /// The amount representing the minor unit for this currency.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// <paramref name="amount"/> is not a valid amount for this currency.
    /// </exception>
    /// <exception cref="OverflowException">
    /// The resulted value is too small or too large for <see cref="long"/>.
    /// </exception>
    /// <remarks>
    /// The minor unit is the smallest unit for a currency (e.g. USD it is 1 cent). For currency that does not have frational digits like JPY this function will
    /// simply convert <see cref="decimal"/> to <see cref="long"/>.
    /// </remarks>
    public long GetMinorUnitAmount(decimal amount)
    {
        // Calculate the value to multiply target amount.
        var digits = this.FractionalDigits;
        var mul = 1m;

        for (var i = 0; i < digits; i++)
        {
            mul *= 10m;
        }

        // Multiply target amount and check if result has no fractional digits.
        var result = amount * mul;

        if (decimal.Truncate(result) != result)
        {
            throw new ArgumentException($"The value is not valid for this currency.", nameof(amount));
        }

        return decimal.ToInt64(result);
    }

    public override sealed string ToString() => this.Code.ToString();
}
