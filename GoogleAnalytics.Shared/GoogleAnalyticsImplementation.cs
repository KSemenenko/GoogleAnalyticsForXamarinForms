using System;
using System.Threading;
using Plugin.GoogleAnalytics.Abstractions;

namespace Plugin.GoogleAnalytics
{
    public class GoogleAnalyticsImplementation : IGoogleAnalytics
    {
        static GoogleAnalyticsImplementation()
        {
            StaticConfig = new TrackerConfig();
        }

        public GoogleAnalyticsImplementation()
        {
            var platform = new PlatformInfoProvider();
            Config.AppVersion = platform.Version.ToString();
            TrackerFactory.Config = Config;
        }

        private static ITrackerConfig StaticConfig { get; set; }

        public ITrackerConfig Config
        {
            get { return StaticConfig; }
            set { StaticConfig = value; }
        }

        public ITracker Tracker
        {
            get { return TrackerFactory.Current.GetTracker(); }
        }

        public void InitTracker()
        {
            TrackerFactory.Current.InitTracker(Config);
        }
    }
}