using System;

namespace GoogleAnalytics.Core
{
    public sealed class PlatformInfoProvider : IPlatformInfoProvider
    {
        private Dimensions screenResolution;
        private Dimensions viewPortResolution;
        public string UserAgent { get; set; }
        public event EventHandler ViewPortResolutionChanged;
        public event EventHandler ScreenResolutionChanged;
        public string AnonymousClientId { get; set; }

        public void OnTracking()
        {
        }

        public int? ScreenColorDepthBits { get; set; }
        public string UserLanguage { get; set; }

        public Dimensions ScreenResolution
        {
            get { return screenResolution; }
            set
            {
                screenResolution = value;
                if(ScreenResolutionChanged != null)
                {
                    ScreenResolutionChanged(this, EventArgs.Empty);
                }
            }
        }

        public Dimensions ViewPortResolution
        {
            get { return viewPortResolution; }
            set
            {
                viewPortResolution = value;
                if(ViewPortResolutionChanged != null)
                {
                    ViewPortResolutionChanged(this, EventArgs.Empty);
                }
            }
        }

        string IPlatformInfoProvider.GetUserAgent()
        {
            return UserAgent;
        }
    }
}