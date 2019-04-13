using TelemetryApp.ViewModels;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TelemetryApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SuspensionPage : Page
    {
        public ViewModel VM { get; set; }

        public SuspensionPage(ViewModel viewModel)
        {
            InitializeComponent();

            VM = viewModel;
        }
    }
}
