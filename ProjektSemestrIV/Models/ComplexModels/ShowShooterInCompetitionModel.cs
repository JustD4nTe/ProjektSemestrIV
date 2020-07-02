using ProjektSemestrIV.DAL.Entities;
using ProjektSemestrIV.DAL.Repositories;
using ProjektSemestrIV.Extensions;
using ProjektSemestrIV.Models.ShowModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ProjektSemestrIV.Models.ComplexModels
{
    class ShowShooterInCompetitionModel
    {
        private readonly Shooter shooter;
        private readonly Competition competition;

        public ShowShooterInCompetitionModel(uint shooterId, uint competitionId)
        {
            shooter = ShooterRepository.GetShooter(shooterId);
            competition = CompetitionRepository.GetCompetition(competitionId);
        }

        public string GetShooterName()
        => $"{shooter.Name} {shooter.Surname}";

        /// <summary>
        /// Get Competition data containing: location, date of the start and optionally date of the end
        /// </summary>
        /// <returns>Formatted string</returns>
        public string GetCompetitionName()
        => $"{competition.Location} , {DateTime.Parse(competition.StartDate):g}{(DateTime.TryParse(competition.EndDate, out var result) ? $" - {result:g}" : "")}";

        public string GetPoints()
        => $"{ShooterRepository.GetCompetitionPoints(shooter.ID, competition.Id):N3}";

        public string GetTime()
        => $"{TimeSpan.FromSeconds(ShooterRepository.GetCompetitionTime(shooter.ID, competition.Id)):g}";

        public string GetPosition()
        => $"{ShooterRepository.GetPositionOnCompetition(shooter.ID, competition.Id):N0}";

        public string GetGeneralAccuracy()
        => $"{ShooterRepository.GetAccuracy(AccuracyTypeEnum.General, shooter.ID, competitionId: competition.Id):P2}";

        public string GetAlphaAccuracy()
        => $"{ShooterRepository.GetAccuracy(AccuracyTypeEnum.Alpha, shooter.ID, competitionId: competition.Id):P2}";

        public string GetCharlieAccuracy()
        => $"{ShooterRepository.GetAccuracy(AccuracyTypeEnum.Charlie, shooter.ID, competitionId: competition.Id):P2}";

        public string GetDeltaAccuracy()
        => $"{ShooterRepository.GetAccuracy(AccuracyTypeEnum.Delta, shooter.ID, competitionId: competition.Id):P2}";

        public IEnumerable<ShooterStatsOnStageShowModel> GetStatsOnStages()
        => ShooterRepository.GetStatsOnStages(shooter.ID, competition.Id)
                            .Select(x => new ShooterStatsOnStageShowModel(x.StageId, x.StageName, x.Points, $"{TimeSpan.Parse(x.Time):g}", x.StagePoints));
    }
}
