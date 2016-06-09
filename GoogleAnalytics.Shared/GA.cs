using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Plugin.GoogleAnalytics
{
    public class GA
    {
        public TrackerConfig Config { get; set; }

        public TrackerFactory Factory { get; set; }
    }
}
