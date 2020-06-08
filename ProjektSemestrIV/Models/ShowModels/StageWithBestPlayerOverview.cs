namespace ProjektSemestrIV.Models.ShowModels
{
    class StageWithBestPlayerOverview
    {
        public uint Id { get; }
        public string StageName { get; }
        public string BestPlayer { get; }
        public double Points { get; }

        public StageWithBestPlayerOverview(uint id, string stageName, string playerName, string playerSurname, double playerPoints)
        {
            Id = id;
            StageName = stageName;
            BestPlayer = playerName + " " + playerSurname;
            Points = playerPoints;
        }
    }
}
