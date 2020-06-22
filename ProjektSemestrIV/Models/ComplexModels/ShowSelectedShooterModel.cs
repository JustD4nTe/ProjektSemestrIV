using ProjektSemestrIV.DAL.Entities;
using ProjektSemestrIV.DAL.Repositories;
using ProjektSemestrIV.Models.ShowModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjektSemestrIV.Models.ComplexModels
{
    class ShowSelectedShooterModel
    {
        private Shooter shooter;

        public ShowSelectedShooterModel(uint id)
            => shooter = ShooterRepository.GetShooterByIdFromDB(id);

        public string GetShooterName() => shooter.Name;

        public string GetShooterSurname() => shooter.Surname;

        public string GetShooterCompetitionGeneralAccuracy()
            => String.Format("{0:P2}", ShooterRepository.GetAccuracy(AccuracyTypeEnum.General, shooter.ID));

        public string GetShooterCompetitionAlphaAccuracy()
            => String.Format("{0:P2}", ShooterRepository.GetAccuracy(AccuracyTypeEnum.Alpha, shooter.ID));

        public string GetShooterCompetitionCharlieAccuracy()
            => String.Format("{0:P2}", ShooterRepository.GetAccuracy(AccuracyTypeEnum.Charlie, shooter.ID));

        public string GetShooterCompetitionDeltaAccuracy()
            => String.Format("{0:P2}", ShooterRepository.GetAccuracy(AccuracyTypeEnum.Delta, shooter.ID));

        public IEnumerable<ShooterCompetitionOverview> GetShooterCompetitions()
            => ShooterRepository.GetShooterAccomplishedCompetitionsFromDB(shooter.ID)
                                .Select(x => new ShooterCompetitionOverview(x.CompetitionId,
                                                                            x.Location,
                                                                            x.StartDate,
                                                                            x.Position,
                                                                            x.Points));

        public string GetShooterGeneralAveragePosition()
            => String.Format("{0:N2}", ShooterRepository.GetShooterGeneralAveragePositionFromDB(shooter.ID));

        public string GetShooterGeneralSumOfPoints()
            => String.Format("{0:N3}", ShooterRepository.GetShooterGeneralSumOfPointsFromDB(shooter.ID));

        public string GetShooterGeneralSumOfTimes()
            => TimeSpan.FromSeconds(ShooterRepository.GetShooterGeneralSumOfTimesFromDB(shooter.ID))
                       .ToString(@"hh\h\:mm\m\:ss\s\:fff\m\s");
    }
}