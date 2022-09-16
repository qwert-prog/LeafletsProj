using LeafletsProj.Core.Enums;
using LeafletsProj.Services;
using System.Linq;
using UWPToolkit.ViewModels;

namespace LeafletsProj.Models
{
    /// <summary>
    /// Текст для открытки
    /// </summary>
    public class Text : BaseViewModel
    {
        #region Private Fields

        /// <summary>
        /// Цвет текста. Строка в шестнадцатиричном виде. например 0хFFFFFF
        /// </summary>
        private string _color;

        /// <summary>
        /// Название шрифта
        /// </summary>
        private string _fontName;

        /// <summary>
        /// Размер шрифта
        /// </summary>
        private int _fontSize;

        /// <summary>
        /// Межстрочный интервал
        /// </summary>
        private int _interval;

        /// <summary>
        /// Виден ли текст
        /// </summary>
        private bool _isVisible;

        #endregion Private Fields

        #region Public Properties

        /// <summary>
        /// Угол поворота
        /// </summary>
        public double Angle { get; set; }

        /// <summary>
        /// Выравнивание текста
        /// </summary>
        public TextAlignmentEnum TextAlignment { get; set; }

        /// <summary>
        /// Возвращает и задает <see cref="_color"/>
        /// </summary>
        public string Color
        {
            get => _color;
            set
            {
                if (_color != value)
                {
                    _color = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Название шрифта
        /// </summary>
        public string FontName
        {
            get => _fontName;
            set
            {
                if (_fontName != value)
                {
                    _fontName = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Размер шрифта
        /// </summary>
        public int FontSize
        {
            get => _fontSize;
            set
            {
                if (_fontSize != value)
                {
                    _fontSize = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Толщина шрифта
        /// </summary>
        public ushort FontWeight { get; set; }

        /// <summary>
        /// Высота блока с надписью
        /// </summary>
        public double Height { get; set; }

        /// <summary>
        /// Возвращает и задает <see cref="_interval"/>
        /// </summary>
        public int Interval
        {
            get => _interval;
            set
            {
                if (_interval != value)
                {
                    _interval = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Устанавливает и возвращает <see cref="_isVisible"/>
        /// </summary>
        public bool IsVisible
        {
            get { return _isVisible; }
            set
            {
                if (_isVisible != value)
                {
                    _isVisible = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Отступ слева
        /// </summary>
        public double Left { get; set; }

        /// <summary>
        /// Отступ
        /// </summary>
        public string Margin { get; set; }

        /// <summary>
        /// Отступ сверху
        /// </summary>
        public double Top { get; set; }

        /// <summary>
        /// Значение текста
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Ширина блока снадписью
        /// </summary>
        public double Width { get; set; }

        #endregion Public Properties

        #region Public Constructors

        /// <summary>
        /// Инициализирует экземпляр класса <see cref="Text"/>
        /// </summary>
        public Text()
        {
            IsVisible = true;
            FontSize = 20;
            Value = "Some text";
            Width = 20;
            FontName = ServiceLocator.FontsService.FontsList.First().FontKey;
            FontWeight = 400;
            Margin = "0";
            Color = "0x000000";
            Interval = 20;
            TextAlignment = TextAlignmentEnum.Left;
        }

        #endregion Public Constructors
    }
}