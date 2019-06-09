using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using TelemetryApp.Models;
using TelemetryApp.ViewModels;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TelemetryApp.Views
{
    /// <summary>
    ///     An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GeneralPage : Page
    {
        public GeneralPage()
        {
            InitializeComponent();
        }

        public ViewModel VM { get; set; }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            VM = (ViewModel) e.Parameter;
        }

        private void GraphComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            VM.CurrentGraph = (Graph) e.AddedItems[0];
        }

        private void Resolution_Checked(object sender, RoutedEventArgs e)
        {
            var rb = sender as RadioButton;
            // TODO: implement changing graph
            //ValueViewModel.Resolution = Convert.ToInt32(rb.Tag);
        }
    }
}