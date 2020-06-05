using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektSemestrIV.DAL.Entities
{
    class ShooterWithPoints
    {
        public String Name { get; set; }
        public String Surname { get; set; }
        public double Points { get; set; }

        public ShooterWithPoints(MySqlDataReader reader)
        {
            Name = reader["Imie"].ToString();
            Surname = reader["Nazwisko"].ToString();
            Points = reader.GetDouble("SumaPunktow");
        }
    }
}
