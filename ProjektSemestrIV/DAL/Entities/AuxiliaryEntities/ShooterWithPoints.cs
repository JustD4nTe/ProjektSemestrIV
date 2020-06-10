using MySql.Data.MySqlClient;
using System;

namespace ProjektSemestrIV.DAL.Entities.AuxiliaryEntities
{
    class ShooterWithPoints
    {
        public uint Id { get; }
        public String Name { get; }
        public String Surname { get; }
        public double Points { get; }

        public ShooterWithPoints(MySqlDataReader reader)
        {
            Id = reader.GetUInt32("Id");
            Name = reader.GetString("imie");
            Surname = reader.GetString("nazwisko");
            Points = reader.GetDouble("sumaPunktow");
        }
    }
}
