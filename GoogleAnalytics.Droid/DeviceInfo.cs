using System;
using Android.OS;
using GoogleAnalytics.Core.Platform;
using GoogleAnalytics.Droid;
using Java.Util;
using Java.Util.Concurrent;
using Xamarin.Forms;

[assembly: Dependency(typeof(DeviceInfo))]

namespace GoogleAnalytics.Droid
{
    internal class DeviceInfo : IDeviceInfo
    {
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
            get { return Build.Serial; }
        }

        public string Model
        {
            get { return Build.Model; }
        }

        public string Version
        {
            get { return Build.VERSION.Release; }
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
            get { return Build.Manufacturer; }
        }

        public string LanguageCode
        {
            get { return Locale.Default.Language; }
        }

        public double TimeZoneOffset
        {
            get
            {
                using(var calendar = new GregorianCalendar())
                {
                    return TimeUnit.Hours.Convert(calendar.TimeZone.RawOffset, TimeUnit.Milliseconds) / 3600;
                }
            }
        }

        public string TimeZone
        {
            get { return Java.Util.TimeZone.Default.ID; }
        }

        public Platform Platform
        {
            get { return Platform.Android; }
        }

        public IDisplay Display { get; set; }

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