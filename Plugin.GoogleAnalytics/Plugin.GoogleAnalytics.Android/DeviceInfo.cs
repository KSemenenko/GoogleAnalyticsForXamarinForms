using System;
using Android.OS;
using Java.Util;
using Java.Util.Concurrent;
using Plugin.GoogleAnalytics.Abstractions;
using Plugin.GoogleAnalytics.Abstractions.Model;
using System.IO;

namespace Plugin.GoogleAnalytics
{
    public class DeviceInfo : IDeviceInfo
    {
        private readonly string GoogleAnalyticsFolder = "ga-store";

        public DeviceInfo()
        {
            UserAgent = Java.Lang.JavaSystem.GetProperty("http.agent");
            Display = new Dimensions(Android.App.Application.Context.Resources.DisplayMetrics.HeightPixels,
                Android.App.Application.Context.Resources.DisplayMetrics.WidthPixels);

            GoogleAnalyticsFolder = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), GoogleAnalyticsFolder);
        }

        public string Id
        {
            get { return Build.Serial; }
        }

        public string Version
        {
            get { return Android.App.Application.Context.PackageManager.GetPackageInfo(Android.App.Application.Context.PackageName, 0).VersionName; }
        }

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
                    return new Version();
                }
            }
        }

        public string LanguageCode
        {
            get { return Locale.Default.Language; }
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
            if(!File.Exists(Path.Combine(GoogleAnalyticsFolder, path)))
            {
                return string.Empty;
            }

            return File.ReadAllText(Path.Combine(GoogleAnalyticsFolder, path));
        }

        public void WriteFile(string path, string content)
        {
            if(!Directory.Exists(GoogleAnalyticsFolder))
            {
                Directory.CreateDirectory(GoogleAnalyticsFolder);
            }
            File.WriteAllText(Path.Combine(GoogleAnalyticsFolder, path), content);
        }
    }
}