using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektSemestrIV.Models.ShowModels
{
    class ShooterWithStagePointsAndCompetitionPointsOverview
    {
        public uint Position { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public double StagePoints { get; set; }
        public double CompetitionPoints { get; set; }

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
