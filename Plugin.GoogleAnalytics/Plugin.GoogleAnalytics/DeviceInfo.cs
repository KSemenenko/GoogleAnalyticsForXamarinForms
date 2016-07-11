using System;
using Plugin.GoogleAnalytics.Abstractions;
using Plugin.GoogleAnalytics.Abstractions.Model;
using System.IO;

namespace Plugin.GoogleAnalytics
{
    public class DeviceInfo : IDeviceInfo
    {
        public DeviceInfo()
        {
            UserAgent = string.Empty;
            Display = new Dimensions(0, 0);
        }

        public string Id { get; set; }

        public string Model { get; set; }

        public string Version { get; set; }

        public string UserAgent { get; set; }

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
                    return new Version(0, 0, 0, 0);
                }
            }
        }

        public string Manufacturer { get; set; }

        public string LanguageCode
        {
            get { return System.Globalization.CultureInfo.CurrentCulture.TwoLetterISOLanguageName; }
        }

        public Dimensions Display { get; set; }
        public Dimensions ViewPortResolution { get; set; }

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

        public string ReadFile(string path)
        {
            // TODO: Fix it
            return string.Empty;
        }

        public void WriteFile(string path, string content)
        {
            // TODO: Fix it
        }
    }
}