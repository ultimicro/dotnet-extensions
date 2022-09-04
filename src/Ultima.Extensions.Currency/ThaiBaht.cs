namespace Ultima.Extensions.Currency;

/// <summary>
/// Represents Thai baht (THB) currency.
/// </summary>
public sealed class ThaiBaht : CurrencyInfo
{
    public const string StaticCode = "THB";

    private ThaiBaht()
    {
        this.Code = CurrencyCode.Parse(StaticCode);
    }

    public static ThaiBaht Instance { get; } = new();

    public override CurrencyCode Code { get; }

    public override int FractionalDigits => 2;

    public override bool IsOfficial => true;
}
