using LeafletsProj.Models;
using LeafletsProj.Services;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace LeafletsProj.View
{
    /// <summary>
    /// Page for saving result
    /// </summary>
    public sealed partial class SavePage : Page
    {
        #region Public Constructors

        /// <summary>
        /// Initialize an instance of a class <see cref="SavePage"/>
        /// </summary>
        public SavePage()
        {
            this.InitializeComponent();
            Loaded += Page_Loaded;
        }

        #endregion Public Constructors

        #region Private Methods

        /// <summary>
        /// Handles the page loaded event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var viewModel = ServiceLocator.SavePageViewModel;
            if (viewModel.TypeOfPaper == typeof(Card))
            {
                viewModel.ElementToSave = CardControl;
                FlyerControl.Visibility = Visibility.Collapsed;
            }
            else
            {
                viewModel.ElementToSave = FlyerControl;
                CardControl.Visibility = Visibility.Collapsed;
            }
        }

        #endregion Private Methods
    }
}