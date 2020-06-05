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

        public static IEnumerable<ShooterWithPoints> GetPlayersWithPointsFromStage(uint competitionId, bool isPodium = false)
        {            
            // calculate points from target
            var pointsFormula = "((sum(alpha)*5 + sum(charlie)*3 + sum(delta))-10*(sum(miss)+sum(tarcza.`n-s`)+sum(proc)+sum(extra)))";

            // calculate points for every shooter
            var rawPoints = $"select strzelec.id as strzelec_id, trasa.id as trasa_id, ({pointsFormula}) as suma "
                            + "from strzelec inner join tarcza on strzelec.id=tarcza.strzelec_id "
                            + "inner join trasa on tarcza.trasa_id=trasa.id "
                            + $"where trasa.id_zawody={competitionId} "
                            + "group by strzelec.id, trasa.id";


            // points / stage time
            var pointsFromStage = "select strzelec.imie as Imie, strzelec.nazwisko as Nazwisko, sum(sumowanieTarcz.suma/przebieg.czas) as SumaPunktow "
                                    + $"from ({rawPoints}) as sumowanieTarcz "
                                    + "inner join przebieg on przebieg.id_strzelec = sumowanieTarcz.strzelec_id and przebieg.id_trasa = sumowanieTarcz.trasa_id "
                                    + "inner join strzelec on strzelec.id = sumowanieTarcz.strzelec_id "
                                    + "group by sumowanieTarcz.strzelec_id "
                                    + "order by sumaPunktow desc ";
            
            if (isPodium)
            {
                pointsFromStage += "Limit 3";
            }

            var shooters = new List<ShooterWithPoints>();
            using (var connection = DatabaseConnection.Instance.Connection)
            {
                var command = new MySqlCommand(pointsFromStage, connection);
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    shooters.Add(new ShooterWithPoints(reader));
                }
                connection.Close();
            }

            return shooters;

        }
    }
}