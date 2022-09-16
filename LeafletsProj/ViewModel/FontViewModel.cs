using UWPToolkit.ViewModels;

namespace LeafletsProj.ViewModel
{
    /// <summary>
    /// Модель представления для шрифта
    /// </summary>
    public class FontViewModel : BaseViewModel
    {
        #region Private Fields

        /// <summary>
        /// Содержит логический флаг о премиальности шрифта
        /// </summary>
        private bool _isPremium;

        #endregion Private Fields

        #region Public Properties

        /// <summary>
        /// Предоставляет или задаёт полный путь до шрифта в папке "Assets"
        /// </summary>
        public string FontAssetPath { get; set; }

        /// <summary>
        /// Предоставляет или задает строковый ключ шрифта
        /// </summary>
        public string FontKey { get; set; }

        /// <summary>
        /// Предоставляет или задает логический флаг о премиальности шрифта
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