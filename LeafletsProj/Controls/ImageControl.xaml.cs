using LeafletsProj.Core.Enums;
using LeafletsProj.Core.Models;
using LeafletsProj.Models;
using LeafletsProj.Services;
using System;
using Windows.Foundation;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Shapes;

namespace LeafletsProj.Controls
{
    /// <summary>
    /// The usercontrol that stores the image
    /// </summary>
    public sealed partial class ImageControl : UserControl
    {
        #region Public Fields

        /// <summary>
        /// Dependency property <see cref="IsDashVisible"/>
        /// </summary>
        public static readonly DependencyProperty IsDashVisibleProperty =
                    DependencyProperty.Register("IsDashVisible", typeof(bool), typeof(TextControl), new PropertyMetadata(true));

        #endregion Public Fields

        #region Private Fields

        /// <summary>
        /// Contains scale factor
        /// </summary>
        private double _factor = 1;

        #endregion Private Fields

        #region Public Properties

        /// <summary>
        /// Gets or sets instance of a class <see cref="ImageModel"/>
        /// </summary>
        public ImageModel CurrentImage { get; set; }

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

        #endregion Public Properties

        #region Private Properties

        /// <summary>
        /// Contains the operation of subtracting two <see cref="double"/> numbers
        /// </summary>
        private Func<double, double, double> Dif => ((lv, rv) => lv - rv);

        /// <summary>
        /// Contains the operation of adding two <see cref="double"/> numbers
        /// </summary>
        private Func<double, double, double> Sum => ((lv, rv) => lv + rv);

        #endregion Private Properties

        #region Public Constructors

        /// <summary>
        /// Initializes an instance of a class <see cref="TextControl"/>
        /// </summary>
        public ImageControl(ImageModel image)
        {
            CurrentImage = image;
            this.InitializeComponent();
            ControlRotateTransform.Angle = CurrentImage.Angle;
        }

        #endregion Public Constructors

        #region Private Methods

        /// <summary>
        /// Method for determining which coordinate quarter a given angle is in
        /// </summary>
        /// <param name="angle"></param>
        /// <returns></returns>
        private CoordinateQuarter CheckCoordinateQuarter(double angle)
        {
            if ((angle >= 270 && angle < 360) || (angle < 0 && angle >= -90))
            {
                return CoordinateQuarter.IV;
            }
            else if ((angle >= 0 && angle < 90) || (angle < -270 && angle >= 0))
            {
                return CoordinateQuarter.I;
            }
            else if ((angle >= 90 && angle < 180) || (angle < -180 && angle >= -270))
            {
                return CoordinateQuarter.II;
            }
            else
            {
                return CoordinateQuarter.III;
            }
        }

        /// <summary>
        /// Handle click event on close control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseEllipse_Tapped(object sender, TappedRoutedEventArgs e)
        {
            CurrentImage.IsVisible = false;
        }

        /// <summary>
        /// Method for obtaining operations on the height and width of the image,
        /// depending on which coordinate quarter the control for resize is located in
        /// </summary>
        /// <param name="quarter"></param>
        /// <returns></returns>
        private (Func<double, double, double> yFunc, Func<double, double, double> xFunc) GetOperationsByCoordQuarter(CoordinateQuarter quarter)
        {
            var funcs = quarter switch
            {
                CoordinateQuarter.I => (Dif, Sum),
                CoordinateQuarter.II => (Sum, Sum),
                CoordinateQuarter.III => (Sum, Dif),
                CoordinateQuarter.IV => (Dif, Dif),
            };

            return funcs;
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
        /// Handling the resize image event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Point_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            try
            {
                double resultAngle = CurrentImage.Angle;
                Rectangle rectangleOfResize = sender as Rectangle;
                //Высчитывает угол точки относительно угла поворота
                if (rectangleOfResize == TopLeftPoint)
                {
                    resultAngle -= Math.Atan(TargetImage.Width / TargetImage.Height) * 180 / Math.PI;
                }
                else if (rectangleOfResize == BottomRightPoint)
                {
                    resultAngle += (180 - Math.Atan(TargetImage.Width / TargetImage.Height) * 180 / Math.PI);
                }

                //Если угол больше 360 градусов
                resultAngle %= 360;

                //Проверяем в какой координатной четверти находится точка
                double yDiff = e.Delta.Translation.Y * Factor;
                double xDiff = e.Delta.Translation.X * Factor;
                CoordinateQuarter coordQuarter = CheckCoordinateQuarter(resultAngle);
                var axisOperations = GetOperationsByCoordQuarter(coordQuarter);
                CurrentImage.Height = axisOperations.yFunc(CurrentImage.Height, yDiff);
                CurrentImage.Width = axisOperations.xFunc(CurrentImage.Width, xDiff);
            }
            catch (Exception)
            {
                CurrentImage.Height = AppConst.MinHeightImage;
                CurrentImage.Width = AppConst.MinWidthImage;
            }
            if (CurrentImage.Height < AppConst.MinHeightImage)
            {
                CurrentImage.Height = AppConst.MinHeightImage;
            }
            if (CurrentImage.Width < AppConst.MinWidthImage)
            {
                CurrentImage.Width = AppConst.MinWidthImage;
            }
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
                double angle = radians / (Math.PI / 180);

                int shiftAngle = -165;
                int turnAngle = 180;

                ControlRotateTransform.Angle = angle + shiftAngle; // изначально точка находится справа по центру.
                                                                   // Но мы ее смещаем на 165 градусов, чтобы была в левом нижнем углу

                if (transPoint.X < 0)
                {
                    ControlRotateTransform.Angle += turnAngle;
                }

                CurrentImage.Angle = ControlRotateTransform.Angle;
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
            Windows.UI.Xaml.Media.GeneralTransform ttv = this.TransformToVisual(Window.Current.Content);
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
        /// Method for update UI for icon
        /// </summary>
        private void UpdateSizeIcon()
        {
            if (double.IsInfinity(Factor) || double.IsNaN(Factor))
                return;

            int defaultIconSize = 30;
            int defaultMargin = -17;
            int defaultIconMargin = 3;
            int resizeIconSize = 15;

            double scalResizeIconSize = resizeIconSize * Factor;
            double scalDefaultIconSize = defaultIconSize * Factor;

            TopLeftPoint.Width = scalResizeIconSize;
            BottomRightPoint.Width = scalResizeIconSize;
            TopLeftPoint.Height = scalResizeIconSize;
            BottomRightPoint.Height = scalResizeIconSize;

            RotateGrid.Margin = new Thickness(defaultMargin * Factor);
            RotateIcon.Margin = new Thickness(defaultIconMargin * Factor);
            CloseGrid.Margin = new Thickness(defaultMargin * Factor);
            CloseIcon.Margin = new Thickness(defaultIconMargin * Factor);

            RotateGrid.Width = scalDefaultIconSize;
            CloseGrid.Width = scalDefaultIconSize;
            RotateGrid.Height = scalDefaultIconSize;
            CloseGrid.Height = scalDefaultIconSize;
        }

        /// <summary>
        /// Handling the control's manipulation event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControl_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            double left = Canvas.GetLeft(this);
            double top = Canvas.GetTop(this);

            Canvas.SetLeft(this, (left + e.Delta.Translation.X * Factor));
            Canvas.SetTop(this, (top + e.Delta.Translation.Y * Factor));

            CurrentImage.Top = Canvas.GetTop(this);
            CurrentImage.Left = Canvas.GetLeft(this);
        }

        /// <summary>
        /// Handles the tapped control event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControl_Tapped(object sender, TappedRoutedEventArgs e)
        {
            CurrentImage.IsFocused = !CurrentImage.IsFocused;
            if (CurrentImage.IsFocused)
            {
                EventBusService.OnSelectedImageChanged(CurrentImage);
                Canvas.SetZIndex(this, 100);
            }
            else
            {
                Canvas.SetZIndex(this, 10);
            }
        }

        #endregion Private Methods
    }
}