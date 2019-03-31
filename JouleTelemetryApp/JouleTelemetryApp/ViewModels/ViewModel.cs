using System.Collections.ObjectModel;
using Telerik.Core;

namespace TelemetryApp
{
    public class ViewModel : ViewModelBase
    {
        // Steering
        private DataPoint<double> steering;

        public DataPoint<double> Steering
        {
            get => steering;
            set
            {
                steering = value;
                OnPropertyChanged("Steering");
            }
        }

        // Graphing
        private ObservableCollection<GraphViewModel> graphs;

        public ObservableCollection<GraphViewModel> Graphs
        {
            get => graphs;
            set
            {
                graphs = value;
                OnPropertyChanged("Graphs");
            }
        }

        private GraphViewModel currentGraph;

        public GraphViewModel CurrentGraph
        {
            get => currentGraph;
            set
            {
                currentGraph = value;
                OnPropertyChanged("CurrentGraph");
            }
        }

        public ViewModel()
        {
            graphs = new ObservableCollection<GraphViewModel>();
            currentGraph = null;
        }
    }
}
