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
            GoogleAnalytics.Current.Config.TrackingId = "UA-11111111-1";
            GoogleAnalytics.Current.Config.AppId = "AppID";
            GoogleAnalytics.Current.Config.AppName = "TEST";
            GoogleAnalytics.Current.Config.AppInstallerId = Guid.NewGuid().ToString();

            //  GoogleAnalytics.Current.Config.Debug = true;


            try
            {
                GoogleAnalytics.Current.Tracker.SendView("MainPage");
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