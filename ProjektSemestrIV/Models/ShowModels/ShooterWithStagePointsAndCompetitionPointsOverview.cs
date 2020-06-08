namespace ProjektSemestrIV.Models.ShowModels
{
    class ShooterWithStagePointsAndCompetitionPointsOverview
    {
        public uint Position { get; }
        public string Name { get; }
        public string Surname { get; }
        public double StagePoints { get; }
        public double CompetitionPoints { get; }

        public ShooterWithStagePointsAndCompetitionPointsOverview(uint position, string name, string surname, double stagePoints, double competitionPoints)
        {
            Position = position;
            Name = name;
            Surname = surname;
            StagePoints = stagePoints;
            CompetitionPoints = competitionPoints;
        }
    }
}
