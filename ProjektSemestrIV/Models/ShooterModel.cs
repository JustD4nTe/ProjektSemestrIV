using ProjektSemestrIV.DAL.Entities;
using ProjektSemestrIV.DAL.Repositories;
using ProjektSemestrIV.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektSemestrIV.Models {
    class ShooterModel {
        public Boolean AddShooter( Shooter shooter )
            => ShooterRepository.AddShooter(shooter);

        public Boolean DeleteShooter( UInt32 shooterID )
            => ShooterRepository.DeleteShooter(shooterID);

        public Boolean EditShooter( Shooter shooter, UInt32 id )
            => ShooterRepository.EditShooter(shooter, id);

        public IEnumerable<Shooter> GetAllShooters()
            => ShooterRepository.GetAllShooters();
    }
}
