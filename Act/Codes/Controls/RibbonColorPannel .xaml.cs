using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;


namespace dastyar.Codes.Controls
{
    /// <summary>
    /// Interaction logic for RcPanel.xaml
    /// </summary>
    public partial class RibbonColorPanel : UserControl
    {
        public RibbonColorPanel()
        {
            InitializeComponent();
        }
        bool mainColorSelected = true;
        public bool PreventChangedEventRasing { get; set; } = false;
        public delegate void ColorChangedEH(RibbonColorPanel sender);
        public event ColorChangedEH ColorChanged;
        public Color MainColor { get { var b = MainColorBtn.Background as SolidColorBrush; if (b != null) { var c = ((SolidColorBrush)MainColorBtn.Background).Color; c.A = (byte)(255 - mainTrans.Value * 255 / 100f); return c; } else return Colors.Transparent; } set { if (value == Colors.Transparent) SecondColorBtn.Background = translabel.Background; else MainColorBtn.Background = new SolidColorBrush(value); if (ColorChanged != null) ColorChanged(this); } }
        public Color secoundColor
        {
            get
            {
                var b = SecondColorBtn.Background as SolidColorBrush; if (b != null)
                {
                    var c = ((SolidColorBrush)SecondColorBtn.Background).Color; c.A = (byte)(255 - secondTrans.Value * 255 / 100f);
                    return c;
                }
                else return Colors.Transparent;
            }
            set { if (value == Colors.Transparent) SecondColorBtn.Background = translabel.Background; else SecondColorBtn.Background = new SolidColorBrush(value); if (ColorChanged != null) ColorChanged(this); }
        }

        private void color_Click(object sender, RoutedEventArgs e)
        {
            if (mainColorSelected)
            {
                MainColorBtn.Background = ((Label)sender).Background;
                if (ColorChanged != null && !PreventChangedEventRasing)
                    ColorChanged(this);
            }
            else
            {
                SecondColorBtn.Background = ((Label)sender).Background;
                if (ColorChanged != null && !PreventChangedEventRasing)
                    ColorChanged(this);
            }
        }

        private void MainColorBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!mainColorSelected)
            {
                mcCont.Background = scCont.Background;
                scCont.Background = null;
            }
            mainColorSelected = true;

        }

        private void SecondColorBtn_Click(object sender, RoutedEventArgs e)
        {
            if (mainColorSelected)
            {
                scCont.Background = mcCont.Background;
                mcCont.Background = null;
            }
            mainColorSelected = false;
        }

        private void mainTrans_ValueChanged(IntBox sender)
        {
            if (ColorChanged != null && !PreventChangedEventRasing)
                ColorChanged(this);

        }
    }
}
