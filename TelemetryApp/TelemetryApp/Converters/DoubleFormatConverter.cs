using System;
using Windows.UI.Xaml.Data;

namespace TelemetryApp.Converters
{
    public class DoubleFormatConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null) return null;
            if (value.GetType() != typeof(double))
                throw new ArgumentException($"Expected double, received {value.GetType()}");
            return $"{value:F2}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotSupportedException();
        }
    }
}