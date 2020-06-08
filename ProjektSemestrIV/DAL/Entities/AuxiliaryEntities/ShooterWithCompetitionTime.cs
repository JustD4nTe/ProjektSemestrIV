using MySql.Data.MySqlClient;
using System;

namespace ProjektSemestrIV.DAL.Entities.AuxiliaryEntities
{
    class ShooterWithCompetitionTime
    {
        public String Name { get;}
        public String Surname { get;}
        public double TimeInSeconds { get;}

        public ShooterWithCompetitionTime(MySqlDataReader reader)
        {
            Name = reader["imie"].ToString();
            Surname = reader["nazwisko"].ToString();
            TimeInSeconds = double.Parse(reader["czas"].ToString());
        }
    }
}
