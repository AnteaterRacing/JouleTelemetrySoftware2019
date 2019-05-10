using System;
using TelemetryApp.Models;
using Windows.UI.Xaml.Data;

namespace TelemetryApp.Converters
{
    public class SelectedDataSourceComboBoxConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            switch (value)
            {
                case DataSource.Random:
                    return "Random";
                case DataSource.Csv:
                    return "Csv";
                case DataSource.Serial:
                    return "Serial";
                default:
                    return string.Format("Cannot convert, unknown value {0}", value);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            var s = (string)value;
            switch (s)
            {
                case "Random":
                    return DataSource.Random;
                case "Serial":
                    return DataSource.Serial;
                case "Csv":
                    return DataSource.Csv;
                default:
                    throw new Exception($"Cannot convert, unknown value {value}");
            }
        }
    }
}
