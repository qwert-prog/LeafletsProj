using LeafletsProj.Services;
using System;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace LeafletsProj.Converts
{
    /// <summary>
    /// Converts a string (font name) to a <see cref="FontFamily"/> value for text
    /// </summary>
    internal sealed class StringToFontFamilyConverter : IValueConverter
    {
        #region Public Methods

        /// <summary>
        /// Converts string to <see cref="FontFamily"/>
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (string.IsNullOrWhiteSpace(value as string) == false)
            {
                string fontName = value as string;
                var fontService = ServiceLocator.FontsService;
                string fontPath = fontService.GetFontByName(fontName).FontAssetPath;
                var fontFamily = new FontFamily(fontPath);
                return fontFamily;
            }
            return FontFamily.XamlAutoFontFamily;
        }

        /// <summary>
        /// Method for reverse conversion
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return FontFamily.XamlAutoFontFamily;
        }

        #endregion Public Methods
    }
}