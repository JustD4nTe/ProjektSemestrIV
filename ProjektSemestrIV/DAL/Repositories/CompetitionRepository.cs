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
        private const string AllCompetitions = "SELECT * FROM zawody";

        public static List<Competition> GetAllCompetitionsFromDB()
        {
            List<Competition> competitions = new List<Competition>();
            using (var connection = DatabaseConnection.Instance.Connection)
            {
                var command = new MySqlCommand(AllCompetitions, connection);
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                    competitions.Add(new Competition(reader));
                connection.Close();
            }
            return competitions;
        }
    }
}