using LeafletsProj.Core.Models;
using LeafletsProj.Enums;
using LeafletsProj.Models;
using LeafletsProj.Services;
using LeafletsProj.ViewModel;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace LeafletsProj.View
{
    /// <summary>
    /// Страница для редактирования визитки/флайера
    /// </summary>
    public sealed partial class EditPage : Page
    {
        #region Private Fields

        /// <summary>
        /// Содержит экземпляр класса <see cref="EditPageViewModel"/>
        /// </summary>
        private EditPageViewModel _viewModel;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// Инициализирует экземпляр класса <see cref="EditPage"/>
        /// </summary>
        public EditPage()
        {
            _viewModel = ServiceLocator.EditPageViewModel;
            this.InitializeComponent();
        }

        #endregion Public Constructors

        #region Private Methods

        /// <summary>
        /// Обрабатывает событие изменения выбранного элемента у <see cref="AbstractsGridView"/>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AbstractsGridView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var abstractVM = AbstractsGridView.SelectedItem as DecorViewModel;

            if (abstractVM == null)
                return;

            _viewModel.AddImage(abstractVM.ImagePath, ImageTypeEnum.Abstract, AbstractsColorPicker.CurrentColor);

            AbstractsGridView.SelectedItem = null;
        }

        /// <summary>
        /// Обрабатывает событие нажатия на кнопку добавления линии-декора
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddLineButton_Click(object sender, RoutedEventArgs e)
        {
            var lineVM = LineComboBox.SelectedItem as DecorViewModel;
            _viewModel.AddImage(lineVM.ImagePath, ImageTypeEnum.Line, LineColorPicker.CurrentColor);
        }

        /// <summary>
        /// Обрабатывает событие изменения формата у листовок
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormatComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PaperFormat currentFormat = FormatComboBox.SelectedItem as PaperFormat;
            if (currentFormat == null)
                return;
        }

        /// <summary>
        /// Обрабатывает событие окончания загрузки страницы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (_viewModel.TypeOfPaper == typeof(Card))
            {
                _viewModel.CurrentCardControl = CardControl;
            }
            else
            {
                _viewModel.CurrentFlyerControl = FlyerControl;
                if (_viewModel.CurrentFlyer.Format == null)
                    FormatComboBox.SelectedIndex = 0;
            }
            LineComboBox.SelectedIndex = 0;
        }

        /// <summary>
        /// Обрабатывает событие изменения выбранного элемента у <see cref="PaintGridView"/>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PaintGridView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var paintVM = PaintGridView.SelectedItem as DecorViewModel;
            if (paintVM == null)
                return;

            _viewModel.AddImage(paintVM.ImagePath, ImageTypeEnum.Paint, PaintsColorPicker.CurrentColor);

            PaintGridView.SelectedItem = null;
        }

        /// <summary>
        /// Обрабатывает событие изменения выбранного элемента у <see cref="SignsGridView"/>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SignsGridView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var signVM = SignsGridView.SelectedItem as DecorViewModel;

            if (signVM == null)
                return;

            _viewModel.AddImage(signVM.ImagePath, ImageTypeEnum.Sign, SignsColorPicker.CurrentColor);

            SignsGridView.SelectedItem = null;
        }

        #endregion Private Methods
    }
}