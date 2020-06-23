using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace ProjektSemestrIV.DAL.Entities.AuxiliaryEntities
{
    class ShooterWithCompetitionTime : IBaseEntity
    {
        public string Name { get; private set; }
        public string Surname { get; private set; }
        public double TimeInSeconds { get; private set; }

        public void SetData(IDataReader dataReader)
        {
            Name = dataReader["imie"].ToString();
            Surname = dataReader["nazwisko"].ToString();
            TimeInSeconds = double.Parse(dataReader["czas"].ToString());
        }

        // shallow copy
        public object Clone() => this.MemberwiseClone();
    }
}
