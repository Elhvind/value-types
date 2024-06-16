using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ValueTypes;

public sealed class EmailTypeConverter : TypeConverter
{
    public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
    {
        if (sourceType == typeof(string))
        {
            return true;
        }
        if (sourceType == typeof(Email))
        {
            return true;
        }
        return base.CanConvertFrom(context, sourceType);
    }

    public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
    {
        if (value is string strValue)
        {
            return new Email(strValue);
        }
        return base.ConvertFrom(context, culture, value);
    }
}

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
            return new Email(reader.GetString());
        }

        return Email.Empty;
    }

    public override bool CanConvert(Type typeToConvert)
    {
        return typeToConvert == typeof(Email);
    }
}

[StructLayout(LayoutKind.Auto)]
[Serializable]
[TypeConverter(typeof(EmailTypeConverter))]
[JsonConverter(typeof(EmailJsonConverter))]
public readonly struct Email : IComparable, IComparable<Email>, IEquatable<Email>
{
    public static readonly Email Empty = new();

    private readonly string _v;

    public Email(string v)
    {
        if (v is null)
            throw new ArgumentNullException(nameof(v));

        if (!v.Contains('@'))
            throw new ArgumentNullException(nameof(v));

        _v = v.ToLower(CultureInfo.InvariantCulture);
    }

    public int CompareTo(object? obj)
    {
        if (obj is null)
            return 1;

        if (obj is not Email email)
            throw new ArgumentException("Must be email", nameof(obj));

        return CompareTo(email);
    }

    public int CompareTo(Email other)
    {
        return string.Compare(_v, other._v, true);
    }

    public override int GetHashCode()
    {
        return _v.GetHashCode();
    }

    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        if (obj is null)
            return false;

        if (obj is not Email email)
            return false;

        return Equals(email);
    }

    public bool Equals(Email other)
    {
        return string.Compare(_v, other._v, true) == 0;
    }

    public static bool operator ==(Email left, Email right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(Email left, Email right)
    {
        return !(left == right);
    }

    public static bool operator <(Email left, Email right)
    {
        return left.CompareTo(right) < 0;
    }

    public static bool operator <=(Email left, Email right)
    {
        return left.CompareTo(right) <= 0;
    }

    public static bool operator >(Email left, Email right)
    {
        return left.CompareTo(right) > 0;
    }

    public static bool operator >=(Email left, Email right)
    {
        return left.CompareTo(right) >= 0;
    }

    public override string ToString()
    {
        return _v;
    }
}
