using System;
using System.Collections.ObjectModel;
using Telerik.Core;
using Windows.UI.Xaml;
using TelemetryApp.Models;

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
                OnPropertyChanged();
            }
        }

        // Steering
        public Steering Steering { get; }

        // Graphs
        private ObservableCollection<GraphViewModel> graphs;
        public ObservableCollection<GraphViewModel> Graphs
        {
            get => graphs;
            set
            {
                graphs = value;
                OnPropertyChanged();
            }
        }

        private GraphViewModel currentGraph;
        public GraphViewModel CurrentGraph
        {
            get => currentGraph;
            set
            {
                currentGraph = value;
                OnPropertyChanged();
            }
        }
        
        protected DispatcherTimer timer;

        public ViewModel()
        {
            // Initialize
            updatePeriod = 1000;
            Steering = new Steering();
            graphs = new ObservableCollection<GraphViewModel>();
            currentGraph = new GraphViewModel("Random");

            // Graphs
            Graphs.Add(currentGraph);
            int i = 0;
            Graphs.Add(new GraphViewModel("Fibonacci",
                dataGenerator: () => new DataPoint<double>(Data.Fibonacci(i++ % 20)),
                maximum: 5000
            ));
            Graphs.Add(new GraphViewModel("Constant",
                dataGenerator: () => new DataPoint<double>(50),
                maximum: 100
            ));
            var csvColumnData = Data.CsvColumnData("Assets/Data/sample1.csv", "Steering Position [Deg]");
            Graphs.Add(new GraphViewModel("CSV Data Loop",
                dataGenerator: () => new DataPoint<double>(Data.Enumerate(csvColumnData, loop: true)),
                minimum: -100, maximum: 100
            ));
            
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
            Steering.Update((int) Data.RandomDouble(low: -180, high: 180));
            OnPropertyChanged(nameof(Steering));
        }
    }

}
