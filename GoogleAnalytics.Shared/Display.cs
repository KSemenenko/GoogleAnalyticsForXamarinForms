namespace Plugin.GoogleAnalytics
{
    public class Display
    {
        public Display(int height, int width)
        {
            if(height > width)
            {
                Height = height;
                Width = width;
            }
            else
            {
                //switch
                Height = width;
                Width = height;
            }
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