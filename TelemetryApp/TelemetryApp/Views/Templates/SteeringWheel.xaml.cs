using TelemetryApp.ViewModels;
using Windows.UI.Xaml.Controls;


namespace TelemetryApp.Views.Templates
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SteeringWheel : Grid
    {
        public ViewModel VM { get; set; }

        public SteeringWheel()
        {
            InitializeComponent();
        }
    }
}
