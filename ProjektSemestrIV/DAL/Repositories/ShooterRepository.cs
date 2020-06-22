using MySql.Data.MySqlClient;
using ProjektSemestrIV.DAL.Entities;
using ProjektSemestrIV.DAL.Entities.AuxiliaryEntities;
using ProjektSemestrIV.Models.ShowModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektSemestrIV.DAL.Repositories
{
    class ShooterRepository
    {
        #region CRUD
        public static bool AddShooterToDB(Shooter shooter)
        {
            bool executed = false;

            string query = @"INSERT INTO strzelec (`imie`, `nazwisko`)
                             VALUES (@imie, @nazwisko)";

            using (MySqlConnection connection = DatabaseConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(query, connection);

                foreach (var parameter in shooter.GetParameters())
                {
                    command.Parameters.Add(parameter);
                }

                connection.Open();
                if (command.ExecuteNonQuery() == 1) executed = true;
                connection.Close();
            }
            return executed;
        }

        public static bool EditShooterInDB(Shooter shooter, uint id)
        {
            bool executed = false;
            var query = $@"UPDATE strzelec 
                            SET `imie` = @imie, `nazwisko` = @nazwisko 
                            WHERE (`id` = '{id}')";

            using (MySqlConnection connection = DatabaseConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(query, connection);

                foreach (var parameter in shooter.GetParameters())
                {
                    command.Parameters.Add(parameter);
                }

                connection.Open();
                if (command.ExecuteNonQuery() == 1) executed = true;
                connection.Close();
            }
            return executed;
        }

        public static List<Shooter> GetAllShootersFromDB()
        {
            var query = "SELECT * FROM strzelec";

            List<Shooter> shooters = new List<Shooter>();
            using (MySqlConnection connection = DatabaseConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    shooters.Add(new Shooter(reader));
                }
                connection.Close();
            }
            return shooters;
        }

        public static Shooter GetShooterByIdFromDB(uint id)
        {
            string query = $"SELECT * FROM strzelec WHERE strzelec.id = {id}";
            Shooter shooter = null;
            using (MySqlConnection connection = DatabaseConnection.Instance.Connection)
            {

                MySqlCommand command = new MySqlCommand(query, connection);
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                    shooter = new Shooter(reader);
                connection.Close();
            }
            return shooter;
        }

        public static bool DeleteShooterFromDB(uint shooterID)
        {
            bool executed = false;
            string query = $"DELETE FROM strzelec WHERE (`id` = '{shooterID}')";

            using (MySqlConnection connection = DatabaseConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                connection.Open();
                if (command.ExecuteNonQuery() == 1) executed = true;
                connection.Close();
            }
            return executed;
        }
        #endregion

        #region Auxiliary queries
        public static double GetShooterCompetitionGeneralAccuracyFromDB(uint id)
        {
            var query = $@"SELECT CAST((SUM(alpha)+SUM(charlie)+SUM(delta)+SUM(extra))
                                        /(SUM(alpha)+SUM(charlie)+SUM(delta)+SUM(miss)+SUM('n-s')+SUM(extra)) AS DECIMAL(10,9)) AS accuracy
                            FROM tarcza
                            INNER JOIN strzelec ON strzelec.id = tarcza.strzelec_id
                            INNER JOIN trasa ON trasa.id = tarcza.trasa_id
                            INNER JOIN zawody ON zawody.id = trasa.id_zawody
                            WHERE strzelec.id = {id};";

            double accuracy = 0;

            using (MySqlConnection connection = DatabaseConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    var readValue = reader["accuracy"];
                    accuracy = readValue is DBNull ? 0.0 : decimal.ToDouble((decimal)readValue);
                }
                connection.Close();
            }
            return accuracy;
        }

        public static double GetShooterOnStageGeneralAccuracyFromDB(uint ShooterId, uint StageId)
        {
            var query = $@"SELECT CAST((SUM(alpha)+SUM(charlie)+SUM(delta)+SUM(extra))
                                        /(SUM(alpha)+SUM(charlie)+SUM(delta)+SUM(miss)+SUM('n-s')+SUM(extra)) AS DECIMAL(10,9)) AS accuracy
                            FROM tarcza
                            WHERE strzelec_id = {ShooterId} and trasa_id = {StageId};";

            double accuracy = 0;

            using (MySqlConnection connection = DatabaseConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    var readValue = reader["accuracy"];
                    accuracy = readValue is DBNull ? 0.0 : decimal.ToDouble((decimal)readValue);
                }
                connection.Close();
            }
            return accuracy;
        }

        public static double GetShooterCompetitionAlphaAccuracyFromDB(uint id)
        {
            var query = $@"SELECT cast(SUM(alpha)/(SUM(alpha)+SUM(charlie)+SUM(delta)+SUM(extra)) AS DECIMAL(10,9)) AS accuracy
                            FROM tarcza
                            INNER JOIN strzelec ON strzelec.id = tarcza.strzelec_id
                            INNER JOIN trasa ON trasa.id = tarcza.trasa_id
                            INNER JOIN zawody ON zawody.id = trasa.id_zawody
                            WHERE strzelec.id = {id};";

            double accuracy = 0;

            using (MySqlConnection connection = DatabaseConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    var readValue = reader["accuracy"];
                    accuracy = readValue != DBNull.Value ? decimal.ToDouble((decimal)readValue) : 0.0;
                }
                connection.Close();
            }
            return accuracy;
        }

        public static double GetShooterOnStageAlphaAccuracyFromDB(uint ShooterId, uint StageId)
        {
            var query = $@"SELECT cast(SUM(alpha)/(SUM(alpha)+SUM(charlie)+SUM(delta)+SUM(extra)) AS DECIMAL(10,9)) AS accuracy
                            FROM tarcza
                            WHERE strzelec_id = {ShooterId} and trasa_id = {StageId};";

            double accuracy = 0;

            using (MySqlConnection connection = DatabaseConnection.Instance.Connection)
            {

                MySqlCommand command = new MySqlCommand(query, connection);
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    var readValue = reader["accuracy"];
                    accuracy = readValue != DBNull.Value ? decimal.ToDouble((decimal)readValue) : 0.0;
                }
                connection.Close();
            }
            return accuracy;
        }

        public static double GetShooterCompetitionCharlieAccuracyFromDB(uint id)
        {
            var query = $@"SELECT cast(SUM(charlie)/(SUM(alpha)+SUM(charlie)+SUM(delta)+SUM(extra)) AS DECIMAL(10,9)) AS accuracy
                            FROM tarcza
                            INNER JOIN strzelec ON strzelec.id = tarcza.strzelec_id
                            INNER JOIN trasa ON trasa.id = tarcza.trasa_id
                            INNER JOIN zawody ON zawody.id = trasa.id_zawody
                            WHERE strzelec.id = {id};";

            double accuracy = 0;

            using (MySqlConnection connection = DatabaseConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    var readValue = reader["accuracy"];
                    accuracy = readValue != DBNull.Value ? decimal.ToDouble((decimal)readValue) : 0.0;
                }
                connection.Close();
            }
            return accuracy;
        }

        public static double GetShooterOnStageCharlieAccuracyFromDB(uint ShooterId, uint StageId)
        {
            var query = $@"SELECT cast(SUM(charlie)/(SUM(alpha)+SUM(charlie)+SUM(delta)+SUM(extra)) AS DECIMAL(10,9)) AS accuracy
                            FROM tarcza
                            WHERE strzelec_id = {ShooterId} and trasa_id = {StageId};";

            double accuracy = 0;

            using (MySqlConnection connection = DatabaseConnection.Instance.Connection)
            {

                MySqlCommand command = new MySqlCommand(query, connection);
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    var readValue = reader["accuracy"];
                    accuracy = readValue != DBNull.Value ? decimal.ToDouble((decimal)readValue) : 0.0;
                }
                connection.Close();
            }
            return accuracy;
        }

        public static double GetShooterCompetitionDeltaAccuracyFromDB(uint id)
        {
            var query = $@"SELECT cast(SUM(delta)/(SUM(alpha)+SUM(charlie)+SUM(delta)+SUM(extra)) AS DECIMAL(10,9)) AS accuracy
                            FROM tarcza
                            INNER JOIN strzelec ON strzelec.id = tarcza.strzelec_id
                            INNER JOIN trasa ON trasa.id = tarcza.trasa_id
                            INNER JOIN zawody ON zawody.id = trasa.id_zawody
                            WHERE strzelec.id = {id};";

            double accuracy = 0;

            using (MySqlConnection connection = DatabaseConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    var readValue = reader["accuracy"];
                    accuracy = readValue != DBNull.Value ? decimal.ToDouble((decimal)readValue) : 0.0;
                }
                connection.Close();
            }
            return accuracy;
        }

        public static double GetShooterOnStageDeltaAccuracyFromDB(uint ShooterId, uint StageId)
        {
            var query = $@"SELECT cast(SUM(delta)/(SUM(alpha)+SUM(charlie)+SUM(delta)+SUM(extra)) AS DECIMAL(10,9)) AS accuracy
                            FROM tarcza
                            WHERE strzelec_id = {ShooterId} and trasa_id = {StageId};";

            double accuracy = 0;

            using (MySqlConnection connection = DatabaseConnection.Instance.Connection)
            {

                MySqlCommand command = new MySqlCommand(query, connection);
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    var readValue = reader["accuracy"];
                    accuracy = readValue != DBNull.Value ? decimal.ToDouble((decimal)readValue) : 0.0;
                }
                connection.Close();
            }
            return accuracy;
        }

        public static IEnumerable<ShooterCompetition> GetShooterAccomplishedCompetitionsFromDB(uint id)
        {
            var results = new List<ShooterCompetition>();
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
                        SELECT  zawody_id AS competitionId, location, startdate, position, points 
                        FROM (SELECT zawody_id, strzelec_id AS shooterId, zawody_miejsce AS location, 
                                        zawody_rozpoczecie AS startDate, 
                                        RANK() OVER(ORDER BY SUM(punktacja.pkt) DESC) AS position, SUM(punktacja.pkt) AS points 
                        FROM punktacja
                        GROUP BY punktacja.strzelec_id, zawody_id) AS subQuery
                        WHERE shooterId = {id};";

            using (MySqlConnection connection = DatabaseConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    results.Add(new ShooterCompetition(reader));
                }
                connection.Close();
            }
            return results;
        }

        public static double GetShooterGeneralAveragePositionFromDB(uint id)
        {
            var query = $@"WITH ranking AS (
                            SELECT RANK() OVER(ORDER BY punkty.suma/przebieg.czas DESC) AS positions, 
                                    punkty.suma/przebieg.czas AS pkt , punkty.strzelec_id
                            FROM (
                                SELECT strzelec.id AS strzelec_id, trasa.id AS trasa_id, 
                                        ((SUM(alpha) * 5 + SUM(charlie) * 3 + SUM(delta)) - 10 * (SUM(miss) + SUM(`n-s`) + SUM(proc) + SUM(extra))) AS suma
                                FROM strzelec
                                INNER JOIN tarcza ON strzelec.id = tarcza.strzelec_id
                                INNER JOIN trasa ON tarcza.trasa_id = trasa.id
                                GROUP BY strzelec.id, trasa.id) AS punkty
                            INNER JOIN przebieg ON przebieg.id_strzelec = punkty.strzelec_id AND przebieg.id_trasa = punkty.trasa_id
                            INNER JOIN strzelec ON strzelec.id = punkty.strzelec_id)
                        SELECT avg(ranking.positions) AS averagePosition FROM ranking
                        WHERE strzelec_id = {id};";

            double position = 0;

            using (MySqlConnection connection = DatabaseConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    var readValue = reader["averagePosition"];
                    position = readValue != DBNull.Value ? decimal.ToDouble((decimal)readValue) : 0.0;
                }
                connection.Close();
            }
            return position;
        }

        public static uint GetShooterOnStagePosition(uint ShooterId, uint StageId)
        {
            var query = $@"SELECT strzelec_id, trasa_id, 
                                (SUM(alpha)*5+SUM(charlie)*3+SUM(delta)-10*(SUM(miss)+SUM(`n-s`)+SUM(proc)+SUM(extra)))
                                /(SELECT czas FROM przebieg WHERE id_strzelec=strzelec_id and id_trasa=trasa_id) AS points
                            FROM tarcza
                            WHERE trasa_id = {StageId}
                            GROUP BY strzelec_id, trasa_id
                            ORDER BY points DESC;";

            uint position = 0;

            using (MySqlConnection connection = DatabaseConnection.Instance.Connection)
            {

                MySqlCommand command = new MySqlCommand(query, connection);
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    position++;
                    var shooter_id = UInt32.Parse(reader["strzelec_id"].ToString());
                    var stage_id = UInt32.Parse(reader["trasa_id"].ToString());
                    if (shooter_id == ShooterId)
                    {
                        break;
                    }
                }
                connection.Close();
            }
            return position;
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
            double points = 0;

            using (MySqlConnection connection = DatabaseConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                    points = reader.GetDouble("sumOfPoints");
                connection.Close();
            }
            return points;
        }

        public static double GetShooterOnStageSumOfPointsFromDB(uint ShooterId, uint StageId)
        {
            var query = $@"SELECT (SUM(alpha)*5+SUM(charlie)*3+SUM(delta)-10*(SUM(miss)+SUM(`n-s`)+SUM(proc)+SUM(extra)))
                                     /(SELECT czas FROM przebieg WHERE id_strzelec = 1 and id_trasa = 1) AS points
                            FROM tarcza
                            WHERE tarcza.strzelec_id = {ShooterId} and tarcza.trasa_id = {StageId}
                            GROUP BY tarcza.strzelec_id, tarcza.trasa_id;";

            double points = 0;

            using (MySqlConnection connection = DatabaseConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                    points = reader.GetDouble("points");
                connection.Close();
            }
            return points;
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

            double time = 0;

            using (MySqlConnection connection = DatabaseConnection.Instance.Connection)
            {

                MySqlCommand command = new MySqlCommand(query, connection);
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                    time = reader.GetDouble("sumOfTimes");
                connection.Close();
            }
            return time;
        }

        public static double GetShooterOnStageTime(uint ShooterId, uint StageId)
        {
            var query = $@"SELECT czas FROM przebieg 
                             WHERE id_strzelec = {ShooterId} and id_trasa = {StageId};";
            double time = 0;

            using (MySqlConnection connection = DatabaseConnection.Instance.Connection)
            {

                MySqlCommand command = new MySqlCommand(query, connection);
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                    time = reader.GetTimeSpan("czas").TotalSeconds;
                connection.Close();
            }
            return time;
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

            ShooterWithPoints shooter = null;

            using (MySqlConnection connection = DatabaseConnection.Instance.Connection)
            {

                MySqlCommand command = new MySqlCommand(query, connection);
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                    shooter = new ShooterWithPoints(reader);
                connection.Close();
            }
            return shooter;
        }

        public static string getShooterOnStageCompetition(uint ShooterId, uint StageId)
        {
            var query = $@"SELECT CONCAT(miejsce, ' ', DATE(rozpoczecie)) AS zawody
                            FROM trasa INNER JOIN zawody ON trasa.id_zawody=zawody.id
                            WHERE trasa.id={StageId}";

            string competition = "";

            using (MySqlConnection connection = DatabaseConnection.Instance.Connection)
            {

                MySqlCommand command = new MySqlCommand(query, connection);
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                    competition = reader.GetString("zawody");
                connection.Close();
            }
            return competition;
        }

        public static IEnumerable<ShooterWithStagePointsAndCompetitionPoints>
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

            var shooters = new List<ShooterWithStagePointsAndCompetitionPoints>();

            using (MySqlConnection connection = DatabaseConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                    shooters.Add(new ShooterWithStagePointsAndCompetitionPoints((uint)reader.GetUInt32("strzelec_id"),
                                                                                (uint)reader.GetUInt64("position"),
                                                                                reader.GetString("name"),
                                                                                reader.GetString("surname"),
                                                                                reader.GetDouble("stagePoints"),
                                                                                reader.GetDouble("competitionPoints")));
                connection.Close();
            }
            return shooters;
        }

        public static Shooter GetShooterFromDB(uint id)
        {
            var query = $"SELECT * FROM strzelec WHERE strzelec.id={id}";
            Shooter shooter = null;

            using (MySqlConnection connection = DatabaseConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    shooter = new Shooter(reader);
                }

                connection.Close();
            }

            return shooter;
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

            double points = 0;
            using (MySqlConnection connection = DatabaseConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    points = reader.GetDouble("sumOfPoints");
                }
                connection.Close();
            }
            return points;
        }

        public static double GetShooterSumOfTimesAtCompetitionFromDB(uint shooterId, uint competitionId)
        {
            var query = $@"SELECT SUM(przebieg.czas) AS sumOfTimes
                            FROM strzelec 
                            INNER JOIN przebieg ON strzelec.id=przebieg.id_strzelec
                            INNER JOIN trasa ON przebieg.id_trasa=trasa.id
                            INNER JOIN zawody ON trasa.id_zawody=zawody.id
                            WHERE strzelec.id={shooterId} AND zawody.id={competitionId};";

            double time = 0;

            using (MySqlConnection connection = DatabaseConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    time = reader.GetDouble("sumOfTimes");
                }

                connection.Close();
            }

            return time;
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

            uint position = 0;

            using (MySqlConnection connection = DatabaseConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    position = reader.GetUInt32("pozycja");
                }

                connection.Close();
            }

            return position;
        }

        public static double GetShooterGeneralAccuracyAtCompetitionFromDB(uint shooterId, uint competitionId)
        {
            var query = $@"SELECT (SUM(alpha)+SUM(charlie)+SUM(delta)+SUM(extra))/(SUM(alpha)+SUM(charlie)+SUM(delta)+SUM(miss)+SUM('n-s')+SUM(extra)) AS accuracy
                            FROM tarcza
                            INNER JOIN strzelec ON strzelec.id = tarcza.strzelec_id
                            INNER JOIN trasa ON trasa.id = tarcza.trasa_id
                            INNER JOIN zawody ON zawody.id = trasa.id_zawody
                            WHERE strzelec.id = {shooterId} and zawody.id={competitionId};";

            double accuracy = 0;

            using (MySqlConnection connection = DatabaseConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    var readValue = reader["accuracy"];
                    accuracy = readValue is DBNull ? 0.0 : (double)readValue;
                }
                connection.Close();
            }
            return accuracy;
        }

        public static double GetShooterAlphaAccuracyAtCompetitionFromDB(uint shooterId, uint competitionId)
        {
            var query = $@"SELECT SUM(alpha)/(SUM(alpha)+SUM(charlie)+SUM(delta)+SUM(extra)) AS accuracy
                            FROM tarcza
                            INNER JOIN strzelec ON strzelec.id = tarcza.strzelec_id
                            INNER JOIN trasa ON trasa.id = tarcza.trasa_id
                            INNER JOIN zawody ON zawody.id = trasa.id_zawody
                            WHERE strzelec.id = {shooterId} and zawody.id={competitionId};";

            double accuracy = 0;

            using (MySqlConnection connection = DatabaseConnection.Instance.Connection)
            {

                MySqlCommand command = new MySqlCommand(query, connection);
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    var readValue = reader["accuracy"];
                    accuracy = readValue != DBNull.Value ? decimal.ToDouble((decimal)readValue) : 0.0;
                }
                connection.Close();
            }
            return accuracy;
        }

        public static double GetShooterCharlieAccuracyAtCompetitionFromDB(uint shooterId, uint competitionId)
        {
            var query = $@"SELECT SUM(charlie)/(SUM(alpha)+SUM(charlie)+SUM(delta)+SUM(extra)) AS accuracy
                            FROM tarcza
                            INNER JOIN strzelec ON strzelec.id = tarcza.strzelec_id
                            INNER JOIN trasa ON trasa.id = tarcza.trasa_id
                            INNER JOIN zawody ON zawody.id = trasa.id_zawody
                            WHERE strzelec.id = {shooterId} and zawody.id={competitionId};";

            double accuracy = 0;

            using (MySqlConnection connection = DatabaseConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    var readValue = reader["accuracy"];
                    accuracy = readValue != DBNull.Value ? decimal.ToDouble((decimal)readValue) : 0.0;
                }
                connection.Close();
            }
            return accuracy;
        }

        public static double GetShooterDeltaAccuracyAtCompetitionFromDB(uint shooterId, uint competitionId)
        {
            var query = $@"SELECT SUM(delta)/(SUM(alpha)+SUM(charlie)+SUM(delta)+SUM(extra)) AS accuracy
                            FROM tarcza
                            INNER JOIN strzelec ON strzelec.id = tarcza.strzelec_id
                            INNER JOIN trasa ON trasa.id = tarcza.trasa_id
                            INNER JOIN zawody ON zawody.id = trasa.id_zawody
                            WHERE strzelec.id = {shooterId} and zawody.id={competitionId};";

            double accuracy = 0;

            using (MySqlConnection connection = DatabaseConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    var readValue = reader["accuracy"];
                    accuracy = readValue != DBNull.Value ? decimal.ToDouble((decimal)readValue) : 0.0;
                }
                connection.Close();
            }
            return accuracy;
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

            var shooters = new List<ShooterStatsOnStage>();

            using (MySqlConnection connection = DatabaseConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    shooters.Add(new ShooterStatsOnStage(reader));
                }

                connection.Close();
            }

            return shooters;
        }
        #endregion
    }
}
