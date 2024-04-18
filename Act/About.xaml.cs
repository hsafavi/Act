using System.Windows;
using System.Windows.Input;

namespace Act
{
    /// <summary>
    /// Interaction logic for About.xaml
    /// </summary>
    public partial class About : Window
    {

        public About()
        {
            InitializeComponent();
            back.MouseLeftButtonDown += Back_MouseLeftButtonDown;
            this.CloseBtn.MouseLeftButtonDown += CloseBtn_MouseLeftButtonDown;
            CloseBtn.Cursor = Cursors.Hand;
            //cloud.MouseLeftButtonDown += Cloud_MouseLeftButtonDown;
            //cloud.MouseMove += Cloud_MouseMove;
            //cloud.MouseLeftButtonUp += Cloud_MouseLeftButtonUp;
        }

        private void CloseBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void Back_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }


        private void teleg_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(@"https://t.me/h_safavi");
        }

        private void email_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(@"mailto:hossein.safavi@gmail.com");
        }
    }
}
