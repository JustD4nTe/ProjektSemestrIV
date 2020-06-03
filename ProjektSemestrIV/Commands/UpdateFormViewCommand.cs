using ProjektSemestrIV.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProjektSemestrIV.Commands {
    class UpdateFormViewCommand : ICommand {
        private MainViewModel mainViewModel;

        public UpdateFormViewCommand(MainViewModel mainViewModel) {
            this.mainViewModel = mainViewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute( object parameter ) {
            return true;
        }

        public void Execute( object parameter ) {
            if(parameter.ToString() == "Connection") {
                mainViewModel.SelectedViewModel = new ConnectionViewModel();
            }
            else if(parameter.ToString() == "EditShooters") {
                mainViewModel.SelectedViewModel = new EditShootersViewModel(mainViewModel.MainModel);
            }
            else if(parameter.ToString() == "Competitions") {
                mainViewModel.SelectedViewModel = new CompetitionsViewModel();
            }
            else if(parameter.ToString() == "Stages") {
                mainViewModel.SelectedViewModel = new StagesViewModel();
            }
            else if(parameter.ToString() == "Shooters") {
                mainViewModel.SelectedViewModel = new ShootersViewModel();
            }
            else if(parameter.ToString() == "Score") {
                mainViewModel.SelectedViewModel = new ScoreViewModel();
            }
        }
    }
}
