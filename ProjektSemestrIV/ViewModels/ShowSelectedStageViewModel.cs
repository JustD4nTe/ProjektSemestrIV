using ProjektSemestrIV.DAL.Entities;
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
        public string Competition { get; }
        public string StageName { get; }
        public string StageRules { get; }
        public uint NumOfTargets { get; }
        public string BestShooter { get; }
        public double AverageTime { get; }
        public ObservableCollection<ShooterWithStagePointsAndCompetitionPoints> Shooters { get; }

    }
}
