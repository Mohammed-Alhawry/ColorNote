using System;
using System.Globalization;
using System.Windows.Data;

namespace ColorNote.Converter;

public class DateTimeToOnlyDateConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var date = (DateTime)value;
        return $"{date.Year}/{date.Month}/{date.Day}";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}