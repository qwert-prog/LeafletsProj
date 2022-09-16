using LeafletsProj.Enums;
using UWPToolkit.ViewModels;

namespace LeafletsProj.Models
{
    /// <summary>
    /// Represents model of decor
    /// </summary>
    public sealed class ImageModel : BaseViewModel
    {
        #region Private Fields

        /// <summary>
        /// Contains current color of image
        /// </summary>
        private string _color;

        /// <summary>
        /// Contains height to image
        /// </summary>
        private double _height;

        /// <summary>
        /// Contains focused flag of image
        /// </summary>
        private bool _isFocused;

        /// <summary>
        /// Contains visibility flag of image
        /// </summary>
        private bool _isVisible;

        /// <summary>
        /// Contains width to image
        /// </summary>
        private double _width;

        #endregion Private Fields

        #region Public Properties

        /// </summary>
        public double Angle { get; set; }

        /// <summary>
        /// Gets or sets current color of image
        /// </summary>
        public string Color
        {
            get => _color;
            set
            {
                if (_color != value)
                {
                    _color = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets height to image
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
        /// Gets or sets full path of image
        /// </summary>
        public string ImagePath { get; set; }

        /// <summary>
        /// Gets or sets focused flag of image
        /// </summary>
        public bool IsFocused
        {
            get => _isFocused;
            set
            {
                if (_isFocused != value)
                {
                    _isFocused = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets visibility flag of image
        /// </summary>
        public bool IsVisible
        {
            get { return _isVisible; }
            set
            {
                if (_isVisible != value)
                {
                    _isVisible = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the left margin of the image
        /// </summary>
        public double Left { get; set; }

        /// <summary>
        /// Gets or sets the top margin of the image
        /// </summary>
        public double Top { get; set; }

        /// <summary>
        /// Gets or sets type of image
        /// </summary>
        public ImageTypeEnum Type { get; private set; }

        /// <summary>
        /// Gets or sets width to image
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

        #region Public Constructors

        /// <summary>
        /// Gets or sets angle of rotation
        /// <summary>
        /// Initializes an instance of a class <see cref="ImageModel"/>
        /// </summary>
        /// <param name="imagePath"></param>
        public ImageModel(ImageTypeEnum type, string imagePath = "", string color = "#000000")
        {
            ImagePath = imagePath;
            Color = color;
            IsVisible = true;
            IsFocused = false;
            Type = type;
            switch (Type)
            {
                case ImageTypeEnum.Line:
                    Width = 200;
                    Height = 30;
                    break;

                case ImageTypeEnum.Sign:
                    Width = 100;
                    Height = 100;
                    break;

                case ImageTypeEnum.Paint:
                    Width = 200;
                    Height = 200;
                    break;

                case ImageTypeEnum.Abstract:
                    Width = 200;
                    Height = 200;
                    break;
            }
        }

        #endregion Public Constructors
    }
}