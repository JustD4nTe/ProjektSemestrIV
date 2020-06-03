using ProjektSemestrIV.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProjektSemestrIV.Commands {
    class EditShooterCommand : ICommand {
        private EditShootersViewModel editShooterViewModel;

        public EditShooterCommand( EditShootersViewModel editShooterViewModel ) {
            this.editShooterViewModel = editShooterViewModel;
        }

        public event EventHandler CanExecuteChanged {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute( object parameter ) {
            if(editShooterViewModel.SelectedIndex != -1) {
                return true;
            }
            else {
                return false;
            }
        }

        public void Execute( object parameter ) {
            editShooterViewModel.Name = editShooterViewModel.SelectedItem.Name;
            editShooterViewModel.Surname = editShooterViewModel.SelectedItem.Surname;
            editShooterViewModel.EditedItemIndex = editShooterViewModel.SelectedIndex;
        }
    }
}
