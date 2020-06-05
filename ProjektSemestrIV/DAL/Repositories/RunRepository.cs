﻿using MySql.Data.MySqlClient;
using ProjektSemestrIV.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektSemestrIV.DAL.Repositories {
    class RunRepository {
        public static Boolean AddRunToDatabase( Run run ) {
            Boolean executed = false;
            using(MySqlConnection connection = DatabaseConnection.Instance.Connection) {
                MySqlCommand command = new MySqlCommand($"INSERT INTO przebieg (`czas`, `id_strzelec`, `id_trasa`) VALUES {run.ToInsert()}", connection);
                connection.Open();
                if(command.ExecuteNonQuery() == 1) executed = true;
                connection.Close();
            }
            return executed;
        }

        public static Boolean EditRunInDatabase( Run run, UInt32 shooter_id, UInt32 stage_id ) {
            Boolean executed = false;
            using(MySqlConnection connection = DatabaseConnection.Instance.Connection) {
                MySqlCommand command = new MySqlCommand($"UPDATE przebieg SET `czas` = '{run.RunTime}', `id_strzelec` = '{run.Shooter_ID}', `id_trasa` = '{run.Stage_ID}' WHERE (`id_strzelec` = '{shooter_id}' and `id_trasa` = '{stage_id}')", connection);
                connection.Open();
                if(command.ExecuteNonQuery() == 1) executed = true;
                connection.Close();
            }
            return executed;
        }

        public static Run GetRunWhere( UInt32 shooter_id, UInt32 stage_id ) {
            Run run = null;
            using(MySqlConnection connection = DatabaseConnection.Instance.Connection) {
                MySqlCommand command = new MySqlCommand($"SELECT * FROM przebieg WHERE (`id_strzelec` = '{shooter_id}' and `id_trasa` = '{stage_id}')", connection);
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                if(reader.Read()) {
                    run = new Run(reader);
                }
                connection.Close();
            }
            return run;
        }

        public static Boolean DeleteRunFromDatabase( UInt32 runID ) {
            Boolean executed = false;
            using(MySqlConnection connection = DatabaseConnection.Instance.Connection) {
                MySqlCommand command = new MySqlCommand($"DELETE FROM przebieg WHERE (`id` = '{runID}')", connection);
                connection.Open();
                if(command.ExecuteNonQuery() == 1) executed = true;
                connection.Close();
            }
            return executed;
        }
    }
}
