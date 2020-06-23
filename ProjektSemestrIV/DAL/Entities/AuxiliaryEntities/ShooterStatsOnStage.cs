using MySql.Data.MySqlClient;
using System.Data;

namespace ProjektSemestrIV.DAL.Entities.AuxiliaryEntities
{
    class ShooterStatsOnStage : IBaseEntity
    {
        public uint StageId { get; private set; }
        public string StageName { get; private set; }
        public double Points { get; private set; }
        public string Time { get; private set; }
        public double StagePoints { get; private set; }

        public void SetData(IDataReader dataReader)
        {
            StageId = uint.Parse(dataReader["trasaId"].ToString());
            StageName = dataReader["nazwaTrasy"].ToString();
            Points = double.Parse(dataReader["punkty"].ToString());
            Time = dataReader["czas"].ToString();
            StagePoints = double.Parse(dataReader["punktyNaTrasie"].ToString());
        }

        // shallow copy
        public object Clone() => this.MemberwiseClone();
    }
}
