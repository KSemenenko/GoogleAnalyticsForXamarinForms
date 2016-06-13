using System;
using System.Globalization;
using Windows.Foundation;
using Windows.Foundation.Metadata;
using Windows.Graphics.Display;
using Windows.Security.ExchangeActiveSyncProvisioning;
using Windows.Storage.Streams;
using Windows.System.Profile;
using Windows.UI.ViewManagement;
using Plugin.GoogleAnalytics.Abstractions;
using Plugin.GoogleAnalytics.Abstractions.Model;

namespace Plugin.GoogleAnalytics
{
    public class DeviceInfo : IDeviceInfo
    {
        private readonly EasClientDeviceInformation deviceInfo;

        public DeviceInfo()
        {
            deviceInfo = new EasClientDeviceInformation();

            var bounds = ApplicationView.GetForCurrentView().VisibleBounds;
            var scaleFactor = DisplayInformation.GetForCurrentView().RawPixelsPerViewPixel;
            var size = new Size(bounds.Width * scaleFactor, bounds.Height * scaleFactor);
            Display = new Dimensions(Convert.ToInt32(size.Height), Convert.ToInt32(size.Width));

            UserAgent = string.Format("Mozilla/5.0 ({0}; ARM; Trident/7.0; Touch; rv11.0; IEMobile/11.0; {1}; {2}) like Gecko",
                deviceInfo.OperatingSystem, deviceInfo.SystemManufacturer, deviceInfo.SystemProductName);
        }

        public string Id
        {
            get
            {
                if(ApiInformation.IsTypePresent("Windows.System.Profile.HardwareIdentification"))
                {
                    var token = HardwareIdentification.GetPackageSpecificToken(null);
                    var hardwareId = token.Id;
                    var dataReader = DataReader.FromBuffer(hardwareId);

                    var bytes = new byte[hardwareId.Length];
                    dataReader.ReadBytes(bytes);

                    return Convert.ToBase64String(bytes);
                }
                return "unsupported";
            }
        }

        public string Model
        {
            get { return deviceInfo.SystemProductName; }
        }

        public string UserAgent { get; set; }

        public string Version
        {
            get { return AnalyticsInfo.VersionInfo.DeviceFamilyVersion; }
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
            get { return CultureInfo.CurrentUICulture.TwoLetterISOLanguageName; }
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