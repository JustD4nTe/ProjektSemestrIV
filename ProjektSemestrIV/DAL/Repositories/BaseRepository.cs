using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ProjektSemestrIV.DAL.Repositories
{
    abstract class BaseRepository
    {
        protected static bool ExecuteAddQuery(string query, IEnumerable<MySqlParameter> parameters)
        {
            bool isExecuted;

            using (MySqlConnection connection = DatabaseConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                
                // replace parameters with values
                // to prevent sql injection
                foreach (var parameter in parameters)
                    command.Parameters.Add(parameter);

                connection.Open();

                // ExecuteNonQuery returns the number of rows affected
                // program expect only one
                isExecuted = command.ExecuteNonQuery() == 1;

                connection.Close();
            }

            return isExecuted;
        }

        protected static DataTable ExecuteSelectQuery(string query)
        {
            DataTable resultOfQuery = new DataTable();

            using (var connection = DatabaseConnection.Instance.Connection)
            {
                var command = new MySqlCommand(query, connection);
                connection.Open();

                // load all data into DataTable
                resultOfQuery.Load(command.ExecuteReader());

                connection.Close();
            }

            return resultOfQuery;
        }

        protected static bool ExecuteUpdateQuery(string query, IEnumerable<MySqlParameter> parameters)
        {
            bool isExecuted;

            using (MySqlConnection connection = DatabaseConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(query, connection);

                // replace parameters with values
                // to prevent sql injection
                foreach (var parameter in parameters)
                    command.Parameters.Add(parameter);

                connection.Open();

                // ExecuteNonQuery returns the number of rows affected
                // program expect only one
                isExecuted = command.ExecuteNonQuery() == 1;

                connection.Close();
            }

            return isExecuted;
        }

        protected static bool ExecuteDeleteQuery(string query)
        {
            bool isExecuted;

            using (MySqlConnection connection = DatabaseConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(query, connection);

                connection.Open();

                // ExecuteNonQuery returns the number of rows affected
                // program expect only one
                isExecuted = command.ExecuteNonQuery() == 1;

                connection.Close();
            }

            return isExecuted;
        }
    }
}
