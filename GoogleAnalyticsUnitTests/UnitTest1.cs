using GoogleAnalytics.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GoogleAnalyticsUnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var config = new TrackerConfig();
            config.AppVersion = "1";
            config.TrackingId = "2";
            config.Debug = true;

            var x = new EasyTracker(config);

            var y = x.GetTracker();
            y.SendView("Main");
        }
    }
}