﻿using System;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace JouleTelemetryApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GeneralPage : Page
    {
        public GeneralPage()
        {
            this.InitializeComponent();
            this.Loaded += MainPage_Loaded;
        }

        private async void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            for (int a = 0; a <= 100; a = a + 1)
            {
                Tachometer.Value=a; 
                Green.Offset = (Tachometer.Value-25)*(25/(Tachometer.Value+1)) / 100;
                Yellow.Offset = (Tachometer.Value - 25) * (25 / (Tachometer.Value + 1))) / 100;
                Red.Offset = (Tachometer.Value-100) / 100;
                await System.Threading.Tasks.Task.Delay(500);
            }
        }
    }
}
