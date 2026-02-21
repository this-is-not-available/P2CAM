using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using Avalonia.Data.Converters;

namespace P2CAM.UI.Avalonia.Converters;

public class PercentageConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is double totalValue && parameter is string paramStr && double.TryParse(paramStr, out double percentage))
        {
            double result = totalValue * percentage;
            return result <= 0 ? 1.0 : result;
        }
        return 1.0;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}