using System;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace LeafletsProj.View
{
    /// <summary>
    /// Кастомный лоадер (экран загрузки приложения)
    /// </summary>
    public sealed partial class ExtendedSplash : Page
    {
        #region Private Fields

        /// <summary>
        /// Содержит экземпляр класса <see cref="SplashScreen"/> текущего приложения
        /// </summary>
        private SplashScreen _splash;

        /// <summary>
        /// Содержит экземпляр класса <see cref="Rect"/> в котором
        /// хранятся координаты изображения заставки
        /// </summary>
        private Rect _splashImageRect;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// Инициализирует экземпляр класса <see cref="ExtendedSplash"/>
        /// </summary>
        /// <param name="splashscreen"></param>
        /// <param name="loadState"></param>
        public ExtendedSplash(SplashScreen splashscreen, bool loadState)
        {
            InitializeComponent();

            // Listen for window resize events to reposition the extended splash screen image accordingly.
            // This is important to ensure that the extended splash screen is formatted properly in response to snapping, unsnapping, rotation, etc...
            Window.Current.SizeChanged += new WindowSizeChangedEventHandler(ExtendedSplash_OnResize);

            _splash = splashscreen;

            if (_splash != null)
            {
                // Retrieve the window coordinates of the splash screen image.
                _splashImageRect = _splash.ImageLocation;
                PositionImage();

                // Optional: Add a progress ring to your splash screen to show users that content is loading
                PositionRing();
            }

            // Restore the saved session state if necessary
            RestoreState(loadState);
        }

        #endregion Public Constructors

        #region Private Methods

        /// <summary>
        /// Обрабатывает событие ресайза лоадера
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExtendedSplash_OnResize(Object sender, WindowSizeChangedEventArgs e)
        {
            // Safely update the extended splash screen image coordinates. This function will be fired in response to snapping, unsnapping, rotation, etc...
            if (_splash != null)
            {
                // Update the coordinates of the splash screen image.
                _splashImageRect = _splash.ImageLocation;
                PositionImage();
                PositionRing();
            }
        }

        /// <summary>
        /// Задает позицию для иконки приложения
        /// </summary>
        private void PositionImage()
        {
            extendedSplashImage.SetValue(Canvas.LeftProperty, _splashImageRect.X);
            extendedSplashImage.SetValue(Canvas.TopProperty, _splashImageRect.Y);
            extendedSplashImage.Height = _splashImageRect.Height;
            extendedSplashImage.Width = _splashImageRect.Width;
        }

        /// <summary>
        /// Задает позицию для <see cref="ProgressRing"/>
        /// </summary>
        private void PositionRing()
        {
            splashProgressRing.SetValue(Canvas.LeftProperty, _splashImageRect.X + (_splashImageRect.Width * 0.5) - (splashProgressRing.Width * 0.5));
            splashProgressRing.SetValue(Canvas.TopProperty, (_splashImageRect.Y + _splashImageRect.Height + _splashImageRect.Height * 0.1));
        }

        /// <summary>
        /// Метод для восстановления сохраненного состояния сеанса
        /// </summary>
        /// <param name="loadState"></param>
        private void RestoreState(bool loadState)
        {
            if (loadState)
            {
                // TODO: write code to load state
            }
        }

        #endregion Private Methods
    }
}