using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektSemestrIV.DAL.Entities
{
    class Stage
    {
        public UInt32 ID { get; set; }
        public UInt32 Competition_ID { get; set; }
        public String Name { get; set; }
        public String Rules { get; set; }

        public Stage(DataRow data)
        {
            ID = UInt32.Parse(data["id"].ToString());
            Competition_ID = UInt32.Parse(data["id_zawody"].ToString());
            Name = data["nazwa"].ToString();
            Rules = data["zasady"].ToString();
        }

        public Stage(UInt32 competition_ID, String name, String rules)
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
    }
}
