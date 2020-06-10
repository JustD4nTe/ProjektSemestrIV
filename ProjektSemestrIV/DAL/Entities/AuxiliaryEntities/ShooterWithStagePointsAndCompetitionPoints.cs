namespace ProjektSemestrIV.DAL.Entities.AuxiliaryEntities
{
    class ShooterWithStagePointsAndCompetitionPoints
    {
        public uint Id { get; }
        public uint Position { get; }
        public string Name { get; }
        public string Surname { get; }
        public double StagePoints { get; }
        public double CompetitionPoints { get; }

        public ShooterWithStagePointsAndCompetitionPoints( uint id, uint position, string name, string surname, double stagePoints, double competitionPoints)
        {
            Id = id;
            Position = position;
            Name = name;
            Surname = surname;
            StagePoints = stagePoints;
            CompetitionPoints = competitionPoints;
        }
    }
}
