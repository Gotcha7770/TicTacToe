using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Avalonia;
using Avalonia.Data.Converters;

namespace TicTacToe.Avalonia.Converters;

public class CompositeConverter : List<ConverterContext>, IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == AvaloniaProperty.UnsetValue)
            return value;

        var result = this.Aggregate((Value: value, Parameter: parameter), (current, context) =>
        {
            var newParameter = context.Parameter ?? current.Parameter;
            var newValue = context.Converter.Convert(current.Value, targetType, newParameter, culture);
            return (newValue, newParameter);
        });

        return result.Value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return this.Reverse<ConverterContext>()
            .Aggregate(value, (current, context) => context.Converter.ConvertBack(current, targetType, context.Parameter, culture));
    }
}

public class ConverterContext
{
    public IValueConverter Converter { get; set; }

    public object Parameter { get; set; }
}