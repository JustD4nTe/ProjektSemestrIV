namespace ProjektSemestrIV.Models.ShowModels
{
    class ShooterStatsOnStageShowModel
    {
        public uint StageId { get; }
        public string StageName { get; }
        public double StagePoints { get; }
        public string Time { get; }
        public double Points { get; }

        public ShooterStatsOnStageShowModel(uint stageId, string stageName, double stagePoints, string time, double points)
        {
            StageId = stageId;
            StageName = stageName;
            StagePoints = stagePoints;
            Time = time;
            Points = points;
        }
    }
}
