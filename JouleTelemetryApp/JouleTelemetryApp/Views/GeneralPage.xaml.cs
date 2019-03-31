using System.Collections.Generic;
using System.IO;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TelemetryApp
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

            // Graphs
            vm = new ViewModel
            {
                CurrentGraph = new GraphViewModel("Random")
            };
            vm.Graphs.Add(vm.CurrentGraph);
            int i = 0;
            vm.Graphs.Add(new GraphViewModel("Fibonacci",
                dataGenerator: () => new DataPoint<double>(Data.Fibonacci(i++ % 20)),
                maximum: 5000
            ));
            vm.Graphs.Add(new GraphViewModel("Constant",
                dataGenerator: () => new DataPoint<double>(50),
                maximum: 100
            ));
            var csvColumnData = Data.CsvColumnData("Assets/Data/sample1.csv", "Steering Position [Deg]");
            vm.Graphs.Add(new GraphViewModel("CSV Data Loop",
                dataGenerator: () => new DataPoint<double>(Data.Enumerate(csvColumnData, loop: true)),
                minimum: -100, maximum: 100
            ));

            Loaded += OnLoaded;
            Unloaded += OnUnloaded;
        }

        private async void OnLoaded(object sender, RoutedEventArgs e)
        {
            foreach (GraphViewModel graph in vm.Graphs)
            {
                graph.Start();
            }
            int a = 0;
            int delta = 1;
            // Speedometer, Tachometer animation
            while (true)
            {

                int val = (a % 100);

                Speed.Value = (val);
                SpeedText.Text = (100-val).ToString() + " MPH";
                int steeringDegrees = a;

                steeringRotate.Rotation = (steeringDegrees);

                gForceTransform.TranslateX = a%100;
                gForceTransform.TranslateY = a % 100;

                if (steeringDegrees > 0)
                {
                    SteeringText.Text = '+'+(steeringDegrees).ToString() + '°';
                }
                else
                {
                    SteeringText.Text = (steeringDegrees).ToString() + '°';
                }

                Motor2CurrentGauge.Value = Math.Abs(a);
                Motor1CurrentGauge.Value = Math.Abs(a);


                Tachometer.Value = 100 - val;
                TachometerText.Text = (val*100).ToString() + " RPM";

                await System.Threading.Tasks.Task.Delay(100);
                
                if (a > 120 || a<-120){
                    delta = delta * -1;
                }
                
                a = a + delta;

            }
        }

        private void OnUnloaded(object sender, RoutedEventArgs e)
        {
            foreach (GraphViewModel graph in vm.Graphs)
            {
                graph.Stop();
            }
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

        private void TextBlock_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }
    }
}
