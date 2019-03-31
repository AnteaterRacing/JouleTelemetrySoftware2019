using System;
using System.Collections.ObjectModel;
using System.Linq;
using Telerik.Core;
using Windows.UI.Xaml;

namespace TelemetryApp
{
    public delegate DataPoint<double> GraphDelegate();

    public class GraphViewModel : ViewModelBase
    {
        protected DispatcherTimer timer;
        protected DataPoint<double> current;
        protected ObservableCollection<DataPoint<double>> history;

        public GraphViewModel(string name = "GraphViewModel", GraphDelegate dataGenerator = null,
            int minimum = 0, int maximum = 1,
            int updatePeriod = 1000, int resolution = 60)
        {
            Name = name;
            UpdatePeriod = updatePeriod;
            Resolution = resolution;
            if (minimum > maximum)
            {
                throw new ArgumentException($"Expected minimum({minimum}) < maximum({maximum})");
            }

            Minimum = minimum;
            Maximum = maximum;
            DataGenerator = dataGenerator ?? new GraphDelegate(() => DataPoint<double>.GetRandomData(Minimum, Maximum));

            timer = new DispatcherTimer() { Interval = TimeSpan.FromSeconds(1) };
            timer.Tick += Tick;
            LoadData();
        }

        public string Name { get; private set; } // name of graph

        public GraphDelegate DataGenerator { get; private set; } // function "pointer" to generate data

        public int UpdatePeriod { get; private set; } // milliseconds per update

        public int Resolution { get; private set; } // seconds of resolution to view history

        public int Minimum { get; private set; }
        public int Maximum { get; private set; }
        public int Step => (Maximum - Minimum) / 5;

        public DataPoint<double> Current
        {
            get => current;
            private set
            {
                current = value;
                OnPropertyChanged("Current");
            }
        } // most recent data point

        public string CurrentString => string.Format("{0:F2}", Current.Value);

        public ObservableCollection<DataPoint<double>> History
        {
            get => history;
            private set
            {
                history = value;
                OnPropertyChanged("ViewHistory");
            }
        } // historical data points

        public string AverageString => string.Format("{0:F2}", History.Average(data => data.Value));

        public string AreaString => string.Format("{0:F2}", History.Sum(data => data.Value));


        public void Start()
        {
            timer.Start();
        }

        public void Stop()
        {
            timer.Stop();
        }

        public override string ToString()
        {
            return Name;
        }

        protected void Tick(object sender, object e)
        {
            Update();
        }

        // Initial data load when starting application
        protected void LoadData()
        {
            ResetHistory();
            Update();
        }

        protected void ResetHistory()
        {
            DateTime now = DateTime.Now;
            System.Collections.Generic.IEnumerable<DataPoint<double>> data = from i in Enumerable.Range(0, Resolution)
                                                                     select new DataPoint<double> { Date = now.AddMilliseconds(-UpdatePeriod * i), Value = 0 };

            history = new ObservableCollection<DataPoint<double>>(data.OrderBy(d => d.Date));
        }

        // Update random values
        protected void Update()
        {
            // Update the historical list with new speed data
            DataPoint<double> last = history.Last();

            current = DataGenerator();

            history.Add(current);
            history.RemoveAt(0);

            OnPropertyChanged("CurrentString");
            OnPropertyChanged("AverageString");
            OnPropertyChanged("AreaString");
        }
    }
}
