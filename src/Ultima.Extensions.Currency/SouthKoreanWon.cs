namespace Ultima.Extensions.Currency;

/// <summary>
/// Represents South Korean won (KRW) currency.
/// </summary>
public sealed class SouthKoreanWon : CurrencyInfo
{
    public const string StaticCode = "KRW";

    private SouthKoreanWon()
    {
        this.Code = CurrencyCode.Parse(StaticCode);
    }

    public static SouthKoreanWon Instance { get; } = new();

    public override CurrencyCode Code { get; }

    public override int FractionalDigits => 0;

    public override bool IsOfficial => true;
}
