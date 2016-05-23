using System;
using Foundation;
using GoogleAnalytics.Core.Platform;
using GoogleAnalytics.iOS;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(DeviceInfo))]

namespace GoogleAnalytics.iOS
{
    internal class DeviceInfo : IDeviceInfo
    {

        public DeviceInfo()
        {
            //https://gist.github.com/crdeutsch/6707396
            UIWebView agentWebView = new UIWebView();
            UserAgent = agentWebView.EvaluateJavascript("navigator.userAgent");
        }

        /// <summary>
        ///     Device major version.
        /// </summary>
        public int MajorVersion { get; private set; }

        /// <summary>
        ///     Device minor version.
        /// </summary>
        public int MinorVersion { get; private set; }

        public string Id
        {
            get { return UIDevice.CurrentDevice.IdentifierForVendor.AsString(); }
        }

        public string Model
        {
            get { return UIDevice.CurrentDevice.Model; }
        }

        public string UserAgent { get; set; }

        public string Version
        {
            get { return UIDevice.CurrentDevice.SystemVersion; }
        }

        public Version VersionNumber
        {
            get
            {
                try
                {
                    return new Version(Version);
                }
                catch
                {
                    return new Version();
                }
            }
        }

        public string Manufacturer
        {
            get { return "Apple"; }
        }

        public string LanguageCode
        {
            get { return NSLocale.PreferredLanguages[0]; }
        }

        public double TimeZoneOffset
        {
            get { return NSTimeZone.LocalTimeZone.GetSecondsFromGMT / 3600.0; }
        }

        public string TimeZone
        {
            get { return NSTimeZone.LocalTimeZone.Name; }
        }

        public Platform Platform
        {
            get { return Platform.iOS; }
        }

        public Display Display { get; set; } = new Display(Convert.ToInt32(UIScreen.MainScreen.Bounds.Size.Height), Convert.ToInt32(UIScreen.MainScreen.Bounds.Size.Width));

        public string GenerateAppId(bool usingPhoneId = false, string prefix = null, string suffix = null)
        {
            var appId = "";

            if(!string.IsNullOrEmpty(prefix))
            {
                appId += prefix;
            }

            appId += Guid.NewGuid().ToString();

            if(usingPhoneId)
            {
                appId += Id;
            }

            if(!string.IsNullOrEmpty(suffix))
            {
                appId += suffix;
            }

            return appId;
        }
    }
}