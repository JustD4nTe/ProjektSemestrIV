using ProjektSemestrIV.Models.ComplexModels;
using ProjektSemestrIV.Models.ShowModels;
using System.Collections.ObjectModel;

namespace ProjektSemestrIV.ViewModels
{
    class ShowSelectedShooterInCompetitionViewModel 
    {
        private readonly ShowSelectedShooterInCompetitionModel model;

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

        public ShowSelectedShooterInCompetitionViewModel(uint shooterId, uint competitionId)
        {
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
        }
    }
}
