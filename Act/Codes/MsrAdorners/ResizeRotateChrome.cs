using System.Windows;
using System.Windows.Controls;

namespace Act.Codes.MsrAdorners
{
    public class MsrResizeRotateChrome : Control
    {
        static MsrResizeRotateChrome()
        {
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(MsrResizeRotateChrome), new FrameworkPropertyMetadata(typeof(MsrResizeRotateChrome)));
        }
    }
}
