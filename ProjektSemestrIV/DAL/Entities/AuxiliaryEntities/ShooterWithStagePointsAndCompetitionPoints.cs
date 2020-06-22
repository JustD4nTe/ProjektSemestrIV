using System.Data;

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

        public ShooterWithStagePointsAndCompetitionPoints(DataRow data)
        {
            Id = uint.Parse(data["strzelec_id"].ToString());
            Position = uint.Parse(data["position"].ToString());
            Name = data["name"].ToString();
            Surname = data["surname"].ToString();
            StagePoints = double.Parse(data["stagePoints"].ToString());
            CompetitionPoints = double.Parse(data["competitionPoints"].ToString());
        }
    }
}
