using System;
using System.Globalization;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using Windows.Storage;
using Microsoft.Phone.Info;
using Plugin.GoogleAnalytics.Abstractions;
using Plugin.GoogleAnalytics.Abstractions.Model;

namespace Plugin.GoogleAnalytics
{
    public class DeviceInfo : IDeviceInfo
    {
        private const string GoogleAnalyticsFolder = "ga-store";

        public DeviceInfo()
        {
            UserAgent =
                $"Mozilla/5.0 (Windows Phone {Environment.OSVersion} ARM; Trident/7.0; Touch; rv11.0; IEMobile/11.0; {DeviceStatus.DeviceManufacturer}; {DeviceStatus.DeviceName}) like Gecko";

            var w = Application.Current.Host.Content.ActualWidth;
            var h = Application.Current.Host.Content.ActualHeight;

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
                catch(Exception)
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
            var result = ReadFileAsync(path).Result;
            return result;
        }

        public void WriteFile(string path, string content)
        {
            WriteFileAsync(path, content).RunSynchronously();
        }

        public async Task<string> ReadFileAsync(string path)
        {
            // Get the local folder.
            StorageFolder local = Windows.Storage.ApplicationData.Current.LocalFolder;

            if(local != null)
            {
                // Get the DataFolder folder.
                var dataFolder = await local.GetFolderAsync(GoogleAnalyticsFolder);

                // Get the file.
                var file = await dataFolder.OpenStreamForReadAsync(path);

                // Read the data.
                using(StreamReader streamReader = new StreamReader(file))
                {
                    return streamReader.ReadToEnd();
                }
            }

            return string.Empty;
        }

        public async Task WriteFileAsync(string path, string content)
        {
            // Get the text data from the textbox. 
            byte[] fileBytes = System.Text.Encoding.UTF8.GetBytes(content.ToCharArray());

            // Get the local folder.
            StorageFolder local = Windows.Storage.ApplicationData.Current.LocalFolder;

            // Create a new folder name DataFolder.
            var dataFolder = await local.CreateFolderAsync(GoogleAnalyticsFolder, CreationCollisionOption.OpenIfExists);

            // Create a new file named DataFile.txt.
            var file = await dataFolder.CreateFileAsync(path, CreationCollisionOption.ReplaceExisting);

            // Write the data from the textbox.
            using(var s = await file.OpenStreamForWriteAsync())
            {
                s.Write(fileBytes, 0, fileBytes.Length);
            }
        }
    }
}