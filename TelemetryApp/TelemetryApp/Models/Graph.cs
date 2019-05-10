using System;
using System.Collections.ObjectModel;
using System.Linq;
using TelemetryApp.Models.DataPoint;
using static TelemetryApp.Models.DataPoint.DataPointDelegate<double>;

namespace TelemetryApp.Models
{
    public class Graph : NotifyPropertyChanged
    {
        private DataPointDelegate<double> _current;
        private ObservableCollection<DataPoint<double>> _history;

        public string Name { get; } // name of graph

        public int UpdatePeriod { get; } // milliseconds per update

        public int Resolution { get; } // seconds of resolution to view history

        public int Minimum { get; } // minimum expected value
        public int Maximum { get; } // maximum expected value
        public int Step => (Maximum - Minimum) / 5;

        public double Average => _history.Average(data => data.Value);
        public double Area => _history.Sum(data => data.Value);

        public DataPointDelegate<double> Current
        {
            get => _current;
            private set
            {
                _current = value;
                OnPropertyChanged(nameof(Current));
            }
        } // most recent data point

        public ObservableCollection<DataPoint<double>> History
        {
            get => _history;
            private set
            {
                _history = value;
                OnPropertyChanged(nameof(History));
            }
        } // historical data points

        public Graph(DataDelegate dataGenerator, string name = "GraphViewModel",
                     int minimum = 0, int maximum = 1,
                     int updatePeriod = 1000, int resolution = 60)
        {
            Name = name;
            if (minimum > maximum) throw new ArgumentException($"Expected minimum({minimum}) < maximum({maximum})");
            Minimum = minimum;
            Maximum = maximum;
            UpdatePeriod = updatePeriod;
            Resolution = resolution;
            Current = new DataPointDelegate<double>(dataGenerator);

            Init();
        }

        public override string ToString()
        {
            return Name;
        }

        // Update the historical list with new data
        public void Update()
        {
            _current.Update();

            _history.Add(new DataPoint<double>(_current.Date, _current.Value));
            _history.RemoveAt(0);

            OnPropertyChanged(null);
        }

        // Initial data load when starting application
        protected void Init()
        {
            ResetHistory();
            Update();
        }

        protected void ResetHistory()
        {
            var now = DateTime.Now;
            var data = from i in Enumerable.Range(0, Resolution)
                                                  select new DataPoint<double> { Date = now.AddMilliseconds(-UpdatePeriod * i), Value = 0 };

            _history = new ObservableCollection<DataPoint<double>>(data.OrderBy(d => d.Date));
        }

    }
}
