using LIFX_Control_Board.Properties;
using System;
using System.Windows;

namespace LIFX_Control_Board
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();

            textBox.Text = loadAPIKey();
        }

        public String loadAPIKey()
        {
            Console.WriteLine(Settings.Default.KEY);
            return Settings.Default.KEY;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Settings.Default.KEY = textBox.Text;
            Settings.Default.Save();

            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Visibility = Visibility.Hidden;
        }
    }
}
