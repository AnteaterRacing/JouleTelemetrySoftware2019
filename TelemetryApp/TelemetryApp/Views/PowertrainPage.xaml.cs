using Windows.UI.Xaml.Navigation;
using TelemetryApp.ViewModels;

namespace TelemetryApp.Views
{
    /// <summary>
    ///     An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PowertrainPage
    {
        public PowertrainPage()
        {
            InitializeComponent();
        }

        public ViewModel Vm { get; set; }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            Vm = (ViewModel) e.Parameter;
        }
    }
}