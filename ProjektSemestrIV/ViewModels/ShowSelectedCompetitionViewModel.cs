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
    class ShowSelectedCompetitionViewModel
    {
        private readonly ShowSelectedCompetitionModel model;
        private readonly NavigationService navigation;
        private readonly uint Id;

        public string DurationDate { get; }
        public string Location { get; }
        public uint ShootersCount { get; }
        public string FastestShooter { get; }
        public string Podium { get; }

        public ObservableCollection<StageWithBestPlayerOverview> Stages { get; }
        public ObservableCollection<ShooterWithPointsOverview> Shooters { get; }

        public StageWithBestPlayerOverview SelectedStage { get; set; }
        public ShooterWithPointsOverview SelectedShooter { get; set; }

        public ICommand SwitchViewToStageCommand { get; }
        public ICommand SwitchViewToShooterCommand { get; }

        public ShowSelectedCompetitionViewModel(NavigationService _navigation, uint id)
        {
            navigation = _navigation;

            Id = id;

            model = new ShowSelectedCompetitionModel(id);

            DurationDate = model.GetDurationDate();
            Location = model.GetLocation();
            ShootersCount = model.GetShootersCount();
            FastestShooter = model.GetFastestShooter();
            Podium = model.GetShootersOnPodium();
            Stages = model.GetStageWithBestShooters().Convert();
            Shooters = model.GetShootersFromCompetition().Convert();
            SwitchViewToStageCommand = new RelayCommand(x => OnSwitchViewToStage(), x => SelectedStage != null);
            SwitchViewToShooterCommand = new RelayCommand(x => OnSwitchViewToShooter(), x => SelectedShooter != null);
        }

        private void OnSwitchViewToStage()
        => navigation.Navigate(new ShowSelectedStageViewModel(navigation, SelectedStage.Id));

        private void OnSwitchViewToShooter()
        => navigation.Navigate(new ShowSelectedShooterInCompetitionViewModel(SelectedShooter.Id, Id));
    }
}
