namespace Ultima.Extensions.Json;

using System;
using System.Net.Mail;
using System.Text.Json;
using System.Text.Json.Serialization;

/// <summary>
/// An implementation of <see cref="JsonConverter"/> for <see cref="MailAddress"/> using <see cref="MailAddress.Address"/> as a value.
/// </summary>
public sealed class MailAddressConverter : JsonConverter<MailAddress>
{
    public override MailAddress? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var json = reader.GetString();

        if (json is null)
        {
            return null;
        }

        try
        {
            return new MailAddress(json);
        }
        catch (Exception ex) when (ex is ArgumentException || ex is FormatException)
        {
            throw new JsonException($"'{json}' is not a valid email address.", ex);
        }
    }

    public override void Write(Utf8JsonWriter writer, MailAddress value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.Address);
    }
}
