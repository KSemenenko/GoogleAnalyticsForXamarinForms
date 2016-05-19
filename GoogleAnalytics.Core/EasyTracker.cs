using System;
using System.Threading.Tasks;

namespace GoogleAnalytics.Core
{
    public class EasyTracker
    {
        private static EasyTracker current;
        private static Tracker tracker;
        private DateTime? suspended;

        public EasyTracker()
        {
            InitTracker(Config);
        }

        public EasyTracker(TrackerConfig config)
        {
            InitTracker(config);
        }

        public Uri ConfigPath { get; set; }
        public static TrackerConfig Config { get; set; }

        public static EasyTracker Current
        {
            get
            {
                if(current == null)
                {
                    current = new EasyTracker(Config);
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

        private void InitTracker(TrackerConfig config)
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