namespace ProjektSemestrIV.Models.ShowModels
{
    class StatsAtStageOverview
    {
        public string StageName { get; }
        public int StagePoints { get; }
        public double Time { get; }
        public double Points { get; }

        public StatsAtStageOverview(string stageName, int stagePoints, double time, double points)
        {
            StageName = stageName;
            StagePoints = stagePoints;
            Time = time;
            Points = points;
        }
    }
}
