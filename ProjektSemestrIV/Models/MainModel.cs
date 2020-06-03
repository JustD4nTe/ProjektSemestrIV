using ProjektSemestrIV.DAL.Entities;
using ProjektSemestrIV.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektSemestrIV.Models {
    class MainModel {
        public Boolean AddShooterToDatabase( Shooter shooter ) {
            if(ShooterRepository.AddShooterToDatabase(shooter)) {
                return true;
            }
            return false;
        }

        public Boolean DeleteShooterFromDatabase( UInt32 shooterID ) {
            if(ShooterRepository.DeleteShooterFromDatabase(shooterID)) {
                return true;
            }
            return false;
        }

        public Boolean EditShooterInDatabase( Shooter shooter, UInt32 id ) {
            if(ShooterRepository.EditShooterInDatabase(shooter, id)) {
                return true;
            }
            return false;
        }

        public ObservableCollection<Shooter> GetAllShooters() {
            List<Shooter> shooters = ShooterRepository.GetAllShooters();
            ObservableCollection<Shooter> shootersCollection = new ObservableCollection<Shooter>();
            foreach(Shooter shooter in shooters) {
                shootersCollection.Add(shooter);
            }
            return shootersCollection;
        }
    }
}
