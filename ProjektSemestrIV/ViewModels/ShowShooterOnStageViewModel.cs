using ProjektSemestrIV.Models.ComplexModels;

namespace ProjektSemestrIV.ViewModels
{
    class ShowShooterOnStageViewModel
    {
        #region Fields and properties
        private readonly ShowShooterOnStageModel model;

        public string Name { get; }
        public string Surname { get; }
        public string Competition { get; }
        public string StageName { get; }
        public string SumOfPoints { get; }
        public string Time { get; }
        public string GeneralAccuracy { get; }
        public string AlphaAccuracy { get; }
        public string CharlieAccuracy { get; }
        public string DeltaAccuracy { get; }
        public uint Position { get; }
        #endregion

        public ShowShooterOnStageViewModel(uint shooterId, uint stageId)
        {
            model = new ShowShooterOnStageModel(shooterId, stageId);

            Name = model.GetShooterName();
            Surname = model.GetShooterSurname();
            
            Competition = model.GetCompetitionName();
            StageName = model.GetStageName();
            SumOfPoints = model.GetStagePoints();
            Time = model.GetStageTime();
            GeneralAccuracy = model.GetGeneralAccuracy();
            AlphaAccuracy = model.GetAlphaAccuracy();
            CharlieAccuracy = model.GetCharlieAccuracy();
            DeltaAccuracy = model.GetDeltaAccuracy();
            Position = model.GetPositionOnStage();
        }
    }
}
