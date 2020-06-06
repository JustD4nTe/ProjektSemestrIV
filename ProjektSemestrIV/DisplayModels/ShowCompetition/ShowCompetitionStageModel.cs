namespace ProjektSemestrIV.DisplayModels
{
    class ShowCompetitionStageModel
    {
        public string StageName { get; set; }
        public string BestPlayer { get; set; }

        public ShowCompetitionStageModel(string stageName, string playerName, 
            string playerSurname, double playerPoints)
        {
            StageName = stageName;
            BestPlayer = playerName + " " + playerSurname 
                            + ": " + playerPoints.ToString();
        }
    }
}
