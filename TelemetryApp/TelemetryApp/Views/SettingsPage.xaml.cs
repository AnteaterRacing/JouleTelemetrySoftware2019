using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using TelemetryApp.Models;
using TelemetryApp.Services;
using TelemetryApp.ViewModels;

namespace TelemetryApp
{
    /// <summary>
    ///     An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SettingsPage : Page
    {
        private static readonly int _updatePeriodLow = 250; // ms
        private static readonly int _updatePeriodHigh = 5000; // ms

        public SettingsPage()
        {
            InitializeComponent();
            Unloaded += SettingsPage_Unloaded;
        }

        public ViewModel Vm { get; private set; }

        private async void SettingsPage_Unloaded(object sender, RoutedEventArgs e)
        {
            await SettingsService.SaveSettings(Vm.SettingsModel);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            Vm = (ViewModel) e.Parameter;
        }

        private void DataSource_RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (!(sender is RadioButton rb) || Vm == null) return;
            var dataSource = rb.Tag.ToString();
            switch (dataSource)
            {
                case "Csv":
                    Vm.UpdateDataSource(DataSource.Csv);
                    break;
                case "Serial":
                    Vm.UpdateDataSource(DataSource.Serial);
                    break;
                default:
                    Vm.UpdateDataSource(DataSource.Random);
                    break;
            }
        }

        private void SerialPort_TextBox_OnKeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key != VirtualKey.Enter) return;
            if (sender is TextBox tb) UpdateSerialPort(tb.Text);
        }

        private void SerialPort_TextBox_OnLostFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox tb) UpdateSerialPort(tb.Text);
        }

        private void UpdatePeriod_TextBox_OnKeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key != VirtualKey.Enter) return;
            if (sender is TextBox tb && int.TryParse(tb.Text, out var updatePeriod)) UpdateUpdatePeriod(updatePeriod);
        }

        private void UpdatePeriod_TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox tb && int.TryParse(tb.Text, out var updatePeriod)) UpdateUpdatePeriod(updatePeriod);
        }

        private void UpdateSerialPort(string portName)
        {
            // TODO: validate port name
            Vm.SettingsModel.SerialPortName = portName;
        }

        private void UpdateUpdatePeriod(int updatePeriod)
        {
            Vm.SettingsModel.UpdatePeriod =
                (int) DataHelper.Constrain(updatePeriod, _updatePeriodLow, _updatePeriodHigh);
        }
    }
}