using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using TelemetryApp.ViewModels;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TelemetryApp.Views
{
    /// <summary>
    ///     An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HumanInterfacePage : Page
    {
        public HumanInterfacePage()
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