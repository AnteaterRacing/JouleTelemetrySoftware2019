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
        private ViewModel vm;

        public GeneralPage()
        {
            InitializeComponent();

            vm = new ViewModel
            {
                CurrentGraph = new GraphViewModel("Value1")
            };
            vm.Graphs.Add(vm.CurrentGraph);
            vm.Graphs.Add(new GraphViewModel("Value2"));
            vm.Graphs.Add(new GraphViewModel("Value3"));

            Loaded += OnLoaded;
            Unloaded += OnUnloaded;
        }

        private async void OnLoaded(object sender, RoutedEventArgs e)
        {
            foreach (GraphViewModel graph in vm.Graphs)
            {
                graph.Start();
            }

            // Speedometer, Tachometer animation
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
        private void OnUnloaded(object sender, RoutedEventArgs e)
        {
            //foreach (GraphViewModel graph in vm.Graphs)
            //    graph.Stop();
        }

        private void GraphComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            vm.CurrentGraph = (GraphViewModel)e.AddedItems[0];
        }

        private void Resolution_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            // TODO: impelement changing graph
            //ValueViewModel.Resolution = Convert.ToInt32(rb.Tag);
        }

    }
}
