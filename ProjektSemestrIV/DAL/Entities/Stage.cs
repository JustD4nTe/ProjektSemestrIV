using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektSemestrIV.DAL.Entities {
    class Stage {
        public UInt32 ID { get; set; }
        public UInt32 Competition_ID { get; set; }
        public String Name { get; set; }
        public String Rules { get; set; }

        public Stage( MySqlDataReader reader ) {
            ID = UInt32.Parse(reader["id"].ToString());
            Competition_ID = UInt32.Parse(reader["id_zawody"].ToString());
            Name = reader["nazwa"].ToString();
            Rules = reader["zasady"].ToString();
        }

        public Stage(UInt32 competition_ID, String name, String rules ) {
            Competition_ID = competition_ID;
            Name = name;
            Rules = rules;
        }

        public string ToInsert() {
            return $"('{Competition_ID}', '{Name}', '{Rules}')";
        }

    }
}
