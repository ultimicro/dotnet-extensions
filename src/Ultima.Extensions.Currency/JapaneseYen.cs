namespace Ultima.Extensions.Currency;

/// <summary>
/// Represents Japanese yen (SGD) currency.
/// </summary>
public sealed class JapaneseYen : CurrencyInfo
{
    public const string StaticCode = "JPY";

    private JapaneseYen()
    {
        this.Code = CurrencyCode.Parse(StaticCode);
    }

    public static JapaneseYen Instance { get; } = new();

    public override CurrencyCode Code { get; }

    public override int FractionalDigits => 0;

    public override bool IsOfficial => true;
}
