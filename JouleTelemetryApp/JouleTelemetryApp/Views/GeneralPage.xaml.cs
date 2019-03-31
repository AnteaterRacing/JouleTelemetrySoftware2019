using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System;
using TelemetryApp.ViewModels;
using TelemetryApp.Models;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TelemetryApp.Views
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

            // ViewModel for page
            vm = new ViewModel();

            Loaded += OnLoaded;
            Unloaded += OnUnloaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            vm.Start();
            //int a = 0;
            //int delta = 1;
            // Speedometer, Tachometer animation
            //while (true)
            //{

            //    int val = (a % 100);

            //    Speed.Value = (val);
            //    SpeedText.Text = (100-val).ToString() + " MPH";
            //    int steeringDegrees = a;

            //    steeringRotate.Rotation = (steeringDegrees);

            //    gForceTransform.TranslateX = a%100;
            //    gForceTransform.TranslateY = a % 100;

            //    if (steeringDegrees > 0)
            //    {
            //        SteeringText.Text = '+'+(steeringDegrees).ToString() + '°';
            //    }
            //    else
            //    {
            //        SteeringText.Text = (steeringDegrees).ToString() + '°';
            //    }

            //    Motor2CurrentGauge.Value = Math.Abs(a);
            //    Motor1CurrentGauge.Value = Math.Abs(a);


            //    Tachometer.Value = 100 - val;
            //    TachometerText.Text = (val*100).ToString() + " RPM";

            //    await System.Threading.Tasks.Task.Delay(100);
                
            //    if (a > 120 || a<-120){
            //        delta = delta * -1;
            //    }
                
            //    a = a + delta;

            //}
        }

        private void OnUnloaded(object sender, RoutedEventArgs e)
        {
            vm.Stop();
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
