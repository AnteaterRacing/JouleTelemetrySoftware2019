using System;
using System.Collections.ObjectModel;
using System.Linq;
using Telerik.Core;
using Windows.UI.Xaml;

namespace JouleTelemetryApp
{
    public delegate double GraphDelegate();

    public class GraphViewModel : ViewModelBase
    {
        public static readonly Random RANDOM = new Random();
        protected DispatcherTimer timer;
        protected GraphData current;
        protected ObservableCollection<GraphData> history;

        public string Name { get; private set; } // name of graph

        public GraphDelegate DataGenerator { get; private set; } // function "pointer" to generate data

        public int UpdatePeriod { get; private set; } // milliseconds per update

        public int Resolution { get; private set; } // seconds of resolution to view history

        public int Minimum { get; private set; }
        public int Maximum { get; private set; }
        public int Step => (Maximum - Minimum) / 5;

        public GraphData Current
        {
            get => current;
            private set
            {
                current = value;
                OnPropertyChanged("Current");
            }
        } // most recent data point

        public string CurrentString => string.Format("{0:F2}", Current.Value);

        public ObservableCollection<GraphData> History
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

        public GraphViewModel(string name = "GraphViewModel", GraphDelegate dataGenerator = null,
            int minimum = 0, int maximum = 100,
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
            DataGenerator = dataGenerator ?? new GraphDelegate(() => Minimum + RANDOM.NextDouble() * Maximum);

            timer = new DispatcherTimer() { Interval = TimeSpan.FromSeconds(1) };
            timer.Tick += Tick;
            LoadData();
        }

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
            System.Collections.Generic.IEnumerable<GraphData> data = from i in Enumerable.Range(0, Resolution)
                                                                     select new GraphData { Date = now.AddMilliseconds(-UpdatePeriod * i), Value = 0 };

            history = new ObservableCollection<GraphData>(data.OrderBy(d => d.Date));
        }

        // Update random values
        protected void Update()
        {
            // Update the historical list with new speed data
            GraphData last = history.Last();

            current = new GraphData
            {
                Date = last.Date.AddMilliseconds(UpdatePeriod),
                Value = DataGenerator()
            };

            history.Add(current);
            history.RemoveAt(0);

            OnPropertyChanged("CurrentString");
            OnPropertyChanged("AverageString");
            OnPropertyChanged("AreaString");
        }

        protected double Map(double value, double fromLow, double fromHigh, double toLow, double toHigh)
        {
            return (value - fromLow) * (toHigh - toLow) / (fromHigh - fromLow) + toLow;
        }
    }

    public class GraphData : ViewModelBase
    {
        private double value;

        public double Value
        {
            get => value;
            set
            {
                this.value = value;
                OnPropertyChanged("Value");
            }
        } // value of data point

        private DateTime date;

        public DateTime Date
        {
            get => date;
            set
            {
                date = value;
                OnPropertyChanged("Date");
            }
        } // timestamp
    }
}
