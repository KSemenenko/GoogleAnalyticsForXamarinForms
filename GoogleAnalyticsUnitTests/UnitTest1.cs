using Microsoft.VisualStudio.TestTools.UnitTesting;
using GoogleAnalytics.Core;

namespace GoogleAnalyticsUnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var x = new GoogleAnalytics.Core.EasyTracker();
            var y = x.GetTracker();
            y.SendView("Main");
        }
    }
}