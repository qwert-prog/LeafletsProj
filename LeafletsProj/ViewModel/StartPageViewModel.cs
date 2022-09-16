using LeafletsProj.Models;
using LeafletsProj.Services;
using LeafletsProj.View.ContentDialogs;
using IoC;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Utils.Navigation;
using Utils.Navigation.Interfaces;
using UWPToolkit.MVVM;
using UWPToolkit.ViewModels;

namespace LeafletsProj.ViewModel
{
    /// <summary>
    /// Represents view model for StartPage
    /// </summary>
    internal class StartPageViewModel : BaseViewModel, INavigateAcceptor
    {
        #region Private Fields

        /// <summary>
        /// Class instance <see cref="NavigationService"/>.
        /// </summary>
        private readonly INavigationService _navigationService;

        /// <summary>
        /// Contains selected card
        /// </summary>
        private Paper _selectedCard;

        #endregion Private Fields

        #region Public Properties

        /// <summary>
        /// Command to open selected card for editing
        /// </summary>
        public ICommand OpenSelectedItemCommand { get; private set; }

        /// <summary>
        /// Gets or sets selected card
        /// </summary>
        public Paper SelectedCard
        {
            get => _selectedCard;
            set
            {
                _selectedCard = value;
                OnPropertyChanged();
            }
        }

        #endregion Public Properties

        #region Public Constructors

        /// <summary>
        /// Initialize an instance of a class <see cref="StartPageViewModel"/>
        /// </summary>
        /// <param name="navigationService"></param>
        public StartPageViewModel(NavigationService navigationService)
        {
            _navigationService = navigationService;
            OpenSelectedItemCommand = new RelayCommand(OpenSelectedItem);
        }

        #endregion Public Constructors

        #region Public Methods

        ///<inheritdoc/>
        public void Notify(INavigationArgs navigationArgs)
        {
            ServiceLocator.AssetsService.UpdateCardsAndFlyersList();
            SelectedCard = null;
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// Method for navigation to the page to edit the card
        /// </summary>
        private void OpenSelectedItem()
        {
            if (SelectedCard == null)
            {
                return;
            }

            INavigationArgs navigationArgs = new NavigationArgs(SelectedCard);
            EditPageViewModel editPageVM = ServicesContainer.GetService<EditPageViewModel>();
            _navigationService.NavigateTo(editPageVM, navigationArgs);
        }

        #endregion Private Methods
    }
}