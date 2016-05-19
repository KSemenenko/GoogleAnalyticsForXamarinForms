using System;
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

            var x = new EasyTracker(config);

            var y = x.GetTracker();
            y.SendView("Main");

            Console.ReadLine();
        }
    }
}