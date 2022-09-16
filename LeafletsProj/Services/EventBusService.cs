using LeafletsProj.Models;
using System;

namespace LeafletsProj.Services
{
    /// <summary>
    /// Application event bus
    /// </summary>
    public class EventBusService
    {
        #region Public Events

        /// <summary>
        /// Change selected image
        /// </summary>
        public static event EventHandler SelectedImageChanged;

        /// <summary>
        /// Change selected text
        /// </summary>
        public static event EventHandler SelectedTextChanged;

        #endregion Public Events

        #region Public Methods

        /// <summary>
        /// Raises the selected image change event
        /// </summary>
        /// <param name="selectedText"></param>
        public static void OnSelectedImageChanged(ImageModel selectedImage)
        {
            SelectedImageChanged?.Invoke(selectedImage, EventArgs.Empty);
        }

        /// <summary>
        /// Raises the selected text change event
        /// </summary>
        /// <param name="selectedText"></param>
        public static void OnSelectedTextChanged(Text selectedText)
        {
            SelectedTextChanged?.Invoke(selectedText, EventArgs.Empty);
        }

        #endregion Public Methods
    }
}