﻿using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;

namespace ValueTypes;

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

    public static Guid Parse(string? input)
    {
        throw new NotImplementedException();
    }

    public static bool TryParse([NotNullWhen(true)] string? input, out Email result)
    {
        throw new NotImplementedException();
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
