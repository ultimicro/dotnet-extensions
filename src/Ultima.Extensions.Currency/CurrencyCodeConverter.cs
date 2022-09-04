namespace Ultima.Extensions.Currency;

using System.ComponentModel;
using System.Globalization;

public sealed class CurrencyCodeConverter : TypeConverter
{
    public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
    {
        return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
    }

    public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
    {
        if (value is string v)
        {
            return CurrencyCode.Parse(v);
        }
        else
        {
            return base.ConvertFrom(context, culture, value);
        }
    }
}
