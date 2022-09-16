using System;
using Windows.UI.Text;
using Windows.UI.Xaml.Data;

namespace LeafletsProj.Converts
{
    /// <summary>
    /// Converts a number (<see cref="ushort"/>) to a <see cref="FontWeight"/> value for text
    /// </summary>
    internal sealed class UshortToFontWeightConverter : IValueConverter
    {
        /// <summary>
        /// Converts <see cref="ushort"/> to <see cref="FontWeight"/>
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is ushort)
            {
                return new FontWeight() { Weight = (ushort)value };
            }
            return new FontWeight() { Weight = 400 };
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
            throw new NotImplementedException();
        }
    }
}