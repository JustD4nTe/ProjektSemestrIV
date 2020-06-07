using ProjektSemestrIV.Events;
using ProjektSemestrIV.Models;
using ProjektSemestrIV.ViewModels.BaseClass;
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

        private readonly BaseViewModel showCompetitionsViewModel;
        private readonly ISubView showSelectedCompetitionViewModel;

        public MainViewModel()
        {
            showCompetitionsViewModel = new ShowCompetitionsViewModel();
            showSelectedCompetitionViewModel = new ShowSelectedCompetitionViewModel();
            showCompetitionsViewModel.SwitchView += SwitchingView;
        }

        public void ExecuteUpdateFormView( object parameter ) {
            switch (parameter.ToString())
            {
                case "Connection":
                    SelectedViewModel = new ConnectionViewModel();
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
                    SelectedViewModel = showCompetitionsViewModel;
                    break;
                case "ShowShooters":
                    SelectedViewModel = new ShowShootersViewModel();
                    break;
            }
        }

        public void SwitchingView(object sender, SwitchViewEventArgs e)
        {
            switch (e.NextView)
            {
                case ViewTypeEnum.ShowSelectedCompetition:
                    SelectedViewModel = showSelectedCompetitionViewModel.GetView(e.NextViewId);
                    break;
                default:
                    break;
            }
        }
    }
}
