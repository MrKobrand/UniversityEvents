using System.Globalization;

namespace MobileMaui.Converters;

public class NullToBooleanConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var isVisible = value is not null;

        if (parameter is string param && param == "false")
        {
            isVisible = !isVisible;
        }

        return isVisible;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}