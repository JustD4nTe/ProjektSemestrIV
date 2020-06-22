using MySql.Data.MySqlClient;
using ProjektSemestrIV.DAL.Entities;
using System;
using System.Collections.Generic;

namespace ProjektSemestrIV.DAL.Repositories
{
    internal class StageRepository
    {
        #region CRUD

        public static List<Stage> GetAllStages()
        {
            var query = "SELECT * FROM trasa";
            List<Stage> stages = new List<Stage>();

            using (MySqlConnection connection = DatabaseConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    stages.Add(new Stage(reader));
                }
                connection.Close();
            }
            return stages;
        }

        public static Stage GetStageByIdFromDB(uint id)
        {
            var query = $"SELECT * FROM trasa WHERE trasa.id = {id}";
            Stage stage = null;

            using (MySqlConnection connection = DatabaseConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                    stage = new Stage(reader);
                connection.Close();
            }
            return stage;
        }

        public static bool AddStageToDatabase(Stage stage)
        {
            bool executed = false;
            var query = @"INSERT INTO trasa (`id_zawody`, `nazwa`, `zasady`) 
                            VALUES (@id_zawody, @nazwa, @zasady)";

            using (MySqlConnection connection = DatabaseConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(query, connection);

                foreach (var parameter in stage.GetParameters())
                {
                    command.Parameters.Add(parameter);
                }

                connection.Open();
                if (command.ExecuteNonQuery() == 1) executed = true;
                connection.Close();
            }
            return executed;
        }

        public static bool EditStageInDatabase(Stage stage, uint id)
        {
            bool executed = false;
            var query = $@"UPDATE `trasa` 
                            SET `id_zawody` = @id_zawody, `nazwa` = @nazwa,
                                `zasady` = @zasady 
                            WHERE (`id` = '{id}');";

            using (MySqlConnection connection = DatabaseConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(query, connection);

                foreach (var parameter in stage.GetParameters())
                {
                    command.Parameters.Add(parameter);
                }

                connection.Open();
                if (command.ExecuteNonQuery() == 1) executed = true;
                connection.Close();
            }
            return executed;
        }

        public static bool DeleteStageFromDatabase(uint stageID)
        {
            bool executed = false;
            var query = $"DELETE FROM trasa WHERE (`id` = '{stageID}')";

            using (MySqlConnection connection = DatabaseConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                connection.Open();
                if (command.ExecuteNonQuery() == 1) executed = true;
                connection.Close();
            }
            return executed;
        }

        #endregion CRUD

        #region Auxiliary queries

        public static uint GetNumOfTargetsOnStageFromDB(uint id)
        {
            var query = $@"SELECT count(tarcza.id) AS numOfTargets FROM trasa
                            INNER JOIN tarcza ON trasa.id = tarcza.trasa_id
                            WHERE trasa.id = {id};";

            uint numOfTargets = 0;

            using (MySqlConnection connection = DatabaseConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                    numOfTargets = (uint)reader.GetInt64("numOfTargets");
                connection.Close();
            }
            return numOfTargets;
        }

        public static double GetAverageTimeOnStageByIdFromDB(uint id)
        {
            var query = $@"SELECT avg(przebieg.czas) AS averageTime FROM przebieg
                            INNER JOIN trasa ON trasa.id = przebieg.id_trasa
                            WHERE trasa.id = {id};";

            double averageTime = 0;

            using (MySqlConnection connection = DatabaseConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                    averageTime = reader.GetDouble("averageTime");
                connection.Close();
            }
            return averageTime;
        }

        #endregion Auxiliary queries
    }
}