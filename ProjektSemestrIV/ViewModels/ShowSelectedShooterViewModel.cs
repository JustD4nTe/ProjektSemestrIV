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
    class ShowSelectedShooterViewModel : SwitchViewModel
    {
        #region Fields and properties
        private readonly ShowSelectedShooterModel model;

        public uint Id { get; private set; }

        public string Name { get; private set; }

        public string Surname { get; private set; }

        public string SumOfPoints { get; private set; }

        public string SumOfTimes { get; private set; }

        public string GeneralAccuracy { get; private set; }

        public string AlphaAccuracy { get; private set; }

        public string CharlieAccuracy { get; private set; }

        public string DeltaAccuracy { get; private set; }

        public string AveragePosition { get; private set; }

        public ObservableCollection<ShooterCompetitionOverview> Competitions { get; private set; }

        public ShooterCompetitionOverview SelectedCompetition { get; set; }

        public ICommand SwitchViewCommand { get; }
        #endregion

        public ShowSelectedShooterViewModel()
        {
            model = new ShowSelectedShooterModel();

            SwitchViewCommand = new RelayCommand(x => OnSwitchView(), x => SelectedCompetition != null);
        }

        public override IBaseViewModel GetViewModel(params uint[] id)
        {
            model.SetNewId(id[0]);

            Id = id[0];

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

            return this;
        }

        public void OnSwitchView()
        => SwitchView(this, new SwitchViewEventArgs(
                                    ViewTypeEnum.ShowSelectedShooterInCompetition,
                                    SelectedCompetition.CompetitionId));
    }
}
