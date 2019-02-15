using System.Collections.ObjectModel;
using Telerik.Core;

namespace JouleTelemetryApp
{
    public class ViewModel : ViewModelBase
    {
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
