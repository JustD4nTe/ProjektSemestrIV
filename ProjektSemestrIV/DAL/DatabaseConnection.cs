using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MySql.Data.MySqlClient;
using ProjektSemestrIV.Properties;

namespace ProjektSemestrIV.DAL {
    class DatabaseConnection {
        private MySqlConnectionStringBuilder connectionStringBuilder = new MySqlConnectionStringBuilder();

        private static DatabaseConnection instance = null;
        public static DatabaseConnection Instance {
            get {
                if(instance == null) {
                    instance = new DatabaseConnection();
                }
                return instance;
            }
        }

        public MySqlConnection Connection {
            get 
            {
                if (!CheckConnection())
                    return null;
                return new MySqlConnection(connectionStringBuilder.ToString()); 
            }
        }

        private DatabaseConnection() {
            connectionStringBuilder.Server = Properties.Settings.Default.ServerAddress;
            connectionStringBuilder.Port = Properties.Settings.Default.Port;
            connectionStringBuilder.Database = Properties.Settings.Default.Database;
            connectionStringBuilder.UserID = Properties.Settings.Default.User;
            connectionStringBuilder.Password = Properties.Settings.Default.Password;
            connectionStringBuilder.ConnectionTimeout = 2;
        }

        //https://stackoverflow.com/questions/17195200/check-mysql-db-connection
        public bool CheckConnection()
        {
            bool isConn = false;
            MySqlConnection conn = null ;
            try
            {
                conn = new MySqlConnection(connectionStringBuilder.ToString());

                conn.Open();
                isConn = true;
            }
            catch (ArgumentException)
            {
                MessageBox.Show(Resources.ConnectionErrorText, Resources.ConnectionError, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (MySqlException ex)
            {

                isConn = false;

                switch (ex.Number)
                {
                    //http://dev.mysql.com/doc/refman/5.0/en/error-messages-server.html
                    case 1042:
                        MessageBox.Show(Resources.ConnectionError1042, Resources.ConnectionError, MessageBoxButton.OK, MessageBoxImage.Error);
                        break;
                    case 1045:
                        MessageBox.Show(Resources.ConnectionError1045, Resources.AccessDenied, MessageBoxButton.OK, MessageBoxImage.Error);
                        break;
                    default:
                        string sqlErrorMessage = $"{Resources.Message}: {ex.Message }\n{Resources.Source}: {ex.Source}\n{Resources.Number}: {ex.Number}";                
                
                        MessageBox.Show(sqlErrorMessage, Resources.MySqlError, MessageBoxButton.OK, MessageBoxImage.Error);
                        break;
                }
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return isConn;
        }
    }
}
