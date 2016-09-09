using System;
using Plugin.GoogleAnalytics.Abstractions;
using Plugin.GoogleAnalytics.Abstractions.Model;

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
            Current.Tracker.SendException(e.ExceptionObject as Exception, true);
        }
    }
}