using System.Windows.Controls.Primitives;

namespace Act.Codes
{
    class ToggleButtonManager
    {

        private ToggleButton CheckedButton;
        public ToggleButtonManager() { }


        public void Add(params ToggleButton[] toggleButtons)
        {
            foreach (var b in toggleButtons)
                b.Checked += Button_Checked;
        }

        private void Button_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            var b = (ToggleButton)sender;
            if (CheckedButton != null && b != CheckedButton)
                CheckedButton.IsChecked = false;
            CheckedButton = b;
        }
    }
}
