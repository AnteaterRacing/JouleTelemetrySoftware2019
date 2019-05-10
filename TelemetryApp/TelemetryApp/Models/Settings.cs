using System.Collections.Generic;

namespace TelemetryApp.Models
{
    public class Settings
    {
        #region Settings Dictionary

        private static readonly Dictionary<string, object> DefaultSettings = new Dictionary<string, object> {
            {nameof(DataSource), DataSource.Random},
            {nameof(SerialPortName), "COM3"},
            {nameof(SerialBaudRate), 9600},
            {nameof(CsvFileName), "/Assets/Data/sample1.csv"},
            {nameof(UpdatePeriod), 1000}
        };

        private Dictionary<string, object> _currentSettings;

        public object this[string key]
        {
            get => _currentSettings[key];
            set => _currentSettings[key] = value;
        }

        #endregion Settings Dictionary

        #region Data Source

        public DataSource DataSource
        {
            get => (DataSource)_currentSettings[nameof(DataSource)];
            set => this[nameof(DataSource)] = value;
        }

        public bool IsDataSourceRandom => (DataSource)this[nameof(DataSource)] == DataSource.Random;
        public bool IsDataSourceSerial => (DataSource)this[nameof(DataSource)] == DataSource.Serial;
        public bool IsDataSourceCsv => (DataSource)this[nameof(DataSource)] == DataSource.Csv;

        #endregion Data Source

        #region Serial

        public string SerialPortName
        {
            get => (string)_currentSettings[nameof(SerialPortName)];
            set => this[nameof(SerialPortName)] = value;
        }

        public int SerialBaudRate
        {
            get => (int)_currentSettings[nameof(SerialBaudRate)];
            set => this[nameof(SerialBaudRate)] = value;
        }

        #endregion Serial

        #region Csv

        public string CsvFileName
        {
            get => (string)_currentSettings[nameof(CsvFileName)];
            set => this[nameof(CsvFileName)] = value;
        }

        #endregion Csv

        #region Update Period

        public int UpdatePeriod {
            get => (int)_currentSettings[nameof(UpdatePeriod)];
            set => this[nameof(UpdatePeriod)] = value;
        }

        #endregion Update Period

        #region Constructors

        public Settings()
        {
            _currentSettings = new Dictionary<string, object>(DefaultSettings);
        }

        #endregion Constructors
    }
}
