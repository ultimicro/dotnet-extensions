namespace Ultima.Extensions.Currency;

/// <summary>
/// Represents Euro (EUR) currency.
/// </summary>
public sealed class Euro : CurrencyInfo
{
    public const string StaticCode = "EUR";

    private Euro()
    {
        this.Code = CurrencyCode.Parse(StaticCode);
    }

    public static Euro Instance { get; } = new();

    public override CurrencyCode Code { get; }

    public override int FractionalDigits => 2;

    public override bool IsOfficial => true;
}
