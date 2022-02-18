using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PL
{
    public class RelayCommand<T> : ICommand
    {
        #region Fields

        readonly Action<object>_execute ;
        readonly object _execute2;
        readonly Func<object, bool> _canExecute ;
        private Action<object, CancelEventArgs> button_ClickCancel;

        public Action<object, MouseButtonEventArgs> Button_DoubleClick { get; }
        public object P { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of <see cref="DelegateCommand{T}"/>.
        /// </summary>
        /// <param name="execute">Delegate to execute when Execute is called on the command.  This can be null to just hook up a CanExecute delegate.</param>
        /// <remarks><seealso cref="CanExecute"/> will always return true.</remarks>
        public RelayCommand(Action<object> execute, object p)
            : this(execute, null)
        {
        }

        public RelayCommand(object executed2, object p) 
        {
            this._execute2 = executed2;  
        }
        /// <summary>
        /// Creates a new command.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        /// <param name="canExecute">The execution status logic.</param>
        public RelayCommand(Action<object> execute, Func<object, bool> canExecute =null, object execute2 = null)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");

            _execute = execute;
            _canExecute = canExecute;
            _execute2 = execute2;
        }

        public RelayCommand(Action<object, MouseButtonEventArgs> button_DoubleClick, object p)
        {
            Button_DoubleClick = button_DoubleClick;
            P = p;
        }

        public RelayCommand(Action<object, CancelEventArgs> button_ClickCancel, object p)
        {
            this.button_ClickCancel = button_ClickCancel;
            P = p;
        }

        #endregion

        #region ICommand Members

        ///<summary>
        ///Defines the method that determines whether the command can execute in its current state.
        ///</summary>
        ///<param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
        ///<returns>
        ///true if this command can be executed; otherwise, false.
        ///</returns>
        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute(parameter) || _execute2 == null;
        }

        ///<summary>
        ///Occurs when changes occur that affect whether or not the command should execute.
        ///</summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        ///<summary>
        ///Defines the method to be called when the command is invoked.
        ///</summary>
        ///<param name="parameter">Data used by the command. If the command does not require data to be passed, this object can be set to <see langword="null" />.</param>
        [DebuggerStepThrough]
        public void Execute(object parameter)
        {
            _execute(parameter);
        }

        #endregion
    }
}
