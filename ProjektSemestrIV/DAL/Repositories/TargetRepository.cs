using MySql.Data.MySqlClient;
using ProjektSemestrIV.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektSemestrIV.DAL.Repositories {
    class TargetRepository {
        public static List<Target> GetTargetsWhere(UInt32 shooter_id, UInt32 stage_id ) {
            List<Target> targets = new List<Target>();
            using(MySqlConnection connection = DatabaseConnection.Instance.Connection) {
                MySqlCommand command = new MySqlCommand($"SELECT * FROM tarcza WHERE strzelec_id = '{shooter_id}' and trasa_id = '{stage_id}'", connection);
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                while(reader.Read()) {
                    targets.Add(new Target(reader));
                }
                connection.Close();
            }
            return targets;
        }

        public static Boolean AddTargetToDatabase( Target target ) {
            Boolean executed = false;
            using(MySqlConnection connection = DatabaseConnection.Instance.Connection) {
                MySqlCommand command = new MySqlCommand($"INSERT INTO tarcza (`strzelec_id`, `trasa_id`, `alpha`, `charlie`, `delta`, `miss`, `n-s`, `proc`, `extra`) VALUES {target.ToInsert()}", connection);
                connection.Open();
                if(command.ExecuteNonQuery() == 1) executed = true;
                connection.Close();
            }
            return executed;
        }

        public static Boolean EditTargetInDatabase( Target target, UInt32 target_id) {
            Boolean executed = false;
            using(MySqlConnection connection = DatabaseConnection.Instance.Connection) {
                MySqlCommand command = new MySqlCommand($"UPDATE `tarcza` SET `strzelec_id` = '{target.Shooter_ID}', `trasa_id` = '{target.Stage_ID}, `alpha` = '{target.Alpha}', `charlie` = '{target.Charlie}', `delta` = '{target.Delta}', `miss` = '{target.Miss}', `n-s` = '{target.NoShoot}', `proc` = '{target.Procedure}', `extra` = '{target.Extra}' WHERE strzelec_id = '{target_id}'", connection);
                connection.Open();
                if(command.ExecuteNonQuery() == 1) executed = true;
                connection.Close();
            }
            return executed;
        }

        public static bool DeleteTargetFromDatabase( UInt32 targetID ) {
            Boolean executed = false;
            using(MySqlConnection connection = DatabaseConnection.Instance.Connection) {
                MySqlCommand command = new MySqlCommand($"DELETE FROM tarcza WHERE (`id` = '{targetID}')", connection);
                connection.Open();
                if(command.ExecuteNonQuery() == 1) executed = true;
                connection.Close();
            }
            return executed;
        }
    }
}
