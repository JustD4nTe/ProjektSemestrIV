using MySql.Data.MySqlClient;
using ProjektSemestrIV.DAL.Entities;
using ProjektSemestrIV.DAL.Entities.AuxiliaryEntities;
using System;
using System.Collections.Generic;
using System.Data;

namespace ProjektSemestrIV.DAL.Repositories
{
    class CompetitionRepository : BaseRepository
    {
        #region CRUD
        public static IEnumerable<Competition> GetAllCompetitionsFromDB()
        {
            var query = "SELECT * FROM zawody";

            DataTable resultOfQuery = ExecuteSelectQuery(query);
            List<Competition> competitions = new List<Competition>();

            foreach (DataRow row in resultOfQuery.Rows)
                competitions.Add(new Competition(row));

            return competitions;
        }

        public static Competition GetCompetitionFromDB(uint id)
        {
            var query = $"SELECT * FROM zawody WHERE id={id}";

            DataTable resultOfQuery = ExecuteSelectQuery(query);

            return resultOfQuery.Rows.Count == 1 ? new Competition(resultOfQuery.Rows[0]) : null;
        }

        public static bool AddCompetitionToDatabase(Competition competition)
        {
            var query = @"INSERT INTO zawody (`miejsce`, `rozpoczecie`, `zakonczenie`)
                            VALUES (@miejsce, @rozpoczecie, @zakonczenie)";

            return ExecuteAddQuery(query, competition.GetParameters());
        }

        public static bool EditCompetitionInDatabase(Competition competition, uint id)
        {
            var query = $@"UPDATE zawody 
                            SET `miejsce` = @miejsce, `rozpoczecie` = @rozpoczenie, `zakonczenie` = @zakonczenie 
                            WHERE (`id` = '{id}')";

            return ExecuteUpdateQuery(query,competition.GetParameters());
        }

        public static bool DeleteCompetitionFromDatabase(uint competitionID)
        {
            var query = $@"DELETE FROM zawody WHERE (`id` = '{competitionID}')";

            return ExecuteDeleteQuery(query);
        }
        #endregion

        #region Auxiliary queries
        public static uint GetNumberOfShootersInCompetition(uint competitionId)
        {
            var query = $@"SELECT COUNT(distinct strzelec.id) AS count FROM strzelec 
                            INNER JOIN tarcza ON strzelec.id=tarcza.strzelec_id
                            INNER JOIN trasa ON tarcza.trasa_id=trasa.id
                            WHERE trasa.id_zawody={competitionId}";

            DataTable resultOfQuery = ExecuteSelectQuery(query);

            var readValue = resultOfQuery.Rows[0]["count"];

            return readValue is DBNull ? 0 : uint.Parse(readValue.ToString());
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
        #endregion
    }
}