using MySql.Data.MySqlClient;
using ProjektSemestrIV.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektSemestrIV.DAL.Repositories {
    class ShooterRepository {
        public static Boolean AddShooterToDatabase(Shooter shooter ) {
            Boolean executed = false;
            using(MySqlConnection connection = DatabaseConnection.Instance.Connection) {
                MySqlCommand command = new MySqlCommand($"INSERT INTO `Zawody`.`strzelec` (`imie`, `nazwisko`) VALUES {shooter.ToInsert()}", connection);
                connection.Open();
                command.ExecuteNonQuery();
                executed = true;
                connection.Close();
            }
            return executed;
        }

        public static bool EditShooterInDatabase( Shooter shooter, UInt32 id ) {
            Boolean executed = false;
            using(MySqlConnection connection = DatabaseConnection.Instance.Connection) {
                MySqlCommand command = new MySqlCommand($"UPDATE `Zawody`.`strzelec` SET `imie` = '{shooter.Name}', `nazwisko` = '{shooter.Surname}' WHERE (`id` = '{id}')", connection);
                connection.Open();
                command.ExecuteNonQuery();
                executed = true;
                connection.Close();
            }
            return executed;
        }

        public static List<Shooter> GetAllShooters() {
            List<Shooter> shooters = new List<Shooter>();
            using(MySqlConnection connection = DatabaseConnection.Instance.Connection) {
                MySqlCommand command = new MySqlCommand("SELECT * FROM Zawody.strzelec", connection);
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                while(reader.Read()) {
                    shooters.Add(new Shooter(reader));
                }
                connection.Close();
            }
            return shooters;
        }

        public static Boolean DeleteShooterFromDatabase( UInt32 shooterID ) {
            Boolean executed = false;
            using(MySqlConnection connection = DatabaseConnection.Instance.Connection) {
                MySqlCommand command = new MySqlCommand($"DELETE FROM `Zawody`.`strzelec` WHERE (`id` = '{shooterID}')", connection);
                connection.Open();
                command.ExecuteNonQuery();
                executed = true;
                connection.Close();
            }
            return executed;
        }
    }
}
