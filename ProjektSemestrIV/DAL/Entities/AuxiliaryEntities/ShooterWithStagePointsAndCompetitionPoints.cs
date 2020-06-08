namespace ProjektSemestrIV.DAL.Entities.AuxiliaryEntities
{
    class ShooterWithStagePointsAndCompetitionPoints
    {
        public uint Position { get; }
        public string Name { get; }
        public string Surname { get; }
        public double StagePoints { get; }
        public double CompetitionPoints { get; }

        public ShooterWithStagePointsAndCompetitionPoints(uint position, string name, string surname, double stagePoints, double competitionPoints)
        {
            Position = position;
            Name = name;
            Surname = surname;
            StagePoints = stagePoints;
            CompetitionPoints = competitionPoints;
        }
    }
}
