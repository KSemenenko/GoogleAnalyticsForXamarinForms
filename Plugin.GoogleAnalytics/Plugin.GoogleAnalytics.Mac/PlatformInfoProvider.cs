using Plugin.GoogleAnalytics.Abstractions;
using Plugin.GoogleAnalytics.Abstractions.Model;

namespace Plugin.GoogleAnalytics
{
    public sealed partial class PlatformInfoProvider : IPlatformInfoProvider
    {
        public PlatformInfoProvider()
        {
            var device = new DeviceInfo();
            ScreenResolution = device.Display;
            UserLanguage = device.LanguageCode;
            UserAgent = device.UserAgent;
            ViewPortResolution = device.ViewPortResolution;
            Version = device.VersionNumber;
			GetAnonymousClientId(device);
        }
    }
}