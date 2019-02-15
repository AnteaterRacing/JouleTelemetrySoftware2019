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
        protected double LOW = 0; // lowest speed value
        protected double HIGH = 100; // highest speed value
        protected double CLAMP = 15; // clamp for limiting random changes
        protected static readonly Random random = new Random();
        protected DispatcherTimer timer;

        public string Name { get; set; } // name of graph

        public GraphDelegate DataGenerator { get; set; } // function "pointer" to generate data

        public int UpdatePeriod { get; set; } // milliseconds per update

        public int Resolution { get; set; } // seconds of resolution to view history

        protected GraphData current;

        public GraphData Current
        {
            get => current;
            set
            {
                current = value;
                OnPropertyChanged("Current");
            }
        } // most recent data point

        public string CurrentString => string.Format("{0:F2}", Current.Value);

        protected ObservableCollection<GraphData> history;

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

        public GraphViewModel(string name = "GraphViewModel", GraphDelegate dataGenerator = null, int updatePeriod = 1000, int resolution = 60)
        {
            Name = name;
            DataGenerator = dataGenerator ?? new GraphDelegate(random.NextDouble);
            UpdatePeriod = updatePeriod;
            Resolution = resolution;

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

            double currentLow = last.Value - CLAMP;
            double currentHigh = last.Value + CLAMP;

            current = new GraphData
            {
                Date = last.Date.AddMilliseconds(UpdatePeriod),
                Value = Map(LOW + DataGenerator() * HIGH,
                                 LOW, HIGH,
                                 currentLow < LOW ? LOW : currentLow,
                                 currentHigh > HIGH ? HIGH : currentHigh)
            };

            history.Add(current);
            history.RemoveAt(0);

            OnPropertyChanged("CurrentString");
            OnPropertyChanged("AverageString");
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
