using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace ProjektSemestrIV.DAL.Entities.AuxiliaryEntities
{
    class ShooterOnCompetition : IBaseEntity
    {
        public uint CompetitionId { get; private set; }
        public string Location { get; private set; }
        public string StartDate { get; private set; }
        public uint Position { get; private set; }
        public double Points { get; private set; }

        public void SetData(IDataReader dataReader)
        {
            CompetitionId = uint.Parse(dataReader["competitionId"].ToString());
            Location = dataReader["location"].ToString();
            StartDate = dataReader["startDate"].ToString();
            Position = uint.Parse(dataReader["position"].ToString());
            Points = double.Parse(dataReader["points"].ToString());
        }

        // shallow copy
        public object Clone() => this.MemberwiseClone();
    }
}