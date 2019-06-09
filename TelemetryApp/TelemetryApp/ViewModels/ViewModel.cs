using System;
using System.Collections.ObjectModel;
using System.IO;
using Windows.UI.Xaml;
using TelemetryApp.Models;
using TelemetryApp.Models.Data;

namespace TelemetryApp.ViewModels
{
    public class ViewModel : NotifyPropertyChanged
    {
        // Timer for calling Update
        private readonly DispatcherTimer _timer;

        private Graph _currentGraph;

        public ViewModel()
        {
            Init();
            // Timer for updating once a second
            _timer = new DispatcherTimer {Interval = TimeSpan.FromMilliseconds(SettingsModel.UpdatePeriod)};
            _timer.Tick += Tick;
        }

        public Data Data { get; private set; }
        public Settings SettingsModel { get; private set; }

        //private Thread _updateDataModel;

        // Graphs
        public ObservableCollection<Graph> Graphs { get; private set; }

        public Graph CurrentGraph
        {
            get => _currentGraph;
            set
            {
                _currentGraph = value;
                OnPropertyChanged(nameof(CurrentGraph));
            }
        }

        private void Init()
        {
            // Settings
            SettingsModel = new Settings();

            UpdateDataSource(SettingsModel.DataSource);

            // Provide access to serial data
            //_updateDataModel = new Thread(() => Data.Update());
            //_updateDataModel.Start();

            // Graphs
            Graphs = new ObservableCollection<Graph>();
            CurrentGraph = new Graph(
                () => DataHelper.RandomDouble(0, 100),
                "Random",
                maximum: 100
            );
            Graphs.Add(_currentGraph);
            //var fibonacci = DataHelper.FibonacciRange(0, 10);
            //Graphs.Add(new Graph(
            //    () => DataHelper.EnumerateInteger(fibonacci, loop: true),
            //    "Fibonacci",
            //    maximum: 5000
            //));
            Graphs.Add(new Graph(
                () => 50,
                "Constant",
                maximum: 100
            ));
            //var csvEnum = new DataPoints<string>(Csv.CsvReader.ReadFromText("Assets/DataHelper/short.csv"));
            //Graphs.Add(new GraphViewModel(
            //    () => new DataPoint<double>(DataHelper.EnumerateDouble(csvEnum["Steering Position [Deg]"], loop: true)),
            //    "CSV DataHelper Loop",
            //    minimum: -100, maximum: 100
            //));
        }

        public void Start()
        {
            _timer.Start();
        }

        public void Stop()
        {
            //_updateDataModel.Join();
            _timer.Stop();
        }

        protected void Tick(object sender, object e)
        {
            Update();
        }

        public void Update()
        {
            //if (!_updateDataModel.IsAlive) _updateDataModel.Start();
            Data.Update();
            OnPropertyChanged(nameof(Data));
            // Graphs
            foreach (var graph in Graphs) graph.Update();
            //OnPropertyChanged(nameof(Graphs));
        }

        public void UpdateDataSource(DataSource dataSource)
        {
            try
            {
                Data?.Unload();
                switch (dataSource)
                {
                    //var port = new SerialPort("COM4", 9600, Parity.None, 8, StopBits.One);
                    case DataSource.Serial:
                        Data = new SerialData(SettingsModel.SerialPortName);
                        break;
                    case DataSource.Csv:
                        Data = new CsvData(SettingsModel.CsvFileName);
                        break;
                    case DataSource.Random:
                        Data = new RandomData();
                        break;
                    default:
                        Data = new RandomData();
                        break;
                }

                SettingsModel.DataSource = dataSource;
            }
            catch (IOException e)
            {
                Console.WriteLine(e);
            }
        }
    }
}