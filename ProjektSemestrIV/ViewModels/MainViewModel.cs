using ProjektSemestrIV.Events;
using ProjektSemestrIV.Models;
using ProjektSemestrIV.ViewModels.BaseClass;
using System.Collections.Generic;
using System.Windows.Input;

namespace ProjektSemestrIV.ViewModels {
    class MainViewModel : BaseViewModel {

		private IBaseViewModel selectedViewModel;
		public IBaseViewModel SelectedViewModel {
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

        private readonly ISwitchViewModel showCompetitionsViewModel;
        private readonly ISwitchViewModel showSelectedCompetitionViewModel;
        private readonly ISwitchViewModel showShootersViewModel;
        private readonly ISwitchViewModel showSelectedShooterViewModel;
        private readonly ISwitchViewModel showSelectedStageViewModel;
        private readonly ISwitchViewModel showSelectedShooterInCompetitionViewModel;

        public MainViewModel()
        {
            showCompetitionsViewModel = new ShowCompetitionsViewModel();
            showSelectedCompetitionViewModel = new ShowSelectedCompetitionViewModel();
            showShootersViewModel = new ShowShootersViewModel();
            showSelectedShooterViewModel = new ShowSelectedShooterViewModel();
            showSelectedStageViewModel = new ShowSelectedStageViewModel();
            showSelectedShooterInCompetitionViewModel = new ShowSelectedShooterInCompetitionViewModel();

            showCompetitionsViewModel.SwitchView += SwitchToSubView;
            showSelectedCompetitionViewModel.SwitchView += SwitchToSubView;
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
                    SelectedViewModel = showCompetitionsViewModel.GetViewModel();
                    break;
                case ViewTypeEnum.ShowShooters:
                    SelectedViewModel = showShootersViewModel.GetViewModel();
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
                    SelectedViewModel = showSelectedCompetitionViewModel.GetViewModel(e.NextViewId);
                    break;
                case ViewTypeEnum.ShowSelectedShooter:
                    SelectedViewModel = showSelectedShooterViewModel.GetViewModel(e.NextViewId);
                    break;
                case ViewTypeEnum.ShowSelectedStage:
                    SelectedViewModel = showSelectedStageViewModel.GetViewModel(e.NextViewId);
                    break;
                case ViewTypeEnum.ShowSelectedShooterInCompetition:
                    var selectedCompetition = sender as ShowSelectedCompetitionViewModel;
                    SelectedViewModel = showSelectedShooterInCompetitionViewModel.GetViewModel(e.NextViewId, selectedCompetition.Id);
                    break;
                default:
                    break;
            }
        }
    }
}
