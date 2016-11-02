using System;

namespace Plugin.GoogleAnalytics.Abstractions
{
    /// <summary>
    /// Interface for Plugin.GoogleAnalytics
    /// </summary>
    public interface ITrackerConfig
    {
        /// <summary>
        ///     The Google Analytics tracking ID to which to send your data. Dashes in the ID must be unencoded. You can disable
        ///     your tracking by not providing this value.
        /// </summary>
        string TrackingId { get; set; }

        /// <summary>
        ///     The name of your app, used in the app name dimension in your reports. Defaults to the value found in the package.
        /// </summary>
        string AppName { get; set; }

        /// <summary>
        ///     The version of your application, used in the app version dimension within your reports. Defaults to the version
        ///     found in the package.
        /// </summary>
        string AppVersion { get; set; }

        /// <summary>
        ///     Application identifier.
        /// </summary>
        string AppId { get; set; }

        /// <summary>
        ///     Application installer identifier.
        /// </summary>
        string AppInstallerId { get; set; }

        /// <summary>
        ///     Flag to enable or writing of debug information to the log, useful for troubleshooting your implementation. false by
        ///     default.
        /// </summary>
        bool Debug { get; set; }

        /// <summary>
        ///     The dispatch period in seconds. Defaults to 30 seconds.
        /// </summary>
        TimeSpan DispatchPeriod { get; set; }

        /// <summary>
        ///     The sample rate to use. Default is 100.0. It can be any value between 0.0 and 100.0
        /// </summary>
        float SampleFrequency { get; set; }

        /// <summary>
        ///     Tells Google Analytics to anonymize the information sent by the tracker objects by removing the last octet of the
        ///     IP address prior to its storage. Note that this will slightly reduce the accuracy of geographic reporting. false by
        ///     default.
        /// </summary>
        bool AnonymizeIp { get; set; }

        /// <summary>
        ///     Automatically track an Exception each time an uncaught exception is thrown in your application. false by default.
        /// </summary>
        bool ReportUncaughtExceptions { get; set; }

        /// <summary>
        ///     The amount of time your application can stay in the background before the session is ended. Default is 30 seconds.
        ///     Null value disables EasyTracker session management.
        /// </summary>
        TimeSpan? SessionTimeout { get; set; }

        /// <summary>
        ///     Automatically track application lifetime events (e.g. suspend and resume). true by default.
        /// </summary>
        bool AutoAppLifetimeTracking { get; set; }

        /// <summary>
        ///     Automatically monitor suspend and resume events. If true, all dispatched events will be sent on suspend and session
        ///     timeout will be honored. true by default.
        /// </summary>
        bool AutoAppLifetimeMonitoring { get; set; }

        /// <summary>
        ///     Tells Google Analytics to automatically monitor network connectivity and avoid sending logs while not connected.
        ///     Default is true.
        /// </summary>
        bool AutoTrackNetworkConnectivity { get; set; }

        /// <summary>
        ///     If true, causes all hits to be sent to the secure (SSL) Google Analytics endpoint. Default is false.
        /// </summary>
        bool UseSecure { get; set; }

        /// <summary>
        ///     Validates the configuration and throws an exception is a problem is found.
        /// </summary>
        void Validate();

        /// <summary>
        ///     Install Event Message
        /// </summary>
        string InstallMessage { get; set; }

        /// <summary>
        ///     App start event Message
        /// </summary>
        string StartMessage { get; set; }

        /// <summary>
        ///     Service Category
        /// </summary>
        string ServiceCategoryName { get; set; }
    }
}