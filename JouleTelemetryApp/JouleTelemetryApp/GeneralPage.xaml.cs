using System;
using System.Collections.Generic;
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
                CurrentGraph = new GraphViewModel("Random")
            };
            vm.Graphs.Add(vm.CurrentGraph);
            int i = 0;
            vm.Graphs.Add(new GraphViewModel("Fibonacci", dataGenerator: () => Fibonacci(i++ % 20), maximum: 5000));
            vm.Graphs.Add(new GraphViewModel("Constant", dataGenerator: () => 50));

            Loaded += OnLoaded;
            Unloaded += OnUnloaded;
        }

        private int Fibonacci(int i)
        {
            if (i <= 0) return 0;
            else if (i == 1 || i == 2) return 1;
            else return Fibonacci(i - 1) + Fibonacci(i - 2);
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
