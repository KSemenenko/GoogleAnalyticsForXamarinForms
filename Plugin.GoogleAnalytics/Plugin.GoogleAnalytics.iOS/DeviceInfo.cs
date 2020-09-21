using System;
using Foundation;
using Plugin.GoogleAnalytics.Abstractions;
using Plugin.GoogleAnalytics.Abstractions.Model;
using UIKit;
using System.IO;
using System.Runtime.InteropServices;

namespace Plugin.GoogleAnalytics
{
    [Preserve(AllMembers = true)]
    public class DeviceInfo : IDeviceInfo
    {
        private readonly string GoogleAnalyticsFolder = "ga-store";

        public DeviceInfo()
        {
            UserAgent = GetUserAgent();
            Display = new Dimensions(Convert.ToInt32(UIScreen.MainScreen.Bounds.Size.Height), Convert.ToInt32(UIScreen.MainScreen.Bounds.Size.Width));

            GoogleAnalyticsFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), GoogleAnalyticsFolder);
        }

        public string Id
        {
            get { return UIDevice.CurrentDevice.IdentifierForVendor.AsString(); }
        }

        public string UserAgent { get; set; }

        public string Version
        {
            get { return NSBundle.MainBundle.InfoDictionary["CFBundleVersion"].ToString(); }
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

        public string LanguageCode
        {
            get { return NSLocale.PreferredLanguages[0]; }
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

        private string GetUserAgent()
        {
	        var dict = NSBundle.MainBundle.InfoDictionary;
	        var appName = dict["CFBundleDisplayName"]?.ToString();
	        var appVersion = dict["CFBundleShortVersionString"]?.ToString();

	        var osName = UIDevice.CurrentDevice.SystemName;
	        var osVersion = UIDevice.CurrentDevice.SystemVersion;

	        var networkHandler = "CFNetwork";
	        var networkHandlerVersion = NSBundle.FromIdentifier("com.apple.CFNetwork")?.InfoDictionary["CFBundleShortVersionString"]?.ToString();

	        var darwinName = "Darwin";
	        var darwinVersion = GetSysPropertyInfo("kern.osrelease");
	        var deviceModel = GetSysPropertyInfo("hw.machine");

	        // <AppName/<version> <iDevice platform><Apple model identifier>  iOS/<OS version> CFNetwork/<version> Darwin/<version>
	        var ua = $"{appName}/{appVersion} {deviceModel} {osName}/{osVersion} {networkHandler}/{networkHandlerVersion} {darwinName}/{darwinVersion}";

	        return ua;
        }

        [DllImport("libc", CallingConvention = CallingConvention.Cdecl)]
        private static extern int sysctlbyname([MarshalAs(UnmanagedType.LPStr)] string property, IntPtr output, IntPtr oldLen, IntPtr newp, uint newlen);

        private static string GetSysPropertyInfo(string hardwareProperty)
        {
	        try
	        {
		        var pLen = Marshal.AllocHGlobal(sizeof(int));

		        sysctlbyname(hardwareProperty, IntPtr.Zero, pLen, IntPtr.Zero, 0);

		        var length = Marshal.ReadInt32(pLen);

		        var pStr = Marshal.AllocHGlobal(length);
		        sysctlbyname(hardwareProperty, pStr, pLen, IntPtr.Zero, 0);

		        var hardwareStr = Marshal.PtrToStringAnsi(pStr);
		        return hardwareStr;
	        }
	        catch (Exception e)
	        {
                System.Diagnostics.Debug.WriteLine("GetSysPropertyInfo failed: " + e.Message);
		        return "?";
	        }
        }
    }
}