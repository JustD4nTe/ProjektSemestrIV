using ProjektSemestrIV.DAL.Entities;
using ProjektSemestrIV.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProjektSemestrIV.Commands {
    class AddShooterCommand : ICommand {
        private EditShootersViewModel editShooterViewModel;

        public AddShooterCommand( EditShootersViewModel addShooterViewModel ) {
            this.editShooterViewModel = addShooterViewModel;
        }

        public event EventHandler CanExecuteChanged {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute( object parameter ) {
            if(editShooterViewModel.EditedItemIndex == -1) {
                return true;
            }
            else {
                return false;
            }
        }

        public void Execute( object parameter ) {
            Shooter newShooter = new Shooter(editShooterViewModel.Name, editShooterViewModel.Surname);
            editShooterViewModel.ShooterModel.AddShooterToDatabase(newShooter);

            editShooterViewModel.Name = "";
            editShooterViewModel.Surname = "";
            editShooterViewModel.Shooters = editShooterViewModel.ShooterModel.GetAllShooters();
        }
    }
}
