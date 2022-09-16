using LeafletsProj.ViewModel;
using System.Collections.Generic;

namespace LeafletsProj.Services
{
    /// <summary>
    /// Сервис для работы со шрифтами
    /// </summary>
    public class FontsService
    {
        #region Public Fields

        /// <summary>
        /// Предоставляет или задает список шрифтов
        /// Каждый шрифт представляет собой экземпляр класса <see cref="FontViewModel"/>
        /// </summary>
        public List<FontViewModel> FontsList = new List<FontViewModel>()
        {
            new FontViewModel(){ FontKey = "NotoSerifDisplay-Bold", FontAssetPath="ms-appx:///Assets/Fonts/NotoSerifDisplay-Bold.ttf#Noto Serif Display", IsPremium = false },
            new FontViewModel(){ FontKey = "NotoSerifDisplay-Regular", FontAssetPath="ms-appx:///Assets/Fonts/NotoSerifDisplay-Regular.ttf#Noto Serif Display", IsPremium = false },
            new FontViewModel(){ FontKey = "Yeseva One", FontAssetPath="ms-appx:///Assets/Fonts/YesevaOne-Regular.ttf#Yeseva One", IsPremium = true },
            new FontViewModel(){ FontKey = "PT Serif", FontAssetPath="ms-appx:///Assets/Fonts/PTSerif-Regular.ttf#PT Serif", IsPremium = true },
            new FontViewModel(){ FontKey = "Cormorant-Medium", FontAssetPath="ms-appx:///Assets/Fonts/Cormorant-Medium.ttf#Cormorant Medium", IsPremium = true },
            new FontViewModel(){ FontKey = "CormorantGaramond-Bold", FontAssetPath="ms-appx:///Assets/Fonts/CormorantGaramond-Bold.ttf#Cormorant Garamond", IsPremium = true },
            new FontViewModel(){ FontKey = "Montserrat-Bold", FontAssetPath="ms-appx:///Assets/Fonts/Montserrat-Bold.ttf#Montserrat", IsPremium = true },
            new FontViewModel(){ FontKey = "Montserrat-Medium", FontAssetPath="ms-appx:///Assets/Fonts/Montserrat-Medium.ttf#Montserrat", IsPremium = true },
            new FontViewModel(){ FontKey = "Montserrat-Regular", FontAssetPath="ms-appx:///Assets/Fonts/Montserrat-Regular.ttf#Montserrat", IsPremium = true },
            new FontViewModel(){ FontKey = "Montserrat-ExtraBold", FontAssetPath="ms-appx:///Assets/Fonts/Montserrat-ExtraBold.ttf#Montserrat", IsPremium = true },
            new FontViewModel(){ FontKey = "Montserrat-SemiBold", FontAssetPath="ms-appx:///Assets/Fonts/Montserrat-SemiBold.ttf#Montserrat", IsPremium = true },
            new FontViewModel(){ FontKey = "Andantino", FontAssetPath="ms-appx:///Assets/Fonts/Andantino_script.ttf#Andantino script", IsPremium = true },
            new FontViewModel(){ FontKey = "Tinos", FontAssetPath="ms-appx:///Assets/Fonts/Tinos-Regular.ttf#Tinos", IsPremium = true },
            new FontViewModel(){ FontKey = "Tinos-Bold", FontAssetPath="ms-appx:///Assets/Fonts/Tinos-Bold.ttf#Tinos", IsPremium = true },
            new FontViewModel(){ FontKey = "Roboto", FontAssetPath="ms-appx:///Assets/Fonts/Roboto-Regular.ttf#Roboto", IsPremium = true },
            new FontViewModel(){ FontKey = "Roboto-Bold", FontAssetPath="ms-appx:///Assets/Fonts/Roboto-Bold.ttf#Roboto", IsPremium = true },
            new FontViewModel(){ FontKey = "BernardoModaContrast", FontAssetPath="ms-appx:///Assets/Fonts/bernardo-moda.modacontrast.ttf#Bernardo Moda Contrast", IsPremium = true },
            new FontViewModel(){ FontKey = "Comfortaa-Bold", FontAssetPath="ms-appx:///Assets/Fonts/Comfortaa-Bold.ttf#Comfortaa", IsPremium = true },
            new FontViewModel(){ FontKey = "CormorantGaramond-BoldItalic", FontAssetPath="ms-appx:///Assets/Fonts/CormorantGaramond-BoldItalic.ttf#Cormorant Garamond", IsPremium = true },
            new FontViewModel(){ FontKey = "CormorantGaramond-SemiBold", FontAssetPath="ms-appx:///Assets/Fonts/CormorantGaramond-SemiBold.ttf#Cormorant Garamond", IsPremium = true },
            new FontViewModel(){ FontKey = "CormorantInfant-Bold", FontAssetPath="ms-appx:///Assets/Fonts/CormorantInfant-Bold.ttf#Cormorant Infant", IsPremium = true },
            new FontViewModel(){ FontKey = "Ubuntu-Bold", FontAssetPath="ms-appx:///Assets/Fonts/Ubuntu-Bold.ttf#Ubuntu", IsPremium = true },
            new FontViewModel(){ FontKey = "Ubuntu-Medium", FontAssetPath="ms-appx:///Assets/Fonts/Ubuntu-Medium.ttf#Ubuntu", IsPremium = true },
            new FontViewModel(){ FontKey = "Ubuntu", FontAssetPath="ms-appx:///Assets/Fonts/Ubuntu-Regular.ttf#Ubuntu", IsPremium = true },
            new FontViewModel(){ FontKey = "GoodVibesPro", FontAssetPath="ms-appx:///Assets/Fonts/GoodVibesCyr.ttf#Good Vibes Pro", IsPremium = true },
            new FontViewModel(){ FontKey = "Oswald-Bold", FontAssetPath="ms-appx:///Assets/Fonts/Oswald-Bold.ttf#Oswald", IsPremium = true },
            new FontViewModel(){ FontKey = "Oswald-Medium", FontAssetPath="ms-appx:///Assets/Fonts/Oswald-Medium.ttf#Oswald", IsPremium = true },
            new FontViewModel(){ FontKey = "Oswald", FontAssetPath="ms-appx:///Assets/Fonts/Oswald-Regular.ttf#Oswald", IsPremium = true },
            new FontViewModel(){ FontKey = "Raleway", FontAssetPath="ms-appx:///Assets/Fonts/Raleway-Regular.ttf#Raleway", IsPremium = true },
            new FontViewModel(){ FontKey = "Raleway-SemiBold", FontAssetPath="ms-appx:///Assets/Fonts/Raleway-SemiBold.ttf#Raleway", IsPremium = true },
            new FontViewModel(){ FontKey = "DancingScript", FontAssetPath="ms-appx:///Assets/Fonts/DancingScript-Regular.ttf#Dancing Script", IsPremium = true },
            new FontViewModel(){ FontKey = "FiraSansExtraCondensed-Medium", FontAssetPath="ms-appx:///Assets/Fonts/FiraSansExtraCondensed-Medium.ttf#Fira Sans Extra Condensed", IsPremium = true },
            new FontViewModel(){ FontKey = "Geometric706-BlackCondensed", FontAssetPath="ms-appx:///Assets/Fonts/Geometric 706.ttf#Geometric 706", IsPremium = true },
            new FontViewModel(){ FontKey = "Jost-Bold", FontAssetPath="ms-appx:///Assets/Fonts/Jost-Bold.ttf#Jost", IsPremium = true },
            new FontViewModel(){ FontKey = "Jost-Medium", FontAssetPath="ms-appx:///Assets/Fonts/Jost-Medium.ttf#Jost", IsPremium = true },
            new FontViewModel(){ FontKey = "Jost", FontAssetPath="ms-appx:///Assets/Fonts/Jost-Regular.ttf#Jost", IsPremium = true },
            new FontViewModel(){ FontKey = "Jost-SemiBold", FontAssetPath="ms-appx:///Assets/Fonts/Jost-SemiBold.ttf#Jost", IsPremium = true },
            new FontViewModel(){ FontKey = "Comfortaa-Regular", FontAssetPath="ms-appx:///Assets/Fonts/Comfortaa-Regular.ttf#Comfortaa", IsPremium = true },
            new FontViewModel(){ FontKey = "TTSouses-BlackItalic", FontAssetPath="ms-appx:///Assets/Fonts/TT Souses Black Italic.ttf#TT Souses", IsPremium = true },
            new FontViewModel(){ FontKey = "Brusher-Regular", FontAssetPath="ms-appx:///Assets/Fonts/ofont.ru_Brusher.ttf#Brusher", IsPremium = true },
            new FontViewModel(){ FontKey = "GoodTimingRg-Bold", FontAssetPath="ms-appx:///Assets/Fonts/good timing bd.ttf#Good Timing", IsPremium = true },
            new FontViewModel(){ FontKey = "Basil-Regular", FontAssetPath="ms-appx:///Assets/Fonts/Karandash - Basil-Regular.otf#Basil", IsPremium = true },
        };

        #endregion Public Fields

        #region Public Methods

        /// <summary>
        /// Возвращает шрифт по его имени (или полному пути)
        /// </summary>
        /// <param name="fontName"></param>
        /// <returns></returns>
        public FontViewModel GetFontByName(string fontName)
        {
            FontViewModel font;
            if (fontName.Contains("#"))
            {
                font = FontsList.Find(font => font.FontAssetPath == fontName);
            }
            else
            {
                font = FontsList.Find(font => font.FontKey == fontName);
            }

            return font;
        }

        #endregion Public Methods
    }
}