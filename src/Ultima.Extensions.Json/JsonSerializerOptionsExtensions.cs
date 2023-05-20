namespace System.Text.Json;

using Ultima.Extensions.Json;

public static class JsonSerializerOptionsExtensions
{
    public static void AddSystemTypeConverters(this JsonSerializerOptions options)
    {
        options.Converters.Add(new CultureInfoConverter());
        options.Converters.Add(new MailAddressConverter());
        options.Converters.Add(new RegionInfoConverter());
    }

    public static string NormalizeProperty(this JsonSerializerOptions options, string property)
    {
        return options.PropertyNamingPolicy?.ConvertName(property) ?? property;
    }
}
