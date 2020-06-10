using ProjektSemestrIV.Models.ComplexModels;
using ProjektSemestrIV.Models.ShowModels;
using ProjektSemestrIV.ViewModels.BaseClass;
using System.Collections.ObjectModel;

namespace ProjektSemestrIV.ViewModels
{
    class ShowSelectedShooterInCompetitionViewModel : SwitchViewModel
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

        public override IBaseViewModel GetViewModel(params uint[] id)
        {
            model.SetNewId(shooterId: id[0],competitionId: id[1]);

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

            return this;
        }
    }
}
