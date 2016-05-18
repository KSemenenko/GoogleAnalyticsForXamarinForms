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
            InitTracker(new EasyTrackerConfig());
        }

        public Uri ConfigPath { get; set; }
        public EasyTrackerConfig Config { get; set; }

        public static EasyTracker Current
        {
            get
            {
                if(current == null)
                {
                    current = new EasyTracker();
                }
                return current;
            }
        }

        public Tracker GetTracker()
        {
            if(tracker == null)
            {
                //Application ctx = null;
                //try
                //{
                //    ctx = Application.Current;
                //}
                //catch
                //{
                //    /* ignore, Win8 JS cannot get the Current Application. Therefore we will pass null instead as context */
                //}
                //Current.SetContext(ctx);
            }
            return tracker;
        }

        private void InitTracker(EasyTrackerConfig config)
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
        }

        public Task Dispatch()
        {
            return GAServiceManager.Current.Dispatch();
        }
    }
}