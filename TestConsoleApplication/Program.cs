using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoogleAnalytics.Core;

namespace TestConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            TrackerConfig config = new TrackerConfig();
            config.AppVersion = "1";
            config.TrackingId = "2";
            config.Debug = true;


            var x = new GoogleAnalytics.Core.EasyTracker(config);

            var y = x.GetTracker();
            y.SendView("Main");
        }
    }
}
