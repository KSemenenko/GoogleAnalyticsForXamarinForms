using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using Foundation;
using GoogleAnalytics.Core.Platform;
using GoogleAnalytics.iOS;
using ObjCRuntime;
using UIKit;
using Xamarin.Forms;
using Platform = GoogleAnalytics.Core.Platform.Platform;

[assembly: Dependency(typeof(DeviceInfo))]

namespace GoogleAnalytics.iOS
{
    internal class DeviceInfo : IDeviceInfo
    {
        /// <summary>
        ///     The iPhone expression.
        /// </summary>
        protected const string PhoneExpression = "iPhone([1-8]),([1-4])";

        /// <summary>
        ///     The iPod expression.
        /// </summary>
        protected const string PodExpression = "iPod([1-5]),([1])";

        /// <summary>
        ///     The iPad expression.
        /// </summary>
        protected const string PadExpression = "iPad([1-4]),([1-8])";

        /// <summary>
        ///     Device major version.
        /// </summary>
        public int MajorVersion { get; private set; }

        /// <summary>
        ///     Device minor version.
        /// </summary>
        public int MinorVersion { get; private set; }

        /// <summary>
        ///     Gets the type of device.
        /// </summary>
        public DeviceType DeviceType { get; private set; }

        public string Id
        {
            get { return UIDevice.CurrentDevice.IdentifierForVendor.AsString(); }
        }

        public string Model
        {
            get { return UIDevice.CurrentDevice.Model; }
        }

        public string Version
        {
            get { return UIDevice.CurrentDevice.SystemVersion; }
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
            get { return "Apple"; }
        }

        public string LanguageCode
        {
            get { return NSLocale.PreferredLanguages[0]; }
        }

        public double TimeZoneOffset
        {
            get { return NSTimeZone.LocalTimeZone.GetSecondsFromGMT / 3600.0; }
        }

        public string TimeZone
        {
            get { return NSTimeZone.LocalTimeZone.Name; }
        }

        public Platform Platform
        {
            get { return Platform.iOS; }
        }

        public IDisplay Display { get; set; }

        /// <summary>
        ///     Sysctlbynames the specified property.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <param name="output">The output.</param>
        /// <param name="oldLen">The old length.</param>
        /// <param name="newp">The newp.</param>
        /// <param name="newlen">The newlen.</param>
        /// <returns>System.Int32.</returns>
        [DllImport(Constants.SystemLibrary)]
        internal static extern int sysctlbyname(
            [MarshalAs(UnmanagedType.LPStr)] string property,
            IntPtr output,
            IntPtr oldLen,
            IntPtr newp,
            uint newlen);

        [DllImport(Constants.SystemLibrary)]
        internal static extern int sysctl(
            [MarshalAs(UnmanagedType.LPArray)] int[] name,
            uint namelen,
            out uint oldp,
            ref int oldlenp,
            IntPtr newp,
            uint newlen);

        public void GetDeviceInfo()
        {
            var hardwareVersion = GetSystemProperty("hw.machine");

            var regex = new Regex(PhoneExpression).Match(hardwareVersion);
            if(regex.Success)
            {
                DeviceType = DeviceType.Phone;
                MajorVersion = int.Parse(regex.Groups[1].Value);
                MinorVersion = int.Parse(regex.Groups[2].Value);
            }

            regex = new Regex(PodExpression).Match(hardwareVersion);
            if(regex.Success)
            {
                DeviceType = DeviceType.Pod;
                MajorVersion = int.Parse(regex.Groups[1].Value);
                MinorVersion = int.Parse(regex.Groups[2].Value);
            }

            regex = new Regex(PadExpression).Match(hardwareVersion);
            if(regex.Success)
            {
                DeviceType = DeviceType.Pad;
                MajorVersion = int.Parse(regex.Groups[1].Value);
                MinorVersion = int.Parse(regex.Groups[2].Value);
            }

            DeviceType = DeviceType.Simulator;
            MajorVersion = 0;
            MinorVersion = 0;
        }

        public static string GetSystemProperty(string property)
        {
            var pLen = Marshal.AllocHGlobal(sizeof(int));
            sysctlbyname(property, IntPtr.Zero, pLen, IntPtr.Zero, 0);
            var length = Marshal.ReadInt32(pLen);
            var pStr = Marshal.AllocHGlobal(length);
            sysctlbyname(property, pStr, pLen, IntPtr.Zero, 0);
            return Marshal.PtrToStringAnsi(pStr);
        }

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

    internal enum DeviceType
    {
        /// <summary>
        ///     Device is an iPhone.
        /// </summary>
        Phone,

        /// <summary>
        ///     Device is an iPad.
        /// </summary>
        Pad,

        /// <summary>
        ///     Device is an iPod.
        /// </summary>
        Pod,

        /// <summary>
        ///     Device is a simulator.
        /// </summary>
        Simulator
    }

    internal static class EnumExtensions
    {
        /// <summary>
        ///     Gets the description.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>System.String.</returns>
        public static string GetDescription(this Enum value)
        {
            var field = value.GetType().GetField(value.ToString());

            var attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;

            return attribute == null ? value.ToString() : attribute.Description;
        }
    }
}