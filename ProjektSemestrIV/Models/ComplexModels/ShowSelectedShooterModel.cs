using ProjektSemestrIV.DAL.Entities;
using ProjektSemestrIV.DAL.Repositories;
using ProjektSemestrIV.Models.ShowModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace ProjektSemestrIV.Models.ComplexModels
{
    class ShowSelectedShooterModel
    {
        private Shooter shooter;

        public ShowSelectedShooterModel(uint id)
        {
            shooter = ShooterRepository.GetShooterByIdFromDB(id);
        }

        public string GetShooterName() => shooter.Name;

        public string GetShooterSurname() => shooter.Surname;

        public double GetShooterCompetitionGeneralAccuracy() => ShooterRepository.GetShooterCompetitionGeneralAccuracyFromDB(shooter.ID);

        public double GetShooterCompetitionAlphaAccuracy() => ShooterRepository.GetShooterCompetitionAlphaAccuracyFromDB(shooter.ID);

        public double GetShooterCompetitionCharlieAccuracy() => ShooterRepository.GetShooterCompetitionCharlieAccuracyFromDB(shooter.ID);

        public double GetShooterCompetitionDeltaAccuracy() => ShooterRepository.GetShooterCompetitionDeltaAccuracyFromDB(shooter.ID);

        public List<ShooterCompetitionOverview> GetShooterCompetitions() => ShooterRepository.GetShooterAccomplishedCompetitionsFromDB(shooter.ID);

        public double GetShooterGeneralAveragePositionFromDB() => ShooterRepository.GetShooterGeneralAveragePositionFromDB(shooter.ID);

    }
}
