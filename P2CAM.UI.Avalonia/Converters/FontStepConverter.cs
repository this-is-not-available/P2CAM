using Avalonia.Data.Converters;
using System;
using System.Globalization;

namespace P2CAM.UI.Avalonia.Converters;

public class FontStepConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        // value = current height/width
        // parameter = "Threshold|SmallSize|BigSize" (e.g. "500|14|24")
        if (value is double currentVal && parameter is string paramStr)
        {
            var parts = paramStr.Split('|');
            if (parts.Length == 3 &&
                double.TryParse(parts[0], out double threshold) &&
                double.TryParse(parts[1], out double smallSize) &&
                double.TryParse(parts[2], out double bigSize))
            {
                return currentVal < threshold ? smallSize : bigSize;
            }
        }
        return 16.0; // Safe default
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}