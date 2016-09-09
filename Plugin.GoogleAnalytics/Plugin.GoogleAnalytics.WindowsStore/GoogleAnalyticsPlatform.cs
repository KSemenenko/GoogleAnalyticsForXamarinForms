using System;
using Windows.UI.Xaml;

namespace Plugin.GoogleAnalytics
{
    public partial class GoogleAnalytics
    {
        static GoogleAnalytics()
        {
            Application.Current.UnhandledException += CurrentDomain_UnhandledException;
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Current.Tracker.SendException(e.Exception, true);
        }
    }
}