using LeafletsProj.Core.Models;
using LeafletsProj.Models;
using LeafletsProj.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UWPToolkit.ViewModels;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Resources;
using Windows.Storage;

namespace LeafletsProj.Services
{
    /// <summary>
    /// Сервис для работы с ассетами
    /// </summary>
    internal class AssetsService : BaseViewModel
    {
        #region Public Properties

        /// <summary>
        /// Предоставляет или задает список, в котором хранится полный путь к изображениям абстакций.
        /// </summary>
        public List<DecorViewModel> AbstractImagesPathsList { get; private set; }

        /// <summary>
        /// Предоставляет или задает список визиток
        /// </summary>
        public List<Card> CardsList { get; private set; }

        /// <summary>
        /// Предоставляет или задает сгрупированный спиское флайеров
        /// </summary>
        public IEnumerable<IGrouping<string, Flyer>> FlyersList
        {
            get; private set;
        }

        /// <summary>
        /// Предоставляет или задает список, в котором хранится полный путь к изображениям линий.
        /// </summary>
        public List<DecorViewModel> LineImagesPathsList { get; private set; }

        /// <summary>
        /// Предоставляет или задает список, в котором хранится полный путь к изображениям пятен.
        /// </summary>
        public List<DecorViewModel> PaintsImagesPathsList { get; private set; }

        /// <summary>
        /// Предоставляет или задает список, в котором хранится полный путь к изображениям значков.
        /// </summary>
        public List<DecorViewModel> SignsImagesPathsList { get; private set; }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Инициализирует экземпляр класса <see cref="AssetsService"/>
        /// </summary>
        /// <returns></returns>
        public async Task InitializeAsync()
        {
            UpdateCardsAndFlyersList();
            AbstractImagesPathsList = await LoadImagesAsync(AppConst.AbstractsFolderName);
            LineImagesPathsList = await LoadImagesAsync(AppConst.LinesFolderName);
            SignsImagesPathsList = await LoadImagesAsync(AppConst.SignsFolderFolderName);
            PaintsImagesPathsList = await LoadImagesAsync(AppConst.PaintsFolderName);
        }

        /// <summary>
        /// Метод для обновления списков флайеров и визиток
        /// </summary>
        public void UpdateCardsAndFlyersList()
        {
            CardsList = LoadCards();
            FlyersList = LoadFlyer();
            OnPropertyChanged(nameof(CardsList));
            OnPropertyChanged(nameof(FlyersList));
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// Метод для генерации списка <see cref="Card"/>
        /// </summary>
        /// <returns></returns>
        private List<Card> LoadCards()
        {
            List<Card> cardsList = new();
            string file = Path.Combine("Data", "BusinessCards", "BusinessCards.json");
            using (StreamReader text = File.OpenText(file))
            {
                cardsList = JsonConvert.DeserializeObject<List<Card>>(text.ReadToEnd());
            }

            cardsList.ForEach(card => card.IsPremium = false);

            return cardsList;
        }

        /// <summary>
        /// Метод для генерации групированного списка <see cref="Flyer"/>
        /// </summary>
        /// <returns></returns>
        private IEnumerable<IGrouping<string, Flyer>> LoadFlyer()
        {
            ResourceLoader resourceLoader = new ResourceLoader();
            List<Flyer> flyers = new();
            string file = Path.Combine("Data", "Flyers", "Flyers.json");
            using (StreamReader text = File.OpenText(file))
            {
                flyers = JsonConvert.DeserializeObject<List<Flyer>>(text.ReadToEnd());
                foreach (Flyer flyer in flyers)
                {
                    flyer.GroupName = resourceLoader.GetString(flyer.GroupName);
                }
            }

            flyers.ForEach(flyer => flyer.IsPremium = false);

            return flyers.GroupBy(x => x.GroupName);
        }

        /// <summary>
        /// Асинхронный метод для генерации списка изображений
        /// </summary>
        /// <param name="folderName">The name of the folder containing the images</param>
        /// <returns>A filled list containing the full paths of the images</returns>
        private async Task<List<DecorViewModel>> LoadImagesAsync(string folderName)
        {
            List<DecorViewModel> currentList = new List<DecorViewModel>();
            string root = Package.Current.InstalledLocation.Path;
            StorageFolder currentFolder = await StorageFolder.GetFolderFromPathAsync(Path.Combine(root, folderName));
            int countFreeDecor = 2;
            foreach (StorageFile currentFile in await currentFolder.GetFilesAsync())
            {
                DecorViewModel decor = new()
                {
                    ImagePath = currentFile.Path,
                };
                if (countFreeDecor > 0)
                {
                    decor.IsPremium = false;
                }
                countFreeDecor--;
                if (!currentList.Contains(decor))
                {
                    currentList.Add(decor);
                }
            }
            return currentList;
        }

        #endregion Private Methods
    }
}