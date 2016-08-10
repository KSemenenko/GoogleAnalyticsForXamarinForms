using System;
using Foundation;
using Plugin.GoogleAnalytics.Abstractions;
using Plugin.GoogleAnalytics.Abstractions.Model;
using UIKit;
using System.IO;

namespace Plugin.GoogleAnalytics
{
    public class DeviceInfo : IDeviceInfo
    {
        private readonly string GoogleAnalyticsFolder = "ga-store";

        public DeviceInfo()
        {
            UIWebView agentWebView = new UIWebView();
            UserAgent = agentWebView.EvaluateJavascript("navigator.userAgent");
            Display = new Dimensions(Convert.ToInt32(UIScreen.MainScreen.Bounds.Size.Height), Convert.ToInt32(UIScreen.MainScreen.Bounds.Size.Width));

            GoogleAnalyticsFolder = Path.Combine(Environment.CurrentDirectory, GoogleAnalyticsFolder);
        }

        public string Id
        {
            get { return UIDevice.CurrentDevice.IdentifierForVendor.AsString(); }
        }

        public string UserAgent { get; set; }

        public string Version
        {
            get { return NSBundle.MainBundle.InfoDictionary["CFBundleVersion"].ToString(); }
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
        public Dimensions ViewPortResolution { get; set; }

        public string GenerateAppId(bool usingPhoneId = false, string prefix = null, string suffix = null)
        {
            var appId = "";

            if (!string.IsNullOrEmpty(prefix))
            {
                appId += prefix;
            }

            appId += Guid.NewGuid().ToString();

            if (usingPhoneId)
            {
                appId += Id;
            }

            if (!string.IsNullOrEmpty(suffix))
            {
                appId += suffix;
            }

            return appId;
        }

        public string ReadFile(string path)
        {
            if (!File.Exists(Path.Combine(GoogleAnalyticsFolder, path)))
            {
                return string.Empty;
            }

            return File.ReadAllText(Path.Combine(GoogleAnalyticsFolder, path));
        }

        public void WriteFile(string path, string content)
        {
            if (!Directory.Exists(GoogleAnalyticsFolder))
            {
                Directory.CreateDirectory(GoogleAnalyticsFolder);
            }
            File.WriteAllText(Path.Combine(GoogleAnalyticsFolder, path), content);
        }
    }
}