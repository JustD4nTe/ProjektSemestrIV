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
        private readonly Shooter shooter;

        public ShowShooterModel(uint id)
            => shooter = ShooterRepository.GetShooter(id);

        public string GetShooterName() => shooter.Name;

        public string GetShooterSurname() => shooter.Surname;

        public string GetGeneralAccuracy()
            => $"{ShooterRepository.GetAccuracy(AccuracyTypeEnum.General, shooter.ID):P2}";

        public string GetAlphaAccuracy()
            => $"{ShooterRepository.GetAccuracy(AccuracyTypeEnum.Alpha, shooter.ID):P2}";

        public string GetCharlieAccuracy()
            => $"{ShooterRepository.GetAccuracy(AccuracyTypeEnum.Charlie, shooter.ID):P2}";

        public string GetDeltaAccuracy()
            => $"{ShooterRepository.GetAccuracy(AccuracyTypeEnum.Delta, shooter.ID):P2}";
        public IEnumerable<ShooterCompetitionOverview> GetShootersOnCompetition()
            => ShooterRepository.GetShootersOnCompetition(shooter.ID)
                                .Select(x => new ShooterCompetitionOverview(x.CompetitionId,
                                                                            x.Location,
                                                                            x.StartDate,
                                                                            x.Position,
                                                                            x.Points));

        public string GetGeneralAvgPosition()
            => $"{ShooterRepository.GetGeneralAvgPosition(shooter.ID):N2}";

        public string GetGeneralPoints()
            => $"{ShooterRepository.GetGeneralPoints(shooter.ID):N3}";

        public string GetGeneralSumOfTimes()
            => $"{TimeSpan.FromSeconds(ShooterRepository.GetGeneralSumOfTimes(shooter.ID)):g}";
    }
}