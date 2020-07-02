using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektSemestrIV.DAL.Entities
{
    class Run : IBaseEntity
    {
        public uint ID { get; private set; }
        public string RunTime { get; private set; }
        public uint Shooter_ID { get; private set; }
        public uint Stage_ID { get; private set; }


        public Run() { }

        public Run(string runTime, uint shooter_id, uint stage_id)
        {
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

        public void SetData(IDataReader dataReader)
        {
            ID = uint.Parse(dataReader["id"].ToString());
            RunTime = ((TimeSpan)dataReader["czas"]).ToString(@"hh\:mm\:ss\:fff");
            Shooter_ID = uint.Parse(dataReader["id_strzelec"].ToString());
            Stage_ID = uint.Parse(dataReader["id_trasa"].ToString());
        }

        // shallow copy
        public object Clone() => this.MemberwiseClone();
    }
}
