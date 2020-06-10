using ProjektSemestrIV.Extensions;
using ProjektSemestrIV.Models.ComplexModels;
using ProjektSemestrIV.Models.ShowModels;
using ProjektSemestrIV.ViewModels.BaseClass;
using System;
using System.Collections.ObjectModel;

namespace ProjektSemestrIV.ViewModels {
    class ShowShooterOnStageViewModel : SwitchViewModel {
        #region Fields and properties
        private readonly ShowShooterOnStageModel model;

        public string Name { get; private set; }

        public string Surname { get; private set; }

        public string Competition { get; private set; }

        public string StageName { get; private set; }

        public string SumOfPoints { get; private set; }

        public string Time { get; private set; }

        public string GeneralAccuracy { get; private set; }

        public string AlphaAccuracy { get; private set; }

        public string CharlieAccuracy { get; private set; }

        public string DeltaAccuracy { get; private set; }

        public uint Position { get; private set; }
        #endregion

        public ShowShooterOnStageViewModel() {
            model = new ShowShooterOnStageModel();
        }

        public override IBaseViewModel GetViewModel( params uint[] id ) {
            model.SetNewShooterId(id[0]);
            model.SetNewStageId(id[1]);

            Name = model.GetShooterName();
            Surname = model.GetShooterSurname();
            Competition = model.getShooterOnStageCompetition();
            StageName = model.getShooterOnStageStageName();
            SumOfPoints = model.GetShooterOnStageSumOfPoints();
            Time = model.GetShooterOnStageTime();
            GeneralAccuracy = model.GetShooterOnStageGeneralAccuracy();
            AlphaAccuracy = model.GetShooterOnStageAlphaAccuracy();
            CharlieAccuracy = model.GetShooterOnStageCharlieAccuracy();
            DeltaAccuracy = model.GetShooterOnStageDeltaAccuracy();
            Position = model.GetShooterOnStagePosition();

            return this;
        }
    }
}
