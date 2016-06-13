using System;
using Foundation;
using Plugin.GoogleAnalytics.Abstractions;
using Plugin.GoogleAnalytics.Abstractions.Model;
using UIKit;

namespace Plugin.GoogleAnalytics
{
    public class DeviceInfo : IDeviceInfo
    {
        public DeviceInfo()
        {
            UIWebView agentWebView = new UIWebView();
            UserAgent = agentWebView.EvaluateJavascript("navigator.userAgent");
            Display = new Dimensions(Convert.ToInt32(UIScreen.MainScreen.Bounds.Size.Height), Convert.ToInt32(UIScreen.MainScreen.Bounds.Size.Width));
        }

        public string Id
        {
            get { return UIDevice.CurrentDevice.IdentifierForVendor.AsString(); }
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

        public string LanguageCode
        {
            get { return NSLocale.PreferredLanguages[0]; }
        }

        public Dimensions Display { get; set; }

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