using GoogleAnalytics.Core.Platform;
using GoogleAnalytics.iOS;
using Xamarin.Forms;

[assembly: Dependency(typeof(Display))]

namespace GoogleAnalytics.iOS
{
    public class Display : IDisplay
    {
        internal Display(int height, int width)
        {
            Height = height;
            Width = width;
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