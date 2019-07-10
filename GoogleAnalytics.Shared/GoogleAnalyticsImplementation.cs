using System;
using System.Threading;
using Plugin.GoogleAnalytics.Abstractions;
#if ANDROID
using Android.Runtime;
#endif

#if __IOS__ || __MACOS__
using Foundation;
#endif

namespace Plugin.GoogleAnalytics
{
#if !WINDOWS_UWP
    [Preserve(AllMembers = true)]
#endif
    public class GoogleAnalyticsImplementation : IGoogleAnalytics
    {
        static GoogleAnalyticsImplementation()
        {
            StaticConfig = new TrackerConfig(new PlatformInfoProvider());
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