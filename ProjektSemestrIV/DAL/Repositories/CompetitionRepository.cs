using MySql.Data.MySqlClient;
using ProjektSemestrIV.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektSemestrIV.DAL.Repositories
{
    class CompetitionRepository
    {
        private const string ALL_COMPETITIONS = "SELECT * FROM zawody";

        public static List<Competition> GetAllCompetitionsFromDB()
        {
            List<Competition> competitions = new List<Competition>();
            using (var connection = DatabaseConnection.Instance.Connection)
            {
                var command = new MySqlCommand(ALL_COMPETITIONS, connection);
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                    competitions.Add(new Competition(reader));
                connection.Close();
            }
            return competitions;
        }

        public static Boolean AddCompetitionToDatabase( Competition competition ) {
            Boolean executed = false;
            using(MySqlConnection connection = DatabaseConnection.Instance.Connection) {
                String start = DateTime.Parse(competition.StartDate).ToString("yyyy-MM-dd HH:mm:ss.fff");
                String end = (competition.EndDate != null) ? "\"" + DateTime.Parse(competition.EndDate).ToString("yyyy-MM-dd HH:mm:ss.fff") + "\"" : "NULL";
                MySqlCommand command = new MySqlCommand($"INSERT INTO zawody (`miejsce`, `rozpoczecie`, `zakonczenie`) VALUES ('{competition.Location}', '{start}', '{end}')", connection);
                connection.Open();
                if(command.ExecuteNonQuery() == 1) executed = true;
                connection.Close();
            }
            return executed;
        }

        public static bool EditCompetitionInDatabase( Competition competition, UInt32 id ) {
            Boolean executed = false;
            using(MySqlConnection connection = DatabaseConnection.Instance.Connection) {
                String start = DateTime.Parse(competition.StartDate).ToString("yyyy-MM-dd HH:mm:ss.fff");
                String end = (competition.EndDate != null) ? "\"" + DateTime.Parse(competition.EndDate).ToString("yyyy-MM-dd HH:mm:ss.fff") + "\"": "NULL";
                MySqlCommand command = new MySqlCommand($"UPDATE zawody SET `miejsce` = '{competition.Location}', `rozpoczecie` = '{start}', `zakonczenie` = {end} WHERE (`id` = '{id}')", connection);
                connection.Open();
                if(command.ExecuteNonQuery() == 1) executed = true;
                connection.Close();
            }
            return executed;
        }

        public static Boolean DeleteCompetitionFromDatabase( UInt32 competitionID ) {
            Boolean executed = false;
            using(MySqlConnection connection = DatabaseConnection.Instance.Connection) {
                MySqlCommand command = new MySqlCommand($"DELETE FROM zawody WHERE (`id` = '{competitionID}')", connection);
                connection.Open();
                if(command.ExecuteNonQuery() == 1) executed = true;
                connection.Close();
            }
            return executed;
        }
    }
}