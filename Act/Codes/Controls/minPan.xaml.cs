using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Act.Codes.Controls
{
    /// <summary>
    /// Interaction logic for minPan.xaml
    /// </summary>
    public partial class minPan : UserControl
    {
        public minPan()
        {
            InitializeComponent();
        }
        private TranslateTransform transform = new TranslateTransform();
        private bool isInDrag;
        private bool dragged;
        private Point currentPoint;
        private Point anchorPoint;

        public bool Dragged { get => dragged; set => dragged = value; }

        private void root_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (isInDrag)
                {
                    var element = sender as FrameworkElement;
                    currentPoint = e.GetPosition(null);
                    if (!currentPoint.Equals(anchorPoint))
                        dragged = true;
                    transform.X += currentPoint.X - anchorPoint.X;
                    transform.Y += (currentPoint.Y - anchorPoint.Y);
                    this.RenderTransform = transform;
                    anchorPoint = currentPoint;
                }
                else
                {
                    isInDrag = false;
                    dragged = false;
                }
            }
        }

        private void UserControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            isInDrag = true;
            anchorPoint = e.GetPosition(null);
            dragged = false;
        }

        private void UserControl_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            isInDrag = false;
        }

        private void ShowBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }

}
