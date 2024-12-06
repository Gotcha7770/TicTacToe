using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace TicTacToe.Avalonia.Converters;

public class EqualityConverter : IValueConverter
{
    public bool IsComparedByReference { get; set; }

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return IsComparedByReference ? ReferenceEquals(value, parameter) : Equals(value, parameter);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}