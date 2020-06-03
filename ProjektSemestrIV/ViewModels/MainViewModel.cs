using ProjektSemestrIV.Models;
using System;
using System.Windows.Input;

namespace ProjektSemestrIV.ViewModels {
    class MainViewModel : BaseViewModel {
		private BaseViewModel selectedViewModel;
		public BaseViewModel SelectedViewModel {
			get { return selectedViewModel; }
			set {
				selectedViewModel = value;
				onPropertyChanged(nameof(SelectedViewModel));
			}
		}

		private ICommand updateFormView = null;
		public ICommand UpdateFormView {
            get {
                if(updateFormView == null) {
                    updateFormView = new RelayCommand(ExecuteUpdateFormView, null);
                }

                return updateFormView;
            }
        }
        public void ExecuteUpdateFormView( object parameter ) {
            if(parameter.ToString() == "Connection") {
                SelectedViewModel = new ConnectionViewModel();
            }
            else if(parameter.ToString() == "EditShooters") {
                SelectedViewModel = new EditShootersViewModel();
            }
            else if(parameter.ToString() == "Competitions") {
                SelectedViewModel = new CompetitionsViewModel();
            }
            else if(parameter.ToString() == "Stages") {
                SelectedViewModel = new StagesViewModel();
            }
            else if(parameter.ToString() == "Shooters") {
                SelectedViewModel = new ShootersViewModel();
            }
            else if(parameter.ToString() == "Score") {
                SelectedViewModel = new ScoreViewModel();
            }
        }
    }
}
