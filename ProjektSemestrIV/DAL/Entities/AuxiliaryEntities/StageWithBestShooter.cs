using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace ProjektSemestrIV.DAL.Entities.AuxiliaryEntities
{
    class StageWithBestShooter : IBaseEntity
    {
        public uint Id { get; private set; }
        public string StageName { get; private set; }
        public string ShooterName { get; private set; }
        public string ShooterSurname { get; private set; }
        public double ShooterPoints { get; private set; }

        public void SetData(IDataReader dataReader)
        {
            Id = uint.Parse(dataReader["trasa_id"].ToString());
            StageName = dataReader["nazwaTrasy"].ToString();
            ShooterName = dataReader["imieStrzelca"].ToString();
            ShooterSurname = dataReader["nazwiskoStrzelca"].ToString();
            ShooterPoints = double.Parse(dataReader["punktyStrzelca"].ToString());
        }

        // shallow copy
        public object Clone() => this.MemberwiseClone();
    }
}
