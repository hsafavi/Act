using System;
using System.Windows;

namespace Act
{
    class act2 : System.Windows.Application
    {
        [STAThread]
        public static void Main()
        {
            //if (SingleInstance<App>.InitializeAsFirstInstance(Unique))
            //{
            try
            {
                var application = new App();
                application.InitializeComponent();
                application.Run();
            }
            catch (Exception x)
            {
                MessageBox.Show(x.ToString());
            }
            // Allow single instance code to perform cleanup operations
            //    SingleInstance<App>.Cleanup();
            //}
        }

        //#region ISingleInstanceApp Members
        //public bool SignalExternalCommandLineArgs(IList<string> args)
        //{
        //    // Handle command line arguments of second instance
        //    return true;
        //}
        //#endregion
    }
}
