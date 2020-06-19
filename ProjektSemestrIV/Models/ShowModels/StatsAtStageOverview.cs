namespace ProjektSemestrIV.Models.ShowModels
{
    class StatsAtStageOverview
    {
        public uint StageId { get; }
        public string StageName { get; }
        public double StagePoints { get; }
        public string Time { get; }
        public double Points { get; }

        public StatsAtStageOverview(uint stageId, string stageName, double stagePoints, string time, double points)
        {
            StageId = stageId;
            StageName = stageName;
            StagePoints = stagePoints;
            Time = time;
            Points = points;
        }
    }
}
