using System;
using Plugin.GoogleAnalytics.Abstractions;
using Plugin.GoogleAnalytics.Abstractions.Model;

namespace Plugin.GoogleAnalytics
{
    public sealed partial class PlatformInfoProvider : IPlatformInfoProvider
    {
        private const string AnonymousIdFileName = "ga-anonymous-id.guid";
        private static bool isInstall;
        private string anonymousClientId;

        private readonly IDeviceInfo device;
        public PlatformInfoProvider(IDeviceInfo deviceInfo)
        {
            device = deviceInfo;
            ScreenResolution = device.Display;
            UserLanguage = device.LanguageCode;
            UserAgent = device.UserAgent;
            ViewPortResolution = device.ViewPortResolution;
            Version = device.VersionNumber;
            GetAnonymousClientId();
        }
        private void GetAnonymousClientId()
        {
            var id = device.ReadFile(AnonymousIdFileName);
            if(string.IsNullOrEmpty(id))
            {
                id = Guid.NewGuid().ToString("D");
                device.WriteFile(AnonymousIdFileName, id);
                IsInstall = true;
            }

            AnonymousClientId = id;
        }

        public void UpdateAnonymousClientId(string newId)
        {
            device.WriteFile(AnonymousIdFileName, newId);
        }

        public string UserAgent { get; set; }

        public Version Version { get; set; }

        public string AnonymousClientId
        {
            get { return anonymousClientId; }
            set
            {
                if(anonymousClientId != value && anonymousClientId != null)
                {
                    UpdateAnonymousClientId(value);
                }
                anonymousClientId = value;
            }
        }

        public void OnTracking()
        {
#if DEBUG
            System.Diagnostics.Debug.WriteLine("GoogleAnalytics: Track.");
#endif
        }

        public int? ScreenColorDepthBits { get; set; }
        public string UserLanguage { get; set; }
        public Dimensions ScreenResolution { get; set; }
        public Dimensions ViewPortResolution { get; set; }

        public bool IsInstall
        {
            get { return isInstall; }
            set { isInstall = value; }
        }
    }
}