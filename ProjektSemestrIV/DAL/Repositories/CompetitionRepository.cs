using MySql.Data.MySqlClient;
using ProjektSemestrIV.DAL.Entities;
using ProjektSemestrIV.DAL.Entities.AuxiliaryEntities;
using System;
using System.Collections.Generic;

namespace ProjektSemestrIV.DAL.Repositories
{
    class CompetitionRepository
    {
        public static List<Competition> GetAllCompetitionsFromDB()
        {
            var query = "SELECT * FROM zawody";

            List<Competition> competitions = new List<Competition>();

            using (var connection = DatabaseConnection.Instance.Connection)
            {
                var command = new MySqlCommand(query, connection);
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
            var query = $"SELECT * FROM zawody WHERE id={id}";
            Competition competition = null;

            using (var connection = DatabaseConnection.Instance.Connection)
            {
                var command = new MySqlCommand(query, connection);
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
            var query = $@"SELECT COUNT(distinct strzelec.id) AS count FROM strzelec 
                            INNER JOIN tarcza ON strzelec.id=tarcza.strzelec_id
                            INNER JOIN trasa ON tarcza.trasa_id=trasa.id
                            WHERE trasa.id_zawody={competitionId}";

            uint count = default;

            using (var connection = DatabaseConnection.Instance.Connection)
            {
                var command = new MySqlCommand(query, connection);
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
            var query = $@"SELECT imie, nazwisko, sum(czas) AS czas FROM strzelec
                            INNER JOIN przebieg ON strzelec.id=przebieg.id_strzelec
                            INNER JOIN trasa ON przebieg.id_trasa=trasa.id
                            WHERE trasa.id_zawody={competitionId}
                            GROUP BY strzelec.id ORDER BY czas LIMIT 1;";

            ShooterWithCompetitionTime shooterNameWithTime = null;

            using (var connection = DatabaseConnection.Instance.Connection)
            {
                var command = new MySqlCommand(query, connection);
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

        public static IEnumerable<ShooterWithPoints> GetShootersWithPointsFromStage(uint competitionId, bool isPodium = false)
        {
            var query = $@"SELECT strzelec.id AS id, strzelec.imie AS imie, strzelec.nazwisko AS nazwisko, 
                                    sum(sumowanieTarcz.suma/przebieg.czas) AS sumaPunktow
                            FROM (
	                            SELECT strzelec.id AS strzelec_id, trasa.id AS trasa_id, 
			                            (((sum(alpha)*5 + sum(charlie)*3 + sum(delta))-10*(sum(miss)+sum(tarcza.`n-s`)+sum(proc)+sum(extra)))) AS suma 
	                            FROM strzelec INNER JOIN tarcza ON strzelec.id=tarcza.strzelec_id
	                            INNER JOIN trasa ON tarcza.trasa_id=trasa.id
	                            WHERE trasa.id_zawody={competitionId}
	                            GROUP BY strzelec.id, trasa.id) AS sumowanieTarcz
                            INNER JOIN przebieg ON przebieg.id_strzelec = sumowanieTarcz.strzelec_id and przebieg.id_trasa = sumowanieTarcz.trasa_id
                            INNER JOIN strzelec ON strzelec.id = sumowanieTarcz.strzelec_id 
                            GROUP BY sumowanieTarcz.strzelec_id
                            ORDER BY sumaPunktow desc ";
            
            if (isPodium)
            {
                query += "LIMIT 3";
            }

            var shooters = new List<ShooterWithPoints>();
            using (var connection = DatabaseConnection.Instance.Connection)
            {
                var command = new MySqlCommand(query, connection);
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

        public static IEnumerable<StageWithBestShooter> GetStagesWithBestShooter(uint competitionId)
        {
            var query = $@"WITH ranking AS (
                            SELECT trasa.id AS trasa_id, trasa.nazwa AS nazwaTrasy, dzikiePunkty.strzelec_id, dzikiePunkty.suma/przebieg.czas AS punktyStrzelca, 
                                RANK() OVER(PARTITION BY trasa.nazwa ORDER BY dzikiePunkty.suma / przebieg.czas DESC) rankingGraczy
                            FROM(
                                SELECT strzelec.id AS strzelec_id, trasa.id AS trasa_id, 
                                    ((sum(alpha) * 5 + sum(charlie) * 3 + sum(delta)) - 10 * (sum(miss) + sum(`n-s`) + sum(proc) + sum(extra))) AS suma 
                                FROM strzelec INNER JOIN tarcza ON strzelec.id = tarcza.strzelec_id 
                                INNER JOIN trasa ON tarcza.trasa_id = trasa.id 
                                WHERE trasa.id_zawody = {competitionId} 
                                GROUP BY strzelec.id, trasa.id) AS dzikiePunkty 
                            INNER JOIN przebieg ON przebieg.id_strzelec = dzikiePunkty.strzelec_id and przebieg.id_trasa = dzikiePunkty.trasa_id 
                            INNER JOIN trasa ON trasa.id = dzikiePunkty.trasa_id 
                            ORDER BY nazwaTrasy)
                        SELECT trasa_id, nazwaTrasy, strzelec.imie AS imieStrzelca, strzelec.nazwisko AS nazwiskoStrzelca, punktyStrzelca
                        FROM ranking
                        INNER JOIN strzelec ON strzelec.id = strzelec_id
                        WHERE rankingGraczy = 1";

            var stages = new List<StageWithBestShooter>();

            using (var connection = DatabaseConnection.Instance.Connection)
            {
                var command = new MySqlCommand(query, connection);
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    stages.Add(new StageWithBestShooter(reader));
                }
                connection.Close();
            }

            return stages;
        }

        public static Boolean AddCompetitionToDatabase( Competition competition ) {
            Boolean executed = false;

            String start = DateTime.Parse(competition.StartDate).ToString("yyyy-MM-dd HH:mm:ss.fff");
            String end = (competition.EndDate != null) ? "\"" + DateTime.Parse(competition.EndDate).ToString("yyyy-MM-dd HH:mm:ss.fff") + "\"" : "NULL";


            var query = $@"INSERT INTO zawody (`miejsce`, `rozpoczecie`, `zakonczenie`)
                            VALUES ('{competition.Location}', '{start}', '{end}')";

            using (MySqlConnection connection = DatabaseConnection.Instance.Connection) {
                
                MySqlCommand command = new MySqlCommand(query, connection);
                connection.Open();

                if(command.ExecuteNonQuery() == 1) executed = true;

                connection.Close();
            }
            return executed;
        }

        public static bool EditCompetitionInDatabase( Competition competition, UInt32 id ) {
            Boolean executed = false;

            var dateFromat = "yyyy-MM-dd HH:mm:ss.fff";

            String start = DateTime.Parse(competition.StartDate).ToString(dateFromat);
            String end = (competition.EndDate != null) 
                            ? "\"" + DateTime.Parse(competition.EndDate).ToString(dateFromat) + "\"" 
                            : "NULL";

            var query = $@"UPDATE zawody 
                            SET `miejsce` = '{competition.Location}', `rozpoczecie` = '{start}', `zakonczenie` = {end} 
                            WHERE (`id` = '{id}')";

            using (MySqlConnection connection = DatabaseConnection.Instance.Connection) {
                
                MySqlCommand command = new MySqlCommand(query, connection);
                connection.Open();

                if(command.ExecuteNonQuery() == 1) executed = true;

                connection.Close();
            }
            return executed;
        }

        public static Boolean DeleteCompetitionFromDatabase( UInt32 competitionID ) {
            Boolean executed = false;

            var query = $@"DELETE FROM zawody WHERE (`id` = '{competitionID}')";

            using (MySqlConnection connection = DatabaseConnection.Instance.Connection) {
                MySqlCommand command = new MySqlCommand(query, connection);
                connection.Open();
                if(command.ExecuteNonQuery() == 1) executed = true;
                connection.Close();
            }
            return executed;
        }
    }
}