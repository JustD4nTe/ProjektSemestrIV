using MySql.Data.MySqlClient;
using System;

namespace ProjektSemestrIV.DAL.Entities.AuxiliaryEntities
{
    class StageWithBestShooter
    {
        public String StageName { get; }
        public String ShooterName { get; }
        public String ShooterSurname { get; }
        public double ShooterPoints { get; }

        public StageWithBestShooter(MySqlDataReader reader)
        {
            StageName = reader.GetString("nazwaTrasy");
            ShooterName = reader.GetString("imieStrzelca");
            ShooterSurname = reader.GetString("nazwiskoStrzelca");
            ShooterPoints = reader.GetDouble("punktyStrzelca");
        }
    }
}
