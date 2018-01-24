using System;

namespace Plugin.GoogleAnalytics
{
    public partial class GoogleAnalytics
    {
        static GoogleAnalytics()
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (!Current.Config.ReportUncaughtExceptions)
                return;
            
            Current.Tracker.SendException(e.ExceptionObject as Exception, true);
        }
    }
}
