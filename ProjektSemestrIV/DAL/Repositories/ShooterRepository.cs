﻿using MySql.Data.MySqlClient;
using ProjektSemestrIV.DAL.Entities;
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

        public static List<ShooterCompetitionOverview> GetShooterAccomplishedCompetitionsFromDB(uint id)
        {
            var results = new List<ShooterCompetitionOverview>();
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
                    results.Add(new ShooterCompetitionOverview(
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

        #endregion
    }
}
