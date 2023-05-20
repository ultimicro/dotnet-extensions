namespace Ultima.Extensions.Json;

using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

/// <summary>
/// An implementation of <see cref="JsonConverter"/> for <see cref="CultureInfo"/> using <see cref="CultureInfo.Name"/> as a value.
/// </summary>
public sealed class CultureInfoConverter : JsonConverter<CultureInfo>
{
    public override CultureInfo? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var json = reader.GetString();

        if (json is null)
        {
            return null;
        }

        try
        {
            return CultureInfo.GetCultureInfo(json);
        }
        catch (CultureNotFoundException ex)
        {
            throw new JsonException($"'{json}' is not a valid culture name.", ex);
        }
    }

    public override void Write(Utf8JsonWriter writer, CultureInfo value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.Name);
    }
}
