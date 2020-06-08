using MySql.Data.MySqlClient;
using System;

namespace ProjektSemestrIV.DAL.Entities.AuxiliaryEntities
{
    class ShooterWithPoints
    {
        public String Name { get; }
        public String Surname { get; }
        public double Points { get; }

        public ShooterWithPoints(MySqlDataReader reader)
        {
            Name = reader.GetString("imie");
            Surname = reader.GetString("nazwisko");
            Points = reader.GetDouble("sumaPunktow");
        }
    }
}
