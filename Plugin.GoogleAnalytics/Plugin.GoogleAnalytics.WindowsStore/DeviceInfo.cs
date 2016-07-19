using System;
using System.Globalization;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.Security.ExchangeActiveSyncProvisioning;
using Windows.System.Profile;
using Windows.UI.Xaml;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;
using Plugin.GoogleAnalytics.Abstractions;
using Plugin.GoogleAnalytics.Abstractions.Model;

namespace Plugin.GoogleAnalytics
{
    public class DeviceInfo : IDeviceInfo
    {
        private const string GoogleAnalyticsFolder = "ga-store";
        private readonly EasClientDeviceInformation deviceInfo;

        public DeviceInfo()
        {
            deviceInfo = new EasClientDeviceInformation();
            UserAgent = $"Mozilla/5.0 ({deviceInfo.OperatingSystem} ARM; Trident/7.0; Touch; rv11.0; IEMobile/11.0; {deviceInfo.SystemManufacturer}; {deviceInfo.SystemProductName}) like Gecko";

            var bounds = Window.Current.Bounds;
            var w = bounds.Width;
            var h = bounds.Height;

            Display = new Dimensions((int)w, (int)h);

            ViewPortResolution = new Dimensions((int)w, (int)h);
        }

        public string Model
        {
            get { return deviceInfo.SystemProductName; }
        }

        public string Id
        {
            get
            {
                try
                {
                    var myToken = HardwareIdentification.GetPackageSpecificToken(null);
                    var hardwareId = myToken.Id;
                    return Convert.ToBase64String(hardwareId.ToArray());
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
                var version = new Version(Package.Current.Id.Version.Major,
                    Package.Current.Id.Version.Minor,
                    Package.Current.Id.Version.Revision,
                    Package.Current.Id.Version.Build);
                return version.ToString();
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
            var result = ReadFileAsync(path).Result;
            return result;
        }

        public void WriteFile(string path, string content)
        {
            WriteFileAsync(path, content).RunSynchronously();
        }

        public async Task<string> ReadFileAsync(string path)
        {
            try
            {
                StorageFolder folder = ApplicationData.Current.LocalFolder;
                StorageFile sampleFile = await folder.GetFileAsync(path);
                return await FileIO.ReadTextAsync(sampleFile); 
            }
            catch
            {
                return string.Empty;
            }
        }

        public async Task WriteFileAsync(string path, string content)
        {
            StorageFolder folder = ApplicationData.Current.LocalFolder;
            StorageFile sampleFile = await folder.CreateFileAsync(path, CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(sampleFile, content);
        }
    }
}