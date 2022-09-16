using System;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace LeafletsProj.Converts
{
    /// <summary>
    /// Converts HEX string (0xFFFFFF) to <see cref="SolidColorBrush"/> value for text
    /// </summary>
    internal sealed class HexStringToSolidColorBrushConverter : IValueConverter
    {
        /// <summary>
        /// Converts string to <see cref="SolidColorBrush"/>
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            try
            {
                int rgb = System.Convert.ToInt32(value as string, 16);

                return new SolidColorBrush(Color.FromArgb(255, (byte)((rgb >> 16) & 0xff), (byte)((rgb >> 8) & 0xff), (byte)((rgb >> 0) & 0xff)));
            }
            catch
            {
                return new SolidColorBrush(Colors.Black);
            }
        }

        /// <summary>
        /// Method for reverse conversion
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            Color color = (value as SolidColorBrush).Color; ;
            string rgb = string.Format("0x{0}{1}{2}", color.R.ToString("X2"), color.G.ToString("X2"), color.B.ToString("X2"));
            return rgb;
        }
    }
}