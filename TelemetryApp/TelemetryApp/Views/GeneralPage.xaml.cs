using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System;
using TelemetryApp.ViewModels;
using TelemetryApp.Models;
using Windows.UI.Xaml.Navigation;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TelemetryApp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GeneralPage : Page
    {
        public ViewModel VM { get; set; }

        public GeneralPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            VM = (ViewModel)e.Parameter;
        }

        private void GraphComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            VM.CurrentGraph = (Graph)e.AddedItems[0];
        }

        private void Resolution_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            // TODO: implement changing graph
            //ValueViewModel.Resolution = Convert.ToInt32(rb.Tag);
        }
    }
}
