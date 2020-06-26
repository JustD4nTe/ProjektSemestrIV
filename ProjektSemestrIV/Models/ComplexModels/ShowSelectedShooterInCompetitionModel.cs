using ProjektSemestrIV.DAL.Entities;
using ProjektSemestrIV.DAL.Repositories;
using ProjektSemestrIV.Extensions;
using ProjektSemestrIV.Models.ShowModels;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace ProjektSemestrIV.Models.ComplexModels
{
    class ShowSelectedShooterInCompetitionModel
    {
        Shooter shooter;
        Competition competition;

        public ShowSelectedShooterInCompetitionModel(uint shooterId, uint competitionId)
        {
            shooter = ShooterRepository.GetShooterFromDB(shooterId);
            competition = CompetitionRepository.GetCompetitionFromDB(competitionId);
        }

        public string GetShooterName()
        => $"{shooter.Name} {shooter.Surname}";

        public string GetCompetitionName()
        => $"{competition.Location} , od {DateTime.Parse(competition.StartDate):g}{(DateTime.TryParse(competition.EndDate, out var result) ? $" do {result:g}" : "")}";

        public string GetPoints()
        => $"{ShooterRepository.GetShooterSumOfPointsAtCompetitionFromDB(shooter.ID, competition.Id):N3}";

        public string GetTime()
        => $"{TimeSpan.FromSeconds(ShooterRepository.GetShooterSumOfTimesAtCompetitionFromDB(shooter.ID, competition.Id)):g}";

        public string GetPosition()
        => $"{ShooterRepository.GetShooterPositionAtCompetitionFromDB(shooter.ID, competition.Id):N0}";

        public string GetGeneralAccuracy()
        => $"{ShooterRepository.GetAccuracy(AccuracyTypeEnum.General, shooter.ID, competitionId: competition.Id):P2}";

        public string GetAlphaAccuracy()
        => $"{ShooterRepository.GetAccuracy(AccuracyTypeEnum.Alpha, shooter.ID, competitionId: competition.Id):P2}";

        public string GetCharlieAccuracy()
        => $"{ShooterRepository.GetAccuracy(AccuracyTypeEnum.Charlie, shooter.ID, competitionId: competition.Id):P2}";

        public string GetDeltaAccuracy()
        => $"{ShooterRepository.GetAccuracy(AccuracyTypeEnum.Delta, shooter.ID, competitionId: competition.Id):P2}";

        public ObservableCollection<StatsAtStageOverview> GetShooterStatsOnStages()
        => ShooterRepository.GetShooterStatsOnStages(shooter.ID, competition.Id)
                            .Select(x => new StatsAtStageOverview(x.StageId, x.StageName, x.Points, $"{TimeSpan.Parse(x.Time):g}", x.StagePoints))
                            .Convert();
    }
}
