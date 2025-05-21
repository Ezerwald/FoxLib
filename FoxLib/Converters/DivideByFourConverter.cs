using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace FoxLib.Converters;

public class DivideByFourConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is double width)
            return width / 4;
        return 100; // fallback
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
