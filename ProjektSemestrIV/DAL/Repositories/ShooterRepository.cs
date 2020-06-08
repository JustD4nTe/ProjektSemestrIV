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
        #region Basic CRUD
        public static Boolean AddShooterToDB(Shooter shooter)
        {
            Boolean executed = false;
            using (MySqlConnection connection = DatabaseConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand($"INSERT INTO strzelec (`imie`, `nazwisko`) VALUES {shooter.ToInsert()}", connection);
                connection.Open();
                if (command.ExecuteNonQuery() == 1) executed = true;
                connection.Close();
            }
            return executed;
        }

        public static bool EditShooterInDB(Shooter shooter, uint id)
        {
            Boolean executed = false;
            using (MySqlConnection connection = DatabaseConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand($"UPDATE strzelec SET `imie` = '{shooter.Name}', `nazwisko` = '{shooter.Surname}' WHERE (`id` = '{id}')", connection);
                connection.Open();
                if (command.ExecuteNonQuery() == 1) executed = true;
                connection.Close();
            }
            return executed;
        }

        public static List<Shooter> GetAllShootersFromDB()
        {
            List<Shooter> shooters = new List<Shooter>();
            using (MySqlConnection connection = DatabaseConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand("SELECT * FROM strzelec", connection);
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

        public static Boolean DeleteShooterFromDB(UInt32 shooterID)
        {
            Boolean executed = false;
            using (MySqlConnection connection = DatabaseConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand($"DELETE FROM strzelec WHERE (`id` = '{shooterID}')", connection);
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
            string query = $@"SELECT (sum(alpha)+sum(charlie)+sum(delta)+sum(extra))/(sum(alpha)+sum(charlie)+sum(delta)+sum(miss)+sum('n-s')+sum(extra)) AS accuracy
                             FROM tarcza
                             INNER JOIN strzelec
                             ON strzelec.id = tarcza.strzelec_id
                             INNER JOIN trasa
                             ON trasa.id = tarcza.trasa_id
                             INNER JOIN zawody
                             ON zawody.id = trasa.id_zawody
                             WHERE strzelec.id = {id};";
            double accuracy = 0;
            using (MySqlConnection connection = DatabaseConnection.Instance.Connection)
            {

                MySqlCommand command = new MySqlCommand(query, connection);
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                    accuracy = reader.GetDouble("accuracy");
                connection.Close();
            }
            return accuracy;
        }

        public static double GetShooterCompetitionAlphaAccuracyFromDB(uint id)
        {
            string query = $@"SELECT sum(alpha)/(sum(alpha)+sum(charlie)+sum(delta)+sum(extra)) AS accuracy
                             FROM tarcza
                             INNER JOIN strzelec
                             ON strzelec.id = tarcza.strzelec_id
                             INNER JOIN trasa
                             ON trasa.id = tarcza.trasa_id
                             INNER JOIN zawody
                             ON zawody.id = trasa.id_zawody
                             WHERE strzelec.id = {id};";
            double accuracy = 0;
            using (MySqlConnection connection = DatabaseConnection.Instance.Connection)
            {

                MySqlCommand command = new MySqlCommand(query, connection);
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                    accuracy = reader.GetDouble("accuracy");
                connection.Close();
            }
            return accuracy;
        }

        public static double GetShooterCompetitionCharlieAccuracyFromDB(uint id)
        {
            string query = $@"SELECT sum(charlie)/(sum(alpha)+sum(charlie)+sum(delta)+sum(extra)) AS accuracy
                             FROM tarcza
                             INNER JOIN strzelec
                             ON strzelec.id = tarcza.strzelec_id
                             INNER JOIN trasa
                             ON trasa.id = tarcza.trasa_id
                             INNER JOIN zawody
                             ON zawody.id = trasa.id_zawody
                             WHERE strzelec.id = {id};";
            double accuracy = 0;
            using (MySqlConnection connection = DatabaseConnection.Instance.Connection)
            {

                MySqlCommand command = new MySqlCommand(query, connection);
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                    accuracy = reader.GetDouble("accuracy");
                connection.Close();
            }
            return accuracy;
        }

        public static double GetShooterCompetitionDeltaAccuracyFromDB(uint id)
        {
            string query = $@"SELECT sum(delta)/(sum(alpha)+sum(charlie)+sum(delta)+sum(extra)) AS accuracy
                            FROM tarcza
                            INNER JOIN strzelec
                            ON strzelec.id = tarcza.strzelec_id
                            INNER JOIN trasa
                            ON trasa.id = tarcza.trasa_id
                            INNER JOIN zawody
                            ON zawody.id = trasa.id_zawody
                            WHERE strzelec.id = {id};";
            double accuracy = 0;
            using (MySqlConnection connection = DatabaseConnection.Instance.Connection)
            {

                MySqlCommand command = new MySqlCommand(query, connection);
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                    accuracy = reader.GetDouble("accuracy");
                connection.Close();
            }
            return accuracy;
        }

        public static IEnumerable<ShooterCompetition> GetShooterAccomplishedCompetitionsFromDB(uint id)
        {
            var results = new List<ShooterCompetition>();
            string query = $@"SELECT shooterId, location, startDate, position, points
                            FROM (
                            SELECT 
                            strzelec.id AS shooterId,
                            zawody.miejsce AS location,
                            zawody.rozpoczecie AS startDate,
                            RANK() OVER (ORDER BY (SELECT sum(points) AS sumOfPoints
                            FROM (SELECT ((sum(alpha)*5 + sum(charlie)*3 + sum(delta))-10*(sum(miss)+sum(tarcza.`n-s`)+sum(proc)+sum(extra)))/czas AS points
                            FROM tarcza
                            INNER JOIN strzelec
                            ON strzelec.id = tarcza.strzelec_id
                            INNER JOIN trasa
                            ON trasa.id = tarcza.trasa_id
                            INNER JOIN zawody
                            ON zawody.id = trasa.id_zawody
                            INNER JOIN przebieg
                            ON trasa.id = przebieg.id_trasa AND strzelec.id = przebieg.id_strzelec
                            WHERE strzelec.id = shooterId
                            GROUP BY trasa.id) AS pointsQuery) DESC) AS position,
                            (SELECT sum(points) AS sumOfPoints
                            FROM (SELECT ((sum(alpha)*5 + sum(charlie)*3 + sum(delta))-10*(sum(miss)+sum(tarcza.`n-s`)+sum(proc)+sum(extra)))/czas AS points
                            FROM tarcza
                            INNER JOIN strzelec
                            ON strzelec.id = tarcza.strzelec_id
                            INNER JOIN trasa
                            ON trasa.id = tarcza.trasa_id
                            INNER JOIN zawody
                            ON zawody.id = trasa.id_zawody
                            INNER JOIN przebieg
                            ON trasa.id = przebieg.id_trasa AND strzelec.id = przebieg.id_strzelec
                            WHERE strzelec.id = shooterId
                            GROUP BY trasa.id) AS pointsQuery) AS points
                            FROM tarcza
                            INNER JOIN strzelec
                            ON strzelec.id = tarcza.strzelec_id
                            INNER JOIN trasa
                            ON trasa.id = tarcza.trasa_id
                            INNER JOIN zawody
                            ON zawody.id = trasa.id_zawody
                            INNER JOIN przebieg
                            ON trasa.id = przebieg.id_trasa
                            GROUP BY strzelec.id, zawody.id) AS subQuery
                            WHERE shooterId = {id};";
            using (MySqlConnection connection = DatabaseConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    results.Add(new ShooterCompetition(
                        reader.GetString("location"), 
                        reader.GetDateTime("startDate").ToString(), 
                        reader.GetUInt32("position"), 
                        reader.GetDouble("points")));
                }
                connection.Close();
            }
            return results;
        }

        public static double GetShooterGeneralAveragePositionFromDB(uint id)
        {
            string query = $@"SELECT AVG(position) AS averagePosition
                            FROM (
                            SELECT 
                            strzelec.id AS shooterId,
                            RANK() OVER (ORDER BY (SELECT sum(points) AS sumOfPoints
                            FROM (SELECT ((sum(alpha)*5 + sum(charlie)*3 + sum(delta))-10*(sum(miss)+sum(tarcza.`n-s`)+sum(proc)+sum(extra)))/czas AS points
                            FROM tarcza
                            INNER JOIN strzelec
                            ON strzelec.id = tarcza.strzelec_id
                            INNER JOIN trasa
                            ON trasa.id = tarcza.trasa_id
                            INNER JOIN zawody
                            ON zawody.id = trasa.id_zawody
                            INNER JOIN przebieg
                            ON trasa.id = przebieg.id_trasa AND strzelec.id = przebieg.id_strzelec
                            WHERE strzelec.id = shooterId
                            GROUP BY trasa.id) AS pointsQuery) DESC) AS position,
                            (SELECT sum(points) AS sumOfPoints
                            FROM (SELECT ((sum(alpha)*5 + sum(charlie)*3 + sum(delta))-10*(sum(miss)+sum(tarcza.`n-s`)+sum(proc)+sum(extra)))/czas AS points
                            FROM tarcza
                            INNER JOIN strzelec
                            ON strzelec.id = tarcza.strzelec_id
                            INNER JOIN trasa
                            ON trasa.id = tarcza.trasa_id
                            INNER JOIN zawody
                            ON zawody.id = trasa.id_zawody
                            INNER JOIN przebieg
                            ON trasa.id = przebieg.id_trasa AND strzelec.id = przebieg.id_strzelec
                            WHERE strzelec.id = shooterId
                            GROUP BY trasa.id) AS pointsQuery) AS points
                            FROM tarcza
                            INNER JOIN strzelec
                            ON strzelec.id = tarcza.strzelec_id
                            INNER JOIN trasa
                            ON trasa.id = tarcza.trasa_id
                            INNER JOIN zawody
                            ON zawody.id = trasa.id_zawody
                            GROUP BY strzelec.id) AS subQuery
                            WHERE  shooterId = {id}";
            double position = 0;
            using (MySqlConnection connection = DatabaseConnection.Instance.Connection)
            {

                MySqlCommand command = new MySqlCommand(query, connection);
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                    position = reader.GetDouble("averagePosition");
                connection.Close();
            }
            return position;
        }

        public static double GetShooterGeneralSumOfPointsFromDB(uint id)
        {
            string query = $@"SELECT sum(points) AS sumOfPoints
                            FROM (SELECT ((sum(alpha)*5 + sum(charlie)*3 + sum(delta))-10*(sum(miss)+sum(tarcza.`n-s`)+sum(proc)+sum(extra)))/czas AS points
                            FROM tarcza
                            INNER JOIN strzelec
                            ON strzelec.id = tarcza.strzelec_id
                            INNER JOIN trasa
                            ON trasa.id = tarcza.trasa_id
                            INNER JOIN zawody
                            ON zawody.id = trasa.id_zawody
                            INNER JOIN przebieg
                            ON trasa.id = przebieg.id_trasa AND strzelec.id = przebieg.id_strzelec
                            WHERE strzelec.id = {id}
                            GROUP BY trasa.id) AS subQuery;";
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

        public static double GetShooterGeneralSumOfTimesFromDB(uint id)
        {
            string query = $@"SELECT sum(przebieg.czas) AS sumOfTimes
                            FROM tarcza
                            INNER JOIN strzelec
                            ON strzelec.id = tarcza.strzelec_id
                            INNER JOIN trasa
                            ON trasa.id = tarcza.trasa_id
                            INNER JOIN zawody
                            ON zawody.id = trasa.id_zawody
                            INNER JOIN przebieg
                            ON trasa.id = przebieg.id_trasa AND strzelec.id = przebieg.id_strzelec
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

        public static ShooterWithPoints GetShooterWithPointsByStageIdFromDB(uint id)
        {
            string query = $@"WITH ranking AS (SELECT 
	                            summing.strzelec_imie AS imie,
                                summing.strzelec_nazwisko AS nazwisko,
                                summing.suma/przebieg.czas AS sumaPunktow,
                                summing.trasa_id AS trasaId,
                                RANK() OVER ( PARTITION BY trasa.id Order by summing.suma/przebieg.czas ) rankingGraczy 
                                from (select strzelec.imie AS strzelec_imie, strzelec.nazwisko AS strzelec_nazwisko, strzelec.id as strzelec_id, trasa.id as trasa_id, (((sum(alpha)*5 + sum(charlie)*3 + sum(delta))-10*(sum(miss)+sum(tarcza.`n-s`)+sum(proc)+sum(extra)))) as suma 
                                    from strzelec inner join tarcza on strzelec.id=tarcza.strzelec_id 
                                    inner join trasa on tarcza.trasa_id=trasa.id        
                                    group by strzelec.id, trasa.id) as summing
                                inner join przebieg on przebieg.id_strzelec = summing.strzelec_id and przebieg.id_trasa = summing.trasa_id
                                inner join trasa on trasa.id=summing.trasa_id)
                            SELECT imie, nazwisko, sumaPunktow FROM ranking
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

        public static IEnumerable<ShooterWithStagePointsAndCompetitionPoints> 
            GetShooterWithStagePointsAndCompetitionPointsByIdFromDB(uint id)
        {
            string query = $@"WITH ranking AS (SELECT 
	                        summing.strzelec_imie AS imie,
                            summing.strzelec_nazwisko AS nazwisko,
                            summing.suma/przebieg.czas AS sumaPunktow,
                            summing.trasa_id AS trasaId,
                            (SELECT ((sum(alpha)*5 + sum(charlie)*3 + sum(delta))-10*(sum(miss)+sum(tarcza.`n-s`)+sum(proc)+sum(extra))) / przebieg.czas AS points FROM strzelec
		                        inner join tarcza on strzelec.id=tarcza.strzelec_id 
		                        inner join trasa on tarcza.trasa_id=trasa.id
                                INNER JOIN przebieg ON przebieg.id_strzelec = strzelec.id AND przebieg.id_trasa = trasa.id
                                WHERE strzelec.id = summing.strzelec_id) AS generalPoints,
                            RANK() OVER ( PARTITION BY trasa.id Order by summing.suma/przebieg.czas DESC) rankingGraczy 
                            from (select strzelec.imie AS strzelec_imie, strzelec.nazwisko AS strzelec_nazwisko, strzelec.id as strzelec_id, trasa.id as trasa_id, (sum(alpha)*5 + sum(charlie)*3 + sum(delta))-10*(sum(miss)+sum(tarcza.`n-s`)+sum(proc)+sum(extra)) as suma 
                                from strzelec inner join tarcza on strzelec.id=tarcza.strzelec_id 
                                inner join trasa on tarcza.trasa_id=trasa.id        
                                group by strzelec.id, trasa.id) as summing
                            inner join przebieg on przebieg.id_strzelec = summing.strzelec_id and przebieg.id_trasa = summing.trasa_id
                            inner join trasa on trasa.id=summing.trasa_id)
                        SELECT rankingGraczy AS position, imie AS name, nazwisko AS surname, sumaPunktow as stagePoints, generalPoints AS competitionPoints FROM ranking
                        WHERE trasaId = {id}";
            var shooters= new List<ShooterWithStagePointsAndCompetitionPoints>();
            using (MySqlConnection connection = DatabaseConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                    shooters.Add(new ShooterWithStagePointsAndCompetitionPoints(
                        (uint)reader.GetUInt64("position"),
                        reader.GetString("name"),
                        reader.GetString("surname"),
                        reader.GetDouble("stagePoints"),
                        reader.GetDouble("competitionPoints")));
                connection.Close();
            }
            foreach(var shooter in shooters)
                Console.WriteLine(shooter.Name + shooter.Surname);
            return shooters;
        }
        #endregion
    }
}
