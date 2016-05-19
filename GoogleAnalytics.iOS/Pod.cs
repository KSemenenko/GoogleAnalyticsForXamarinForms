// ***********************************************************************
// Assembly         : XLabs.Platform.iOS
// Author           : XLabs Team
// Created          : 12-27-2015
// 
// Last Modified By : XLabs Team
// Last Modified On : 01-04-2016
// ***********************************************************************
// <copyright file="Pod.cs" company="XLabs Team">
//     Copyright (c) XLabs Team. All rights reserved.
// </copyright>
// <summary>
//       This project is licensed under the Apache 2.0 license
//       https://github.com/XLabs/Xamarin-Forms-Labs/blob/master/LICENSE
//       
//       XLabs is a open source project that aims to provide a powerfull and cross 
//       platform set of controls tailored to work with Xamarin Forms.
// </summary>
// ***********************************************************************
// 

using System.ComponentModel;
using GoogleAnalytics.Core.Platform;
using GoogleAnalytics.iOS;


namespace XLabs.Platform.Device
{
    /// <summary>
    /// Apple iPod.
    /// </summary>
    public class Pod 
    {
        /// <summary>
        /// Enum PodVersion
        /// </summary>
        public enum PodVersion
        {
            /// <summary>
            /// The first generation
            /// </summary>
            [Description("iPod Touch 1G")]
            FirstGeneration = 1,

            /// <summary>
            /// The second generation
            /// </summary>
            [Description("iPod Touch 2G")]
            SecondGeneration,

            /// <summary>
            /// The third generation
            /// </summary>
            [Description("iPod Touch 3G")]
            ThirdGeneration,

            /// <summary>
            /// The fourth generation
            /// </summary>
            [Description("iPod Touch 4G")]
            FourthGeneration,

            /// <summary>
            /// The fifth generation
            /// </summary>
            [Description("iPod Touch 5G")]
            FifthGeneration
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Pod" /> class.
        /// </summary>
        /// <param name="majorVersion">Major version.</param>
        /// <param name="minorVersion">Minor version.</param>
        public Pod(int majorVersion, int minorVersion)
        {
            Version = (PodVersion)majorVersion;
          

            Name = HardwareVersion = Version.GetDescription();

            if (majorVersion > 4)
            {
                Display = new Display(1136, 640);
            }
            else if (majorVersion > 3)
            {
                Display = new Display(960, 640);
            }
            else
            {
                Display = new Display(480, 320);
            }
        }

        public string Name { get; set; }

        public string HardwareVersion { get; set; }

        public IDisplay Display { get; set; }

        /// <summary>
        /// Gets the version of iPod.
        /// </summary>
        /// <value>The version.</value>
        public PodVersion Version { get; private set; }
    }
}