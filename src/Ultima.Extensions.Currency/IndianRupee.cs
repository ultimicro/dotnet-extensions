namespace Ultima.Extensions.Currency;

/// <summary>
/// Represents Indian rupee (INR) currency.
/// </summary>
public sealed class IndianRupee : CurrencyInfo
{
    public const string StaticCode = "INR";

    private IndianRupee()
    {
        this.Code = CurrencyCode.Parse(StaticCode);
    }

    public static IndianRupee Instance { get; } = new();

    public override CurrencyCode Code { get; }

    public override int FractionalDigits => 2;

    public override bool IsOfficial => true;
}
