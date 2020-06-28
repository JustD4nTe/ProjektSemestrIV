using ProjektSemestrIV.Models.ComplexModels;
using ProjektSemestrIV.Models.ShowModels;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using System.Windows.Navigation;

namespace ProjektSemestrIV.ViewModels
{
    class ShowSelectedShooterInCompetitionViewModel 
    {
        private readonly ShowSelectedShooterInCompetitionModel model;
        private readonly NavigationService navigation;
        private readonly uint shooterId;

        public string ShooterName { get;  }
        public string CompetitionName { get; }
        public string Position { get; }
        public string Points { get; }
        public string Time { get; }

        public string Accuracy { get; }
        public string AlphaAccuracy { get;  }
        public string CharlieAccuracy { get;  }
        public string DeltaAccuracy { get;  }

        public ObservableCollection<StatsAtStageOverview> StageStats { get; }

        public StatsAtStageOverview SelectedStage { get; set; }

        public ICommand SwitchViewCommand { get; }

        public ShowSelectedShooterInCompetitionViewModel(NavigationService navigation, uint shooterId, uint competitionId)
        {
            this.navigation = navigation;
            this.shooterId = shooterId;

            model = new ShowSelectedShooterInCompetitionModel(shooterId, competitionId);

            ShooterName = model.GetShooterName();
            CompetitionName = model.GetCompetitionName();
            Position = model.GetPosition();
            Points = model.GetPoints();
            Time = model.GetTime();

            Accuracy = model.GetGeneralAccuracy();
            AlphaAccuracy = model.GetAlphaAccuracy();
            CharlieAccuracy = model.GetCharlieAccuracy();
            DeltaAccuracy = model.GetDeltaAccuracy();

            StageStats = model.GetShooterStatsOnStages();

            SwitchViewCommand = new RelayCommand(x => OnSwitchView(), 
                                                 x => SelectedStage != null);
        }

        private void OnSwitchView()
        => navigation.Navigate(new ShowShooterOnStageViewModel(shooterId, SelectedStage.StageId));
    }
}
