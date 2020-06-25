using MySql.Data.MySqlClient;
using ProjektSemestrIV.DAL.Entities;
using ProjektSemestrIV.DAL.Entities.AuxiliaryEntities;
using ProjektSemestrIV.Models;
using ProjektSemestrIV.Models.ShowModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektSemestrIV.DAL.Repositories
{
    class ShooterRepository : BaseRepository
    {
        #region CRUD
        public static bool AddShooterToDB(Shooter shooter)
        {
            var query = @"INSERT INTO strzelec (`imie`, `nazwisko`)
                            VALUES (@imie, @nazwisko)";

            return ExecuteModifyQuery(query, shooter.GetParameters());
        }

        public static bool EditShooterInDB(Shooter shooter, uint id)
        {
            var query = $@"UPDATE strzelec 
                            SET `imie` = @imie, `nazwisko` = @nazwisko 
                            WHERE (`id` = '{id}')";

            return ExecuteModifyQuery(query, shooter.GetParameters());
        }

        public static IEnumerable<Shooter> GetAllShootersFromDB()
        {
            var query = "SELECT * FROM strzelec";

            return ExecuteSelectQuery<Shooter>(query);
        }

        public static Shooter GetShooterByIdFromDB(uint id)
        {
            string query = $"SELECT * FROM strzelec WHERE strzelec.id = {id}";

            return ExecuteSelectQuery<Shooter>(query).FirstOrDefault();
        }

        public static bool DeleteShooterFromDB(uint shooterID)
        {
            string query = $"DELETE FROM strzelec WHERE (`id` = '{shooterID}')";

            return ExecuteModifyQuery(query);
        }
        #endregion

        #region Auxiliary queries
        /// <summary>
        /// Get specified accuracy achieved by Shooter in general, stage or competition.
        /// </summary>
        /// <param name="accuracyType">Type of accuracy</param>
        /// <param name="shooterId">Id of the shooter</param>
        /// <param name="stageId">Id of the stage. If 0 it doesn't take this parameter in consideration</param>
        /// <param name="competitionId">Id of the competition. If 0 it doesn't take this parameter in consideration</param>  
        public static double GetAccuracy(AccuracyTypeEnum accuracyType, uint shooterId, uint stageId = 0, uint competitionId = 0)
        {
            string sumQuery;
            switch (accuracyType)
            {
                case AccuracyTypeEnum.General:
                    sumQuery = "SUM(alpha)+SUM(charlie)+SUM(delta)+SUM(extra)";
                    break;
                case AccuracyTypeEnum.Alpha:
                    sumQuery = "SUM(alpha)";
                    break;
                case AccuracyTypeEnum.Charlie:
                    sumQuery = "SUM(charlie)";
                    break;
                case AccuracyTypeEnum.Delta:
                    sumQuery = "SUM(delta)";
                    break;
                default:
                    return 0.0;
            }

            string additionalwhereStatement = "";
            if (stageId != 0)
                additionalwhereStatement = $"AND trasa.id = {stageId}";
            else if (competitionId != 0)
                additionalwhereStatement = $"AND zawody.id = {competitionId}";

            var query = $@"SELECT ({sumQuery}) / (SUM(alpha)+SUM(charlie)+SUM(delta)+SUM(miss)+SUM('n-s')+SUM(extra)) AS accuracy
                            FROM tarcza
                            INNER JOIN strzelec ON strzelec.id = tarcza.strzelec_id
                            INNER JOIN trasa ON trasa.id = tarcza.trasa_id
                            INNER JOIN zawody ON zawody.id = trasa.id_zawody
                            WHERE strzelec.id = {shooterId} {additionalwhereStatement};";



            return ExecuteSelectQuery<double>(query).FirstOrDefault();
        }

        public static IEnumerable<ShooterCompetition> GetShooterAccomplishedCompetitionsFromDB(uint id)
        {
            var query = $@"WITH punktacja AS (
                            SELECT  punkty.zawody_id, punkty.suma/przebieg.czas AS pkt , 
                                    punkty.strzelec_id, punkty.trasa_id, punkty.zawody_miejsce, 
                                    punkty.zawody_rozpoczecie 
                            FROM (SELECT strzelec.id AS strzelec_id, trasa.id AS trasa_id, zawody.id AS zawody_id, 
                                            zawody.miejsce AS zawody_miejsce, zawody.rozpoczecie AS zawody_rozpoczecie, 
                                            ((SUM(alpha) * 5 + SUM(charlie) * 3 + SUM(delta)) - 10 * (SUM(miss) + SUM(`n-s`) + SUM(proc) + SUM(extra))) AS suma
                            FROM strzelec
                            INNER JOIN tarcza ON strzelec.id = tarcza.strzelec_id
                            INNER JOIN trasa ON tarcza.trasa_id = trasa.id
                            INNER JOIN zawody ON zawody.id = trasa.id_zawody
                            GROUP BY strzelec.id, zawody.id,trasa.id) AS punkty
                        INNER JOIN przebieg ON przebieg.id_strzelec = punkty.strzelec_id AND przebieg.id_trasa = punkty.trasa_id
                        INNER JOIN strzelec ON strzelec.id = punkty.strzelec_id)
                        SELECT zawody_id AS competitionId, location, startdate, position, points 
                        FROM (SELECT zawody_id, strzelec_id AS shooterId, zawody_miejsce AS location, 
                                        zawody_rozpoczecie AS startDate, 
                                        RANK() OVER(PARTITION BY punktacja.zawody_id ORDER BY SUM(punktacja.pkt) DESC) AS position, SUM(punktacja.pkt) AS points 
                        FROM punktacja
                        GROUP BY punktacja.strzelec_id, zawody_id) AS subQuery
                        WHERE shooterId = {id};";

            return ExecuteSelectQuery<ShooterCompetition>(query);
        }

        public static double GetShooterGeneralAveragePositionFromDB(uint id)
        {
            var query = $@"WITH ranking AS (
                            SELECT RANK() OVER(PARTITION BY punkty.zawody_id ORDER BY sum(punkty.suma/przebieg.czas) DESC) AS positions, 
                                    sum(punkty.suma/przebieg.czas) AS pkt , punkty.strzelec_id, punkty.zawody_id
                            FROM (
                                SELECT strzelec.id AS strzelec_id, trasa.id AS trasa_id, 
                                        ((SUM(alpha) * 5 + SUM(charlie) * 3 + SUM(delta)) - 10 * (SUM(miss) + SUM(`n-s`) + SUM(proc) + SUM(extra))) AS suma, zawody.id AS zawody_id
                                FROM strzelec
                                INNER JOIN tarcza ON strzelec.id = tarcza.strzelec_id 
                                INNER JOIN trasa ON tarcza.trasa_id = trasa.id
                                INNER JOIN zawody ON zawody.id = trasa.id_zawody
                                INNER JOIN przebieg ON przebieg.id_strzelec = strzelec.id AND przebieg.id_trasa = trasa.id
                                GROUP BY strzelec.id, zawody.id, trasa.id) AS punkty
                            INNER JOIN przebieg ON przebieg.id_strzelec = punkty.strzelec_id AND przebieg.id_trasa = punkty.trasa_id
                            INNER JOIN strzelec ON strzelec.id = punkty.strzelec_id
                            GROUP BY zawody_id,strzelec_id)
                            SELECT avg(ranking.positions) AS averagePosition FROM ranking
                            WHERE strzelec_id = {id};";

            return ExecuteSelectQuery<double>(query).FirstOrDefault();
        }

        public static uint GetShooterOnStagePosition(uint shooterId, uint stageId)
        {
            var query = $@"WITH ranking AS (
                                SELECT strzelec.id AS strzelec_id, 
                                        RANK() OVER(ORDER BY (SUM(alpha)*5+SUM(charlie)*3+SUM(delta)-10*(SUM(miss)+SUM(`n-s`)+SUM(proc)+SUM(extra)))
                                                    /(SELECT czas FROM przebieg WHERE id_strzelec=strzelec_id AND id_trasa=trasa_id) DESC) AS pozycja
                            FROM strzelec
                            INNER JOIN tarcza ON strzelec.id = tarcza.strzelec_id
                            INNER JOIN trasa ON tarcza.trasa_id = trasa.id
                            WHERE trasa.id = {stageId}
                            GROUP BY strzelec.id)
                            SELECT pozycja FROM ranking WHERE strzelec_id = {shooterId}";

            return ExecuteSelectQuery<uint>(query).FirstOrDefault();
        }

        public static double GetShooterGeneralSumOfPointsFromDB(uint id)
        {
            var query = $@"SELECT SUM(subQuery.points/przebieg.czas) AS sumOfPoints
                            FROM (
                                SELECT trasa.id AS trasa_id, strzelec.id AS strzelec_id, 
                                        ((SUM(alpha)*5 + SUM(charlie)*3 + SUM(delta))-10*(SUM(miss)+SUM(tarcza.`n-s`)+SUM(proc)+SUM(extra))) AS points
                                FROM tarcza INNER JOIN strzelec ON strzelec.id = tarcza.strzelec_id
                                INNER JOIN trasa ON trasa.id = tarcza.trasa_id
                                INNER JOIN zawody ON zawody.id = trasa.id_zawody
                                WHERE strzelec.id = {id}
                                GROUP BY trasa.id) AS subQuery
                            INNER JOIN przebieg ON przebieg.id_trasa=subQuery.trasa_id and przebieg.id_strzelec=subQuery.strzelec_id;";

            return ExecuteSelectQuery<double>(query).FirstOrDefault();
        }

        public static double GetShooterOnStageSumOfPointsFromDB(uint ShooterId, uint StageId)
        {
            var query = $@"SELECT (SUM(alpha)*5+SUM(charlie)*3+SUM(delta)-10*(SUM(miss)+SUM(`n-s`)+SUM(proc)+SUM(extra)))
                                     /(SELECT czas FROM przebieg WHERE id_strzelec = 1 and id_trasa = 1) AS points
                            FROM tarcza
                            WHERE tarcza.strzelec_id = {ShooterId} and tarcza.trasa_id = {StageId}
                            GROUP BY tarcza.strzelec_id, tarcza.trasa_id;";

            return ExecuteSelectQuery<double>(query).FirstOrDefault();
        }

        public static double GetShooterGeneralSumOfTimesFromDB(uint id)
        {
            var query = $@"SELECT SUM(przebieg.czas) AS sumOfTimes
                            FROM tarcza
                            INNER JOIN strzelec ON strzelec.id = tarcza.strzelec_id
                            INNER JOIN trasa ON trasa.id = tarcza.trasa_id
                            INNER JOIN zawody ON zawody.id = trasa.id_zawody
                            INNER JOIN przebieg ON trasa.id = przebieg.id_trasa 
                                AND strzelec.id = przebieg.id_strzelec
                            WHERE strzelec.id = {id};";

            return ExecuteSelectQuery<double>(query).FirstOrDefault();
        }

        public static double GetShooterOnStageTime(uint ShooterId, uint StageId)
        {
            var query = $@"SELECT czas FROM przebieg 
                             WHERE id_strzelec = {ShooterId} and id_trasa = {StageId};";

            return ExecuteSelectQuery<TimeSpan>(query).FirstOrDefault().TotalSeconds;
        }

        public static ShooterWithPoints GetShooterWithPointsByStageIdFromDB(uint id)
        {
            var query = $@"WITH ranking AS (
                            SELECT summing.strzelec_id AS strzelec_id, summing.strzelec_imie AS imie, 
                                    summing.strzelec_nazwisko AS nazwisko, summing.suma/przebieg.czas AS sumaPunktow, 
                                    summing.trasa_id AS trasaId, 
                                    RANK() OVER ( PARTITION BY trasa.id ORDER BY summing.suma/przebieg.czas DESC) rankingGraczy 
                            FROM (
                                SELECT strzelec.imie AS strzelec_imie, strzelec.nazwisko AS strzelec_nazwisko, 
                                        strzelec.id AS strzelec_id, trasa.id AS trasa_id, 
                                        (((SUM(alpha)*5 + SUM(charlie)*3 + SUM(delta))-10*(SUM(miss)+SUM(tarcza.`n-s`)+SUM(proc)+SUM(extra)))) AS suma 
                                FROM strzelec
                                INNER JOIN tarcza ON strzelec.id=tarcza.strzelec_id 
                                INNER JOIN trasa ON tarcza.trasa_id=trasa.id        
                                GROUP BY strzelec.id, trasa.id) AS summing
                            INNER JOIN przebieg ON przebieg.id_strzelec = summing.strzelec_id and przebieg.id_trasa = summing.trasa_id
                            INNER JOIN trasa ON trasa.id=summing.trasa_id)
                        SELECT strzelec_id AS Id, imie, nazwisko, sumaPunktow FROM ranking
                        WHERE trasaId = {id}
                        LIMIT 1;";
            return ExecuteSelectQuery<ShooterWithPoints>(query).FirstOrDefault();
        }

        public static string GetShooterOnStageCompetition(uint shooterId, uint stageId)
        {
            var query = $@"SELECT CONCAT(miejsce, ' ', DATE(rozpoczecie)) AS zawody
                            FROM trasa 
                            INNER JOIN zawody ON trasa.id_zawody=zawody.id
                            INNER JOIN przebieg ON trasa.id=przebieg.id_trasa
                            WHERE trasa.id = {stageId} AND przebieg.id_strzelec = {shooterId}";

            return ExecuteSelectQuery<object>(query).FirstOrDefault()?.ToString();
        }

        public static IEnumerable<ShooterWithStageAndCompetitionPoints>
            GetShootersWithStagePointsAndCompetitionPointsByIdFromDB(uint id)
        {
            var query = $@"WITH punktacja AS (
                            SELECT punkty.suma/przebieg.czas AS pkt ,punkty.strzelec_imie, punkty.strzelec_nazwisko, 
                                    punkty.strzelec_id, punkty.trasa_id, punkty.zawody_miejsce, punkty.zawody_rozpoczecie, punkty.zawody_id
                            FROM (
                                SELECT strzelec.imie AS strzelec_imie, strzelec.nazwisko AS strzelec_nazwisko, strzelec.id AS strzelec_id, 
                                        trasa.id AS trasa_id, zawody.id AS zawody_id, zawody.miejsce AS zawody_miejsce, zawody.rozpoczecie AS zawody_rozpoczecie, 
                                        ((SUM(alpha) * 5 + SUM(charlie) * 3 + SUM(delta)) - 10 * (SUM(miss) + SUM(`n-s`) + SUM(proc) + SUM(extra))) AS suma
                                FROM strzelec
                                INNER JOIN tarcza ON strzelec.id = tarcza.strzelec_id
                                INNER JOIN trasa ON tarcza.trasa_id = trasa.id
                                INNER JOIN zawody ON zawody.id = trasa.id_zawody
                                WHERE trasa.id = trasa_id
                                GROUP BY strzelec.id, zawody.id,trasa.id) AS punkty
                            INNER JOIN przebieg ON przebieg.id_strzelec = punkty.strzelec_id AND przebieg.id_trasa = punkty.trasa_id
                            INNER JOIN strzelec ON strzelec.id = punkty.strzelec_id)
                        SELECT subQuery.strzelec_id, subQuery.zawody_id, subQuery.trasa_id, subQuery.zawody_miejsce AS location, 
                                subQuery.strzelec_imie AS name, subQuery.strzelec_nazwisko AS surname, subQuery.position, 
                                subQuery.stagePoints, SUM(compQuery.compPoints) AS competitionPoints  
                        FROM (
                            SELECT punktacja.strzelec_imie, punktacja.strzelec_nazwisko, punktacja.strzelec_id, punktacja.zawody_miejsce, 
                            punktacja.zawody_rozpoczecie AS startDate, SUM(pkt) AS compPoints, punktacja.trasa_id, punktacja.zawody_id
                            FROM punktacja
                            GROUP BY strzelec_id, zawody_miejsce, zawody_rozpoczecie, trasa_id, zawody_id) AS compQuery,
                            (SELECT punktacja.strzelec_imie, punktacja.strzelec_nazwisko, punktacja.strzelec_id, punktacja.zawody_miejsce, 
                                    punktacja.zawody_rozpoczecie AS startDate, RANK() OVER(ORDER BY SUM(punktacja.pkt) DESC) AS position, 
                                    SUM(pkt) AS stagePoints, punktacja.trasa_id, punktacja.zawody_id
                            FROM punktacja
                            WHERE trasa_id = {id}
                            GROUP BY strzelec_id, zawody_miejsce, zawody_rozpoczecie, trasa_id, zawody_id) AS subQuery
                        WHERE compQuery.zawody_id = subQuery.zawody_id AND compQuery.strzelec_id = subQuery.strzelec_id
                        GROUP BY subQuery.zawody_id, subQuery.trasa_id, location, strzelec_id, subQuery.position, subQuery.stagePoints;";

            return ExecuteSelectQuery<ShooterWithStageAndCompetitionPoints>(query);
        }

        public static Shooter GetShooterFromDB(uint id)
        {
            var query = $"SELECT * FROM strzelec WHERE strzelec.id={id}";

            return ExecuteSelectQuery<Shooter>(query).FirstOrDefault();
        }

        public static double GetShooterSumOfPointsAtCompetitionFromDB(uint shooterId, uint competitionId)
        {
            var query = $@"SELECT SUM(subQuery.points/przebieg.czas) AS sumOfPoints
                            FROM (
                                SELECT trasa.id AS trasa_id, strzelec.id AS strzelec_id, 
                                        ((SUM(alpha) * 5 + SUM(charlie) * 3 + SUM(delta)) - 10 * (SUM(miss) + SUM(`n-s`) + SUM(proc) + SUM(extra))) AS points
                                FROM tarcza INNER JOIN strzelec ON strzelec.id = tarcza.strzelec_id
                                INNER JOIN trasa ON trasa.id = tarcza.trasa_id
                                INNER JOIN zawody ON zawody.id = trasa.id_zawody
                                WHERE strzelec.id = {shooterId} and zawody.id = {competitionId}
                                GROUP BY trasa.id) AS subQuery
                            INNER JOIN przebieg ON przebieg.id_trasa = subQuery.trasa_id and przebieg.id_strzelec = subQuery.strzelec_id; ";

            return ExecuteSelectQuery<double>(query).FirstOrDefault();
        }

        public static double GetShooterSumOfTimesAtCompetitionFromDB(uint shooterId, uint competitionId)
        {
            var query = $@"SELECT SUM(przebieg.czas) AS sumOfTimes
                            FROM strzelec 
                            INNER JOIN przebieg ON strzelec.id=przebieg.id_strzelec
                            INNER JOIN trasa ON przebieg.id_trasa=trasa.id
                            INNER JOIN zawody ON trasa.id_zawody=zawody.id
                            WHERE strzelec.id={shooterId} AND zawody.id={competitionId};";

            return ExecuteSelectQuery<double>(query).FirstOrDefault();
        }

        public static uint GetShooterPositionAtCompetitionFromDB(uint shooterId, uint competitionId)
        {
            var query = $@"WITH ranking AS (
                            SELECT strzelec.id AS strzelec_id, 
                                    SUM(sumowanieTarcz.suma/przebieg.czas) AS sumaPunktow, 
                                    RANK() OVER (ORDER BY SUM(sumowanieTarcz.suma/przebieg.czas) desc) AS pozycja
                            FROM (
                                SELECT strzelec.id AS strzelec_id, trasa.id AS trasa_id, 
                                        (((SUM(alpha)*5 + SUM(charlie)*3 + SUM(delta))-10*(SUM(miss)+SUM(tarcza.`n-s`)+SUM(proc)+SUM(extra)))) AS suma
                                FROM strzelec INNER JOIN tarcza ON strzelec.id=tarcza.strzelec_id
                                INNER JOIN trasa ON tarcza.trasa_id=trasa.id
                                WHERE trasa.id_zawody={competitionId}
                                GROUP BY strzelec.id, trasa.id) AS sumowanieTarcz
                            INNER JOIN przebieg ON przebieg.id_strzelec = sumowanieTarcz.strzelec_id and przebieg.id_trasa = sumowanieTarcz.trasa_id 
                            INNER JOIN strzelec ON strzelec.id = sumowanieTarcz.strzelec_id 
                            GROUP BY sumowanieTarcz.strzelec_id 
                            ORDER BY sumaPunktow desc)
                        SELECT ranking.pozycja FROM ranking WHERE strzelec_id={shooterId}";

            return ExecuteSelectQuery<uint>(query).FirstOrDefault();
        }

        public static IEnumerable<ShooterStatsOnStage> GetShooterStatsOnStages(uint shooterId, uint competitionId)
        {
            var query = $@"SELECT trasa.id AS trasaId, trasa.nazwa AS nazwaTrasy, subQuery.points AS punkty, 
                                 przebieg.czas, subQuery.points/przebieg.czas AS punktyNaTrasie
                            FROM (
                                SELECT trasa.id AS trasa_id, strzelec.id AS strzelec_id, 
                                    ((SUM(alpha) * 5 + SUM(charlie) * 3 + SUM(delta)) - 10 * (SUM(miss) + SUM(tarcza.`n-s`) + SUM(proc) + SUM(extra))) AS points 
                                FROM tarcza INNER JOIN strzelec ON strzelec.id = tarcza.strzelec_id 
                                INNER JOIN trasa ON trasa.id = tarcza.trasa_id 
                                INNER JOIN zawody ON zawody.id = trasa.id_zawody 
                                WHERE strzelec.id = {shooterId} and zawody.id={competitionId}
                                GROUP BY trasa.id) AS subQuery 
                            INNER JOIN przebieg ON przebieg.id_trasa = subQuery.trasa_id and przebieg.id_strzelec = subQuery.strzelec_id
                            INNER JOIN trasa ON trasa.id=subQuery.trasa_id;";

            return ExecuteSelectQuery<ShooterStatsOnStage>(query);
        }
        #endregion
    }
}