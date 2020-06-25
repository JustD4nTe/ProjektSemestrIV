using ProjektSemestrIV.Extensions;
using ProjektSemestrIV.Models;
using ProjektSemestrIV.Models.ComplexModels;
using ProjektSemestrIV.Models.ShowModels;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Navigation;

namespace ProjektSemestrIV.ViewModels
{
    class ShowShooterViewModel
    {
        #region Fields and properties
        private readonly ShowShooterModel model;
        private readonly NavigationService navigation;
        private readonly uint id;

        public string Name { get; }
        public string Surname { get; }
        public string SumOfPoints { get; }
        public string SumOfTimes { get; }
        public string GeneralAccuracy { get; }
        public string AlphaAccuracy { get; }
        public string CharlieAccuracy { get; }
        public string DeltaAccuracy { get; }
        public string AveragePosition { get; }

        public ObservableCollection<ShooterCompetitionOverview> Competitions { get; }

        public ShooterCompetitionOverview SelectedCompetition { get; set; }

        public ICommand SwitchViewCommand { get; }
        #endregion

        public ShowShooterViewModel(NavigationService navigation, uint id)
        {
            this.navigation = navigation;
            this.id = id;

            model = new ShowShooterModel(id);

            SwitchViewCommand = new RelayCommand(x => OnSwitchView(), 
                                                 x => SelectedCompetition != null);

            Name = model.GetShooterName();
            Surname = model.GetShooterSurname();
            SumOfPoints = model.GetShooterGeneralSumOfPoints();
            SumOfTimes = model.GetShooterGeneralSumOfTimes();
            GeneralAccuracy = model.GetShooterCompetitionGeneralAccuracy();
            AlphaAccuracy = model.GetShooterCompetitionAlphaAccuracy();
            CharlieAccuracy = model.GetShooterCompetitionCharlieAccuracy();
            DeltaAccuracy = model.GetShooterCompetitionDeltaAccuracy();
            AveragePosition = model.GetShooterGeneralAveragePosition();
            Competitions = model.GetShooterCompetitions().Convert();
        }

        public void OnSwitchView()
        => navigation.Navigate(new ShowShooterInCompetitionViewModel(navigation, id, SelectedCompetition.CompetitionId));
    }
}
