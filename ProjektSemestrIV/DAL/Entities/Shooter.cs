using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ProjektSemestrIV.DAL.Entities
{
    public class Shooter
    {
        public UInt32 ID { get; set; }
        public String Name { get; set; }
        public String Surname { get; set; }

        public Shooter(DataRow data)
        {
            ID = UInt32.Parse(data["id"].ToString());
            Name = data["imie"].ToString();
            Surname = data["nazwisko"].ToString();
        }

        public Shooter(String name, String surname)
        {
            Name = name;
            Surname = surname;
        }

        public IEnumerable<MySqlParameter> GetParameters()
            => new List<MySqlParameter>()
            {
                new MySqlParameter("@imie", Name),
                new MySqlParameter("@nazwisko", Surname)
            };
    }
}
