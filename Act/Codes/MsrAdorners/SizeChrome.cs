using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Act.Codes.MsrAdorners
{
    public class MsrSizeChrome : Control
    {
        static MsrSizeChrome()
        {
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(MsrSizeChrome), new FrameworkPropertyMetadata(typeof(MsrSizeChrome)));
        }
    }

    public class DoubleFormatConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double d = (double)value;
            return Math.Round(d);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
