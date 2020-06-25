using ProjektSemestrIV.Extensions;
using ProjektSemestrIV.Models.ComplexModels;
using ProjektSemestrIV.Models.ShowModels;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Navigation;

namespace ProjektSemestrIV.ViewModels
{
    class ShowStageViewModel
    {
        private readonly ShowStageModel model;
        private readonly NavigationService navigation;
        private readonly uint stageId;

        public string CompetitionLocation { get; }
        public string StageName { get; }
        public string StageRules { get; }
        public uint NumOfTargets { get; }
        public string BestShooter { get; }
        public double AverageTime { get; }

        public ObservableCollection<ShooterWithStageAndCompetitionPointsOverview> Shooters { get; }
        public ShooterWithStageAndCompetitionPointsOverview SelectedShooter { get; set; }

        public ICommand SwitchViewCommand { get; }

        public ShowStageViewModel(NavigationService navigation, uint stageId)
        {
            this.stageId = stageId;
            this.navigation = navigation;
            model = new ShowStageModel(stageId);
            SwitchViewCommand = new RelayCommand(x => OnSwitchView(), x => SelectedShooter != null);

            CompetitionLocation = model.GetCompetitionLocation();
            StageName = model.GetStageName();
            StageRules = model.GetStageRules();
            NumOfTargets = model.GetNumOfTargets();
            BestShooter = model.GetShooterWithPoints();
            AverageTime = model.GetAverageTime();
            Shooters = model.GetShooters().Convert();           
        }

        private void OnSwitchView()
        => navigation.Navigate(new ShowShooterOnStageViewModel(SelectedShooter.Id, stageId));
    }
}
