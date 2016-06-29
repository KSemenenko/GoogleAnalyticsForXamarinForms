using System;
using Plugin.GoogleAnalytics.Abstractions.Model;

namespace Plugin.GoogleAnalytics.Abstractions
{
    /// <summary>
    /// Interface for Plugin.GoogleAnalytics
    /// </summary>
    public interface ITracker
    {
        string TrackingId { get; }
        bool IsAnonymizeIpEnabled { get; set; }
        string AppName { get; set; }
        string AppVersion { get; set; }
        string AppId { get; set; }
        string AppInstallerId { get; set; }
        Dimensions AppScreen { get; set; }
        string CampaignName { get; set; }
        string CampaignSource { get; set; }
        string CampaignMedium { get; set; }
        string CampaignKeyword { get; set; }
        string CampaignContent { get; set; }
        string CampaignId { get; set; }
        string Referrer { get; set; }
        string DocumentEncoding { get; set; }
        string GoogleAdWordsId { get; set; }
        string GoogleDisplayAdsId { get; set; }
        string IpOverride { get; set; }
        string UserAgentOverride { get; set; }
        string DocumentLocationUrl { get; set; }
        string DocumentHostName { get; set; }
        string DocumentPath { get; set; }
        string DocumentTitle { get; set; }
        string LinkId { get; set; }
        string ExperimentId { get; set; }
        string ExperimentVariant { get; set; }

        /// <summary>
        ///     Optional. Indicates the data source of the hit. Hits sent from analytics.js will have data source set to 'web';
        ///     hits sent from one of the mobile SDKs will have data source set to 'app'.
        /// </summary>
        string DataSource { get; set; }

        /// <summary>
        ///     Optional. This is intended to be a known identifier for a user provided by the site owner/tracking library user. It
        ///     may not itself be PII (personally identifiable information). The value should never be persisted in GA cookies or
        ///     other Analytics provided storage.
        /// </summary>
        string UserId { get; set; }

        /// <summary>
        ///     Optional. The geographical location of the user. The geographical ID should be a two letter country code or a
        ///     criteria ID representing a city or region (see
        ///     http://developers.google.com/analytics/devguides/collection/protocol/v1/geoid). This parameter takes precedent over
        ///     any location derived from IP address, including the IP Override parameter. An invalid code will result in
        ///     geographical dimensions to be set to '(not set)'.
        /// </summary>
        string GeographicalId { get; set; }

        float SampleRate { get; set; }
        bool IsUseSecure { get; set; }
        bool IsDebug { get; set; }
        bool ThrottlingEnabled { get; set; }
        void SetCustomDimension(int index, string value);
        void SetCustomMetric(int index, long value);
        void SendView(string screenName);
        void SendException(string description, bool isFatal);
        void SendException(Exception exception, bool isFatal);
        void SendSocial(string network, string action, string target);
        void SendTiming(TimeSpan time, string category, string variable, string label);
        void SendEvent(string category, string action, string label, long value);
        void SendEvent(string category, string action, string label, int value);
        void SendEvent(string category, string action, string label);
        void SendEvent(string category, string action);
        void SendTransaction(Transaction transaction);
        void SendTransactionItem(TransactionItem transactionItem);
        void SetStartSession(bool value);
        void SetEndSession(bool value);
    }
}