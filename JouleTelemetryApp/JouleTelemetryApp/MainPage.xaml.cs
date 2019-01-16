using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace JouleTelemetryApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            genFrame.Navigate(typeof(GeneralPage));
        }

        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            MySplitView.IsPaneOpen = !MySplitView.IsPaneOpen;

        }
        private void General_Click(object sender, RoutedEventArgs e)
        {
            genFrame.Navigate(typeof(GeneralPage));
        }
        private void Suspension_Click(object sender, RoutedEventArgs e)
        {
            genFrame.Navigate(typeof(SuspensionPage));
        }
        private void Powertrain_Click(object sender, RoutedEventArgs e)
        {
            genFrame.Navigate(typeof(Powertrain));
        }
        private void Battery_Click(object sender, RoutedEventArgs e)
        {
            genFrame.Navigate(typeof(Battery));
        }
        private void HumanInterface_Click(object sender, RoutedEventArgs e)
        {
            genFrame.Navigate(typeof(HumanInterfacePage));
        }
    }
}
