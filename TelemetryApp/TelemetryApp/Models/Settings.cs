using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Windows.Storage;
using System.Linq;

namespace TelemetryApp.Models
{
    public class Settings : NotifyPropertyChanged
    {
        private static Dictionary<string, object> _defaultSettings = new Dictionary<string, object> {
            {"SelectedDataSource", DataSource.Serial},
            {"SerialPortName", "COM1"},
            {"CSVFileName", "/Assets/Data/sample1.csv"}
        };
        private ApplicationDataContainer _localSettings;

        public DataSource SelectedDataSource
        {
            get
            {
                if (_localSettings.Values[nameof(SelectedDataSource)] == null)
                {
                    _localSettings.Values[nameof(SelectedDataSource)] = _defaultSettings[nameof(SelectedDataSource)];
                }
                return (DataSource)_localSettings.Values[nameof(SelectedDataSource)];
            }
            set
            {
                _localSettings.Values[nameof(SelectedDataSource)] = value;
                OnPropertyChanged(nameof(SelectedDataSource));
            }
        }

        public ObservableCollection<string> DataSources
        {
            get => new ObservableCollection<string>(Enum.GetNames(typeof(DataSource)));
        }

        public Settings()
        {
            _localSettings = ApplicationData.Current.LocalSettings;
        }
    }
}
