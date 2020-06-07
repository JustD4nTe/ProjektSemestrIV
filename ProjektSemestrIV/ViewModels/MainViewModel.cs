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
            switch (parameter.ToString())
            {
                case "Connection":
                    SelectedViewModel = new ShowSelectedStageViewModel(1);
                    break;
                case "EditCompetitions":
                    SelectedViewModel = new EditCompetitionsViewModel();
                    break;
                case "EditShooters":
                    SelectedViewModel = new EditShootersViewModel();
                    break;
                case "EditStages":
                    SelectedViewModel = new EditStagesViewModel();
                    break;       
                case "EditScore":
                    SelectedViewModel = new EditScoreViewModel();
                    break;
                case "ShowCompetitions":
                    SelectedViewModel = new ShowCompetitionsViewModel();
                    break;
                case "ShowShooters":
                    SelectedViewModel = new ShowShootersViewModel();
                    break;
            }
        }
    }
}
