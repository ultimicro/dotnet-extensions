namespace Ultima.Extensions.Currency;

/// <summary>
/// Represents Sterling (GBP) currency.
/// </summary>
public sealed class Sterling : CurrencyInfo
{
    public const string StaticCode = "GBP";

    private Sterling()
    {
        this.Code = CurrencyCode.Parse(StaticCode);
    }

    public static Sterling Instance { get; } = new();

    public override CurrencyCode Code { get; }

    public override int FractionalDigits => 2;

    public override bool IsOfficial => true;
}
