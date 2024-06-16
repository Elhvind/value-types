using System.ComponentModel;
using System.Globalization;

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
