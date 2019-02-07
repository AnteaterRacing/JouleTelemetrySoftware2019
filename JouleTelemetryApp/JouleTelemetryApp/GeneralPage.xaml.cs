using OxyPlot;
using OxyPlot.Series;
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
        public GeneralPage()
        {
            this.InitializeComponent();
            this.Loaded += MainPage_Loaded;
        }

        private async void MainPage_Loaded(object sender, RoutedEventArgs e)
        {

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
    }

    public class GeneralPageModel
    {
        public GeneralPageModel()
        {
            this.SpeedModel = new PlotModel { Title = "Speed (MPH)" };
            LineSeries ls = new LineSeries();
            ls.Points.AddRange(new List<DataPoint> {
                new DataPoint(0, 4),
                new DataPoint(10, 13),
                new DataPoint(20, 15),
                new DataPoint(30, 16),
                new DataPoint(40, 12),
                new DataPoint(50, 12)
            });
            this.SpeedModel.Series.Add(ls);
            // this.SpeedModel.InvalidatePlot(true);
        }

        public PlotModel SpeedModel { get; private set; }
    }
}
