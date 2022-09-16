using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace LeafletsProj.Converts
{
    /// <summary>
    /// Converts null to <see cref="Visibility.Collapsed"/>, object to  <see cref="Visibility.Visible"/>
    /// </summary>
    internal sealed class ObjectToVisibilityConvert : IValueConverter
    {
        #region Public Methods

        /// <summary>
        /// Method for convert object to visibility
        /// </summary>
        /// <param name="value">Значение для преобразования</param>
        /// <param name="targetType">Тип к которому нужно преобразовать значение</param>
        /// <param name="parameter">Вспомогательный параметр привязки</param>
        /// <param name="language">Языковой код</param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is null)
            {
                return Visibility.Collapsed;
            }
            else
            {
                return Visibility.Visible;
            }
        }

        /// <summary>
        /// Method for reverse conversion
        /// </summary>
        /// <param name="value">Значение для преобразования</param>
        /// <param name="targetType">Тип к которому нужно преобразовать значение</param>
        /// <param name="parameter">Вспомогательный параметр привязки</param>
        /// <param name="language">Языковой код</param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value;
        }

        #endregion Public Methods
    }
}