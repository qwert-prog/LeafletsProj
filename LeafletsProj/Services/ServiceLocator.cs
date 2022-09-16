using LeafletsProj.ViewModel;
using IoC;
using Utils.Services;

namespace LeafletsProj.Services
{
    /// <summary>
    /// Class to access ViewModels and services
    /// </summary>
    internal class ServiceLocator
    {
        #region Public Properties

        /// <summary>
        /// Gets <see cref="StartPageViewModel"/>
        /// </summary>
        public static StartPageViewModel StartPageViewModel => ServicesContainer.GetService<StartPageViewModel>();

        /// <summary>
        /// Gets <see cref="EditPageViewModel"/>
        /// </summary>
        public static EditPageViewModel EditPageViewModel => ServicesContainer.GetService<EditPageViewModel>();

        /// <summary>
        /// Gets <see cref="SavePageViewModel"/>
        /// </summary>
        public static SavePageViewModel SavePageViewModel => ServicesContainer.GetService<SavePageViewModel>();

        /// <summary>
        /// Gets <see cref="AssetsService"/>
        /// </summary>
        public static AssetsService AssetsService => ServicesContainer.GetService<AssetsService>();

        /// <summary>
        /// Gets <see cref="FontsService"/>
        /// </summary>
        public static FontsService FontsService => ServicesContainer.GetService<FontsService>();

        /// <summary>
        /// Gets <see cref="RenderUIElementService"/>
        /// </summary>
        public static RenderUIElementService RenderUIElementService => ServicesContainer.GetService<RenderUIElementService>();

        /// <summary>
        /// Gets <see cref="SaveDataService"/>
        /// </summary>
        public static SaveDataService SaveDataService => ServicesContainer.GetService<SaveDataService>();

        #endregion Public Properties
    }
}