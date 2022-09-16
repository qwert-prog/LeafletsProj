using LeafletsProj.Models;
using LeafletsProj.Services;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace LeafletsProj.Controls
{
    /// <summary>
    /// Control for managing the appearance of a business card
    /// </summary>
    public sealed partial class CardControl : UserControl
    {
        #region Public Fields

        /// <summary>
        /// Dependency property <see cref="CurrentCard"/>
        /// </summary>
        public static readonly DependencyProperty CurrentCardProperty =
            DependencyProperty.Register("CurrentCard", typeof(Card), typeof(CardControl), new PropertyMetadata(null));

        /// <summary>
        /// Dependency property <see cref="IsDashVisible"/>
        /// </summary>
        public static readonly DependencyProperty IsDashVisibleProperty =
                    DependencyProperty.Register("IsDashVisible", typeof(bool), typeof(TextControl), new PropertyMetadata(true));

        #endregion Public Fields

        #region Private Fields

        /// <summary>
        /// Contains all <see cref="TextControl"/> in the current card
        /// </summary>
        private readonly List<ImageControl> _imageControls = new List<ImageControl>();

        /// <summary>
        /// Contains all <see cref="TextControl"/> in the current card
        /// </summary>
        private readonly List<TextControl> _textControls = new List<TextControl>();

        /// <summary>
        /// Contains selected <see cref="Canvas"/> for editing
        /// </summary>
        private Canvas _selectedCanvas;

        #endregion Private Fields

        #region Public Properties

        /// <summary>
        /// Gets or sets current card
        /// </summary>
        public Card CurrentCard
        {
            get { return (Card)GetValue(CurrentCardProperty); }
            set
            {
                SetValue(CurrentCardProperty, value);
                if (value != null)
                {
                    foreach (Text text in CurrentCard.FrontTextTemplates)
                    {
                        AddText(FrontTextCanvas, text);
                    }
                    foreach (Text text in CurrentCard.BackTextTemplates)
                    {
                        AddText(BackTextCanvas, text);
                    }
                    foreach (ImageModel image in CurrentCard.FrontImageTemplates)
                    {
                        AddDecor(image, FrontTextCanvas);
                    }
                    foreach (ImageModel image in CurrentCard.BackImageTemplates)
                    {
                        AddDecor(image, BackTextCanvas);
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
        /// Initializes an instance of a class <see cref="CardControl"/>
        /// </summary>
        public CardControl()
        {
            this.InitializeComponent();
        }

        #endregion Public Constructors

        #region Public Methods

        /// <summary>
        /// Method that adds <see cref="ImageModel"/> to a given canvas
        /// </summary>
        /// <param name="newImage"></param>
        public void AddDecor(ImageModel newImage, Canvas targetCanvas = null)
        {
            //Todo: переработать логику IsFocused
            newImage.IsFocused = false;
            if (targetCanvas == null)
            {
                if (_selectedCanvas == FrontTextCanvas)
                    CurrentCard.FrontImageTemplates.Add(newImage);
                else
                    CurrentCard.BackImageTemplates.Add(newImage);
                targetCanvas = _selectedCanvas;
            }
            ImageControl control = new ImageControl(newImage)
            {
                Factor = targetCanvas.Height / this.ActualHeight,
                IsDashVisible = this.IsDashVisible,
            };
            targetCanvas.Children.Add(control);
            Canvas.SetLeft(control, newImage.Left);
            Canvas.SetTop(control, newImage.Top);
            _imageControls.Add(control);
        }

        /// <summary>
        /// Method that adds <see cref="TextControl"/> to a given canvas
        /// </summary>
        /// <param name="targetCanvas"></param>
        /// <param name="newText"></param>
        public void AddText(Canvas targetCanvas = null, Text newText = null)
        {
            if (CurrentCard == null)
            {
                return;
            }
            if (newText == null)
            {
                newText = new Text();
                if (_selectedCanvas == FrontTextCanvas)
                    CurrentCard.FrontTextTemplates.Add(newText);
                else
                    CurrentCard.BackTextTemplates.Add(newText);
                targetCanvas = _selectedCanvas;
            }
            TextControl control = new TextControl(newText)
            {
                Factor = targetCanvas.Height / this.ActualHeight,
                IsDashVisible = this.IsDashVisible,
            };
            targetCanvas.Children.Add(control);
            Canvas.SetLeft(control, newText.Left);
            Canvas.SetTop(control, newText.Top);
            _textControls.Add(control);
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// Handles the control loaded event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (IsDashVisible)
            {
                CardListView.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// Handles the tapped event on the current control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControl_Tapped(object sender, TappedRoutedEventArgs e)
        {
            EventBusService.OnSelectedTextChanged(null);
        }

        /// <summary>
        /// Handles the resize event on the current control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Viewbox_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            foreach (TextControl control in _textControls)
            {
                control.Factor = FrontTextCanvas.Height / this.ActualHeight;
            }
            foreach (var control in _imageControls)
            {
                control.Factor = FrontTextCanvas.Height / this.ActualHeight;
            }
        }

        #endregion Private Methods
    }
}