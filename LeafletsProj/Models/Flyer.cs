using LeafletsProj.Core.Models;
using Windows.Graphics.Display;

namespace LeafletsProj.Models
{
    /// <summary>
    /// Represents model of flyer
    /// </summary>
    public sealed class Flyer : Paper
    {
        #region Private Fields

        /// <summary>
        /// Contains the format of the flyer
        /// </summary>
        private PaperFormat _format;

        /// <summary>
        /// Contains the height of the flyer
        /// </summary>
        private double _height = AppConst.DefaultHeightFlyer;

        /// <summary>
        /// Contains the width of the flyer
        /// </summary>
        private double _width = AppConst.DefaultWidthFlyer;

        #endregion Private Fields

        #region Public Properties

        /// <summary>
        /// Gets or sets the format of the flyer
        /// </summary>
        public PaperFormat Format
        {
            get => _format;
            set
            {
                _format = value;
                if (value != null)
                {
                    this.Height = MillimetersToPixelsConvert(value.Height);
                    this.Width = MillimetersToPixelsConvert(value.Width);
                }
            }
        }

        /// <summary>
        ///  Gets or sets the name of the group
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// Gets or sets the height of the flyer
        /// </summary>
        public double Height
        {
            get => _height;
            set
            {
                _height = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the width of the flyer
        /// </summary>
        public double Width
        {
            get => _width;
            set
            {
                _width = value;
                OnPropertyChanged();
            }
        }

        #endregion Public Properties

        #region Private Methods

        /// <summary>
        /// Method for convert size in millimeters
        /// to the number of pixels based on the DPI value
        /// </summary>
        /// <param name="millimeters"></param>
        /// <returns></returns>
        private double MillimetersToPixelsConvert(double millimeters)
        {
            DisplayInformation display = DisplayInformation.GetForCurrentView();
            double mmInInch = 25.4;
            double sizeInInch = millimeters / mmInInch;
            double sizeInEffectivePixels = sizeInInch * display.LogicalDpi;

            return sizeInEffectivePixels;
        }

        #endregion Private Methods
    }
}