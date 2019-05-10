using System;
using System.Collections.ObjectModel;
using TelemetryApp.Models;
using TelemetryApp.Models.GForce;
using TelemetryApp.Models.Gps;
using Windows.UI.Xaml;
using TelemetryApp.Models.Tire;

namespace TelemetryApp.ViewModels
{
    public class ViewModel : NotifyPropertyChanged
    {
        // Timer for calling Update
        private readonly DispatcherTimer _timer;

        public Settings SettingsModel { get; private set; }

        public SerialData SerialModel { get; private set; }

        // Steering
        public SteeringWheel SteeringWheelModel { get; private set; }

        // G-Force
        public GForce GForceModel { get; private set; }

        // Tires
        public Tire FrontLeftTireModel { get; private set; }
        public Tire FrontRightTireModel { get; private set; }
        public Tire BackLeftTireModel { get; private set; }
        public Tire BackRightTireModel { get; private set; }

        // GPS
        public Gps GpsModel { get; private set; }

        // Graphs
        public ObservableCollection<Graph> Graphs { get; private set; }

        private Graph _currentGraph;
        public Graph CurrentGraph
        {
            get => _currentGraph;
            set
            {
                _currentGraph = value;
                OnPropertyChanged(nameof(CurrentGraph));
            }
        }

        public ViewModel()
        {
            Init();
            // Timer for updating once a second
            _timer = new DispatcherTimer() { Interval = TimeSpan.FromMilliseconds(SettingsModel.UpdatePeriod) };
            _timer.Tick += Tick;
        }

        private void Init()
        {
            // Settings
            SettingsModel = new Settings();

            // Provide access to serial data
            SerialModel = new SerialData(SettingsModel.SerialPortName, SettingsModel.SerialBaudRate);

            // SteeringWheel
            SteeringWheelModel = new SteeringWheel();

            // GForce
            GForceModel = new GForce();

            // Tires
            FrontLeftTireModel = new Tire();
            FrontRightTireModel = new Tire();
            BackLeftTireModel = new Tire();
            BackRightTireModel = new Tire();

            // GPS
            GpsModel = new Gps();

            // Graphs
            Graphs = new ObservableCollection<Graph>();
            CurrentGraph = new Graph(
                () => Data.RandomDouble(0, 100),
                "Random",
                maximum: 100
            );
            Graphs.Add(_currentGraph);
            //var fibonacci = Data.FibonacciRange(0, 10);
            //Graphs.Add(new Graph(
            //    () => Data.EnumerateInteger(fibonacci, loop: true),
            //    "Fibonacci",
            //    maximum: 5000
            //));
            Graphs.Add(new Graph(
                () => 50,
                "Constant",
                maximum: 100
            ));
            //var csvEnum = new DataPoints<string>(Csv.CsvReader.ReadFromText("Assets/Data/short.csv"));
            //Graphs.Add(new GraphViewModel(
            //    () => new DataPoint<double>(Data.EnumerateDouble(csvEnum["Steering Position [Deg]"], loop: true)),
            //    "CSV Data Loop",
            //    minimum: -100, maximum: 100
            //));
        }

        public void Start()
        {
            _timer.Start();
        }

        public void Stop()
        {
            _timer.Stop();
        }

        protected void Tick(object sender, object e)
        {
            Update();
        }

        public void Update()
        {
            // Steering
            SteeringWheelModel.Update();
            OnPropertyChanged(nameof(SteeringWheelModel));
            // GForce
            GForceModel.Update();
            OnPropertyChanged(nameof(GForceModel));
            // Tires
            FrontLeftTireModel.Update();
            OnPropertyChanged(nameof(FrontLeftTireModel));
            FrontRightTireModel.Update();
            OnPropertyChanged(nameof(FrontRightTireModel));
            BackLeftTireModel.Update();
            OnPropertyChanged(nameof(BackLeftTireModel));
            BackRightTireModel.Update();
            OnPropertyChanged(nameof(BackRightTireModel));
            // GPS
            GpsModel.Update();
            OnPropertyChanged(nameof(GpsModel));
            // Graphs
            foreach (var graph in Graphs) graph.Update();
            OnPropertyChanged(nameof(Graphs));
        }

        public void UpdateDataSource()
        {
            switch (SettingsModel.DataSource)
            {
                case DataSource.Serial:
                    SetSerialDataSource();
                    break;
                case DataSource.Csv:
                    SetRandomDataSource();
                    break;
                default:
                    SetRandomDataSource();
                    break;
            }
        }

        public void SetRandomDataSource()
        {
            SettingsModel.DataSource = DataSource.Random;
            // SteeringWheel
            SteeringWheelModel.DataGenerator = SteeringWheel.Default;
            // GForce
            GForceModel.X.DataGenerator = GForceAxis.Default;
            GForceModel.Y.DataGenerator = GForceAxis.Default;
            // Tires
            FrontLeftTireModel.Pressure.DataGenerator = Pressure.Default;
            FrontLeftTireModel.Temperature.DataGenerator = Temperature.Default;
            FrontRightTireModel.Pressure.DataGenerator = Pressure.Default;
            FrontRightTireModel.Temperature.DataGenerator = Temperature.Default;
            BackLeftTireModel.Pressure.DataGenerator = Pressure.Default;
            BackLeftTireModel.Temperature.DataGenerator = Temperature.Default;
            BackRightTireModel.Pressure.DataGenerator = Pressure.Default;
            BackRightTireModel.Temperature.DataGenerator = Temperature.Default;
            // GPS
            GpsModel.Latitude.DataGenerator = Latitude.Default;
            GpsModel.Longitude.DataGenerator = Longitude.Default;
        }

        public void SetSerialDataSource()
        {
            SettingsModel.DataSource = DataSource.Serial;
            // SteeringWheel
            SteeringWheelModel.DataGenerator = SerialModel.GetData;
            // GForce
            GForceModel.X.DataGenerator = SerialModel.GetData;
            GForceModel.Y.DataGenerator = SerialModel.GetData;
            // Tires
            FrontLeftTireModel.Pressure.DataGenerator = SerialModel.GetData;
            FrontLeftTireModel.Temperature.DataGenerator = SerialModel.GetData;
            FrontRightTireModel.Pressure.DataGenerator = SerialModel.GetData;
            FrontRightTireModel.Temperature.DataGenerator = SerialModel.GetData;
            BackLeftTireModel.Pressure.DataGenerator = SerialModel.GetData;
            BackLeftTireModel.Temperature.DataGenerator = SerialModel.GetData;
            BackRightTireModel.Pressure.DataGenerator = SerialModel.GetData;
            BackRightTireModel.Temperature.DataGenerator = SerialModel.GetData;
            // GPS
            GpsModel.Latitude.DataGenerator = SerialModel.GetData;
            GpsModel.Longitude.DataGenerator = SerialModel.GetData;
        }

        public void SetCsvDataSource()
        {
            SettingsModel.DataSource = DataSource.Csv;
            //// SteeringWheel
            //SteeringWheelModel.DataGenerator = SteeringWheel.Default;
            //// GForce
            //GForceXModel.DataGenerator = GForce.Default;
            //GForceYModel.DataGenerator = GForce.Default;
            //// Pressure
            //PressureFrontLeftTireModel.DataGenerator = Pressure.Default;
            //PressureFrontRightTireModel.DataGenerator = Pressure.Default;
            //PressureBackLeftTireModel.DataGenerator = Pressure.Default;
            //PressureBackRightTireModel.DataGenerator = Pressure.Default;
            //// Temperature
            //TemperatureFrontLeftTireModel.DataGenerator = Temperature.Default;
            //TemperatureFrontRightTireModel.DataGenerator = Temperature.Default;
            //TemperatureBackLeftTireModel.DataGenerator = Temperature.Default;
            //TemperatureBackRightTireModel.DataGenerator = Temperature.Default;
            //// GPS
            //LatitudeModel.DataGenerator = Latitude.Default;
            //LongitudeModel.DataGenerator = Longitude.Default;
        }
    }
}
