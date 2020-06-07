using ProjektSemestrIV.DAL.Entities;
using ProjektSemestrIV.Models.ComplexModels;
using ProjektSemestrIV.Models.ShowModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektSemestrIV.ViewModels
{
    class ShowSelectedStageViewModel : BaseViewModel
    {
        private ShowSelectedStageModel model;

        public string CompetitionLocation { get; }
        public string StageName { get; }
        public string StageRules { get; }
        public uint NumOfTargets { get; }
        public string BestShooter { get; }
        public double AverageTime { get; }

        public ObservableCollection<ShooterWithStagePointsAndCompetitionPointsOverview> Shooters { get; }

        public ShowSelectedStageViewModel(uint id)
        {
            model = new ShowSelectedStageModel(id);
            CompetitionLocation = model.GetCompetitionLocation();
            StageName = model.GetStageName();
            StageRules = model.GetStageRules();
            NumOfTargets = model.GetNumOfTargets();
        }

    }
}
