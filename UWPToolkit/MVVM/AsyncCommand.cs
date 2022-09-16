using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace UWPToolkit.MVVM
{
    /// <summary>
    /// Методы-расширения для задачи.
    /// </summary>
    public static class TaskExtension
    {
        #region Public Methods

        /// <summary>
        /// Запускает задачу, не отслеживая её завершения.
        /// </summary>
        /// <param name="task">Задача.</param>
        /// <param name="exceptionHandler">Обработчик исключений.</param>
        public static async void FireAndForgetAsync(this Task task, Action<Exception> exceptionHandler = null)
        {
            try
            {
                await task;
            }
            catch (Exception e)
            {
                exceptionHandler?.Invoke(e);
            }
        }

        #endregion Public Methods
    }

    /// <summary>
    /// Асинхронная команда с параметром типа <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">Тип параметра команды.</typeparam>
    public sealed class AsyncCommand<T> : ICommand
    {
        #region Private Fields

        /// <summary>
        /// Делегат, проверяющий можно ли выполнить команду.
        /// </summary>
        private readonly Func<T, bool> _canExecute;

        /// <summary>
        /// Делегат - перехватчик исключений.
        /// </summary>
        private readonly Action<Exception> _exceptionHandler;

        /// <summary>
        /// Делегат, выполняемый при исполнении команды.
        /// </summary>
        private readonly Func<T, Task> _execute;

        /// <summary>
        /// Выполняется ли сейчас команда.
        /// </summary>
        private bool _isExecuting;

        #endregion Private Fields

        #region Public Events

        /// <summary>
        /// Событие изменения готовности команды к исполнению.
        /// </summary>
        public event EventHandler CanExecuteChanged;

        #endregion Public Events

        #region Public Constructors

        /// <summary>
        /// Создаёт асинхронную команду.
        /// </summary>
        /// <param name="execute">Делегат команды.</param>
        /// <param name="canExecute">Делегат проверки возможности исполнения команды.</param>
        /// <param name="exceptionHandler">Делегат-перехватчик исключений.</param>
        public AsyncCommand(Func<T, Task> execute, Func<T, bool> canExecute = null, Action<Exception> exceptionHandler = null)
        {
            if (execute is null)
            {
                throw new ArgumentNullException();
            }

            _execute = execute;
            _canExecute = canExecute;
            _exceptionHandler = exceptionHandler ?? LogException;
        }

        #endregion Public Constructors

        #region Public Methods

        /// <summary>
        /// Проверяет, может ли асинхронная команда быть исполнена.
        /// </summary>
        /// <param name="parameter">Параметр команды.</param>
        /// <returns>Флаг готовности команды к исполнению.</returns>
        public bool CanExecute(T parameter)
        {
            return !_isExecuting && (_canExecute?.Invoke(parameter) ?? true);
        }

        /// <summary>
        /// Проверяет, может ли команда быть исполнена.
        /// </summary>
        /// <param name="parameter">Параметр команды.</param>
        /// <returns>Флаг готовности команды к исполнению.</returns>
        bool ICommand.CanExecute(object parameter)
        {
            return parameter != null
                ? CanExecute((T)parameter)
                : CanExecute(default);
        }

        /// <summary>
        /// Выполняет асинхронную команду, не дожидаясь результата её завершения.
        /// </summary>
        /// <param name="parameter">Параметр команды.</param>
        void ICommand.Execute(object parameter)
        {
            if (parameter != null)
            {
                ExecuteAsync((T)parameter).FireAndForgetAsync(_exceptionHandler);
            }
            else
            {
                ExecuteAsync(default).FireAndForgetAsync(_exceptionHandler);
            }
        }

        /// <summary>
        /// Выполняет асинхронную команду.
        /// </summary>
        /// <param name="parameter">Параметр команды.</param>
        public async Task ExecuteAsync(T parameter)
        {
            if (CanExecute(parameter))
            {
                try
                {
                    _isExecuting = true;
                    await _execute(parameter);
                }
                finally
                {
                    _isExecuting = false;
                }
            }

            RaiseCanExecuteChanged();
        }

        /// <summary>
        /// Запускает событие изменения готовности команды к исполнению.
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// Логирует исключение <paramref name="exception"/>  в отладочную консоль.
        /// Этот метод используется в качестве обработчика исключений, если не задан другой.
        /// </summary>
        /// <param name="exception">Исключение.</param>
        private static void LogException(Exception exception)
        {
            System.Diagnostics.Debug.WriteLine(exception);
        }

        #endregion Private Methods
    }

    /// <summary>
    /// Асинхронная команда.
    /// </summary>
    public sealed class AsyncCommand : ICommand
    {
        #region Private Fields

        /// <summary>
        /// Делегат, проверяющий можно ли выполнить команду.
        /// </summary>
        private readonly Func<bool> _canExecute;

        /// <summary>
        /// Делегат - перехватчик исключений.
        /// </summary>
        private readonly Action<Exception> _exceptionHandler;

        /// <summary>
        /// Делегат, выполняемый при исполнении команды.
        /// </summary>
        private readonly Func<Task> _execute;

        /// <summary>
        /// Выполняется ли сейчас команда.
        /// </summary>
        private bool _isExecuting;

        #endregion Private Fields

        #region Public Events

        /// <summary>
        /// Событие изменения готовности команды к исполнению.
        /// </summary>
        public event EventHandler CanExecuteChanged;

        #endregion Public Events

        #region Public Constructors

        /// <summary>
        /// Создаёт асинхронную команду.
        /// </summary>
        /// <param name="execute">Делегат команды.</param>
        /// <param name="canExecute">Делегат проверки возможности исполнения команды.</param>
        /// <param name="exceptionHandler">Делегат-перехватчик исключений.</param>
        public AsyncCommand(Func<Task> execute, Func<bool> canExecute = null, Action<Exception> exceptionHandler = null)
        {
            if (execute is null)
            {
                throw new ArgumentNullException();
            }

            _execute = execute;
            _canExecute = canExecute;
            _exceptionHandler = exceptionHandler ?? LogException;
        }

        #endregion Public Constructors

        #region Public Methods

        /// <summary>
        /// Проверяет, может ли команда быть выполнена.
        /// </summary>
        /// <returns>Флаг готовности команды к выполнению.</returns>
        public bool CanExecute()
        {
            return !_isExecuting && (_canExecute?.Invoke() ?? true);
        }

        /// <summary>
        /// Проверяет, может ли команда быть выполнена.
        /// </summary>
        /// <param name="parameter">Параметр команды. Будет проигнорирован.</param>
        /// <returns>Флаг готовности команды к выполнению.</returns>
        bool ICommand.CanExecute(object parameter)
        {
            return CanExecute();
        }

        /// <summary>
        /// Запускает выполнение команды.
        /// </summary>
        /// <param name="parameter">Параметр команды. Будет проигнорирован.</param>
        void ICommand.Execute(object parameter)
        {
            ExecuteAsync().FireAndForgetAsync(_exceptionHandler);
        }

        /// <summary>
        /// Выполняет команду.
        /// </summary>
        public async Task ExecuteAsync()
        {
            if (CanExecute())
            {
                try
                {
                    _isExecuting = true;
                    await _execute();
                }
                finally
                {
                    _isExecuting = false;
                }
            }

            RaiseCanExecuteChanged();
        }

        /// <summary>
        /// Запускает событие изменения готовности команды к исполнению.
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// Логирует исключение <paramref name="exception"/>  в отладочную консоль.
        /// Этот метод используется в качестве обработчика исключений, если не задан другой.
        /// </summary>
        /// <param name="exception">Исключение.</param>
        private static void LogException(Exception exception)
        {
            System.Diagnostics.Debug.WriteLine(exception);
        }

        #endregion Private Methods
    }
}