using System;
using System.Xml;

namespace GoogleAnalytics.Core
{
    public sealed class EasyTrackerConfig
    {
        public EasyTrackerConfig()
        {
            SessionTimeout = TimeSpan.FromSeconds(30);
            DispatchPeriod = TimeSpan.Zero;
            SampleFrequency = 100.0F;
            AutoAppLifetimeMonitoring = true;
            AutoTrackNetworkConnectivity = true;
        }

        /// <summary>
        ///     The Google Analytics tracking ID to which to send your data. Dashes in the ID must be unencoded. You can disable
        ///     your tracking by not providing this value.
        /// </summary>
        public string TrackingId { get; set; }

        /// <summary>
        ///     The name of your app, used in the app name dimension in your reports. Defaults to the value found in the package.
        /// </summary>
        public string AppName { get; set; }

        /// <summary>
        ///     The version of your application, used in the app version dimension within your reports. Defaults to the version
        ///     found in the package.
        /// </summary>
        public string AppVersion { get; set; }

        /// <summary>
        ///     Application identifier.
        /// </summary>
        public string AppId { get; set; }

        /// <summary>
        ///     Application installer identifier.
        /// </summary>
        public string AppInstallerId { get; set; }

        /// <summary>
        ///     Flag to enable or writing of debug information to the log, useful for troubleshooting your implementation. false by
        ///     default.
        /// </summary>
        public bool Debug { get; set; }

        /// <summary>
        ///     The dispatch period in seconds. Defaults to 30 seconds.
        /// </summary>
        public TimeSpan DispatchPeriod { get; set; }

        /// <summary>
        ///     The sample rate to use. Default is 100.0. It can be any value between 0.0 and 100.0
        /// </summary>
        public float SampleFrequency { get; set; }

        /// <summary>
        ///     Tells Google Analytics to anonymize the information sent by the tracker objects by removing the last octet of the
        ///     IP address prior to its storage. Note that this will slightly reduce the accuracy of geographic reporting. false by
        ///     default.
        /// </summary>
        public bool AnonymizeIp { get; set; }

        /// <summary>
        ///     Automatically track an Exception each time an uncaught exception is thrown in your application. false by default.
        /// </summary>
        public bool ReportUncaughtExceptions { get; set; }

        /// <summary>
        ///     The amount of time your application can stay in the background before the session is ended. Default is 30 seconds.
        ///     Null value disables EasyTracker session management.
        /// </summary>
        public TimeSpan? SessionTimeout { get; set; }

        /// <summary>
        ///     Automatically track application lifetime events (e.g. suspend and resume). true by default.
        /// </summary>
        public bool AutoAppLifetimeTracking { get; set; }

        /// <summary>
        ///     Automatically monitor suspend and resume events. If true, all dispatched events will be sent on suspend and session
        ///     timeout will be honored. true by default.
        /// </summary>
        public bool AutoAppLifetimeMonitoring { get; set; }

        /// <summary>
        ///     Tells Google Analytics to automatically monitor network connectivity and avoid sending logs while not connected.
        ///     Default is true.
        /// </summary>
        public bool AutoTrackNetworkConnectivity { get; set; }

        /// <summary>
        ///     If true, causes all hits to be sent to the secure (SSL) Google Analytics endpoint. Default is false.
        /// </summary>
        public bool UseSecure { get; set; }

        internal static EasyTrackerConfig Load(XmlReader reader)
        {
            // advance to first element
            while(reader.NodeType != XmlNodeType.Element && !reader.EOF)
            {
                reader.Read();
            }
            if(!reader.EOF && reader.Name == "analytics")
            {
                return LoadConfigXml(reader);
            }
            return new EasyTrackerConfig();
        }

        private static EasyTrackerConfig LoadConfigXml(XmlReader reader)
        {
            var result = new EasyTrackerConfig();
            reader.ReadStartElement("analytics");
            do
            {
                if(reader.IsStartElement())
                {
                    switch(reader.Name)
                    {
                        case "trackingId":
                            result.TrackingId = reader.ReadElementContentAsString();
                            break;
                        case "appName":
                            result.AppName = reader.ReadElementContentAsString();
                            break;
                        case "appVersion":
                            result.AppVersion = reader.ReadElementContentAsString();
                            break;
                        case "appId":
                            result.AppId = reader.ReadElementContentAsString();
                            break;
                        case "appInstallerId":
                            result.AppInstallerId = reader.ReadElementContentAsString();
                            break;
                        case "sampleFrequency":
                            result.SampleFrequency = reader.ReadElementContentAsFloat();
                            break;
                        case "dispatchPeriod":
                            var dispatchPeriodInSeconds = reader.ReadElementContentAsInt();
                            result.DispatchPeriod = TimeSpan.FromSeconds(dispatchPeriodInSeconds);
                            break;
                        case "sessionTimeout":
                            var sessionTimeoutInSeconds = reader.ReadElementContentAsInt();
                            result.SessionTimeout = (sessionTimeoutInSeconds >= 0) ? TimeSpan.FromSeconds(sessionTimeoutInSeconds) : (TimeSpan?)null;
                            break;
                        case "debug":
                            result.Debug = reader.ReadElementContentAsBoolean();
                            break;
                        case "autoAppLifetimeTracking":
                            result.AutoAppLifetimeTracking = reader.ReadElementContentAsBoolean();
                            break;
                        case "autoAppLifetimeMonitoring":
                            result.AutoAppLifetimeMonitoring = reader.ReadElementContentAsBoolean();
                            break;
                        case "anonymizeIp":
                            result.AnonymizeIp = reader.ReadElementContentAsBoolean();
                            break;
                        case "reportUncaughtExceptions":
                            result.ReportUncaughtExceptions = reader.ReadElementContentAsBoolean();
                            break;
                        case "useSecure":
                            result.UseSecure = reader.ReadElementContentAsBoolean();
                            break;
                        case "autoTrackNetworkConnectivity":
                            result.AutoTrackNetworkConnectivity = reader.ReadElementContentAsBoolean();
                            break;
                        default:
                            reader.Skip();
                            break;
                    }
                }
                else
                {
                    reader.ReadEndElement();
                    break;
                }
            }
            while(true);
            return result;
        }

        /// <summary>
        ///     Validates the configuration and throws an exception is a problem is found.
        /// </summary>
        internal void Validate()
        {
            if(AutoAppLifetimeTracking && !AutoAppLifetimeMonitoring)
            {
                throw new ArgumentOutOfRangeException("AutoAppLifetimeTracking cannot be true if AutoAppLifetimeMonitoring is false.");
            }
        }
    }
}