using ProjektSemestrIV.Extensions;
using ProjektSemestrIV.Models.ComplexModels;
using ProjektSemestrIV.Models.ShowModels;
using ProjektSemestrIV.ViewModels.BaseClass;
using System;
using System.Collections.ObjectModel;

namespace ProjektSemestrIV.ViewModels
{
    class ShowSelectedStageViewModel : SwitchViewModel
    {
        private readonly ShowSelectedStageModel model;

        public string CompetitionLocation { get; private set; }
        public string StageName { get; private set; }
        public string StageRules { get; private set; }
        public uint NumOfTargets { get; private set; }
        public string BestShooter { get; private set; }
        public double AverageTime { get; private set; }

        public ObservableCollection<ShooterWithStagePointsAndCompetitionPointsOverview> Shooters { get; private set; }

        public ShowSelectedStageViewModel()
        {
            model = new ShowSelectedStageModel();
        }

        public override IBaseViewModel GetViewModel(params uint[] id)
        {
            model.SetNewId(id[0]);

            CompetitionLocation = model.GetCompetitionLocation();
            StageName = model.GetStageName();
            StageRules = model.GetStageRules();
            NumOfTargets = model.GetNumOfTargets();
            BestShooter = model.GetShooterWithPoints();
            AverageTime = model.GetAverageTime();
            Shooters = model.GetShooters().Convert();

            return this;
        }
    }
}
