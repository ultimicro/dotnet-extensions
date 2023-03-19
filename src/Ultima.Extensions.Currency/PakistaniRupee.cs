namespace Ultima.Extensions.Currency;

/// <summary>
/// Represents Pakistani rupee (PKR) currency.
/// </summary>
public sealed class PakistaniRupee : CurrencyInfo
{
    public const string StaticCode = "PKR";

    private PakistaniRupee()
    {
        this.Code = CurrencyCode.Parse(StaticCode);
    }

    public static PakistaniRupee Instance { get; } = new();

    public override CurrencyCode Code { get; }

    public override int FractionalDigits => 2;

    public override bool IsOfficial => true;
}
