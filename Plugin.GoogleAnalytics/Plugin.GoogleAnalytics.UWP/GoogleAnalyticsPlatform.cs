using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace Plugin.GoogleAnalytics
{
    public partial class GoogleAnalytics
    {
        static GoogleAnalytics()
        {
            Application.Current.UnhandledException += CurrentDomain_UnhandledException;
            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (!Current.Config.ReportUncaughtExceptions)
                return;
            
            Current.Tracker.SendException(e.Exception, true);
            Task.Delay(1000).Wait(); //delay
        }

        private static void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            if (!Current.Config.ReportUncaughtExceptions)
                return;

            Current.Tracker.SendException(e.Exception, true);
        }
    }
}