using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace ProjektSemestrIV.DAL.Entities.AuxiliaryEntities
{
    class ShooterWithPoints : IBaseEntity
    {
        public uint Id { get; private set; }
        public string Name { get; private set; }
        public string Surname { get; private set; }
        public double Points { get; private set; }

        public void SetData(IDataReader dataReader)
        {
            Id = uint.Parse(dataReader["Id"].ToString());
            Name = dataReader["imie"].ToString();
            Surname = dataReader["nazwisko"].ToString();
            Points = double.Parse(dataReader["sumaPunktow"].ToString());
        }

        // shallow copy
        public object Clone() => this.MemberwiseClone();
    }
}
