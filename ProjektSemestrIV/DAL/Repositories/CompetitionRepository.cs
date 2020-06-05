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

        public static Competition GetCompetitionFromDB(uint id)
        {
            string GetCompetitionQuery = $"SELECT * FROM zawody WHERE id={id}";
            Competition competition = null;

            using (var connection = DatabaseConnection.Instance.Connection)
            {
                var command = new MySqlCommand(GetCompetitionQuery, connection);
                connection.Open();

                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    competition = new Competition(reader);
                }

                connection.Close();
            }
            return competition;
        }

        public static uint GetNumberOfShootersInCompetition(uint competitionId)
        {
            string GetNumberOfShootersQuery = "SELECT COUNT(distinct strzelec.id) as count FROM strzelec " 
                                                + "INNER JOIN tarcza ON strzelec.id=tarcza.strzelec_id "
                                                + "INNER JOIN trasa ON tarcza.trasa_id=trasa.id "
                                                + $"WHERE trasa.id_zawody={competitionId}";
            uint count = default;

            using (var connection = DatabaseConnection.Instance.Connection)
            {
                var command = new MySqlCommand(GetNumberOfShootersQuery, connection);
                connection.Open();

                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    count = uint.Parse(reader["count"].ToString());
                }

                connection.Close();
            }

            return count;
        }

        public static ShooterWithCompetitionTime GetFastestShooterOfCompetition(uint competitionId)
        {
            var GetFastestShooterOfCompetitionQuery = "SELECT imie, nazwisko, sum(czas) AS czas FROM strzelec " 
                                                        + "INNER JOIN przebieg ON strzelec.id=przebieg.id_strzelec "
                                                        + "INNER JOIN trasa ON przebieg.id_trasa=trasa.id "
                                                        + $"WHERE trasa.id_zawody={competitionId} "
                                                        + "GROUP BY strzelec.id ORDER BY czas LIMIT 1;";

            ShooterWithCompetitionTime shooterNameWithTime = null;

            using (var connection = DatabaseConnection.Instance.Connection)
            {
                var command = new MySqlCommand(GetFastestShooterOfCompetitionQuery, connection);
                connection.Open();

                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    shooterNameWithTime = new ShooterWithCompetitionTime(reader);
                }

                connection.Close();
            }

            return shooterNameWithTime;
        }
    }
}