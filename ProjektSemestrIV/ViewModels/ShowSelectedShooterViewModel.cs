using ProjektSemestrIV.Extensions;
using ProjektSemestrIV.Models.ComplexModels;
using ProjektSemestrIV.Models.ShowModels;
using ProjektSemestrIV.ViewModels.BaseClass;
using System;
using System.Collections.ObjectModel;

namespace ProjektSemestrIV.ViewModels
{
    class ShowSelectedShooterViewModel : BaseViewModel, ISubView
    {
        #region Fields and properties
        private readonly ShowSelectedShooterModel model;

        public string Name { get; private set; }

        public string Surname { get; private set; }

        public string GeneralAccuracy { get; private set; }

        public string AlphaAccuracy { get; private set; }

        public string CharlieAccuracy { get; private set; }

        public string DeltaAccuracy { get; private set; }

        public string AveragePosition { get; private set; }

        public ObservableCollection<ShooterCompetitionOverview> Competitions { get; private set; }
        #endregion

        public ShowSelectedShooterViewModel()
        {
            model = new ShowSelectedShooterModel();
        }

        public BaseViewModel GetView(uint id)
        {
            model.SetNewId(id);

            Name = model.GetShooterName();
            Surname = model.GetShooterSurname();
            GeneralAccuracy = model.GetShooterCompetitionGeneralAccuracy();
            AlphaAccuracy = model.GetShooterCompetitionAlphaAccuracy();
            CharlieAccuracy = model.GetShooterCompetitionCharlieAccuracy();
            DeltaAccuracy = model.GetShooterCompetitionDeltaAccuracy();
            AveragePosition = model.GetShooterGeneralAveragePosition();
            Competitions = model.GetShooterCompetitions().Convert();

            return this;
        }
    }
}
