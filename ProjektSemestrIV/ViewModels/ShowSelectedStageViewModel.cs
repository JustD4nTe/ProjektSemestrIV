using ProjektSemestrIV.Events;
using ProjektSemestrIV.Extensions;
using ProjektSemestrIV.Models;
using ProjektSemestrIV.Models.ComplexModels;
using ProjektSemestrIV.Models.ShowModels;
using ProjektSemestrIV.ViewModels.BaseClass;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

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
        public uint Id { get; private set; }

        public ObservableCollection<ShooterWithStagePointsAndCompetitionPointsOverview> Shooters { get; private set; }

        public ShooterWithStagePointsAndCompetitionPointsOverview SelectedShooter { get; set; }

        public ICommand SwitchViewCommand { get; }

        public ShowSelectedStageViewModel()
        {
            model = new ShowSelectedStageModel();
            SwitchViewCommand = new RelayCommand(x => OnSwitchView(), x => SelectedShooter != null);
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
            Id = id[0];

            return this;
        }


        private void OnSwitchView()
        => SwitchView(this, new SwitchViewEventArgs(
                                    ViewTypeEnum.ShowShooterOnStage,
                                    SelectedShooter.Id));
    }
}
