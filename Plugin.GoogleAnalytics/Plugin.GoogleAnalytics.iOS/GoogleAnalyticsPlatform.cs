using System;
using System.Threading;
using System.Threading.Tasks;
using Foundation;

namespace Plugin.GoogleAnalytics
{
    [Preserve(AllMembers = true)]
    public partial class GoogleAnalytics
    {
        static GoogleAnalytics()
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;
        }

        private static void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            if (!Current.Config.ReportUncaughtExceptions)
                return;

            Current.Tracker.SendException(e.Exception, true);
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (!Current.Config.ReportUncaughtExceptions)
                return;
            
            Current.Tracker.SendException(e.ExceptionObject as Exception, true);
            Thread.Sleep(1000); //delay
        }
    }
}
