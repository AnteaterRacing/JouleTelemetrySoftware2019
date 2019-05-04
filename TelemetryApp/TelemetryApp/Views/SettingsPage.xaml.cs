using TelemetryApp.Models;
using TelemetryApp.ViewModels;
using Windows.UI.Xaml;
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

        //private void SelectedDataSourceComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    string selectedDataSourceName = e.AddedItems[0].ToString();
        //    switch(selectedDataSourceName)
        //    {
        //        case "Csv":
        //            VM.SetCsvDataSource();
        //            break;
        //        case "Serial":
        //            VM.SetSerialDataSource();
        //            break;
        //        default:
        //            VM.SetRandomDataSource();
        //            break;
        //    }
        //}

        private void DataSourceRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;

            if (rb != null && VM != null)
            {
                string colorName = rb.Tag.ToString();
                switch (colorName)
                {
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

        private void SerialPortTextBox_TextChanged(object sender, RoutedEventArgs e)
        {

        }

        private void UpdatePeriodTextBox_TextChanged(object sender, RoutedEventArgs e)
        {

        }
    }
}
