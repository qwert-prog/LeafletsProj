using UWPToolkit.ViewModels;

namespace LeafletsProj.ViewModel
{
    /// <summary>
    /// Модель представления декора
    /// </summary>
    internal class DecorViewModel : BaseViewModel
    {
        #region Private Fields

        /// <summary>
        /// Содержит логический флаг о премиальности декора
        /// </summary>
        private bool _isPremium = true;

        #endregion Private Fields

        #region Public Properties

        /// <summary>
        /// Предоставляет или задает полный путь до изображения декора
        /// </summary>
        public string ImagePath { get; set; }

        /// <summary>
        /// Предоставляет или задает логический флаг о премиальности декора
        /// </summary>
        public bool IsPremium
        {
            get => _isPremium;
            set
            {
                if (_isPremium != value)
                {
                    _isPremium = value;
                    OnPropertyChanged();
                }
            }
        }

        #endregion Public Properties
    }
}