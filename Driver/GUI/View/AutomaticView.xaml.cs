using System.Windows.Controls;
using Driver.GUI.ViewModels;
using System.Windows.Input;
using System.Text.RegularExpressions;


namespace Driver.GUI.View
{
    public partial class AutomaticView : Page
    {
        private readonly Regex numberRegex;

        public AutomaticView()
        {
            InitializeComponent();
            DataContext = new AutomaticViewModel(Workspace);
            numberRegex = new Regex("[^0-9.-]+");
        }

        private bool IsValidNumber(string text)
        {
            return !string.IsNullOrWhiteSpace(text) && numberRegex.IsMatch(text);
        }

        private void xPosInput_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = IsValidNumber(e.Text);
        }

        private void yPosInput_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = IsValidNumber(e.Text);
        }

        private void tPosInput_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = IsValidNumber(e.Text);
        }
    }
}