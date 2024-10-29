using System.Globalization;

namespace MobileMaui.Converters;

public class FullNameConverter : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values.Length < 3)
        {
            return string.Empty;
        }

        var lastName = values[0]?.ToString() ?? string.Empty;
        var firstName = values[1]?.ToString() ?? string.Empty;
        var middleName = values[2]?.ToString() ?? string.Empty;

        return $"{lastName} {firstName} {middleName}".Trim();
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}