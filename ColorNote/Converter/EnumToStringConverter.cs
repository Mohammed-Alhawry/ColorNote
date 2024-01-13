using ColorNote.Model;
using System;
using System.Globalization;
using System.Windows.Data;

namespace ColorNote.Converter;
class EnumToStringConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is Enum)
        {
            var color = (Color)value;
            return color.ToString();
        }

        return value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return (Color)Enum.Parse(typeof(Color), value.ToString());
    }
}

