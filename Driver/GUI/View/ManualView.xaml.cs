using System.Windows;
using System.Windows.Controls;
using Driver.GUI.ViewModels;
using System.Windows.Input;
using System;
using System.Diagnostics;

namespace Driver.GUI.View
{
    public partial class ManualView : Page
    {
        public ManualView()
        {
            InitializeComponent();
            DataContext = new ManualViewModel();
            Debug.WriteLine($"Actual width: {SystemParameters.PrimaryScreenWidth}");
            Debug.WriteLine($"Actual height: {SystemParameters.PrimaryScreenHeight}");
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
        }

        private void ProgressBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        private void ComboBoxItem_Selected(object sender, RoutedEventArgs e)
        {

        }

        private void ComboBoxItem_Selected_1(object sender, RoutedEventArgs e)
        {

        }

        private void ControlButton_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}
