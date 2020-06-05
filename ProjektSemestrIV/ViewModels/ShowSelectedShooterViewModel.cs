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
    class ShowSelectedShooterViewModel : BaseViewModel
    {
        #region Fields and properties
        private ShowSelectedShooterModel model;

        public string Name { get; }

        public string Surname { get; }

        public double GeneralAccuracy { get; }

        public double AlphaAccuracy { get; }

        public double CharlieAccuracy { get; }

        public double DeltaAccuracy { get; }

        public double AveragePosition { get; }

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
