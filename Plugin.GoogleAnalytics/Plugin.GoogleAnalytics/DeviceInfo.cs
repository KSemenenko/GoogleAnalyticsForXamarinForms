using System;
using Plugin.GoogleAnalytics.Abstractions;

namespace Plugin.GoogleAnalytics
{
    public class DeviceInfo : IDeviceInfo
    {
        public DeviceInfo()
        {
            UserAgent = string.Empty;
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
            get { return Build.Serial; }
        }

        public string Model
        {
            get { return Build.Model; }
        }

        public string Version
        {
            get
            {
                 
            }
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
                    return new Version(0,0,0,0);
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

        public Dimensions Display { get; set; } =
            new Dimensions(Android.App.Application.Context.Resources.DisplayMetrics.HeightPixels,
                Android.App.Application.Context.Resources.DisplayMetrics.WidthPixels);

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