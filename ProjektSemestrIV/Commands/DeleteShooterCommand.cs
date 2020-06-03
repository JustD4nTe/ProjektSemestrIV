using ProjektSemestrIV.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProjektSemestrIV.Commands {
    class DeleteShooterCommand : ICommand {
        private EditShootersViewModel editShooterViewModel;

        public DeleteShooterCommand( EditShootersViewModel editShooterViewModel ) {
            this.editShooterViewModel = editShooterViewModel;
        }

        public event EventHandler CanExecuteChanged {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute( object parameter ) {
            if( (editShooterViewModel.SelectedIndex != -1) &&
                (editShooterViewModel.SelectedIndex != editShooterViewModel.EditedItemIndex)) {
                return true;
            }
            else {
                return false;
            }
        }

        public void Execute( object parameter ) {
            UInt32 id = editShooterViewModel.SelectedItem.ID;
            editShooterViewModel.MainModel.DeleteShooterFromDatabase(id);
            editShooterViewModel.Shooters = editShooterViewModel.MainModel.GetAllShooters();
        }
    }
}
