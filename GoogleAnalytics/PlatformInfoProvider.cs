using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoogleAnalytics.Core.Platform;
using Xamarin.Forms;

namespace GoogleAnalytics.Core
{
    public sealed partial class PlatformInfoProvider 
    {
        public PlatformInfoProvider()
        {
            var device = DependencyService.Get<IDeviceInfo>();
            ScreenResolution = new Dimensions(device.Display.Width, device.Display.Height);
            UserLanguage = device.LanguageCode;
        }
    }
}
