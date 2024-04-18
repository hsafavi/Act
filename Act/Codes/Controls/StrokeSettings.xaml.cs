using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace dastyar.Codes.Controls
{
    /// <summary>
    /// Interaction logic for StrokeSettings.xaml
    /// </summary>
    public partial class StrokeSettings : UserControl
    {
        public StrokeSettings()
        {
            InitializeComponent();
            intbox.ValueChanged += IntBox_ValueChanged;
            intbox.TextAlignment = TextAlignment.Center;
        }
        public delegate void ChangedEH(StrokeSettings sender);
        public event ChangedEH Changed;
        public int Weight { get { return intbox.Value; } set { intbox.Value = value; } }
        private void IntBox_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            if (!Regex.IsMatch(e.DataObject.GetData(typeof(string)).ToString(), "^[0-9]+$"))
            {
                e.CancelCommand();
            }
        }

        private void IntBox_ValueChanged(IntBox sender)
        {
            if (Changed != null)
                Changed(this);
        }
    }
}
