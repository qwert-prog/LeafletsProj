using LeafletsProj.Models;
using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace LeafletsProj.Controls
{
    /// <summary>
    /// Control for managing the appearance of a flyer
    /// </summary>
    public sealed partial class FlyerControl : UserControl
    {
        #region Public Fields

        /// <summary>
        /// Dependency property <see cref="CurrentFlyer"/>
        /// </summary>
        public static readonly DependencyProperty CurrentFlyerProperty =
            DependencyProperty.Register("CurrentFlyer", typeof(Flyer), typeof(FlyerControl), new PropertyMetadata(null));

        /// <summary>
        /// Dependency property <see cref="IsDashVisible"/>
        /// </summary>
        public static readonly DependencyProperty IsDashVisibleProperty =
                    DependencyProperty.Register("IsDashVisible", typeof(bool), typeof(TextControl), new PropertyMetadata(true));

        #endregion Public Fields

        #region Private Fields

        /// <summary>
        /// Contains all <see cref="TextControl"/> in the current flyer
        /// </summary>
        private readonly List<ImageControl> _imageControls = new List<ImageControl>();

        /// <summary>
        /// Contains all <see cref="TextControl"/> in the current flyer
        /// </summary>
        private readonly List<TextControl> _textControls = new List<TextControl>();

        #endregion Private Fields

        #region Public Properties

        /// <summary>
        /// Gets or sets current flyer
        /// </summary>
        public Flyer CurrentFlyer
        {
            get { return (Flyer)GetValue(CurrentFlyerProperty); }
            set
            {
                SetValue(CurrentFlyerProperty, value);
                if (value != null)
                {
                    foreach (Text text in CurrentFlyer.FrontTextTemplates)
                    {
                        AddText(text);
                    }
                    foreach (ImageModel image in CurrentFlyer.FrontImageTemplates)
                    {
                        AddDecor(image, false);
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets a boolean flag of visibility of dash
        /// </summary>
        public bool IsDashVisible
        {
            get { return (bool)GetValue(IsDashVisibleProperty); }
            set { SetValue(IsDashVisibleProperty, value); }
        }

        #endregion Public Properties

        #region Public Constructors

        /// <summary>
        /// Initializes an instance of a class <see cref="FlyerControl"/>
        /// </summary>
        public FlyerControl()
        {
            this.InitializeComponent();
        }

        #endregion Public Constructors

        #region Public Methods

        /// <summary>
        /// Method that adds <see cref="ImageModel"/> to a given canvas
        /// </summary>
        /// <param name="newImage"></param>
        public void AddDecor(ImageModel newImage, bool isStartGenerate = true)
        {
            //Todo: переработать логику IsFocused
            newImage.IsFocused = false;
            if (isStartGenerate)
            {
                CurrentFlyer.FrontImageTemplates.Add(newImage);
            }
            ImageControl control = new ImageControl(newImage)
            {
                Factor = FlyerCanvas.Height / this.ActualHeight,
                IsDashVisible = this.IsDashVisible,
            };
            FlyerCanvas.Children.Add(control);
            Canvas.SetLeft(control, newImage.Left);
            Canvas.SetTop(control, newImage.Top);
            _imageControls.Add(control);
        }

        /// <summary>
        /// Method that adds <see cref="TextControl"/> to a given canvas
        /// </summary>
        /// <param name="targetCanvas"></param>
        /// <param name="newText"></param>
        public void AddText(Text newText = null)
        {
            if (CurrentFlyer == null)
            {
                return;
            }
            if (newText == null)
            {
                newText = new Text();
                CurrentFlyer.FrontTextTemplates.Add(newText);
            }
            TextControl control = new TextControl(newText)
            {
                Factor = FlyerCanvas.Height / this.ActualHeight,
                IsDashVisible = this.IsDashVisible,
            };
            FlyerCanvas.Children.Add(control);
            Canvas.SetLeft(control, newText.Left);
            Canvas.SetTop(control, newText.Top);
            _textControls.Add(control);
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// Handles the canvas resize event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FlyerCanvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.PreviousSize.Width == 0 || e.PreviousSize.Height == 0)
                return;
            foreach (TextControl textControl in _textControls)
            {
                textControl.UserText.Top /= (e.PreviousSize.Height / e.NewSize.Height);
                textControl.UserText.Left /= (e.PreviousSize.Width / e.NewSize.Width);
                textControl.UserText.FontSize = (int)Math.Round(textControl.UserText.FontSize / (e.PreviousSize.Height / e.NewSize.Height));
                textControl.Factor = FlyerCanvas.Height / this.ActualHeight;
                Canvas.SetLeft(textControl, textControl.UserText.Left);
                Canvas.SetTop(textControl, textControl.UserText.Top);
            }
            foreach (ImageControl imageControl in _imageControls)
            {
                imageControl.CurrentImage.Top /= (e.PreviousSize.Height / e.NewSize.Height);
                imageControl.CurrentImage.Left /= (e.PreviousSize.Width / e.NewSize.Width);
                imageControl.CurrentImage.Width /= (e.PreviousSize.Width / e.NewSize.Width);
                imageControl.CurrentImage.Height /= (e.PreviousSize.Height / e.NewSize.Height);
                imageControl.Factor = FlyerCanvas.Height / this.ActualHeight;
                Canvas.SetLeft(imageControl, imageControl.CurrentImage.Left);
                Canvas.SetTop(imageControl, imageControl.CurrentImage.Top);
            }
        }

        #endregion Private Methods
    }
}