namespace Ultima.Extensions.Currency;

/// <summary>
/// Represents a test currency.
/// </summary>
public sealed class TestCurrency : CurrencyInfo
{
    public const string StaticCode = "XTS";

    private int fractionalDigits;

    private TestCurrency()
    {
        this.Code = CurrencyCode.Parse(StaticCode);
    }

    public static TestCurrency Instance { get; } = new();

    public override CurrencyCode Code { get; }

    public override int FractionalDigits => this.fractionalDigits;

    public override bool IsOfficial => false;

    public void SetFractionalDigits(int digits)
    {
        if (digits < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(digits));
        }

        this.fractionalDigits = digits;
    }
}
