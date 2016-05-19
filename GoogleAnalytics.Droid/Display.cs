using Android.Util;
using Android.Views;
using GoogleAnalytics.Core.Platform;
using Xamarin.Forms;
using Application = Android.App.Application;

[assembly: Dependency(typeof(Display))]

namespace GoogleAnalytics.Droid
{
    public class Display : IDisplay
    {
        public Display()
        {
            var dm = Metrics;
            Height = dm.HeightPixels;
            Width = dm.WidthPixels;
        }

        /// <summary>
        ///     Gets the metrics.
        /// </summary>
        /// <value>The metrics.</value>
        public static DisplayMetrics Metrics
        {
            get { return Application.Context.Resources.DisplayMetrics; }
        }

        /// <summary>
        ///     Gets the screen height in pixels
        /// </summary>
        public int Height { get; private set; }

        /// <summary>
        ///     Gets the screen width in pixels
        /// </summary>
        public int Width { get; private set; }
    }
}