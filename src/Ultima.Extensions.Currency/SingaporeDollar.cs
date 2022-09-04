namespace Ultima.Extensions.Currency;

/// <summary>
/// Represents Singapore dollar (SGD) currency.
/// </summary>
public sealed class SingaporeDollar : CurrencyInfo
{
    public const string StaticCode = "SGD";

    private SingaporeDollar()
    {
        this.Code = CurrencyCode.Parse(StaticCode);
    }

    public static SingaporeDollar Instance { get; } = new();

    public override CurrencyCode Code { get; }

    public override int FractionalDigits => 2;

    public override bool IsOfficial => true;
}
