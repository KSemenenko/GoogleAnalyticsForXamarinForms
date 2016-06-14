using System;
using Windows.Foundation;
using Windows.Graphics.Display;
using Windows.Storage;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Plugin.GoogleAnalytics.Abstractions;
using Plugin.GoogleAnalytics.Abstractions.Model;

namespace Plugin.GoogleAnalytics
{
    public class DeviceInfo : IDeviceInfo
    {
        private readonly EasClientDeviceInformation deviceInfo;

        public DeviceInfo()
        {

            
            var sysInfo = new Windows.Security.ExchangeActiveSyncProvisioning.EasClientDeviceInformation();
            UserAgent =  string.Format("Mozilla/5.0 (Windows Phone 8.1; ARM; Trident/7.0; Touch; rv11.0; IEMobile/11.0; {0}; {1}) like Gecko", 
                sysInfo.SystemManufacturer, sysInfo.SystemProductName);
            

            var bounds = Window.Current.Bounds;
            double w = bounds.Width;
            double h = bounds.Height;
            var displayInfo = DisplayInformation.GetForCurrentView();
            w = Math.Round(w * displayInfo.RawPixelsPerViewPixel);
            h = Math.Round(h * displayInfo.RawPixelsPerViewPixel);

            if ((displayInfo.CurrentOrientation & DisplayOrientations.Landscape) == DisplayOrientations.Landscape)
            {
                Display = new Dimensions((int)w, (int)h);
            }
            else // portrait
            {
                Display = new Dimensions((int)h, (int)w);
            }
            ViewPortResolution = new Dimensions((int)bounds.Width, (int)bounds.Height); // leave viewport at the scale unadjusted size
            Window.Current.SizeChanged += Current_SizeChanged;
            windowInitialized = true;




            deviceInfo = DisplayInformation.GetForCurrentView();

   
    
        }

        public string Id
        {
            get
            {
                if (ApiInformation.IsTypePresent("Windows.System.Profile.HardwareIdentification"))
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
            get { return System.Globalization.CultureInfo.CurrentUICulture.Name; }
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
    }
}