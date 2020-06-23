using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektSemestrIV.DAL.Entities
{
    class Target : IBaseEntity
    {
        public uint ID { get; private set; }
        public uint Shooter_ID { get; private set; }
        public uint Stage_ID { get; private set; }
        public byte Alpha { get; private set; }
        public byte Charlie { get; private set; }
        public byte Delta { get; private set; }
        public byte Miss { get; private set; }
        public byte NoShoot { get; private set; }
        public byte Procedure { get; private set; }
        public byte Extra { get; private set; }

        public Target() { }

        public Target(uint shooter_id, uint stage_id, byte alpha,
            byte charlie, byte delta, byte miss, byte noShoot, byte procedure, byte extra)
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
                new MySqlParameter("@ns", NoShoot),
                new MySqlParameter("@proc", Procedure),
                new MySqlParameter("@extra", Extra)
            };

        public void SetData(IDataReader dataReader)
        {
            ID = uint.Parse(dataReader["id"].ToString());
            Shooter_ID = uint.Parse(dataReader["strzelec_id"].ToString());
            Stage_ID = uint.Parse(dataReader["trasa_id"].ToString());
            Alpha = byte.Parse(dataReader["alpha"].ToString());
            Charlie = byte.Parse(dataReader["charlie"].ToString());
            Delta = byte.Parse(dataReader["delta"].ToString());
            Miss = byte.Parse(dataReader["miss"].ToString());
            NoShoot = byte.Parse(dataReader["n-s"].ToString());
            Procedure = byte.Parse(dataReader["proc"].ToString());
            Extra = byte.Parse(dataReader["extra"].ToString());
        }

        // shallow copy
        public object Clone() => this.MemberwiseClone();
    }
}
