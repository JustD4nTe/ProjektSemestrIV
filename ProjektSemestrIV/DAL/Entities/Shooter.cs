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
    public class Shooter : IBaseEntity
    {
        public uint ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public Shooter() { }

        public Shooter(string name, string surname)
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

        public void SetData(IDataReader dataReader)
        {
            ID = uint.Parse(dataReader["id"].ToString());
            Name = dataReader["imie"].ToString();
            Surname = dataReader["nazwisko"].ToString();
        }

        // shallow copy
        public object Clone() => this.MemberwiseClone();
    }
}
