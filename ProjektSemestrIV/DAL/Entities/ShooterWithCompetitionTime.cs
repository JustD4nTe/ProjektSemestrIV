using MySql.Data.MySqlClient;
using System;

namespace ProjektSemestrIV.DAL.Entities
{
    class ShooterWithCompetitionTime
    {
        public String Name { get; set; }
        public String Surname { get; set; }
        public double TimeInSeconds { get; set; }

        public ShooterWithCompetitionTime(MySqlDataReader reader)
        {
            Name = reader["imie"].ToString();
            Surname = reader["nazwisko"].ToString();
            TimeInSeconds = double.Parse(reader["czas"].ToString());
        }
    }
}
