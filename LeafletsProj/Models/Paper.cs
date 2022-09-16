using System.Collections.Generic;
using UWPToolkit.ViewModels;

namespace LeafletsProj.Models
{
    /// <summary>
    /// Base class for business car and flyer
    /// </summary>
    public class Paper : BaseViewModel
    {
        #region Private Fields

        /// <summary>
        /// Contains a boolean flag of premium card
        /// </summary>
        private bool _isPremium;

        #endregion Private Fields

        #region Public Properties

        /// <summary>
        /// Gets or sets path to the front image
        /// </summary>
        public string FrontImagePath { get; set; }

        /// <summary>
        /// Gets or sets a boolean flag of premium card
        /// </summary>
        public bool IsPremium
        {
            get => _isPremium;
            set
            {
                _isPremium = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets path to the preview image
        /// </summary>
        public string PreviewImagePath { get; set; }

        /// <summary>
        /// Gets or sets list texts in front image
        /// </summary>
        public List<Text> FrontTextTemplates { get; set; }

        /// <summary>
        /// Gets or sets list images in front image
        /// </summary>
        public List<ImageModel> FrontImageTemplates { get; set; } = new List<ImageModel>();

        #endregion Public Properties
    }
}