namespace Ultima.Extensions.Currency;

/// <summary>
/// Represents Philippine peso (PHP) currency.
/// </summary>
public sealed class PhilippinePeso : CurrencyInfo
{
    public const string StaticCode = "PHP";

    private PhilippinePeso()
    {
        this.Code = CurrencyCode.Parse(StaticCode);
    }

    public static PhilippinePeso Instance { get; } = new();

    public override CurrencyCode Code { get; }

    public override int FractionalDigits => 2;

    public override bool IsOfficial => true;
}
