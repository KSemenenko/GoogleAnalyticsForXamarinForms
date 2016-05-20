using System;
using System.Collections.Generic;
using System.Runtime;
using System.Runtime.InteropServices;
using GoogleAnalytics.Core;

namespace TestConsoleApplication
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var config = new TrackerConfig();
            config.AppVersion = "1";
            config.TrackingId = "2";
            config.Debug = true;

            var x = new TrackerFactory(config);

            var y = x.GetTracker();
            y.SendView("Main");


            var trackerManager = new TrackerManager(new PlatformInfoProvider()
                                                    {
                                                        AnonymousClientId = "12321312",
                                                        ScreenResolution = new Dimensions(1920, 1080),
                                                        UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64; Trident/7.0; rv:11.0) like Gecko",
                                                        UserLanguage = "en-us",
                                                        ViewPortResolution = new Dimensions(1920, 1080)
                                                    });

            var tracker = trackerManager.GetTracker("UA-39959863-1"); // saves as default
            tracker.AppName = "My app";
            tracker.AppVersion = "1.0.0.0";

            // Log something to GA
            tracker.SendView("MainPage");


        }

    }

}