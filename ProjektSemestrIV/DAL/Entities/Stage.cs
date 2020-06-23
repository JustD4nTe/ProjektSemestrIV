using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektSemestrIV.DAL.Entities
{
    class Stage : IBaseEntity
    {
        public uint ID { get; private set; }
        public uint Competition_ID { get; private set; }
        public string Name { get; private set; }
        public string Rules { get; private set; }

        public Stage() { }

        public Stage(uint competition_ID, string name, string rules)
        {
            Competition_ID = competition_ID;
            Name = name;
            Rules = rules;
        }

        public IEnumerable<MySqlParameter> GetParameters()
        => new List<MySqlParameter>()
            {
                new MySqlParameter("@id_zawody", Competition_ID),
                new MySqlParameter("@nazwa", Name),
                new MySqlParameter("@zasady", Rules)
            };

        public void SetData(IDataReader dataReader)
        {
            ID = UInt32.Parse(dataReader["id"].ToString());
            Competition_ID = UInt32.Parse(dataReader["id_zawody"].ToString());
            Name = dataReader["nazwa"].ToString();
            Rules = dataReader["zasady"].ToString();
        }

        // shallow copy
        public object Clone() => this.MemberwiseClone();
    }
}
