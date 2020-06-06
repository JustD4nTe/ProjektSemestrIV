using MySql.Data.MySqlClient;
using System;

namespace ProjektSemestrIV.DAL.Entities.AuxiliaryEntities
{
    class ShooterWithPoints
    {
        public String Name { get; set; }
        public String Surname { get; set; }
        public double Points { get; set; }

        public ShooterWithPoints(MySqlDataReader reader)
        {
            Name = reader.GetString("imie");
            Surname = reader.GetString("nazwisko");
            Points = reader.GetDouble("sumaPunktow");
        }
    }
}
