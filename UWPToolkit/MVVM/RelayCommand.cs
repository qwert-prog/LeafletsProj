using System;
using System.Windows.Input;

namespace UWPToolkit.MVVM
{
    /// <summary>
    /// The base class for a delegate-based command
    /// </summary>
    public sealed class RelayCommand : ICommand
    {
        #region Private Fields

        /// <summary>
        /// A delegate called to check if a command can be executed
        /// </summary>
        private readonly Func<bool> _canExecute;

        /// <summary>
        /// A delegate called when the command is executed
        /// </summary>
        private readonly Action _execute;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// Creates a command object based on delegates with no parameter
        /// </summary>
        /// <param name="execute">A delegate called when the command is executed</param>
        /// <param name="canExecute">A delegate called to check if a command can be executed</param>
        public RelayCommand(Action execute, Func<bool> canExecute = null)
        {
            if (execute == null)
            {
                throw new ArgumentNullException(nameof(execute));
            }
            _execute = execute;
            _canExecute = canExecute;
        }

        #endregion Public Constructors

        #region Public Events

        /// <summary>
        /// Command "running" state change event
        /// </summary>
        public event EventHandler CanExecuteChanged;

        #endregion Public Events

        #region Public Methods

        /// <summary>
        /// Checks if the command can be executed
        /// </summary>
        /// <param name="parameter">Command parameter. Will be ignored</param>
        /// <returns><see langword="true"/> - if the command can be executed, otherwise <see langword="false"/></returns>
        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute();
        }

        /// <summary>
        /// Executes a command
        /// </summary>
        /// <param name="parameter">Параметр команды. Будет проигнорирован.</param>
        public void Execute(object parameter)
        {
            _execute();
        }

        /// <summary>
        /// Updates the command, causing it to be rechecked for feasibility
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        #endregion Public Methods
    }

    /// <summary>
    /// Delegate-based parameterized command class
    /// </summary>
    /// <typeparam name="T">The type of the command parameter</typeparam>
    public sealed class RelayCommand<T> : ICommand
    {
        #region Private Fields

        /// <summary>
        /// A delegate called to check if a command can be executed
        /// </summary>
        private readonly Func<T, bool> _canExecute;

        /// <summary>
        /// A delegate called when the command is executed
        /// </summary>
        private readonly Action<T> _execute;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// Creates a parameterized command object
        /// </summary>
        /// <param name="execute">A delegate called when the command is executed</param>
        /// <param name="canExecute">A delegate called to check if a command can be executed</param>
        public RelayCommand(Action<T> execute, Func<T, bool> canExecute = null)
        {
            if (execute == null)
            {
                throw new ArgumentNullException(nameof(execute));
            }

            _execute = execute;
            _canExecute = canExecute;
        }

        #endregion Public Constructors

        #region Public Events

        /// <summary>
        /// Command "running" state change event
        /// </summary>
        public event EventHandler CanExecuteChanged;

        #endregion Public Events

        #region Public Methods

        /// <summary>
        /// Checks if the command can be executed
        /// </summary>
        /// <param name="parameter">Command parameter</param>
        /// <returns><see langword="true"/> - if the command can be executed, otherwise <see langword="false"/>.</returns>
        public bool CanExecute(object parameter)
        {
            return _canExecute == null ||
                parameter != null && _canExecute((T)parameter) || parameter == null && _canExecute(default);
        }

        /// <summary>
        /// Executes a command
        /// </summary>
        /// <param name="parameter">Параметр команды.</param>
        public void Execute(object parameter)
        {
            _execute((T)parameter);
        }

        /// <summary>
        /// Updates the command, causing it to be rechecked for feasibility
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        #endregion Public Methods
    }
}