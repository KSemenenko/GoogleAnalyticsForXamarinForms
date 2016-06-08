using System;

namespace GoogleAnalytics.Core
{
    public sealed partial class PlatformInfoProvider : IPlatformInfoProvider
    {
        public string UserAgent { get; set; }
        public string AnonymousClientId { get; set; } = Guid.NewGuid().ToString("D");

        public void OnTracking()
        {
        }

        public int? ScreenColorDepthBits { get; set; }
        public string UserLanguage { get; set; }
        public Dimensions ScreenResolution { get; set; }
        public Dimensions ViewPortResolution { get; set; }
    }
}