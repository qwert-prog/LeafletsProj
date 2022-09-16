using LeafletsProj.Controls;
using LeafletsProj.Core.Models;
using LeafletsProj.Enums;
using LeafletsProj.Models;
using LeafletsProj.Services;
using IoC;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Utils.Navigation;
using Utils.Navigation.Interfaces;
using UWPToolkit.MVVM;
using UWPToolkit.ViewModels;
using Windows.UI;

namespace LeafletsProj.ViewModel
{
    /// <summary>
    /// Represents view model for EditPage
    /// </summary>
    internal class EditPageViewModel : BaseViewModel, INavigateAcceptor
    {
        #region Private Fields

        /// <summary>
        /// Contains current card
        /// </summary>
        private Card _currentCard;

        /// <summary>
        /// Contains current flyer
        /// </summary>
        private Flyer _currentFlyer;

        /// <summary>
        /// Contains service for working with fonts
        /// </summary>
        private FontsService _fontsService;

        /// <summary>
        /// Class instance <see cref="NavigationService"/>.
        /// </summary>
        private NavigationService _navigationService;

        /// <summary>
        /// Contains sekected image abstract
        /// </summary>
        private ImageModel _selectedAbstract;

        /// <summary>
        /// Contains selected font
        /// </summary>
        private FontViewModel _selectedFont;

        /// <summary>
        /// Contains sekected image line
        /// </summary>
        private ImageModel _selectedLine;

        /// <summary>
        /// Contains sekected image paint
        /// </summary>
        private ImageModel _selectedPaint;

        /// <summary>
        /// Contains sekected image sign
        /// </summary>
        private ImageModel _selectedSign;

        /// <summary>
        /// Contains selected text
        /// </summary>
        private Text _selectedText;

        #endregion Private Fields

        #region Public Properties

        /// <summary>
        /// Command for add text on current <see cref="Paper"/>
        /// </summary>
        public ICommand AddTextCommand { get; private set; }

        /// <summary>
        /// Command to return the default size of the current flyer
        /// </summary>
        public ICommand ReturnDefaulSizeForFlyerCommand { get; private set; }

        /// <summary>
        /// Gets list of fonts
        /// </summary>
        /*public  AllFontsList { get => _fontsService.GetAllFonts(); }*/

        /// <summary>
        /// Command to return on start page
        /// </summary>
        public ICommand BackToStartPageCommand { get; private set; }

        /// <summary>
        /// Gets or sets current card
        /// </summary>
        public Card CurrentCard
        {
            get => _currentCard;
            private set
            {
                if (_currentCard != value)
                {
                    _currentCard = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets instance <see cref="CardControl"/>
        /// </summary>
        public CardControl CurrentCardControl { get; set; }

        /// <summary>
        /// Gets or sets current flyer
        /// </summary>
        public Flyer CurrentFlyer
        {
            get => _currentFlyer;
            private set
            {
                if (_currentFlyer != value)
                {
                    _currentFlyer = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets instance <see cref="FlyerControl"/>
        /// </summary>
        public FlyerControl CurrentFlyerControl { get; set; }

        /// <summary>
        /// Gets or sets list of font sizes
        /// </summary>
        public ObservableCollection<int> FontSizes { get; set; } = new ObservableCollection<int>();

        /// <summary>
        /// Gets or sets list of line spacing values
        /// </summary>
        public ObservableCollection<int> IntervalSizes { get; set; } = new ObservableCollection<int>();

        /// <summary>
        /// Command to open selected card for saving
        /// </summary>
        public ICommand NavigationSavePageCommand { get; private set; }

        /// <summary>
        /// Gets or sets selected image abstract
        /// </summary>
        public ImageModel SelectedAbstract
        {
            get => _selectedAbstract;
            set
            {
                _selectedAbstract = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets selected font
        /// </summary>
        public FontViewModel SelectedFont
        {
            get => _selectedFont;
            set
            {
                _selectedFont = value;
                if (value != null && SelectedText != null)
                {
                    SelectedText.FontName = value.FontAssetPath;
                }
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets selected image line
        /// </summary>
        public ImageModel SelectedLine
        {
            get => _selectedLine;
            set
            {
                _selectedLine = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets selected image paint
        /// </summary>
        public ImageModel SelectedPaint
        {
            get => _selectedPaint;
            set
            {
                _selectedPaint = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets selected image sign
        /// </summary>
        public ImageModel SelectedSign
        {
            get => _selectedSign;
            set
            {
                _selectedSign = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets selected text
        /// </summary>
        public Text SelectedText
        {
            get => _selectedText;
            set
            {
                _selectedText = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets type of current <see cref="Paper"/> for edit
        /// </summary>
        public Type TypeOfPaper { get; set; }

        #endregion Public Properties

        #region Public Constructors

        /// <summary>
        /// Initialize an instance of a class <see cref="EditPageViewModel"/>
        /// </summary>
        /// <param name="navigationService"></param>
        public EditPageViewModel(NavigationService navigationService)
        {
            for (int i = AppConst.MinimumFontSize; i <= AppConst.MaximumFontSize; i++)
            {
                FontSizes.Add(i);
            }
            for (double i = AppConst.MinimumInterval; i <= AppConst.MaximumInterval; i++)
            {
                IntervalSizes.Add((int)i);
            }
            SelectedLine = new ImageModel(ImageTypeEnum.Line);
            SelectedSign = new ImageModel(ImageTypeEnum.Sign);
            SelectedAbstract = new ImageModel(ImageTypeEnum.Abstract);
            SelectedPaint = new ImageModel(ImageTypeEnum.Paint);
            _fontsService = ServiceLocator.FontsService;
            _navigationService = navigationService;
            BackToStartPageCommand = new RelayCommand(BackToStartPage);
            NavigationSavePageCommand = new RelayCommand(NavigationSavePage);
            AddTextCommand = new RelayCommand(AddText);
            ReturnDefaulSizeForFlyerCommand = new RelayCommand(ReturnDefaulSizeForFlyer);

            EventBusService.SelectedTextChanged += EventBusService_SelectedTextChanged;
            EventBusService.SelectedImageChanged += EventBusService_SelectedImageChanged;
        }

        #endregion Public Constructors

        #region Public Methods

        /// <summary>
        /// Method for adding image in the target control
        /// </summary>
        /// <param name="imagePath"></param>
        public void AddImage(string imagePath, ImageTypeEnum imageType, Color color)
        {
            string rgb = string.Format($"0x{color.R.ToString("X2")}{color.G.ToString("X2")}{color.B.ToString("X2")}");
            ImageModel image = new ImageModel(imageType, imagePath, rgb);
            if (TypeOfPaper == typeof(Card))
            {
                CurrentCardControl.AddDecor(image);
            }
            else
            {
                CurrentFlyerControl.AddDecor(image);
            }
        }

        ///<inheritdoc/>
        public void Notify(INavigationArgs navigationArgs)
        {
            if (navigationArgs.TryNavigationDataCast(out Card card))
            {
                CurrentCard = card;
                TypeOfPaper = CurrentCard.GetType();
            }
            if (navigationArgs.TryNavigationDataCast(out Flyer flyer))
            {
                CurrentFlyer = flyer;
                TypeOfPaper = CurrentFlyer.GetType();
            }
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// Method for add text on current <see cref="Paper"/>
        /// </summary>
        private void AddText()
        {
            if (TypeOfPaper == typeof(Card))
                CurrentCardControl.AddText();
            else
                CurrentFlyerControl.AddText();
        }

        /// <summary>
        /// Method for return the default size of the current flyer
        /// </summary>
        private void ReturnDefaulSizeForFlyer()
        {
            if (TypeOfPaper == typeof(Flyer))
            {
                CurrentFlyer.Width = AppConst.DefaultWidthFlyer;
                CurrentFlyer.Height = AppConst.DefaultHeightFlyer;
            }
        }

        /// <summary>
        /// Method for return to the start page
        /// </summary>
        private void BackToStartPage()
        {
            StartPageViewModel startPageVM = ServicesContainer.GetService<StartPageViewModel>();
            _navigationService.NavigateTo(startPageVM, default);
            ResetModels();
        }

        /// <summary>
        /// Handling the selected image change event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EventBusService_SelectedImageChanged(object sender, EventArgs e)
        {
            ImageModel imageSender = sender as ImageModel;

            if (imageSender == null)
                return;

            switch (imageSender.Type)
            {
                case ImageTypeEnum.Line:
                    if (SelectedLine != null && SelectedLine != imageSender)
                        SelectedLine.IsFocused = false;
                    SelectedLine = imageSender;
                    break;

                case ImageTypeEnum.Paint:
                    if (SelectedPaint != null && SelectedPaint != imageSender)
                        SelectedPaint.IsFocused = false;
                    SelectedPaint = imageSender;
                    break;

                case ImageTypeEnum.Abstract:
                    if (SelectedAbstract != null && SelectedAbstract != imageSender)
                        SelectedAbstract.IsFocused = false;
                    SelectedAbstract = imageSender;
                    break;

                case ImageTypeEnum.Sign:
                    if (SelectedSign != null && SelectedSign != imageSender)
                        SelectedSign.IsFocused = false;
                    SelectedSign = imageSender;
                    break;
            }
        }

        /// <summary>
        /// Handling the selected text change event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EventBusService_SelectedTextChanged(object sender, EventArgs e)
        {
            SelectedText = sender as Text;
            if (SelectedText != null)
            {
                SelectedFont = _fontsService.GetFontByName(SelectedText.FontName);
            }
        }

        /// <summary>
        /// Method for navigation to the page to save the card
        /// </summary>
        private void NavigationSavePage()
        {
            INavigationArgs navigationArgs;
            if (TypeOfPaper == typeof(Card))
            {
                navigationArgs = new NavigationArgs(CurrentCard);
            }
            else
            {
                navigationArgs = new NavigationArgs(CurrentFlyer);
            }
            SavePageViewModel savePageVM = ServicesContainer.GetService<SavePageViewModel>();
            _navigationService.NavigateTo(savePageVM, navigationArgs);
            ResetModels();
        }

        /// <summary>
        /// Method for sets null value for models
        /// </summary>
        private void ResetModels()
        {
            CurrentFlyer = null;
            CurrentCard = null;
            SelectedText = null;
            SelectedAbstract = null;
            SelectedLine = null;
            SelectedPaint = null;
            SelectedSign = null;
            CurrentCardControl = null;
            CurrentFlyerControl = null;
        }

        #endregion Private Methods
    }
}