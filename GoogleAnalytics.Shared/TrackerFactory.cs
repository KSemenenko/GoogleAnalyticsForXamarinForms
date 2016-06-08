using System;
using System.Threading.Tasks;
using Plugin.GoogleAnalytics;

namespace Plugin.GoogleAnalytics
{
    public class TrackerFactory
    {
        private static TrackerFactory current;
        private static Tracker tracker;
        private DateTime? suspended;

        public TrackerFactory()
        {
            InitTracker(Config);
        }

        public TrackerFactory(TrackerConfig config)
        {
            InitTracker(config);
        }

        public Uri ConfigPath { get; set; }
        public static TrackerConfig Config { get; set; }

        public static TrackerFactory Current
        {
            get
            {
                if(current == null && Config != null)
                {
                    current = new TrackerFactory(Config);
                }
                return current;
            }
        }

        public Tracker GetTracker()
        {
            if(tracker == null)
            {
                InitTracker(Config);
            }
            return tracker;
        }

        public void InitTracker(TrackerConfig config)
        {
            Config = config;
            Config.Validate();

            var analyticsEngine = AnalyticsEngine.Current;
            analyticsEngine.IsDebugEnabled = Config.Debug;
            GAServiceManager.Current.DispatchPeriod = Config.DispatchPeriod;
            tracker = analyticsEngine.GetTracker(Config.TrackingId);
            tracker.SetStartSession(Config.SessionTimeout.HasValue);
            tracker.IsUseSecure = Config.UseSecure;
            tracker.AppName = Config.AppName;
            tracker.AppVersion = Config.AppVersion;
            tracker.AppId = Config.AppId;
            tracker.AppInstallerId = Config.AppInstallerId;
            tracker.IsAnonymizeIpEnabled = Config.AnonymizeIp;
            tracker.SampleRate = Config.SampleFrequency;
            tracker.IsDebug = Config.Debug;
        }

        public Task Dispatch()
        {
            return GAServiceManager.Current.Dispatch();
        }
    }
}