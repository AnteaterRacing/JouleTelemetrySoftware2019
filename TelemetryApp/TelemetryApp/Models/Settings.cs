using System.Collections.Generic;

namespace TelemetryApp.Models
{
    public class Settings : NotifyPropertyChanged
    {
        private static Dictionary<string, object> _defaultSettings = new Dictionary<string, object> {
            {nameof(SelectedDataSource), DataSource.Random},
            {nameof(SerialPortName), "COM1"},
            {nameof(SerialBaudRate), 9600},
            {nameof(CsvFileName), "/Assets/Data/sample1.csv"}
        };

        private Dictionary<string, object> _currentSettings;

        public object this[string key]
        {
            get { return _currentSettings[key]; }
            set {
                _currentSettings[key] = value;
                OnPropertyChanged(key);
            }
        }

        public DataSource SelectedDataSource
        {
            get => (DataSource)_currentSettings[nameof(SelectedDataSource)];
            set
            {
                this[nameof(SelectedDataSource)] = value;
            }
        }

        public string SerialPortName
        {
            get => (string)_currentSettings[nameof(SerialPortName)];
            set
            {
                this[nameof(SerialPortName)] = value;
            }
        }

        public int SerialBaudRate
        {
            get => (int)_currentSettings[nameof(SerialBaudRate)];
            set
            {
                this[nameof(SerialBaudRate)] = value;
            }
        }

        public string CsvFileName
        {
            get => (string)_currentSettings[nameof(CsvFileName)];
            set
            {
                this[nameof(CsvFileName)] = value;
            }
        }

        //public ObservableCollection<string> DataSources => new ObservableCollection<string>(Enum.GetValues(typeof(DataSource))
        //                                                                                    .Cast<DataSource>()
        //                                                                                    .Select(x => x.ToString())
        //                                                                                    .ToArray());

        public Settings()
        {
            _currentSettings = new Dictionary<string, object>(_defaultSettings);
            OnPropertyChanged(null);
        }
    }
}
