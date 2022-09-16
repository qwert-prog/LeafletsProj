using LeafletsProj.Models;
using LeafletsProj.View.ContentDialogs;
using IoC;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Utils.Navigation;
using Utils.Navigation.Interfaces;
using UWPToolkit.MVVM;
using UWPToolkit.ViewModels;
using Windows.UI.Xaml;

namespace LeafletsProj.ViewModel
{
    /// <summary>
    /// Represents view model for SavePage
    /// </summary>
    internal class SavePageViewModel : BaseViewModel, INavigateAcceptor
    {
        #region Private Fields

        /// <summary>
        /// Class instance <see cref="NavigationService"/>.
        /// </summary>
        private NavigationService _navigationService;

        #endregion Private Fields

        #region Public Properties

        /// <summary>
        /// Command to return on edit page
        /// </summary>
        public ICommand BackToEditPageCommand { get; private set; }

        /// <summary>
        /// Gets or sets current card for saving
        /// </summary>
        public Card CurrentCard { get; set; }

        /// <summary>
        /// Gets or sets current flyer for saving
        /// </summary>
        public Flyer CurrentFlyer { get; set; }

        /// <summary>
        /// Gets or sets <see cref="UIElement"/> to save
        /// </summary>
        public UIElement ElementToSave { get; set; }

        /// <summary>
        /// Command to open contnet dialog with save card
        /// </summary>
        public ICommand OpenSaveDialogAsyncCommand { get; private set; }

        /// <summary>
        /// Gets or sets type of current <see cref="Paper"/> for save
        /// </summary>
        public Type TypeOfPaper { get; set; }

        #endregion Public Properties

        #region Public Constructors

        /// <summary>
        /// Initialize an instance of a class <see cref="SavePageViewModel"/>
        /// </summary>
        /// <param name="navigationService"></param>
        public SavePageViewModel(NavigationService navigationService)
        {
            _navigationService = navigationService;
            BackToEditPageCommand = new RelayCommand(BackToEditPage);
            OpenSaveDialogAsyncCommand = new AsyncCommand(OpenSaveDialogAsync);
        }

        #endregion Public Constructors

        #region Public Methods

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
        /// Method for return to the edit page
        /// </summary>
        private void BackToEditPage()
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
            CurrentCard = null;
            CurrentFlyer = null;
            EditPageViewModel startPageVM = ServicesContainer.GetService<EditPageViewModel>();
            _navigationService.NavigateTo(startPageVM, navigationArgs);
        }

        /// <summary>
        /// Method to open custom content dialog for save result
        /// </summary>
        /// <returns></returns>
        private async Task OpenSaveDialogAsync()
        {
            SaveContentDialog saveContentDialog = new SaveContentDialog(ElementToSave, TypeOfPaper);
            await saveContentDialog.ShowAsync();
        }

        #endregion Private Methods
    }
}