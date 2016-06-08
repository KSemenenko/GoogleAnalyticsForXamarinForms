using Plugin.GoogleAnalytics.Abstractions;
using System;

namespace Plugin.GoogleAnalytics
{
  /// <summary>
  /// Cross platform Plugin.GoogleAnalytics implemenations
  /// </summary>
  public class CrossPlugin.GoogleAnalytics
  {
    static Lazy<IPlugin.GoogleAnalytics> Implementation = new Lazy<IPlugin.GoogleAnalytics>(() => CreatePlugin.GoogleAnalytics(), System.Threading.LazyThreadSafetyMode.PublicationOnly);

    /// <summary>
    /// Current settings to use
    /// </summary>
    public static IPlugin.GoogleAnalytics Current
    {
      get
      {
        var ret = Implementation.Value;
        if (ret == null)
        {
          throw NotImplementedInReferenceAssembly();
        }
        return ret;
      }
    }

    static IPlugin.GoogleAnalytics CreatePlugin.GoogleAnalytics()
    {
#if PORTABLE
        return null;
#else
        return new Plugin.GoogleAnalyticsImplementation();
#endif
    }

    internal static Exception NotImplementedInReferenceAssembly()
    {
      return new NotImplementedException("This functionality is not implemented in the portable version of this assembly.  You should reference the NuGet package from your main application project in order to reference the platform-specific implementation.");
    }
  }
}
