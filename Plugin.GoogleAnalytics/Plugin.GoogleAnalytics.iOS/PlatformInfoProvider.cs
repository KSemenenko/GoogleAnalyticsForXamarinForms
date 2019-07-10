using Foundation;
using Plugin.GoogleAnalytics.Abstractions;
using Plugin.GoogleAnalytics.Abstractions.Model;

namespace Plugin.GoogleAnalytics
{
    [Preserve(AllMembers = true)]
    public sealed partial class PlatformInfoProvider : IPlatformInfoProvider
    {
        public PlatformInfoProvider() : this(new DeviceInfo())
        {

        }
    }
}