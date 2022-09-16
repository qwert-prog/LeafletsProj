using LeafletsProj.Models;
using LeafletsProj.Services;
using System;
using Windows.Foundation;
using Windows.UI.Core;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

namespace LeafletsProj.Controls
{
    /// <summary>
    /// Class extension <see cref="RichEditBox"/>
    /// </summary>
    public static class RichEditBoxExtensions
    {
        #region Public Fields

        /// <summary>
        /// Dependency property <see cref="Interval"/>
        /// </summary>
        public static readonly DependencyProperty IntervalProperty =
            DependencyProperty.RegisterAttached("Interval", typeof(int), typeof(RichEditBox), new PropertyMetadata(10));

        #endregion Public Fields

        #region Public Methods

        /// <summary>
        /// Method for getting interval value
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int GetInterval(DependencyObject obj)
        {
            RichEditBox richEditBox = obj as RichEditBox;
            ITextParagraphFormat format = richEditBox.Document.GetDefaultParagraphFormat();
            return (int)format.LineSpacing;
        }

        /// <summary>
        /// Method for setting interval value
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        public static void SetInterval(DependencyObject obj, int value)
        {
            obj.SetValue(IntervalProperty, value);
            RichEditBox richEditBox = obj as RichEditBox;
            ITextParagraphFormat format = richEditBox.Document.GetDefaultParagraphFormat();
            format.SetLineSpacing(LineSpacingRule.Exactly, value);
            richEditBox.Document.SetDefaultParagraphFormat(format);
        }

        #endregion Public Methods
    }

    /// <summary>
    /// The usercontrol that stores the text
    /// </summary>
    public sealed partial class TextControl : UserControl
    {
        #region Public Fields

        /// <summary>
        /// Dependency property <see cref="IsDashVisible"/>
        /// </summary>
        public static readonly DependencyProperty IsDashVisibleProperty =
                    DependencyProperty.Register("IsDashVisible", typeof(bool), typeof(TextControl), new PropertyMetadata(true));

        /// <summary>
        /// Dependency property <see cref="IsFocused"/>
        /// </summary>
        public static readonly DependencyProperty IsFocusedProperty =
                    DependencyProperty.Register("IsFocused", typeof(bool), typeof(TextControl), new PropertyMetadata(false));

        #endregion Public Fields

        #region Private Fields

        /// <summary>
        /// Contains converting degrees to radians
        /// </summary>
        private const double RADIAN = Math.PI / 180;

        /// <summary>
        /// Contains scale factor
        /// </summary>
        private double _factor = 1;

        #endregion Private Fields

        #region Public Properties

        /// <summary>
        /// Gets or sets scale factor
        /// </summary>
        public double Factor
        {
            get => _factor;
            set
            {
                if (double.IsNaN(value))
                {
                    value = 1;
                }
                _factor = value;
                UpdateSizeIcon();
            }
        }

        /// <summary>
        /// Gets or sets a boolean flag of visibility of dash rectangle
        /// </summary>
        public bool IsDashVisible
        {
            get { return (bool)GetValue(IsDashVisibleProperty); }
            set { SetValue(IsDashVisibleProperty, value); }
        }

        /// <summary>
        /// Gets or sets a boolean flag indicating whether the control has focus
        /// </summary>
        public bool IsFocused
        {
            get { return (bool)GetValue(IsFocusedProperty); }
            set { SetValue(IsFocusedProperty, value); }
        }

        /// <summary>
        /// Gets or sets <see cref="Text"/>
        /// </summary>
        public Text UserText { get; set; }

        #endregion Public Properties

        #region Public Constructors

        /// <summary>
        /// Initializes an instance of a class <see cref="TextControl"/>
        /// </summary>
        /// <param name="text">Текст, который необходимо разместить</param>
        public TextControl(Text text)
        {
            text.Value = text.Value.Replace("\r", "");
            UserText = text;
            this.InitializeComponent();
            UserTextBox.Document.SetText(TextSetOptions.None, UserText.Value);
            UserTextBox.TextAlignment = TextAlignment.Left;
            GotFocus += TextControl_GotFocus;
            LostFocus += TextControl_LostFocus;
            ControlRotateTransform.Angle = UserText.Angle;
        }

        #endregion Public Constructors

        #region Private Methods

        /// <summary>
        /// Handle click event on close control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseEllipse_Tapped(object sender, TappedRoutedEventArgs e)
        {
            UserText.IsVisible = false;
        }

        /// <summary>
        /// Handling the event of the cursor entering the movement zone
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MovedRectangle_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new CoreCursor(CoreCursorType.SizeAll, 1);
        }

        /// <summary>
        /// Handling the move zone cursor leaving event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MovedRectangle_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new CoreCursor(CoreCursorType.Arrow, 1);
        }

        /// <summary>
        /// Rotates the control
        /// </summary>
        /// <param name="mousePosition">Текущее положение указателя мыши</param>
        private void Rotate(Point mousePosition)
        {
            Point transPoint = new Point((mousePosition.X), (mousePosition.Y));
            if (transPoint.X != 0 && transPoint.Y != 0)
            {
                double radians = Math.Atan(transPoint.Y / transPoint.X);
                double angle = radians / RADIAN;

                ControlRotateTransform.Angle = angle - 165; // изначально точка находится справа по центру.
                                                            // Но мы ее смещаем на 165 градусов, чтобы была в левом нижнем углу

                if (transPoint.X < 0)
                {
                    ControlRotateTransform.Angle += 180;
                }
                UserText.Angle = ControlRotateTransform.Angle;
            }
        }

        /// <summary>
        /// Handling a rotation manipulation event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RotateEllipse_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            if (double.IsNaN(this.ActualWidth))
            {
                return;
            }
            //Получаем координаты курсора относительно экрана
            Point coursorPositionInWindow = Window.Current.CoreWindow.PointerPosition;
            //Получаем координаты курсора относительно окна
            coursorPositionInWindow = new Point(coursorPositionInWindow.X - Window.Current.Bounds.X, coursorPositionInWindow.Y - Window.Current.Bounds.Y);

            //Получаем координаты элемента (можно наверное заменить на this) относительно окна приложения
            GeneralTransform ttv = this.TransformToVisual(Window.Current.Content);
            Point screenCoords = ttv.TransformPoint(new Point(this.ActualWidth / 2, this.ActualHeight / 2));

            //Расчитываем центральную точку элемента относительно экна
            double centerPositionElementX = screenCoords.X + this.ActualWidth / 2;
            double centerPositionElementY = screenCoords.Y + this.ActualHeight / 2;
            Point centerPositionElement = new Point(centerPositionElementX, centerPositionElementY);

            //Рассчитываем позицию курсора относительно центра элемента
            double coursorPositionX = coursorPositionInWindow.X - centerPositionElement.X + (this.ActualWidth / 2);
            double coursorPositionY = coursorPositionInWindow.Y - centerPositionElement.Y + (this.ActualHeight / 2);
            Point coursorPosition = new Point(coursorPositionX, coursorPositionY);

            Rotate(coursorPosition);
        }

        /// <summary>
        /// Handling the focus event
        /// </summary>
        private void TextControl_GotFocus(object sender, RoutedEventArgs e)
        {
            EventBusService.OnSelectedTextChanged(UserText);
            Canvas.SetZIndex(this, 100);
            IsFocused = true;
        }

        /// <summary>
        /// Handling the lost focus event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextControl_LostFocus(object sender, RoutedEventArgs e)
        {
            IsFocused = false;
            UserTextBox.Document.GetText(TextGetOptions.None, out string text);
            UserText.Value = text;
            Canvas.SetZIndex(this, 10);
        }

        /// <summary>
        /// Method for update UI for icon
        /// </summary>
        private void UpdateSizeIcon()
        {
            if (double.IsInfinity(Factor) || double.IsNaN(Factor))
                return;

            int defaultIconSize = 30;
            int defaultMargin = -17;
            int defaultIconMargin = 5;

            double scalDefaultIconSize = defaultIconSize * Factor;

            RotateGrid.Width = scalDefaultIconSize;
            CloseGrid.Width = scalDefaultIconSize;

            RotateGrid.Margin = new Thickness(defaultMargin * Factor);
            RotateIcon.Margin = new Thickness(defaultIconMargin * Factor);
            CloseGrid.Margin = new Thickness(defaultMargin * Factor);
            CloseIcon.Margin = new Thickness(defaultIconMargin * Factor);

            CloseGrid.Height = scalDefaultIconSize;
            RotateGrid.Height = scalDefaultIconSize;

            DashRectangle.StrokeDashArray = new DoubleCollection() { 0, 4 * Factor, 0 };
        }

        /// <summary>
        /// Handling the control's manipulation event
        /// </summary>
        private void UserControl_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            double left = Canvas.GetLeft(this);
            double top = Canvas.GetTop(this);

            Canvas.SetLeft(this, (left + e.Delta.Translation.X * Factor));
            Canvas.SetTop(this, (top + e.Delta.Translation.Y * Factor));

            UserText.Top = Canvas.GetTop(this);
            UserText.Left = Canvas.GetLeft(this);
        }

        #endregion Private Methods
    }
}