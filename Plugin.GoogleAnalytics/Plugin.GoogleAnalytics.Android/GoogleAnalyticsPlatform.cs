using System;
using System.Threading;
using Android.Runtime;

namespace Plugin.GoogleAnalytics
{
    public partial class GoogleAnalytics
    {
        static GoogleAnalytics()
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            AndroidEnvironment.UnhandledExceptionRaiser += AndroidEnvironment_UnhandledExceptionRaiser;
        }

        private static void AndroidEnvironment_UnhandledExceptionRaiser(object sender, RaiseThrowableEventArgs e)
        {
            if (!Current.Config.ReportUncaughtExceptions)
                return;
            
            Current.Tracker.SendException(e.Exception, true);
            Thread.Sleep(1000); //delay
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (!Current.Config.ReportUncaughtExceptions)
                return;
            
            Current.Tracker.SendException(e.ExceptionObject as Exception, true);
        }
    }
}
