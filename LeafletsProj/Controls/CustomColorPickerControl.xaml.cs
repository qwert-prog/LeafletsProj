using LeafletsProj.Core.Models;
using System.Collections.Generic;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace LeafletsProj.Controls
{
    /// <summary>
    /// Control to select the color of the element
    /// </summary>
    public sealed partial class CustomColorPickerControl : UserControl
    {
        #region Public Fields

        /// <summary>
        /// Dependency property <see cref="CurrentColor"/>
        /// </summary>
        public static readonly DependencyProperty CurrentColorProperty =
            DependencyProperty.Register("CurrentColor", typeof(Color), typeof(CustomColorPickerControl), new PropertyMetadata(Colors.Black));

        /// <summary>
        /// Dependency property <see cref="CurrentFlyout"/>
        /// </summary>
        public static readonly DependencyProperty CurrentFlyoutProperty =
            DependencyProperty.Register("CurrentFlyout", typeof(Flyout), typeof(CustomColorPickerControl), new PropertyMetadata(default));

        #endregion Public Fields

        #region Private Fields

        /// <summary>
        /// Contains a list of colors in string representation for examples
        /// </summary>
        private readonly List<RgbaColor> _colors;

        #endregion Private Fields

        #region Public Properties

        /// <summary>
        /// Gets or sets current color in string format
        /// </summary>
        public Color CurrentColor
        {
            get { return (Color)GetValue(CurrentColorProperty); }
            set
            {
                if (CurrentColor != value)
                {
                    SetValue(CurrentColorProperty, value);
                }
            }
        }

        /// <summary>
        /// Gets or sets flyout which contains this control
        /// </summary>
        public Flyout CurrentFlyout
        {
            get { return (Flyout)GetValue(CurrentFlyoutProperty); }
            set { SetValue(CurrentFlyoutProperty, value); }
        }

        #endregion Public Properties

        #region Public Constructors

        /// <summary>
        /// Initialize an instance of a class <see cref="CustomColorPickerControl"/>
        /// </summary>
        public CustomColorPickerControl()
        {
            _colors = new List<RgbaColor>();
            foreach (int color in AppConst.Colors)
            {
                _colors.Add(new RgbaColor(color));
            }
            this.InitializeComponent();
        }

        #endregion Public Constructors

        #region Private Methods

        /// <summary>
        /// Handles the selection changed event of the examples colors list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExamplesColorGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            RgbaColor selectedColor = (RgbaColor)e.ClickedItem;
            CurrentColor = selectedColor.Color;
        }

        #endregion Private Methods

        /// <summary>
        /// Handles the event of clicking on the button to close
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            CurrentFlyout.Hide();
        }
    }
}