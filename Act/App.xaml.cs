
using System.Windows;
using System.Windows.Threading;

namespace Act
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            // Process unhandled exception
            MessageBox.Show("خطایی غیرمنتظره‌ای اتفاق افتاده است/n"+e.ToString(),"خطا در نرم‌افزاار",MessageBoxButton.OK,MessageBoxImage.Error);
            // Prevent default unhandled exception processing
            e.Handled = true;
        }
    }
}
