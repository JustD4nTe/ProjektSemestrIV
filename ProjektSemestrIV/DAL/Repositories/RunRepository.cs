using MySql.Data.MySqlClient;
using ProjektSemestrIV.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektSemestrIV.DAL.Repositories
{
    class RunRepository
    {
        #region CRUD
        public static bool AddRunToDatabase(Run run)
        {
            bool executed = false;

            var query = $@"INSERT INTO przebieg (`czas`, `id_strzelec`, `id_trasa`)
                            VALUES {run.ToInsert()}";

            using (MySqlConnection connection = DatabaseConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                connection.Open();
                if (command.ExecuteNonQuery() == 1) executed = true;
                connection.Close();
            }
            return executed;
        }

        public static bool EditRunInDatabase(Run run, uint shooter_id, uint stage_id)
        {
            bool executed = false;
            var query = $@"UPDATE przebieg 
                            SET `czas` = '{run.RunTime}', `id_strzelec` = '{run.Shooter_ID}', `id_trasa` = '{run.Stage_ID}' 
                            WHERE (`id_strzelec` = '{shooter_id}' and `id_trasa` = '{stage_id}')";

            using (MySqlConnection connection = DatabaseConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                connection.Open();
                if (command.ExecuteNonQuery() == 1) executed = true;
                connection.Close();
            }
            return executed;
        }

        public static Run GetRunWhere(uint shooter_id, uint stage_id)
        {
            var query = $@"SELECT * FROM przebieg 
                           WHERE (`id_strzelec` = '{shooter_id}' and `id_trasa` = '{stage_id}')";

            Run run = null;

            using (MySqlConnection connection = DatabaseConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    run = new Run(reader);
                }
                connection.Close();
            }
            return run;
        }

        public static bool DeleteRunFromDatabase(uint runID)
        {
            var query = $@"DELETE FROM przebieg WHERE (`id` = '{runID}')";

            bool executed = false;

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
    }
}
