using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektSemestrIV.DAL.Entities
{
    class Target
    {
        public UInt32 ID { get; set; }
        public UInt32 Shooter_ID { get; set; }
        public UInt32 Stage_ID { get; set; }
        public Byte Alpha { get; set; }
        public Byte Charlie { get; set; }
        public Byte Delta { get; set; }
        public Byte Miss { get; set; }
        public Byte NoShoot { get; set; }
        public Byte Procedure { get; set; }
        public Byte Extra { get; set; }

        public Target(DataRow data)
        {
            ID = UInt32.Parse(data["id"].ToString());
            Shooter_ID = UInt32.Parse(data["strzelec_id"].ToString());
            Stage_ID = UInt32.Parse(data["trasa_id"].ToString());
            Alpha = Byte.Parse(data["alpha"].ToString());
            Charlie = Byte.Parse(data["charlie"].ToString());
            Delta = Byte.Parse(data["delta"].ToString());
            Miss = Byte.Parse(data["miss"].ToString());
            NoShoot = Byte.Parse(data["n-s"].ToString());
            Procedure = Byte.Parse(data["proc"].ToString());
            Extra = Byte.Parse(data["extra"].ToString());
        }

        public Target(UInt32 shooter_id, UInt32 stage_id, Byte alpha, Byte charlie, Byte delta, Byte miss, Byte noShoot, Byte procedure, Byte extra)
        {
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

        public IEnumerable<MySqlParameter> GetParameters()
        => new List<MySqlParameter>()
            {
                new MySqlParameter("@strzelec_id", Shooter_ID),
                new MySqlParameter("@trasa_id", Stage_ID),
                new MySqlParameter("@alpha", Alpha),
                new MySqlParameter("@charlie", Charlie),
                new MySqlParameter("@delta", Delta),
                new MySqlParameter("@miss", Miss),
                new MySqlParameter("@n-s", NoShoot),
                new MySqlParameter("@proc", Procedure),
                new MySqlParameter("@extra", Extra)
            };
    }
}
