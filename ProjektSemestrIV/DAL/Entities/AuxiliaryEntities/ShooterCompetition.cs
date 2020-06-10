using MySql.Data.MySqlClient;

namespace ProjektSemestrIV.DAL.Entities.AuxiliaryEntities
{
    class ShooterCompetition
    {
        public uint CompetitionId { get; }
        public string Location { get; }
        public string StartDate { get; }
        public uint Position { get; }
        public double Points { get; }

        public ShooterCompetition(MySqlDataReader reader)
        {
            CompetitionId = reader.GetUInt32("competitionId");
            Location = reader.GetString("location");
            StartDate = reader.GetDateTime("startDate").ToString();
            Position = reader.GetUInt32("position");
            Points = reader.GetDouble("points");
        }
    }
}