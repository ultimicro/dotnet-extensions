namespace Ultima.Extensions.Currency;

using System.Text.Json;
using System.Text.Json.Serialization;

public sealed class CurrencyCodeJsonConverter : JsonConverter<CurrencyCode>
{
    public override CurrencyCode? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        // Read JSON.
        string? json;

        try
        {
            json = reader.GetString();
        }
        catch (InvalidOperationException ex)
        {
            throw new JsonException(null, ex);
        }

        if (json == null)
        {
            return null;
        }

        // Parse.
        try
        {
            return CurrencyCode.Parse(json);
        }
        catch (FormatException ex)
        {
            throw new JsonException(null, ex);
        }
    }

    public override CurrencyCode ReadAsPropertyName(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return this.Read(ref reader, typeToConvert, options) ?? throw new JsonException();
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
