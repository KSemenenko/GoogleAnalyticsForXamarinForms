using System;
using Android.OS;
using Java.Util;
using Java.Util.Concurrent;
using Plugin.GoogleAnalytics.Abstractions;
using Plugin.GoogleAnalytics.Abstractions.Model;

namespace Plugin.GoogleAnalytics
{
    public class DeviceInfo : IDeviceInfo
    {
        public DeviceInfo()
        {
            UserAgent = Java.Lang.JavaSystem.GetProperty("http.agent");
            Display = new Dimensions(Android.App.Application.Context.Resources.DisplayMetrics.HeightPixels,
                Android.App.Application.Context.Resources.DisplayMetrics.WidthPixels);
        }

        public string Id
        {
            get { return Build.Serial; }
        }

        public string Version
        {
            get { return Build.VERSION.Release; }
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