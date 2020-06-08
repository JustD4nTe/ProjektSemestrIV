namespace ProjektSemestrIV.Models.ShowModels
{
    class StageWithBestPlayerOverview
    {
        public string StageName { get; }
        public string BestPlayer { get; }
        public double Points { get; }

        public StageWithBestPlayerOverview(string stageName, string playerName, string playerSurname, double playerPoints)
        {
            StageName = stageName;
            BestPlayer = playerName + " " + playerSurname;
            Points = playerPoints;
        }
    }
}
