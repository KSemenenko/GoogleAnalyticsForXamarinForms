using System;
using Plugin.GoogleAnalytics.Abstractions;
using Plugin.GoogleAnalytics.Abstractions.Model;

namespace Plugin.GoogleAnalytics
{
    public sealed partial class PlatformInfoProvider : IPlatformInfoProvider
    {
        public PlatformInfoProvider() : this(new DeviceInfo())
        {

        }
    }
}