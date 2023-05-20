namespace Ultima.Extensions.Json;

using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

/// <summary>
/// An implementation of <see cref="JsonConverter"/> for <see cref="RegionInfo"/> using <see cref="RegionInfo.Name"/> as a value.
/// </summary>
public sealed class RegionInfoConverter : JsonConverter<RegionInfo>
{
    public override RegionInfo? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var json = reader.GetString();

        if (json is null)
        {
            return null;
        }

        try
        {
            return new RegionInfo(json);
        }
        catch (ArgumentException ex)
        {
            throw new JsonException($"'{json}' is not a valid region name.", ex);
        }
    }

    public override void Write(Utf8JsonWriter writer, RegionInfo value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.Name);
    }
}
