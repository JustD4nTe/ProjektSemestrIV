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
        public uint Position { get; }
        public double Points { get; }
        public double Time { get; }

        public double Accuracy { get; }
        public double AlphaAccuracy { get;  }
        public double CharlieAccuracy { get;  }
        public double DeltaAccuracy { get;  }

        public ObservableCollection<StatsAtStageOverview> StageStats { get; }

        public StatsAtStageOverview SelectedStage { get; set; }

        public ICommand SwitchViewCommand { get; }

        public ShowSelectedShooterInCompetitionViewModel(NavigationService _navigation, uint _shooterId, uint _competitionId)
        {
            navigation = _navigation;
            shooterId = _shooterId;

            model = new ShowSelectedShooterInCompetitionModel(_shooterId, _competitionId);

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
