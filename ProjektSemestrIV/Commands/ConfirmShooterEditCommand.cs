using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ProjektSemestrIV.ViewModels;
using ProjektSemestrIV.DAL.Entities;

namespace ProjektSemestrIV.Commands {
    class ConfirmShooterEditCommand : ICommand {
        private EditShootersViewModel editShooterViewModel;

        public ConfirmShooterEditCommand( EditShootersViewModel editShooterViewModel ) {
            this.editShooterViewModel = editShooterViewModel;
        }

        public event EventHandler CanExecuteChanged {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute( object parameter ) {
            if(editShooterViewModel.EditedItemIndex != -1) {
                return true;
            }
            else {
                return false;   
            }
        }

        public void Execute( object parameter ) {
            Shooter newShooter = new Shooter(editShooterViewModel.Name, editShooterViewModel.Surname);
            UInt32 id = editShooterViewModel.Shooters[editShooterViewModel.SelectedIndex].ID;
            editShooterViewModel.MainModel.EditShooterInDatabase(newShooter, id);

            editShooterViewModel.Name = "";
            editShooterViewModel.Surname = "";
            editShooterViewModel.EditedItemIndex = -1;
            editShooterViewModel.Shooters = editShooterViewModel.MainModel.GetAllShooters();
        }
    }
}
