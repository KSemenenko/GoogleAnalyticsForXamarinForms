using System;

namespace GoogleAnalytics.Core.Platform
{
    public interface IDeviceInfo
    {
        /// <summary>
        ///     This is the device specific Id (remember the correct permissions in your app to use this)
        /// </summary>
        string Id { get; }

        /// <summary>
        ///     Get the model of the device
        /// </summary>
        string Model { get; }

        /// <summary>
        ///     Get the version of the Operating System
        /// </summary>
        string Version { get; }

        /// <summary>
        ///     Get the UserAgent string
        /// </summary>
        string UserAgent { get; }

        /// <summary>
        ///     Gets the version number of the operating system
        /// </summary>
        Version VersionNumber { get; }

        /// <summary>
        ///     Gets the display information for the device.
        /// </summary>
        IDisplay Display { get; }

        /// <summary>
        ///     Get the platform of the device
        /// </summary>
        Platform Platform { get; }

        /// <summary>
        ///     Gets the manufacturer.
        /// </summary>
        string Manufacturer { get; }

        /// <summary>
        ///     Gets the ISO Language Code
        /// </summary>
        string LanguageCode { get; }

        /// <summary>
        ///     Gets the UTC offset
        /// </summary>
        double TimeZoneOffset { get; }

        /// <summary>
        ///     Gets the timezone name
        /// </summary>
        string TimeZone { get; }

        /// <summary>
        ///     Generates a an AppId optionally using the PhoneId a prefix and a suffix and a Guid to ensure uniqueness
        ///     The AppId format is as follows {prefix}guid{phoneid}{suffix}, where parts in {} are optional.
        /// </summary>
        /// <param name="usingPhoneId">
        ///     Setting this to true adds the device specific id to the AppId (remember to give the app the
        ///     correct permissions)
        /// </param>
        /// <param name="prefix">Sets the prefix of the AppId</param>
        /// <param name="suffix">Sets the suffix of the AppId</param>
        /// <returns></returns>
        string GenerateAppId(bool usingPhoneId = false, string prefix = null, string suffix = null);
    }

    public enum Platform
    {
        Android,
        iOS,
        WindowsPhone,
        Windows
    }
}