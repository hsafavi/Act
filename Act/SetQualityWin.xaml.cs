using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Act
{
    /// <summary>
    /// Interaction logic for SetQualityWin.xaml
    /// </summary>
    public partial class SetQualityWin : Window
    {
        public int Quality { get; set; }
        public SetQualityWin()
        {
            InitializeComponent();
            Quality = 70;
        }

        private void high_clicked(object sender, RoutedEventArgs e)
        {
            Quality = 100;
            this.Close();
        }

        private void medium_clicked(object sender, RoutedEventArgs e)
        {
            Quality = 70;
            Close();
        }

        private void low_clicked(object sender, RoutedEventArgs e)
        {
            Quality = 40;
            Close();
        }
    }
}
