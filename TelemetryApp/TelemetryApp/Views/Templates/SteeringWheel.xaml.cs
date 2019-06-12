using Windows.UI.Xaml.Controls;
using TelemetryApp.ViewModels;

namespace TelemetryApp.Views.Templates
{
    /// <summary>
    ///     An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SteeringWheel : Grid
    {
        public SteeringWheel()
        {
            InitializeComponent();
        }

        public ViewModel Vm { get; set; }
    }
}