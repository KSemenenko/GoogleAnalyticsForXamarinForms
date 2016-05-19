namespace GoogleAnalytics.Core
{
    public sealed partial class PlatformInfoProvider : IPlatformInfoProvider
    {
        public string UserAgent { get; set; }
        public string AnonymousClientId { get; set; } = "GoogleAnaltyics.AnonymousClientId";

        public void OnTracking()
        {
        }

        public int? ScreenColorDepthBits { get; set; }
        public string UserLanguage { get; set; }
        public Dimensions ScreenResolution { get; set; }
        public Dimensions ViewPortResolution { get; set; }
    }
}