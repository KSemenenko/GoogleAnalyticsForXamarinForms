using System.Collections.Generic;
using Plugin.GoogleAnalytics.Abstractions;
using Plugin.GoogleAnalytics.Abstractions.Model;
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
    public sealed class TrackerManager : IServiceManager
    {
        private readonly Dictionary<string, Tracker> trackers;

        public TrackerManager(IPlatformInfoProvider platformTrackingInfo)
        {
            trackers = new Dictionary<string, Tracker>();
            PlatformTrackingInfo = platformTrackingInfo;
            GAServiceManager.Current.UserAgent = PlatformTrackingInfo.UserAgent;
        }

        public Tracker DefaultTracker { get; set; }
        public bool IsDebugEnabled { get; set; }
        public bool AppOptOut { get; set; }
        public IPlatformInfoProvider PlatformTrackingInfo { get; }

        public void SendPayload(Payload payload)
        {
            if(!AppOptOut)
            {
                GAServiceManager.Current.SendPayload(payload);
            }
        }

        public string UserAgent
        {
            get { return GAServiceManager.Current.UserAgent; }
            set { GAServiceManager.Current.UserAgent = value; }
        }

        public Tracker GetTracker(string propertyId)
        {
            propertyId = propertyId ?? string.Empty;
            if(!trackers.ContainsKey(propertyId))
            {
                var tracker = new Tracker(propertyId, PlatformTrackingInfo, this);
                trackers.Add(propertyId, tracker);
                if(DefaultTracker == null)
                {
                    DefaultTracker = tracker;
                }
                return tracker;
            }
            return trackers[propertyId];
        }

        public void CloseTracker(Tracker tracker)
        {
            trackers.Remove(tracker.TrackingId);
            if(DefaultTracker == tracker)
            {
                DefaultTracker = null;
            }
        }
    }
}