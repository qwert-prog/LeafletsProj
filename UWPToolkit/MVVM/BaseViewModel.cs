using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace UWPToolkit.ViewModels
{
    /// <summary>
    /// Basic view model
    /// </summary>
    public class BaseViewModel : INotifyPropertyChanged
    {
        #region Public Events

        /// <summary>
        /// Property change event
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Public Events

        #region Public Methods

        /// <summary>
        /// Handles the property change event
        /// </summary>
        /// <param name="prop"></param>
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }

        #endregion Public Methods
    }
}