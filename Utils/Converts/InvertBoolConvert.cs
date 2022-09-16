﻿using System;
using Windows.UI.Xaml.Data;

namespace Utils.Converts
{
    public class InvertBoolConvert : IValueConverter

    {
        #region Public Methods

        /// <summary>
        /// Инверитрует полученное булевое значение
        /// </summary>
        /// <param name="value">Значение для преобразования</param>
        /// <param name="targetType">Тип к которому нужно преобразовать значение</param>
        /// <param name="parameter">Вспомогательный параметр привязки</param>
        /// <param name="language">Языковой код</param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if ((bool)value)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Обратное преобразование
        /// </summary>
        /// <param name="value">Значение для преобразования</param>
        /// <param name="targetType">Тип к которому нужно преобразовать значение</param>
        /// <param name="parameter">Вспомогательный параметр привязки</param>
        /// <param name="language">Языковой код</param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return !(bool)value;
        }

        #endregion Public Methods
    }
}