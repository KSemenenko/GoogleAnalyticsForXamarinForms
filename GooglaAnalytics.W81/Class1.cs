//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Xamarin.Forms;

//[assembly: Dependency(typeof(DeviceInfo))]
//namespace GooglaAnalytics.W81
//{
//    public class DeviceInfo : IDeviceInfo
//    {
//        public DeviceInfo()
//        {
            
//            var sysInfo = new Windows.Security.ExchangeActiveSyncProvisioning.EasClientDeviceInformation();
//            UserAgent = string.Format("Mozilla/5.0 (Windows Phone 8.1; ARM; Trident/7.0; Touch; rv11.0; IEMobile/11.0; {0}; {1}) like Gecko", sysInfo.SystemManufacturer, sysInfo.SystemProductName);

            
//             var displayInfo = DisplayInformation.GetForCurrentView();
             
//           var w = Math.Round(w * displayInfo.RawPixelsPerViewPixel);
//           var h = Math.Round(h * displayInfo.RawPixelsPerViewPixel);
//           Display = new Display(w,h);
            
//        }

//        /// <summary>
//        ///     Device major version.
//        /// </summary>
//        public int MajorVersion { get; private set; }

//        /// <summary>
//        ///     Device minor version.
//        /// </summary>
//        public int MinorVersion { get; private set; }

//        public string Id
//        {
//            get { return Build.Serial; }
//        }

//        public string Model
//        {
//            get { return Build.Model; }
//        }

//        public string Version
//        {
//            get { return Build.VERSION.Release; }
//        }

//        public string UserAgent { get; set; }

//        public Version VersionNumber
//        {
//            get
//            {
//                try
//                {
//                    return new Version(Version);
//                }
//                catch
//                {
//                    return new Version();
//                }
//            }
//        }

//        public string Manufacturer
//        {
//            get { return Build.Manufacturer; }
//        }

//        public string LanguageCode
//        {
//            get { return System.Globalization.CultureInfo.CurrentUICulture.Name; }
//        }

//        public double TimeZoneOffset
//        {
//            get
//            {
//                using(var calendar = new GregorianCalendar())
//                {
//                    return TimeUnit.Hours.Convert(calendar.TimeZone.RawOffset, TimeUnit.Milliseconds) / 3600;
//                }
//            }
//        }

//        public string TimeZone
//        {
//            get { return Java.Util.TimeZone.Default.ID; }
//        }

//        public Platform Platform
//        {
//            get { return Platform.Android; }
//        }

//        public GoogleAnalytics.Core.Platform.Display Display { get; set; }

//        public string GenerateAppId(bool usingPhoneId = false, string prefix = null, string suffix = null)
//        {
//            var appId = "";

//            if(!string.IsNullOrEmpty(prefix))
//            {
//                appId += prefix;
//            }

//            appId += Guid.NewGuid().ToString();

//            if(usingPhoneId)
//            {
//                appId += Id;
//            }

//            if(!string.IsNullOrEmpty(suffix))
//            {
//                appId += suffix;
//            }

//            return appId;
//        }
//    }
//}
