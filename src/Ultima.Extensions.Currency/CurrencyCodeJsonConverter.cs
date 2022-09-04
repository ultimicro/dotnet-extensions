namespace Ultima.Extensions.Currency;

using System.Text.Json;
using System.Text.Json.Serialization;

public sealed class CurrencyCodeJsonConverter : JsonConverter<CurrencyCode>
{
    public override CurrencyCode Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        // Read JSON.
        string? json;

        try
        {
            json = reader.GetString();
        }
        catch (InvalidOperationException ex)
        {
            throw new JsonException("The value is not a valid currency code.", ex);
        }

        if (json == null)
        {
            // This should already handled by the framework.
            throw new JsonException("The value is not a valid currency code.");
        }

        // Parse.
        try
        {
            return CurrencyCode.Parse(json);
        }
        catch (FormatException ex)
        {
            throw new JsonException("The value is not a valid currency code.", ex);
        }
    }

    public override CurrencyCode ReadAsPropertyName(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return this.Read(ref reader, typeToConvert, options);
    }

    public override void Write(Utf8JsonWriter writer, CurrencyCode value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.Value);
    }

    public override void WriteAsPropertyName(Utf8JsonWriter writer, CurrencyCode value, JsonSerializerOptions options)
    {
        writer.WritePropertyName(value.Value);
    }
}
