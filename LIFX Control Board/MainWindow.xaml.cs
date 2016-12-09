using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Windows;
using System.Windows.Media;
using LIFX_Control_Board.Properties;
using System.Diagnostics;

namespace LIFX_Control_Board
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public String KEY;

        public MainWindow()
        {
            InitializeComponent();

            if (Debugger.IsAttached)
                Settings.Default.Reset();

            label.Content = "Red: " + slider.Value;
            label1.Content = "Green: " + slider1.Value;
            label2.Content = "Blue: " + slider2.Value;

            updateCanvas();
        }

        public String loadAPIKey()
        {
            if (Settings.Default.KEY == "")
            {
                Application.Current.MainWindow.Height = 357;

            } else
            {
                Application.Current.MainWindow.Height = 307;
            }
            Console.WriteLine(Settings.Default.KEY);
            return Settings.Default.KEY;
        }

        public String generateHex()
        {
            int red, green, blue;

            red = (int) slider.Value;
            green = (int) slider1.Value;
            blue = (int) slider2.Value;

            String finalHex = "#" + red.ToString("X2") + green.ToString("X2") + blue.ToString("X2");

            Console.WriteLine(finalHex);
            return finalHex;
        }

        public String getBrightness()
        {
            Console.WriteLine((slider3.Value / 100).ToString());
            return (slider3.Value / 100).ToString();
        }


        public void updateCanvas()
        {
            canvas.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom(generateHex()));
        }

        //Update Color
        private async void button_Click(object sender, RoutedEventArgs e)
        {
            KEY = loadAPIKey();

            var webAddr = "https://maker.ifttt.com/trigger/windows_lifx_update/with/key/" + KEY;
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(webAddr);
            httpWebRequest.ContentType = "application/json; charset=utf-8";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = "{\"value1\":\"" + generateHex() + "\", \"value2\" : \"" + getBrightness() + "\"}";

                streamWriter.Write(json);
                streamWriter.Flush();
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                Console.WriteLine(result);
            }
        }

        //Toggle Lights
        private async void button1_Click(object sender, RoutedEventArgs e)
        {
            KEY = loadAPIKey();
            var client = new HttpClient();

            var requestContent2 = new FormUrlEncodedContent(new[] {
                new KeyValuePair<string, string>("", ""),
            });

            HttpResponseMessage responseString = await client.PostAsync("https://maker.ifttt.com/trigger/windows_light/with/key/" + KEY, requestContent2);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Visibility = Visibility.Hidden;
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            slider.Value = 255;
            slider1.Value = 214;
            slider2.Value = 170;
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            slider.Value = 255;
            slider1.Value = 241;
            slider2.Value = 224;
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            slider.Value = 255;
            slider1.Value = 250;
            slider2.Value = 244;
        }

        private void button5_Click(object sender, RoutedEventArgs e)
        {
            slider.Value = 255;
            slider1.Value = 255;
            slider2.Value = 255;
        }

        private void button6_Click(object sender, RoutedEventArgs e)
        {
            slider.Value = 156;
            slider1.Value = 67;
            slider2.Value = 183;
        }

        private void button7_Click(object sender, RoutedEventArgs e)
        {
            slider.Value = 201;
            slider1.Value = 226;
            slider2.Value = 255;
        }

        private void button8_Click(object sender, RoutedEventArgs e)
        {
            HelpWindow help = new LIFX_Control_Board.HelpWindow();
            help.Show();
        }

        private void slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            label.Content = "Red: " + slider.Value;
            updateCanvas();
        }

        private void slider1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            label1.Content = "Green: " + slider1.Value;
            updateCanvas();
        }

        private void slider2_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            label2.Content = "Blue: " + slider2.Value;
            updateCanvas();
        }

        private void slider3_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            label4.Content = "Brightness: " + slider3.Value;
        }
    }
}
