using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProjektSemestrIV.ViewModels {
    class RelayCommand : ICommand {
        readonly Action<object> execute;
        readonly Predicate<object> canExecute;

        public RelayCommand( Action<object> execute, Predicate<object> canExecute ) {
            this.execute = execute ?? throw new ArgumentNullException(nameof(execute));
            this.canExecute = canExecute;
        }

        public bool CanExecute( object parameter ) {
            return canExecute == null ? true : canExecute(parameter);
        }

        public event EventHandler CanExecuteChanged {
            add {
                if(canExecute != null) CommandManager.RequerySuggested += value;
            }
            remove {
                if(canExecute != null) CommandManager.RequerySuggested -= value;
            }
        }

        public void Execute( object parameter ) {
            execute(parameter);
        }
    }
}
