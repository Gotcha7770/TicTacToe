using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace TicTacToe.Avalonia.Converters;

public class InvertBoolConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value is false;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value is false;
    }
}