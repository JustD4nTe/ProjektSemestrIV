namespace ProjektSemestrIV.Models.ShowModels
{
    class StageWithBestPlayerOverview
    {
        public string StageName { get; set; }
        public string BestPlayer { get; set; }
        public double Points { get; set; }

        public StageWithBestPlayerOverview(string stageName, string playerName, 
            string playerSurname, double playerPoints)
        {
            StageName = stageName;
            BestPlayer = playerName + " " + playerSurname;
            Points = playerPoints;
        }
    }
}
