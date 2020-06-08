using ProjektSemestrIV.Events;
using ProjektSemestrIV.Models;
using ProjektSemestrIV.ViewModels.BaseClass;
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
                    updateFormView = new RelayCommand(ChooseGeneralView, null);
                }

                return updateFormView;
            }
        }

        private readonly BaseViewModel showCompetitionsViewModel;
        private readonly ISubView showSelectedCompetitionViewModel;
        private readonly BaseViewModel showShootersViewModel;
        private readonly ISubView showSelectedShooterViewModel;

        public MainViewModel()
        {
            showCompetitionsViewModel = new ShowCompetitionsViewModel();
            showSelectedCompetitionViewModel = new ShowSelectedCompetitionViewModel();
            showShootersViewModel = new ShowShootersViewModel();
            showSelectedShooterViewModel = new ShowSelectedShooterViewModel();

            showCompetitionsViewModel.SwitchView += SwitchToSubView;
            showShootersViewModel.SwitchView += SwitchToSubView;
        }

        public void ChooseGeneralView( object parameter ) {
            switch ((ViewTypeEnum)parameter)
            {
                case ViewTypeEnum.Configuration:
                    SelectedViewModel = new ConnectionViewModel();
                    break;
                case ViewTypeEnum.EditCompetitions:
                    SelectedViewModel = new EditCompetitionsViewModel();
                    break;
                case ViewTypeEnum.EditShooters:
                    SelectedViewModel = new EditShootersViewModel();
                    break;
                case ViewTypeEnum.EditStages:
                    SelectedViewModel = new EditStagesViewModel();
                    break;       
                case ViewTypeEnum.EditScore:
                    SelectedViewModel = new EditScoreViewModel();
                    break;
                case ViewTypeEnum.ShowCompetitions:
                    SelectedViewModel = showCompetitionsViewModel;
                    break;
                case ViewTypeEnum.ShowShooters:
                    SelectedViewModel = showShootersViewModel;
                    break;
                default:
                    break;
            }
        }

        public void SwitchToSubView(object sender, SwitchViewEventArgs e)
        {
            switch (e.NextView)
            {
                case ViewTypeEnum.ShowSelectedCompetition:
                    SelectedViewModel = showSelectedCompetitionViewModel.GetView(e.NextViewId);
                    break;
                case ViewTypeEnum.ShowSelectedShooter:
                    SelectedViewModel = showSelectedShooterViewModel.GetView(e.NextViewId);
                    break;
                default:
                    break;
            }
        }
    }
}
