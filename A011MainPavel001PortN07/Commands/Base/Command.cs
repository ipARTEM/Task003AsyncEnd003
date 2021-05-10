using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Task003AsyncEnd003.Commands.Base
{
    public abstract class Command : ICommand
    {

        // Передача управления командами CommandManager
        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;

            remove => CommandManager.RequerySuggested -= value;

        }

        public abstract bool CanExecute(object parameter);

        public abstract void Execute(object parameter);
    }
}
