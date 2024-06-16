using System.Text.Json;
using System.Text.Json.Serialization;

namespace ValueTypes;

public sealed class EmailJsonConverter : JsonConverter<Email>
{
    public override void Write(Utf8JsonWriter writer, Email value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }

    public override Email Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.String)
        {
            if (Email.TryParse(reader.GetString(), out var email))
            {
                return email;
            }
        }

        return Email.Empty;
    }

    public override bool CanConvert(Type typeToConvert)
    {
        return typeToConvert == typeof(Email);
    }
}
