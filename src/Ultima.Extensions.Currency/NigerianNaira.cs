namespace Ultima.Extensions.Currency;

/// <summary>
/// Represents Nigerian naira (NGN) currency.
/// </summary>
public sealed class NigerianNaira : CurrencyInfo
{
    public const string StaticCode = "NGN";

    private NigerianNaira()
    {
        this.Code = CurrencyCode.Parse(StaticCode);
    }

    public static NigerianNaira Instance { get; } = new();

    public override CurrencyCode Code { get; }

    public override int FractionalDigits => 2;

    public override bool IsOfficial => true;
}
