using Plugin.GoogleAnalytics.Abstractions;

namespace Plugin.GoogleAnalytics
{
    public sealed partial class PlatformInfoProvider : IPlatformInfoProvider
    {
        public PlatformInfoProvider() : this(new DeviceInfo())
        {

        }
    }
}