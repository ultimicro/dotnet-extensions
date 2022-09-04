namespace Ultima.Extensions.Currency;

/// <summary>
/// Represents United States dollar (USD) currency.
/// </summary>
public sealed class UnitedStatesDollar : CurrencyInfo
{
    public const string StaticCode = "USD";

    private UnitedStatesDollar()
    {
        this.Code = CurrencyCode.Parse(StaticCode);
    }

    public static UnitedStatesDollar Instance { get; } = new();

    public override CurrencyCode Code { get; }

    public override int FractionalDigits => 2;

    public override bool IsOfficial => true;
}
