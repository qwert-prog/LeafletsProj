using LeafletsProj.Core.Enums;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace LeafletsProj.Converts
{
    /// <summary>
    /// Converts <see cref="TextAlignmentEnum"/> to <see cref="TextAlignment"/> and reverse
    /// </summary>
    internal class EnumAlignmentToTextAlignment : IValueConverter
    {
        /// <summary>
        /// <see cref="TextAlignmentEnum"/> to <see cref="TextAlignment"/>
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is TextAlignmentEnum)
            {
                TextAlignment textAlignment = (TextAlignmentEnum)value switch
                {
                    TextAlignmentEnum.Center => TextAlignment.Center,
                    TextAlignmentEnum.Left => TextAlignment.Left,
                    TextAlignmentEnum.Right => TextAlignment.Right,
                    TextAlignmentEnum.Justify => TextAlignment.Justify,
                };
                return textAlignment;
            }
            return TextAlignment.Center;
        }

        /// <summary>
        /// <see cref="TextAlignment"/> to <see cref="TextAlignmentEnum"/>
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value is TextAlignment)
            {
                var textAlignmentEnum = (TextAlignment)value switch
                {
                    TextAlignment.Center => TextAlignmentEnum.Center,
                    TextAlignment.Left => TextAlignmentEnum.Left,
                    TextAlignment.Right => TextAlignmentEnum.Right,
                    TextAlignment.Justify => TextAlignmentEnum.Justify,
                };
                return textAlignmentEnum;
            }
            return TextAlignmentEnum.Center;
        }
    }
}