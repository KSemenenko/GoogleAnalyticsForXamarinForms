namespace Plugin.GoogleAnalytics.Abstractions.Model
{
    public sealed class Dimensions
    {
        public Dimensions(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public int Width { get; set; }
        public int Height { get; set; }
    }
}