using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektSemestrIV.DAL.Entities {
    class Run {
        public UInt32 ID { get; set; }
        public String RunTime { get; set; }
        public UInt32 Shooter_ID { get; set; }
        public UInt32 Stage_ID { get; set; }

        public Run( MySqlDataReader reader ) {
            ID = UInt32.Parse(reader["id"].ToString());
            RunTime = reader["czas"].ToString();
            Shooter_ID = UInt32.Parse(reader["id_strzelec"].ToString());
            Stage_ID = UInt32.Parse(reader["id_trasa"].ToString());
        }

        public Run(String runTime, UInt32 shooter_id, UInt32 stage_id ) {
            RunTime = runTime;
            Shooter_ID = shooter_id;
            Stage_ID = stage_id;
        }

        public IEnumerable<MySqlParameter> GetParameters()
        => new List<MySqlParameter>()
            {
                new MySqlParameter("@czas", RunTime),
                new MySqlParameter("@id_strzelec", Shooter_ID),
                new MySqlParameter("@id_trasa", Stage_ID)
            };
    }
}
