using System;

namespace Plugin.GoogleAnalytics.Abstractions
{
    /// <summary>
    /// Interface for Plugin.GoogleAnalytics
    /// </summary>
    public interface IGoogleAnalytics
    {
        ITrackerConfig Config { get; set; }

        ITracker Tracker { get; }
    }
}