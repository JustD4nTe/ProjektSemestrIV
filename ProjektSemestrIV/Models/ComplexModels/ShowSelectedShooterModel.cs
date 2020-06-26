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
        private readonly Shooter shooter;

        public ShowSelectedShooterModel(uint id)
            => shooter = ShooterRepository.GetShooterByIdFromDB(id);

        public string GetShooterName() => shooter.Name;

        public string GetShooterSurname() => shooter.Surname;

        public string GetShooterCompetitionGeneralAccuracy()
            => $"{ShooterRepository.GetAccuracy(AccuracyTypeEnum.General, shooter.ID):P2}";

        public string GetShooterCompetitionAlphaAccuracy()
            => $"{ShooterRepository.GetAccuracy(AccuracyTypeEnum.Alpha, shooter.ID):P2}";

        public string GetShooterCompetitionCharlieAccuracy()
            => $"{ShooterRepository.GetAccuracy(AccuracyTypeEnum.Charlie, shooter.ID):P2}";

        public string GetShooterCompetitionDeltaAccuracy()
            => $"{ShooterRepository.GetAccuracy(AccuracyTypeEnum.Delta, shooter.ID):P2}";

        public IEnumerable<ShooterCompetitionOverview> GetShooterCompetitions()
            => ShooterRepository.GetShooterAccomplishedCompetitionsFromDB(shooter.ID)
                                .Select(x => new ShooterCompetitionOverview(x.CompetitionId,
                                                                            x.Location,
                                                                            x.StartDate,
                                                                            x.Position,
                                                                            x.Points));

        public string GetShooterGeneralAveragePosition()
            => $"{ShooterRepository.GetShooterGeneralAveragePositionFromDB(shooter.ID):N2}";

        public string GetShooterGeneralSumOfPoints()
            => $"{ShooterRepository.GetShooterGeneralSumOfPointsFromDB(shooter.ID):N3}";

        public string GetShooterGeneralSumOfTimes()
            => $"{TimeSpan.FromSeconds(ShooterRepository.GetShooterGeneralSumOfTimesFromDB(shooter.ID)):g}";


    }
}