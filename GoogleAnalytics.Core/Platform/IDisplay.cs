namespace GoogleAnalytics.Core.Platform
{
    public interface IDisplay
    {
        /// <summary>
        ///     Gets the screen height in pixels
        /// </summary>
        int Height { get; }

        /// <summary>
        ///     Gets the screen width in pixels
        /// </summary>
        int Width { get; }
    }
}