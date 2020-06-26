using ProjektSemestrIV.DAL.Entities;
using ProjektSemestrIV.DAL.Repositories;
using ProjektSemestrIV.Models.ShowModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjektSemestrIV.Models.ComplexModels
{
    class ShowShooterModel
    {
        private Shooter shooter;

        public ShowShooterModel(uint id)
            => shooter = ShooterRepository.GetShooter(id);

        public string GetShooterName() => shooter.Name;

        public string GetShooterSurname() => shooter.Surname;

        public string GetGeneralAccuracy()
            => String.Format("{0:P2}", ShooterRepository.GetAccuracy(AccuracyTypeEnum.General, shooter.ID));

        public string GetAlphaAccuracy()
            => String.Format("{0:P2}", ShooterRepository.GetAccuracy(AccuracyTypeEnum.Alpha, shooter.ID));

        public string GetCharlieAccuracy()
            => String.Format("{0:P2}", ShooterRepository.GetAccuracy(AccuracyTypeEnum.Charlie, shooter.ID));

        public string GetDeltaAccuracy()
            => String.Format("{0:P2}", ShooterRepository.GetAccuracy(AccuracyTypeEnum.Delta, shooter.ID));

        public IEnumerable<ShooterCompetitionOverview> GetShootersOnCompetition()
            => ShooterRepository.GetShootersOnCompetition(shooter.ID)
                                .Select(x => new ShooterCompetitionOverview(x.CompetitionId,
                                                                            x.Location,
                                                                            x.StartDate,
                                                                            x.Position,
                                                                            x.Points));

        public string GetGeneralAvgPosition()
            => String.Format("{0:N2}", ShooterRepository.GetGeneralAvgPosition(shooter.ID));

        public string GetGeneralPoints()
            => String.Format("{0:N3}", ShooterRepository.GetGeneralPoints(shooter.ID));

        public string GetGeneralSumOfTimes()
            => TimeSpan.FromSeconds(ShooterRepository.GetGeneralSumOfTimes(shooter.ID))
                       .ToString(@"hh\h\:mm\m\:ss\s\:fff\m\s");
    }
}