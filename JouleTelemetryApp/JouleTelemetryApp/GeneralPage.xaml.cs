using OxyPlot;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Telerik.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace JouleTelemetryApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GeneralPage : Page
    {
        public GeneralPage()
        {
            this.InitializeComponent();
            this.Loaded += this.OnLoaded;
            this.Unloaded += this.OnUnloaded;
        }

        private async void OnLoaded(object sender, RoutedEventArgs e)
        {
            SpeedViewModel model = this.Resources["ViewModel"] as SpeedViewModel;
            model.StartTimer();

            for (int a = 0; 1 < 2; a = a + 1)
            {

                int val = (a % 100);
                Speed.Value = (val);
                SpeedText.Text = (100 - val).ToString() + " MPH";

                Tachometer.Value = 100 - val;
                TachometerText.Text = (val).ToString() + " RPM";

                await System.Threading.Tasks.Task.Delay(100);
            }
        }
        private async void OnUnloaded(object sender, RoutedEventArgs e)
        {
            SpeedViewModel model = this.Resources["ViewModel"] as SpeedViewModel;
            model.StopTimer();
        }
    }

    public class SpeedViewModel : ViewModelBase
    {
        private DispatcherTimer timer;
        private Random r;
        private const int RESOLUTION = 60; // seconds of resolution to view history
        private const int SPEED_LOW = 0; // lowest speed value
        private const int SPEED_HIGH = 100; // highest speed value
        private const int SPEED_UPDATE_PERIOD = 1000; // milliseconds

        //private int callDuration;

        //public int CallDuration
        //{
        //    get { return callDuration; }
        //    set
        //    {
        //        callDuration = value;
        //        this.OnPropertyChanged("CallDuration");
        //    }
        //}

        //private int holdTime;

        //public int HoldTime
        //{
        //    get { return holdTime; }
        //    set
        //    {
        //        holdTime = value;
        //        this.OnPropertyChanged("HoldTime");
        //    }
        //}

        //private int abandonment;

        //public int Abandoment
        //{
        //    get { return abandonment; }
        //    set
        //    {
        //        abandonment = value;
        //        this.OnPropertyChanged("Abandoment");
        //    }
        //}

        //private double callVSResolution;

        //public double CallsVSResolution
        //{
        //    get { return callVSResolution; }
        //    set
        //    {
        //        callVSResolution = value;
        //        this.OnPropertyChanged("CallsVSResolution");
        //    }
        //}

        private SpeedData currentSpeed;

        public SpeedData CurrentSpeed
        {
            get { return currentSpeed; }
            set
            {
                currentSpeed = value;
                this.OnPropertyChanged("CurrentSpeed");
            }
        }

        private ObservableCollection<SpeedData> speedHistory;

        public ObservableCollection<SpeedData> SpeedHistory
        {
            get { return speedHistory; }
            private set
            {
                speedHistory = value;
                this.OnPropertyChanged("SpeedHistory");
            }
        }

        public SpeedViewModel()
        {
            r = new Random();
            timer = new DispatcherTimer() { Interval = TimeSpan.FromSeconds(1) };
            timer.Tick += timer_Tick;
            this.LoadData();
        }

        public void StopTimer()
        {
            this.timer.Stop();
        }

        public void StartTimer()
        {
            this.timer.Start();
        }

        void timer_Tick(object sender, object e)
        {
            this.Update();
        }

        // Initial data load when starting application
        private void LoadData()
        {
            var now = DateTime.Now;
            var historyData = from i in Enumerable.Range(0, RESOLUTION)
                              select new SpeedData { Date = now.AddMilliseconds(-SPEED_UPDATE_PERIOD * i), Speed = 0 };

            this.speedHistory = new ObservableCollection<SpeedData>(historyData.OrderBy(data => data.Date));

            Update();
        }

        // Update random values
        private void Update()
        {
            // Update the historical list with new speed data
            var lastSpeedData = this.speedHistory.Last();
            this.currentSpeed = new SpeedData { Date = lastSpeedData.Date.AddMilliseconds(SPEED_UPDATE_PERIOD), Speed = r.Next(SPEED_LOW, SPEED_HIGH) };
            this.speedHistory.Add(CurrentSpeed);
            this.speedHistory.RemoveAt(0);
            //this.Abandoment = r.Next(0, 100);
            //this.CallDuration = r.Next(0, 175);
            //this.CallsVSResolution = r.Next(0, 30) / 10.0;
            //this.HoldTime = r.Next(0, 175);
        }
    }

    public class SpeedData : ViewModelBase
    {
        private double speed;

        public double Speed
        {
            get { return speed; }
            set
            {
                speed = value;
                this.OnPropertyChanged("Speed");
            }
        }


        private DateTime date;

        public DateTime Date
        {
            get { return date; }
            set
            {
                date = value;
                this.OnPropertyChanged("Date");
            }
        }
    }


    //public class GeneralPageModel
    //{
    //    public GeneralPageModel()
    //    {
    //        this.SpeedModel = new PlotModel { Title = "Speed (MPH)" };
    //        LineSeries ls = new LineSeries();
    //        ls.Points.AddRange(new List<DataPoint> {
    //            new DataPoint(0, 4),
    //            new DataPoint(10, 13),
    //            new DataPoint(20, 15),
    //            new DataPoint(30, 16),
    //            new DataPoint(40, 12),
    //            new DataPoint(50, 12)
    //        });
    //        this.SpeedModel.Series.Add(ls);
    //        // this.SpeedModel.InvalidatePlot(true);
    //    }

    //    public PlotModel SpeedModel { get; private set; }
    //}
}
