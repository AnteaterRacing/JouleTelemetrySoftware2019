using System;
using System.Collections.ObjectModel;
using TelemetryApp.Models;
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
            SteeringWheelModel = new SteeringWheel();
            GForceXModel = new GForce();
            GForceYModel = new GForce();
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
            SteeringWheelModel.Update();
            GForceXModel.Update();
            GForceYModel.Update();
            // Notify all properties have changed as mentioned here:
            // https://docs.microsoft.com/en-us/dotnet/api/system.componentmodel.inotifypropertychanged.propertychanged?redirectedfrom=MSDN&view=netframework-4.7.2#remarks
            OnPropertyChanged(null);
        }
    }
}
