using MySql.Data.MySqlClient;

namespace ProjektSemestrIV.DAL.Entities.AuxiliaryEntities
{
    class ShooterStatsOnStage
    {
        public string StageName { get; }
        public double Points { get; }
        public string Time { get; }
        public double StagePoints { get; }

        public ShooterStatsOnStage(MySqlDataReader reader)
        {
            StageName = reader.GetString("nazwaTrasy");
            Points = reader.GetDouble("punkty");
            Time = reader.GetTimeSpan("czas").ToString();
            StagePoints = reader.GetDouble("punktyNaTrasie");
        }
    }
}
