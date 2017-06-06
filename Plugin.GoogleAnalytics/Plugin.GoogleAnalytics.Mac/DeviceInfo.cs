using System;
using System.Globalization;
using Plugin.GoogleAnalytics.Abstractions;
using Plugin.GoogleAnalytics.Abstractions.Model;
using System.IO;
using AppKit;
using WebKit;

namespace Plugin.GoogleAnalytics
{
    public class DeviceInfo : IDeviceInfo
    {
        private const string GoogleAnalyticsFolder = "ga-store";

        // private readonly EasClientDeviceInformation deviceInfo;

        public DeviceInfo()
        {
            WebView agentWebView = new WebView();
            UserAgent = agentWebView.CustomUserAgent;

            var w = NSScreen.MainScreen?.Frame.Width ?? 0;
            var h = NSScreen.MainScreen?.Frame.Height ?? 0;

            Display = new Dimensions((int)w, (int)h);
            ViewPortResolution = new Dimensions((int)w, (int)h);
        }

        public string Model
        {
            get { return ""; } // return deviceInfo.SystemProductName; }
        }

        public string Id
        {
            get
            {
                return "unsupported";
                try
                {
                    //var myToken = HardwareIdentification.GetPackageSpecificToken(null);
                    //var hardwareId = myToken.Id;
                    //return Convert.ToBase64String(hardwareId.ToArray());
                }
                catch (Exception)
                {
                    //throw new UnauthorizedAccessException( 
                    //"Application has no access to device identity. To enable access consider enabling ID_CAP_IDENTITY_DEVICE on app manifest."); 
                    return "unsupported";
                }
            }
        }

        public string UserAgent { get; set; }

        public string Version
        {
            get
            {
                return "0.0.0.0";
            }
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
                    return new Version(0, 0);
                }
            }
        }

        public string LanguageCode
        {
            get { return CultureInfo.CurrentCulture.TwoLetterISOLanguageName; }
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