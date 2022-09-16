using System.Collections.Generic;
using System.IO;
using Windows.ApplicationModel.Resources;

namespace LeafletsProj.Core.Models
{
    /// <summary>
    /// Сlass that contains consts for application
    /// </summary>
    public static class AppConst
    {
        #region Public Properties

        /// <summary>
        /// Gets the name of the folder containing abstract images
        /// </summary>
        public static string AbstractsFolderName { get; } = Path.Combine("Assets", "Abstracts");

        /// <summary>
        /// Gets list of RGB colors in number format
        /// </summary>
        public static List<int> Colors { get; }

        /// <summary>
        /// Get the default height value for flyer
        /// </summary>
        public static double DefaultHeightFlyer { get; } = 620;

        /// <summary>
        /// Get the default width value for flyer
        /// </summary>
        public static double DefaultWidthFlyer { get; } = 437;

        /// <summary>
        /// Gets the name of the folder containing lines images
        /// </summary>
        public static string LinesFolderName { get; } = Path.Combine("Assets", "Lines");

        /// <summary>
        /// Get the maximum value of the slider that is responsible for the font size
        /// </summary>
        public static int MaximumFontSize { get; } = 300;

        /// <summary>
        /// Get the maximum value of the slider that is responsible for the line spacing
        /// </summary>
        public static int MaximumInterval { get; } = 180;

        /// <summary>
        /// Get the minimum height image of decor
        /// </summary>
        public static double MinHeightImage { get; } = 10;

        /// <summary>
        /// Get the minimum value of the slider that is responsible for the font size
        /// </summary>
        public static int MinimumFontSize { get; } = 4;

        /// <summary>
        /// Get the minimum value of the slider that is responsible for the line spacing
        /// </summary>
        public static int MinimumInterval { get; } = 10;

        /// <summary>
        /// Get the minimum width image of decor
        /// </summary>
        public static double MinWidthImage { get; } = 10;

        /// <summary>
        /// Gets the name of the folder containing paints images
        /// </summary>
        public static string PaintsFolderName { get; } = Path.Combine("Assets", "Paints");

        /// <summary>
        /// Get a list of all available formats
        /// </summary>
        public static List<PaperFormat> PaperFormatsList { get; }

        /// <summary>
        /// Gets the name of the folder containing signs images
        /// </summary>
        public static string SignsFolderFolderName { get; } = Path.Combine("Assets", "Signs");

        #endregion Public Properties

        #region Public Constructors

        /// <summary>
        /// Initialize <see cref="AppConst"/>
        /// </summary>
        static AppConst()
        {
            ResourceLoader resourceLoader = new ResourceLoader();

            Colors = new List<int>()
            {
                0xFFFFFF,
                0x000000,
                0x4C0A83,
                0x9F5ED4,
                0x2131A5,
                0x699AFD,
                0x48DFCC,
                0xB1FFF5,
                0x1E3C02,
                0x077C30,
                0x6FC139,
                0x312001,
                0xF0AD39,
                0xFF7210,
                0x5D0404,
                0xA50C0C,
                0xF75656,
                0xFF97CA,
                0xE52080,
                0xB20559,
                0x5C022E,
            };

            PaperFormatsList = new List<PaperFormat>()
            {
                new PaperFormat("А3 (297*420)",297,420),
                new PaperFormat("А4 (210*297)",210,297),
                new PaperFormat("А5 (148*210)",148,210),
                new PaperFormat("А6 (105*148)",105,148),
                new PaperFormat("А7 (74*105)",74,105),
            };
        }

        #endregion Public Constructors
    }
}