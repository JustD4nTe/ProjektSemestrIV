using ProjektSemestrIV.Models.ShowModels;
using System.Collections.ObjectModel;

namespace ProjektSemestrIV.ViewModels
{
    class ShowSelectedShooterInCompetitionViewModel 
    {
        private readonly ShowSelectedShooterInCompetitionModel model;

        public string ShooterName { get; private set; }
        public string CompetitionName { get; private set; }
        public uint Position { get; private set; }
        public double Points { get; private set; }
        public double Time { get; private set; }

        public double Accuracy { get; private set; }
        public double AlphaAccuracy { get; private set; }
        public double CharlieAccuracy { get; private set; }
        public double DeltaAccuracy { get; private set; }

        public ObservableCollection<StatsAtStageOverview> StageStats { get; private set; }

        public ShowSelectedShooterInCompetitionViewModel()
        {
            model = new ShowSelectedShooterInCompetitionModel();
        }
        }
    }
}
