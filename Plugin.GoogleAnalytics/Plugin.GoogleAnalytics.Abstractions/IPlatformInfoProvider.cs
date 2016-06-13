using Plugin.GoogleAnalytics.Abstractions.Model;

namespace Plugin.GoogleAnalytics.Abstractions
{
    public interface IPlatformInfoProvider
    {
        string AnonymousClientId { get; set; }
        int? ScreenColorDepthBits { get; }
        Dimensions ScreenResolution { get; }
        string UserLanguage { get; }
        Dimensions ViewPortResolution { get; }
        void OnTracking();
        string UserAgent { get; }
    }
}