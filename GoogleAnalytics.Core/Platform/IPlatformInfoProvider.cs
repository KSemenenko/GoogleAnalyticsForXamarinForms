using System;

namespace GoogleAnalytics.Core
{
    public interface IPlatformInfoProvider
    {
        string AnonymousClientId { get; set; }
        int? ScreenColorDepthBits { get; }
        Dimensions ScreenResolution { get; }
        string UserLanguage { get; }
        Dimensions ViewPortResolution { get; }
        void OnTracking();
        string GetUserAgent();
        event EventHandler ViewPortResolutionChanged;
        event EventHandler ScreenResolutionChanged;
    }
}