using System;
using System.Threading.Tasks;
using Plugin.GoogleAnalytics;
using Plugin.GoogleAnalytics.Abstractions;

namespace Plugin.GoogleAnalytics
{
    public class TrackerFactory
    {
        private static TrackerFactory current;
        private static Tracker tracker;
        private static bool firstRun= true;

        public TrackerFactory()
        {
            InitTracker(Config);
        }

        public TrackerFactory(ITrackerConfig config)
        {
            Config = config;
            InitTracker(Config);
        }

        public Uri ConfigPath { get; set; }
        public static ITrackerConfig Config { get; set; }

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

        public ITracker GetTracker()
        {
            if(tracker == null)
            {
                InitTracker(Config);
            }
            return tracker;
        }

        public ITracker GetTracker(ITrackerConfig config)
        {
            InitTracker(config);
            return tracker;
        }

        public void InitTracker(ITrackerConfig config)
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

            if(firstRun)
            {
                firstRun = false;
                if (analyticsEngine.PlatformInfoProvider.IsInstall)
                {
                    tracker.SendEvent(Config.ServiceCategoryName, Config.InstallMessage, "v."+Config.AppVersion);
                }

                tracker.SendEvent(Config.ServiceCategoryName, Config.StartMessage, "v." + Config.AppVersion);
                
            }

            
        }

        public Task Dispatch()
        {
            return GAServiceManager.Current.Dispatch();
        }
    }
}