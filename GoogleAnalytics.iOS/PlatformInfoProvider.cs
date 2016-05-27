using GoogleAnalytics.iOS;

namespace GoogleAnalytics.Core
{
    public sealed partial class PlatformInfoProvider
    {
        public PlatformInfoProvider()
        {
            var device = new DeviceInfo();
            ScreenResolution = new Dimensions(device.Display.Width, device.Display.Height);
            UserLanguage = device.LanguageCode;
            UserAgent = device.UserAgent;
        }
    }
}
