using System.Globalization;
using TaskTracker.Extensions;
using TaskTracker.Models;

namespace TaskTracker.Converters;

public class EnumToDisplayNameConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is TaskCategory category)
            return category.GetDisplayName();

        return value?.ToString() ?? string.Empty;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}