using Plugin.GoogleAnalytics.Abstractions;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Plugin.GoogleAnalytics
{
    /// <summary>
    /// Cross platform Plugin.GoogleAnalytics implemenations
    /// </summary>
    public partial class GoogleAnalytics
    {
        private static readonly Lazy<IGoogleAnalytics> Implementation =
            new Lazy<IGoogleAnalytics>(CreatePluginGoogleAnalytics, System.Threading.LazyThreadSafetyMode.PublicationOnly);

        /// <summary>
        /// Current settings to use
        /// </summary>
        public static IGoogleAnalytics Current
        {
            get
            {
                var ret = Implementation.Value;
                if(ret == null)
                {
                    throw NotImplementedInReferenceAssembly();
                }
                return ret;
            }
        }

        private static IGoogleAnalytics CreatePluginGoogleAnalytics()
        {
            return new GoogleAnalyticsImplementation();
        }

        static Exception NotImplementedInReferenceAssembly()
        {
            return
                new NotImplementedException(
                    "This functionality is not implemented in the portable version of this assembly.  " +
                    "You should reference the NuGet package from your main application project in order to reference the platform-specific implementation.");
        }
    }
}