using LeafletsProj.Controls;
using LeafletsProj.Models;
using LeafletsProj.Services;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace LeafletsProj.View.ContentDialogs
{
    /// <summary>
    /// Content dialog for save card or flyer
    /// </summary>
    public sealed partial class SaveContentDialog : ContentDialog
    {
        #region Private Fields

        /// <summary>
        /// Contains <see cref="UIElement"/> for save
        /// </summary>
        private UIElement _targetUIElement;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// Initialize an instance of a class <see cref="SaveContentDialog"/>
        /// </summary>
        public SaveContentDialog(UIElement element, Type typePaper)
        {
            if (typePaper == typeof(Card))
            {
                _targetUIElement = element as CardControl;
            }
            else
            {
                _targetUIElement = element as FlyerControl;
            }
            this.InitializeComponent();
        }

        #endregion Public Constructors

        #region Private Methods

        /// <summary>
        /// Handles the "Cancel" button click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        /// <summary>
        /// Handles the "Save" button click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (JpegModeCheckBox.IsChecked == true)
                await ServiceLocator.SaveDataService.SaveToJPEG(_targetUIElement);
            if (PngModeCheckBox.IsChecked == true)
                await ServiceLocator.SaveDataService.SaveToPNG(_targetUIElement);
            this.Hide();
        }

        #endregion Private Methods
    }
}