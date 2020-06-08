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

        public MainViewModel()
        {
            showCompetitionsViewModel = new ShowCompetitionsViewModel();
            showSelectedCompetitionViewModel = new ShowSelectedCompetitionViewModel();
            showCompetitionsViewModel.SwitchView += SwitchToSubView;
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
                    SelectedViewModel = new ShowShootersViewModel();
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
                default:
                    break;
            }
        }
    }
}
