using MySql.Data.MySqlClient;
using System;

namespace ProjektSemestrIV.DAL.Entities.AuxiliaryEntities
{
    class StageWithBestShooter
    {
        public String StageName { get; set; }
        public String ShooterName { get; set; }
        public String ShooterSurname { get; set; }
        public double ShooterPoints { get; set; }

        public StageWithBestShooter(MySqlDataReader reader)
        {
            StageName = reader.GetString("nazwaTrasy");
            ShooterName = reader.GetString("imieStrzelca");
            ShooterSurname = reader.GetString("nazwiskoStrzelca");
            ShooterPoints = reader.GetDouble("punktyStrzelca");
        }
    }
}
