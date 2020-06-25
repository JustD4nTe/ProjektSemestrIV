using System.Data;

namespace ProjektSemestrIV.DAL.Entities.AuxiliaryEntities
{
    class ShooterWithStageAndCompetitionPoints : IBaseEntity
    {
        public uint Id { get; private set; }
        public uint Position { get; private set; }
        public string Name { get; private set; }
        public string Surname { get; private set; }
        public double StagePoints { get; private set; }
        public double CompetitionPoints { get; private set; }

        public void SetData(IDataReader dataReader)
        {
            Id = uint.Parse(dataReader["strzelec_id"].ToString());
            Position = uint.Parse(dataReader["position"].ToString());
            Name = dataReader["name"].ToString();
            Surname = dataReader["surname"].ToString();
            StagePoints = double.Parse(dataReader["stagePoints"].ToString());
            CompetitionPoints = double.Parse(dataReader["competitionPoints"].ToString());
        }

        // shallow copy
        public object Clone() => this.MemberwiseClone();
    }
}
