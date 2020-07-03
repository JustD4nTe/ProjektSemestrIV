using MySql.Data.MySqlClient;
using ProjektSemestrIV.DAL.Entities;
using ProjektSemestrIV.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ProjektSemestrIV.DAL.Repositories
{
    internal abstract class BaseRepository
    {
        /// <summary>
        /// Execute insert/update/delete query
        /// </summary>
        /// <param name="query">SQL query</param>
        /// <param name="parameters">Optional, to prevent SQL Injection</param>
        /// <returns>Query success</returns>
        protected static bool ExecuteModifyQuery(string query, IEnumerable<MySqlParameter> parameters = null)
        {
            bool isExecuted;

            using (MySqlConnection connection = DatabaseConnection.Instance.Connection)
            {
                if (connection == null)
                    return false;

                MySqlCommand command = new MySqlCommand(query, connection);

                // replace parameters with values
                // to prevent sql injection
                if (parameters != null)
                {
                    foreach (var parameter in parameters)
                        command.Parameters.Add(parameter);
                }

                connection.Open();

                // ExecuteNonQuery returns the number of rows affected
                // program expect only one
                isExecuted = command.ExecuteNonQuery() == 1;

                connection.Close();
            }

            return isExecuted;
        }

        /// <summary>
        /// Execute select query
        /// </summary>
        /// <typeparam name="T">class inherited by IBaseEntity or any value type (like int, double)</typeparam>
        /// <param name="query">SQL Query</param>
        /// <returns>Collection of query results</returns>
        protected static IEnumerable<T> ExecuteSelectQuery<T>(string query) where T : new()
        {
            List<T> values = new List<T>();

            using (var connection = DatabaseConnection.Instance.Connection)
            {
                if (connection == null)
                    return default;

                var command = new MySqlCommand(query, connection);
                connection.Open();

                MySqlDataReader reader = command.ExecuteReader();

                T temp = new T();

                // when T is entity class 
                if (temp is IBaseEntity entity)
                {
                    while (reader.Read())
                    {
                        // load data to temporary object
                        entity.SetData(reader);
                        // add copy of object, not a reference
                        values.Add((T)entity.Clone());
                    }
                }

                // when T is value type (int, double...)
                else if (temp.GetType().IsValueType)
                {
                    while (reader.Read())
                    {
                        // when db return 'null' value
                        if (reader[0] is DBNull)
                            values.Add(default);

                        // convert object into value type
                        else
                            values.Add((T)Convert.ChangeType(reader[0], typeof(T)));
                    }
                }

                // it's primitive solution for string type
                // because string hasn't parameterless constructor -_-
                else
                {
                    try
                    {
                        while (reader.Read())
                        {
                            values.Add((T)reader[0]);
                        }
                    }
                    // but for all other types, just throw exception about unhandled type T
                    catch
                    {
                        throw new Exception($"{typeof(T).Name} {Properties.Resources.InvalidCasting}");
                    }

                }

                connection.Close();
            }

            return values;
        }
    }
}
