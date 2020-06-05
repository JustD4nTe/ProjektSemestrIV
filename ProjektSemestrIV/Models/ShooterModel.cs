using ProjektSemestrIV.DAL.Entities;
using ProjektSemestrIV.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektSemestrIV.Models {
    class ShooterModel {
        public Boolean AddShooterToDatabase( Shooter shooter )
            => ShooterRepository.AddShooterToDB(shooter);

        public Boolean DeleteShooterFromDatabase( UInt32 shooterID )
            => ShooterRepository.DeleteShooterFromDB(shooterID);

        public Boolean EditShooterInDatabase( Shooter shooter, UInt32 id )
            => ShooterRepository.EditShooterInDB(shooter, id);

        public ObservableCollection<Shooter> GetAllShooters() {
            List<Shooter> shooters = ShooterRepository.GetAllShootersFromDB();
            return new ObservableCollection<Shooter>(shooters);
        }
    }
}
