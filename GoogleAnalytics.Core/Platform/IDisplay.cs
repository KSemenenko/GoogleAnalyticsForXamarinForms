using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleAnalytics.Core.Platform
{
    public interface IDisplay
    {
        /// <summary>
        /// Gets the screen height in pixels
        /// </summary>
        int Height { get; }

        /// <summary>
        /// Gets the screen width in pixels
        /// </summary>
        int Width { get; }

        /// <summary>
        /// Gets the screens X pixel density per inch
        /// </summary>
        double Xdpi { get; }

        /// <summary>
        /// Gets the screens Y pixel density per inch
        /// </summary>
        double Ydpi { get; }

        /// <summary>
        /// Gets the scale value of the display.
        /// </summary>
        double Scale { get; }

        /// <summary>
        /// Convert width in inches to runtime pixels
        /// </summary>
        double WidthRequestInInches(double inches);

        /// <summary>
        /// Convert height in inches to runtime pixels
        /// </summary>
        double HeightRequestInInches(double inches);
    }
}
