using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

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
            get { return new MySqlConnection(connectionStringBuilder.ToString()); }
        }

        private DatabaseConnection() {
            connectionStringBuilder.Server = Properties.Settings.Default.ServerAddress;
            connectionStringBuilder.Port = Properties.Settings.Default.Port;
            connectionStringBuilder.Database = Properties.Settings.Default.Database;
            connectionStringBuilder.UserID = Properties.Settings.Default.User;
            connectionStringBuilder.Password = Properties.Settings.Default.Password;
        }
    }
}
