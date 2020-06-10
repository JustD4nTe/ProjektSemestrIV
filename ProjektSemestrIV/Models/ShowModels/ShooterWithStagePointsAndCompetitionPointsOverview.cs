using System;

namespace ProjektSemestrIV.Models.ShowModels
{
    class ShooterWithStagePointsAndCompetitionPointsOverview
    {
        public uint Id { get; }
        public uint Position { get; }
        public string Name { get; }
        public string Surname { get; }
        public double StagePoints { get; }
        public double CompetitionPoints { get; }

        public ShooterWithStagePointsAndCompetitionPointsOverview(uint id ,uint position, string name, string surname, double stagePoints, double competitionPoints)
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
