using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektSemestrIV.DAL.Entities {
    class Target {
        public UInt32 ID { get; set; }
        public UInt32 Shooter_ID { get; set; }
        public UInt32 Stage_ID { get; set; }
        public UInt16 Alpha { get; set; }
        public UInt16 Charlie { get; set; }
        public UInt16 Delta { get; set; }
        public UInt16 Miss { get; set; }
        public UInt16 NoShoot { get; set; }
        public UInt16 Procedure { get; set; }
        public UInt16 Extra { get; set; }

        public Target( MySqlDataReader reader ) {
            ID = UInt32.Parse(reader["id"].ToString());
            Shooter_ID = UInt32.Parse(reader["strzelec_id"].ToString());
            Stage_ID = UInt32.Parse(reader["trasa_id"].ToString());
            Alpha = UInt16.Parse(reader["alpha"].ToString());
            Charlie = UInt16.Parse(reader["charlie"].ToString());
            Delta = UInt16.Parse(reader["delta"].ToString());
            Miss = UInt16.Parse(reader["miss"].ToString());
            NoShoot = UInt16.Parse(reader["n-s"].ToString());
            Procedure = UInt16.Parse(reader["proc"].ToString());
            Extra = UInt16.Parse(reader["extra"].ToString());
        }

        public Target(UInt32 shooter_id, UInt32 stage_id, UInt16 alpha, UInt16 charlie, UInt16 delta, UInt16 miss, UInt16 noShoot, UInt16 procedure, UInt16 extra ) {
            Shooter_ID = shooter_id;
            Stage_ID = stage_id;
            Alpha = alpha;
            Charlie = charlie;
            Delta = delta;
            Miss = miss;
            NoShoot = noShoot;
            Procedure = procedure;
            Extra = extra;
        }

        public string ToInsert() {
            return $"('{Shooter_ID}', '{Stage_ID}', '{Alpha}', '{Charlie}', '{Delta}', '{Miss}', '{NoShoot}', '{Procedure}', '{Extra}')";
        }
    }
}
