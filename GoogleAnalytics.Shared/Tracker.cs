using System;
using System.Collections.Generic;
using Plugin.GoogleAnalytics.Abstractions;
using Plugin.GoogleAnalytics.Abstractions.Model;

namespace Plugin.GoogleAnalytics
{
    public sealed class Tracker : ITracker
    {
        private readonly AnalyticsEngine analyticsEngine;
        private readonly PayloadFactory engine;
        private readonly TokenBucket hitTokenBucket;
        private readonly IPlatformInfoProvider platformInfoProvider;
        private readonly IServiceManager serviceManager;
        private bool endSession;
        private bool startSession;

        public Tracker(string propertyId, IPlatformInfoProvider platformInfoProvider, IServiceManager serviceManager)
        {
            this.serviceManager = serviceManager;
            if(string.IsNullOrEmpty(serviceManager.UserAgent))
            {
                serviceManager.UserAgent = platformInfoProvider.UserAgent;
            }
            this.platformInfoProvider = platformInfoProvider;
            engine = new PayloadFactory
            {
                PropertyId = propertyId,
                AnonymousClientId = platformInfoProvider.AnonymousClientId,
                ScreenColorDepthBits = platformInfoProvider.ScreenColorDepthBits,
                ScreenResolution = platformInfoProvider.ScreenResolution,
                UserAgentOverride = platformInfoProvider.UserAgent,
                UserLanguage = platformInfoProvider.UserLanguage,
                ViewportSize = platformInfoProvider.ViewPortResolution,                
                //DocumentEncoding = platformInfoProvider.DocumentEncoding,
            };
            SampleRate = 100.0F;
            hitTokenBucket = new TokenBucket(60, .5);
        }

        public string TrackingId
        {
            get { return engine.PropertyId; }
        }

        public bool IsAnonymizeIpEnabled
        {
            get { return engine.AnonymizeIP; }
            set { engine.AnonymizeIP = value; }
        }

        public string AppName
        {
            get { return engine.AppName; }
            set { engine.AppName = value; }
        }

        public string AppVersion
        {
            get { return engine.AppVersion; }
            set { engine.AppVersion = value; }
        }

        public string AppId
        {
            get { return engine.AppId; }
            set { engine.AppId = value; }
        }

        public string AppInstallerId
        {
            get { return engine.AppInstallerId; }
            set { engine.AppInstallerId = value; }
        }

        public Dimensions AppScreen
        {
            get { return engine.ViewportSize; }
            set { engine.ViewportSize = value; }
        }

        public string CampaignName
        {
            get { return engine.CampaignName; }
            set { engine.CampaignName = value; }
        }

        public string CampaignSource
        {
            get { return engine.CampaignSource; }
            set { engine.CampaignSource = value; }
        }

        public string CampaignMedium
        {
            get { return engine.CampaignMedium; }
            set { engine.CampaignMedium = value; }
        }

        public string CampaignKeyword
        {
            get { return engine.CampaignKeyword; }
            set { engine.CampaignKeyword = value; }
        }

        public string CampaignContent
        {
            get { return engine.CampaignContent; }
            set { engine.CampaignContent = value; }
        }

        public string CampaignId
        {
            get { return engine.CampaignId; }
            set { engine.CampaignId = value; }
        }

        public string Referrer
        {
            get { return engine.Referrer; }
            set { engine.Referrer = value; }
        }

        public string DocumentEncoding
        {
            get { return engine.DocumentEncoding; }
            set { engine.DocumentEncoding = value; }
        }

        public string GoogleAdWordsId
        {
            get { return engine.GoogleAdWordsId; }
            set { engine.GoogleAdWordsId = value; }
        }

        public string GoogleDisplayAdsId
        {
            get { return engine.GoogleDisplayAdsId; }
            set { engine.GoogleDisplayAdsId = value; }
        }

        public string IpOverride
        {
            get { return engine.IpOverride; }
            set { engine.IpOverride = value; }
        }

        public string UserAgentOverride
        {
            get { return engine.UserAgentOverride; }
            set { engine.UserAgentOverride = value; }
        }

        public string DocumentLocationUrl
        {
            get { return engine.DocumentLocationUrl; }
            set { engine.DocumentLocationUrl = value; }
        }

        public string DocumentHostName
        {
            get { return engine.DocumentHostName; }
            set { engine.DocumentHostName = value; }
        }

        public string DocumentPath
        {
            get { return engine.DocumentPath; }
            set { engine.DocumentPath = value; }
        }

        public string DocumentTitle
        {
            get { return engine.DocumentTitle; }
            set { engine.DocumentTitle = value; }
        }

        public string LinkId
        {
            get { return engine.LinkId; }
            set { engine.LinkId = value; }
        }

        public string ExperimentId
        {
            get { return engine.ExperimentId; }
            set { engine.ExperimentId = value; }
        }

        public string ExperimentVariant
        {
            get { return engine.ExperimentVariant; }
            set { engine.ExperimentVariant = value; }
        }

        /// <summary>
        ///     Optional. Indicates the data source of the hit. Hits sent from analytics.js will have data source set to 'web';
        ///     hits sent from one of the mobile SDKs will have data source set to 'app'.
        /// </summary>
        public string DataSource
        {
            get { return engine.DataSource; }
            set { engine.DataSource = value; }
        }

        /// <summary>
        ///     Optional. This is intended to be a known identifier for a user provided by the site owner/tracking library user. It
        ///     may not itself be PII (personally identifiable information). The value should never be persisted in GA cookies or
        ///     other Analytics provided storage.
        /// </summary>
        public string UserId
        {
            get { return engine.UserId; }
            set { engine.UserId = value; }
        }

        /// <summary>
        ///     Optional. The geographical location of the user. The geographical ID should be a two letter country code or a
        ///     criteria ID representing a city or region (see
        ///     http://developers.google.com/analytics/devguides/collection/protocol/v1/geoid). This parameter takes precedent over
        ///     any location derived from IP address, including the IP Override parameter. An invalid code will result in
        ///     geographical dimensions to be set to '(not set)'.
        /// </summary>
        public string GeographicalId
        {
            get { return engine.GeographicalId; }
            set { engine.GeographicalId = value; }
        }

        public float SampleRate { get; set; }
        public bool IsUseSecure { get; set; }
        public bool IsDebug { get; set; }
        public bool ThrottlingEnabled { get; set; }

        private SessionControl SessionControl
        {
            get
            {
                if(endSession)
                {
                    endSession = false;
                    return SessionControl.End;
                }
                if(startSession)
                {
                    startSession = false;
                    return SessionControl.Start;
                }
                return SessionControl.None;
            }
        }

        public void SetCustomDimension(int index, string value)
        {
            engine.CustomDimensions[index] = value;
        }

        public void SetCustomMetric(int index, long value)
        {
            engine.CustomMetrics[index] = value;
        }

        private void platformTrackingInfo_ViewPortResolutionChanged(object sender, EventArgs args)
        {
            engine.ViewportSize = platformInfoProvider.ViewPortResolution;
        }

        private void platformTrackingInfo_ScreenResolutionChanged(object sender, EventArgs args)

        {
            engine.ScreenResolution = platformInfoProvider.ScreenResolution;
        }

        public void SendView(string screenName)
        {
            platformInfoProvider.OnTracking(); // give platform info provider a chance to refresh.
            var payload = engine.TrackView(screenName, SessionControl);
            SendPayload(payload);
        }

        public void SendException(string description, bool isFatal)
        {
            platformInfoProvider.OnTracking(); // give platform info provider a chance to refresh.
            var payload = engine.TrackException(description, isFatal, SessionControl);
            SendPayload(payload);
        }

        public void SendSocial(string network, string action, string target)
        {
            platformInfoProvider.OnTracking(); // give platform info provider a chance to refresh.
            var payload = engine.TrackSocialInteraction(network, action, target, SessionControl);
            SendPayload(payload);
        }

        public void SendTiming(TimeSpan time, string category, string variable, string label)
        {
            platformInfoProvider.OnTracking(); // give platform info provider a chance to refresh.
            var payload = engine.TrackUserTiming(category, variable, time, label, null, null, null, null, null, null, SessionControl);
            SendPayload(payload);
        }

        public void SendEvent(string category, string action, string label, long value)
        {
            platformInfoProvider.OnTracking(); // give platform info provider a chance to refresh.
            var payload = engine.TrackEvent(category, action, label, value, SessionControl);
            SendPayload(payload);
        }

        public void SendEvent(string category, string action)
        {
            platformInfoProvider.OnTracking(); // give platform info provider a chance to refresh.
            var payload = engine.TrackEvent(category, action, string.Empty, null, SessionControl);
            SendPayload(payload);
        }

        public void SendTransaction(Transaction transaction)
        {
            platformInfoProvider.OnTracking(); // give platform info provider a chance to refresh.
            foreach(var payload in TrackTransaction(transaction, SessionControl))
            {
                SendPayload(payload);
            }
        }

        public void SendTransactionItem(TransactionItem transactionItem)
        {
            platformInfoProvider.OnTracking(); // give platform info provider a chance to refresh.
            var payload = engine.TrackTransactionItem(transactionItem.TransactionId, transactionItem.Name, (double)transactionItem.PriceInMicros / 1000000, transactionItem.Quantity,
                transactionItem.SKU, transactionItem.Category, transactionItem.CurrencyCode, SessionControl);
            SendPayload(payload);
        }

        private IEnumerable<Payload> TrackTransaction(Transaction transaction, SessionControl sessionControl = SessionControl.None, bool isNonInteractive = false)
        {
            yield return
                engine.TrackTransaction(transaction.TransactionId, transaction.Affiliation, (double)transaction.TotalCostInMicros / 1000000,
                    (double)transaction.ShippingCostInMicros / 1000000, (double)transaction.TotalTaxInMicros / 1000000, transaction.CurrencyCode, sessionControl,
                    isNonInteractive);

            foreach(var item in transaction.Items)
            {
                yield return
                    engine.TrackTransactionItem(transaction.TransactionId, item.Name, (double)item.PriceInMicros / 1000000, item.Quantity, item.SKU, item.Category,
                        transaction.CurrencyCode, sessionControl, isNonInteractive);
            }
        }

        public void SetStartSession(bool value)
        {
            startSession = value;
        }

        public void SetEndSession(bool value)
        {
            endSession = value;
        }

        private void SendPayload(Payload payload)
        {
            if(string.IsNullOrEmpty(TrackingId))
            {
                System.Diagnostics.Debug.WriteLine("Error: TrackingId not set.");
                return;
            }

            if(!IsSampledOut())
            {
                if(!ThrottlingEnabled || hitTokenBucket.Consume())
                {
                    payload.IsUseSecure = IsUseSecure;
                    payload.IsDebug = IsDebug;
                    serviceManager.SendPayload(payload);
                }
            }
            
        }

        private bool IsSampledOut()
        {
            if(SampleRate <= 0.0F)
            {
                return true;
            }
            if(SampleRate < 100.0F)
            {
                var clientId = platformInfoProvider.AnonymousClientId;
                return ((clientId != null) && (Math.Abs(clientId.GetHashCode()) % 10000 >= SampleRate * 100.0F));
            }
            return false;
        }
    }
}