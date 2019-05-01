using TelemetryApp.Models;
using TelemetryApp.ViewModels;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace TelemetryApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SettingsPage : Page
    {
        public ViewModel VM { get; private set; }

        public SettingsPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            VM = (ViewModel)e.Parameter;
        }

        private void SelectedDataSourceComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedDataSourceName = e.AddedItems[0].ToString();
            switch(selectedDataSourceName)
            {
                case "Random":
                    VM.SetRandomDataSource();
                    break;
                case "Csv":
                    VM.SetCsvDataSource();
                    break;
                case "Serial":
                    VM.SetSerialDataSource();
                    break;
                default:
                    VM.SetRandomDataSource();
                    break;
            }
        }
    }
}
