namespace ProjektSemestrIV.Models.ShowModels
{
    class StatsAtStageOverview
    {
        public string StageName { get; }
        public double StagePoints { get; }
        public string Time { get; }
        public double Points { get; }

        public StatsAtStageOverview(string stageName, double stagePoints, string time, double points)
        {
            StageName = stageName;
            StagePoints = stagePoints;
            Time = time;
            Points = points;
        }
    }
}
