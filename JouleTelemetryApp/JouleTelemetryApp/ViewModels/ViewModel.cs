using System;
using System.Collections.ObjectModel;
using TelemetryApp.Models;
using TelemetryApp.Models.DataPoint;
using TelemetryApp.Models.GPS;
using Windows.UI.Xaml;

namespace TelemetryApp.ViewModels
{
    public class ViewModel : NotifyPropertyChanged
    {
        // Update Period
        // milliseconds
        private int updatePeriod;
        public int UpdatePeriod
        {
            get => updatePeriod;
            set
            {
                UpdatePeriod = value;
                OnPropertyChanged(nameof(UpdatePeriod));
            }
        }

        // Steering
        public SteeringWheel SteeringWheelModel { get; }

        // G-Force
        public GForce GForceXModel { get; }
        public GForce GForceYModel { get; }

        // Tire PSI
        public Pressure PressureFrontLeftTireModel { get; }
        public Pressure PressureFrontRightTireModel { get; }
        public Pressure PressureBackLeftTireModel { get; }
        public Pressure PressureBackRightTireModel { get; }

        // Tire Temperature
        public Temperature TemperatureFrontLeftTireModel { get; }
        public Temperature TemperatureFrontRightTireModel { get; }
        public Temperature TemperatureBackLeftTireModel { get; }
        public Temperature TemperatureBackRightTireModel { get; }

        // GPS
        public Latitude LatitudeModel { get; }
        public Longitude LongitudeModel { get; }

        // Graphs
        private ObservableCollection<GraphViewModel> graphs;
        public ObservableCollection<GraphViewModel> Graphs
        {
            get => graphs;
            set
            {
                graphs = value;
                OnPropertyChanged(nameof(Graphs));
            }
        }

        private GraphViewModel currentGraph;
        public GraphViewModel CurrentGraph
        {
            get => currentGraph;
            set
            {
                currentGraph = value;
                OnPropertyChanged(nameof(CurrentGraph));
            }
        }

        protected DispatcherTimer timer;

        public ViewModel()
        {
            // Initialize
            updatePeriod = 1000;
            // SteeringWheel
            SteeringWheelModel = new SteeringWheel();
            // GForce
            GForceXModel = new GForce();
            GForceYModel = new GForce();
            // Pressure
            PressureFrontLeftTireModel = new Pressure();
            PressureFrontRightTireModel = new Pressure();
            PressureBackLeftTireModel = new Pressure();
            PressureBackRightTireModel = new Pressure();
            // Temperature
            TemperatureFrontLeftTireModel = new Temperature();
            TemperatureFrontRightTireModel = new Temperature();
            TemperatureBackLeftTireModel = new Temperature();
            TemperatureBackRightTireModel = new Temperature();
            // GPS
            LatitudeModel = new Latitude();
            LongitudeModel = new Longitude();

            graphs = new ObservableCollection<GraphViewModel>();
            currentGraph = new GraphViewModel(
                () => new DataPoint<double>(Data.RandomDouble(0, 100)),
                "Random",
                maximum: 100
                );

            // Graphs
            Graphs.Add(currentGraph);
            //var fibonacci = Data.FibonacciRange(0, 10);
            //Graphs.Add(new GraphViewModel(
            //    () => new DataPoint<double>(Data.EnumerateInteger(fibonacci, loop: true)),
            //    "Fibonacci",
            //    maximum: 5000
            //));
            Graphs.Add(new GraphViewModel(
                () => new DataPoint<double>(50),
                "Constant",
                maximum: 100
            ));
            //var csvEnum = new DataPoints<string>(Csv.CsvReader.ReadFromText("Assets/Data/short.csv"));
            //Graphs.Add(new GraphViewModel(
            //    () => new DataPoint<double>(Data.EnumerateDouble(csvEnum["Steering Position [Deg]"], loop: true)),
            //    "CSV Data Loop",
            //    minimum: -100, maximum: 100
            //));

            // Timer for updating once a second
            timer = new DispatcherTimer() { Interval = TimeSpan.FromMilliseconds(UpdatePeriod) };
            timer.Tick += Tick;
        }

        public void Start()
        {
            timer.Start();
            foreach (GraphViewModel graph in Graphs)
            {
                graph.Start();
            }
        }

        public void Stop()
        {
            timer.Stop();
            foreach (GraphViewModel graph in Graphs)
            {
                graph.Stop();
            }
        }

        protected void Tick(object sender, object e)
        {
            Update();
        }

        public void Update()
        {
            // Steering
            SteeringWheelModel.Update();
            // GForce
            GForceXModel.Update();
            GForceYModel.Update();
            // Pressure
            PressureFrontLeftTireModel.Update();
            PressureFrontRightTireModel.Update();
            PressureBackLeftTireModel.Update();
            PressureBackRightTireModel.Update();
            // Temperature
            TemperatureFrontLeftTireModel.Update();
            TemperatureFrontRightTireModel.Update();
            TemperatureBackLeftTireModel.Update();
            TemperatureBackRightTireModel.Update();
            // GPS
            LatitudeModel.Update();
            LongitudeModel.Update();
            // Notify all properties have changed as mentioned here:
            // https://docs.microsoft.com/en-us/dotnet/api/system.componentmodel.inotifypropertychanged.propertychanged?redirectedfrom=MSDN&view=netframework-4.7.2#remarks
            OnPropertyChanged(null);
        }
    }
}
