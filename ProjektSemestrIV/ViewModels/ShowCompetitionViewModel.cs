using ProjektSemestrIV.DAL.Entities;
using ProjektSemestrIV.Extensions;
using ProjektSemestrIV.Models;
using ProjektSemestrIV.Models.ComplexModels;
using ProjektSemestrIV.Models.ShowModels;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Navigation;

namespace ProjektSemestrIV.ViewModels
{
    class ShowCompetitionViewModel
    {
        private readonly ShowCompetitionModel model;
        private readonly NavigationService navigation;
        private readonly uint id;

        public string DurationDate { get; }
        public string Location { get; }
        public uint ShootersCount { get; }
        public string FastestShooter { get; }
        public string Podium { get; }

        public ObservableCollection<StageWithBestShooterOverview> Stages { get; }
        public ObservableCollection<ShooterWithPointsOverview> Shooters { get; }

        public StageWithBestShooterOverview SelectedStage { get; set; }
        public ShooterWithPointsOverview SelectedShooter { get; set; }

        public ICommand SwitchViewToStageCommand { get; }
        public ICommand SwitchViewToShooterCommand { get; }

        public ShowCompetitionViewModel(NavigationService navigation, uint id)
        {
            this.navigation = navigation;

            this.id = id;

            model = new ShowCompetitionModel(id);

            DurationDate = model.GetDurationDate();
            Location = model.GetLocation();
            ShootersCount = model.GetShootersCount();
            FastestShooter = model.GetFastestShooter();
            Podium = model.GetPodium();
            Stages = model.GetStagesWithBestShooters().Convert();
            Shooters = model.GetShootersWithPointsOnStage().Convert();
            SwitchViewToStageCommand = new RelayCommand(x => OnSwitchViewToStage(), x => SelectedStage != null);
            SwitchViewToShooterCommand = new RelayCommand(x => OnSwitchViewToShooter(), x => SelectedShooter != null);
        }

        private void OnSwitchViewToStage()
        => navigation.Navigate(new ShowStageViewModel(navigation, SelectedStage.Id));

        private void OnSwitchViewToShooter()
        => navigation.Navigate(new ShowShooterInCompetitionViewModel(navigation, SelectedShooter.Id, id));
    }
}
