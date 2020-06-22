using MySql.Data.MySqlClient;
using System.Data;

namespace ProjektSemestrIV.DAL.Entities.AuxiliaryEntities
{
    class ShooterStatsOnStage
    {
        public uint StageId { get; }
        public string StageName { get; }
        public double Points { get; }
        public string Time { get; }
        public double StagePoints { get; }

        public ShooterStatsOnStage(DataRow data)
        {
            StageId = uint.Parse(data["trasaId"].ToString());
            StageName = data["nazwaTrasy"].ToString();
            Points = double.Parse(data["punkty"].ToString());
            Time = data["czas"].ToString();
            StagePoints = double.Parse(data["punktyNaTrasie"].ToString());
        }
    }
}
