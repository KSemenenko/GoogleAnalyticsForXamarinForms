using System;
using System.Collections.Generic;
using System.Runtime;
using System.Runtime.InteropServices;
using Plugin.GoogleAnalytics;

namespace TestConsoleApplication
{
    public class Program
    {
        private static void Main(string[] args)
        {
            CrossGoogleAnalytics.Current.Config.TrackingId = "UA-11111111-1";
            CrossGoogleAnalytics.Current.Config.AppId = "AppID";
            CrossGoogleAnalytics.Current.Config.AppName = "TEST";
            CrossGoogleAnalytics.Current.Config.AppInstallerId = Guid.NewGuid().ToString();

            CrossGoogleAnalytics.Current.Config.Debug = true;


            try
            {
                CrossGoogleAnalytics.Current.Tracker.SendView("MainPage");
            }
            catch (Exception ex)
            {
                int a = 5;
            }
        }

        private static void Tets2()
        {
            //GA.Config.TrackingId = "UA-1111111";

            //GA.Tracker.SendEvent("sdfdsf");
        }
    }
}