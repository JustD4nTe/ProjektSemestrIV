using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ProjektSemestrIV.DAL.Entities {
    public class Shooter {
        public UInt32 ID { get; set; }
        public String Name { get; set; }
        public String Surname { get; set; }

        public Shooter( MySqlDataReader reader ) {
            ID = UInt32.Parse(reader["id"].ToString());
            Name = reader["imie"].ToString();
            Surname = reader["nazwisko"].ToString();
        }

        public Shooter(String name, String surname ) {
            Name = name;
            Surname = surname;
        }

        public string ToInsert() {
            return $"('{Name}', '{Surname}')";
        }
    }
}
