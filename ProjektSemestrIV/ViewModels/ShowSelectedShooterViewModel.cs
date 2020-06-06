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
    class ShowSelectedShooterViewModel : BaseViewModel
    {
        #region Fields and properties
        private ShowSelectedShooterModel model;

        public string Name { get; }

        public string Surname { get; }

        public string GeneralAccuracy { get; }

        public string AlphaAccuracy { get; }

        public string CharlieAccuracy { get; }

        public string DeltaAccuracy { get; }

        public string AveragePosition { get; }

        public ObservableCollection<ShooterCompetitionOverview> Competitions { get; }
        #endregion

        public ShowSelectedShooterViewModel(uint id)
        {
            model = new ShowSelectedShooterModel(id);
            Name = model.GetShooterName();
            Surname = model.GetShooterSurname();
            GeneralAccuracy = model.GetShooterCompetitionGeneralAccuracy();
            AlphaAccuracy = model.GetShooterCompetitionAlphaAccuracy();
            CharlieAccuracy = model.GetShooterCompetitionCharlieAccuracy();
            DeltaAccuracy = model.GetShooterCompetitionDeltaAccuracy();
            AveragePosition = model.GetShooterGeneralAveragePosition();
            Competitions = new ObservableCollection<ShooterCompetitionOverview>(model.GetShooterCompetitions());
        }
    }
}
