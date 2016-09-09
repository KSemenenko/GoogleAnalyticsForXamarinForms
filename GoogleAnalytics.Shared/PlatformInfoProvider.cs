using System;
using Plugin.GoogleAnalytics.Abstractions;
using Plugin.GoogleAnalytics.Abstractions.Model;

namespace Plugin.GoogleAnalytics
{
    public sealed partial class PlatformInfoProvider : IPlatformInfoProvider
    {
        private const string AnonymousIdFileName = "ga-anonymous-id.guid";

        private void GetAnonymousClientId(IDeviceInfo deviceInfo)
        {
            var id = deviceInfo.ReadFile(AnonymousIdFileName);
            if(string.IsNullOrEmpty(id))
            {
                id = Guid.NewGuid().ToString("D");
                deviceInfo.WriteFile(AnonymousIdFileName, id);
            }

            AnonymousClientId = id;
        }

        public string UserAgent { get; set; }

        public Version Version { get; set; }
        public string AnonymousClientId { get; set; }

        public void OnTracking()
        {
#if DEBUG
            System.Diagnostics.Debug.WriteLine("GoogleAnalytics: Track.");
#endif
        }

        public int? ScreenColorDepthBits { get; set; }
        public string UserLanguage { get; set; }
        public Dimensions ScreenResolution { get; set; }
        public Dimensions ViewPortResolution { get; set; }
    }
}