using ProjektSemestrIV.DAL.Entities;
using ProjektSemestrIV.DAL.Repositories;
using ProjektSemestrIV.Extensions;
using ProjektSemestrIV.Models.ShowModels;
using System.Collections.ObjectModel;
using System.Linq;

namespace ProjektSemestrIV.Models.ComplexModels
{
    class ShowShooterInCompetitionModel
    {
        Shooter shooter;
        Competition competition;

        public ShowShooterInCompetitionModel(uint shooterId, uint competitionId)
        {
            shooter = ShooterRepository.GetShooter(shooterId);
            competition = CompetitionRepository.GetCompetition(competitionId);
        }

        public string GetShooterName()
        => shooter.Name + " " + shooter.Surname;

        public string GetCompetitionName()
        => competition.Location + ", " + competition.StartDate.Substring(0, 10)
                + " - " + competition.EndDate.Substring(0, 10);

        public double GetPoints()
        => ShooterRepository.GetShooterSumOfPointsAtCompetition(shooter.ID, competition.Id);

        public double GetTime()
        => ShooterRepository.GetShooterSumOfTimesAtCompetition(shooter.ID, competition.Id);

        public uint GetPosition()
        => ShooterRepository.GetShooterPositionAtCompetition(shooter.ID, competition.Id);

        public double GetGeneralAccuracy()
        => ShooterRepository.GetAccuracy(AccuracyTypeEnum.General, shooter.ID, competitionId: competition.Id);

        public double GetAlphaAccuracy()
        => ShooterRepository.GetAccuracy(AccuracyTypeEnum.Alpha, shooter.ID, competitionId: competition.Id);

        public double GetCharlieAccuracy()
        => ShooterRepository.GetAccuracy(AccuracyTypeEnum.Charlie, shooter.ID, competitionId: competition.Id);

        public double GetDeltaAccuracy()
        => ShooterRepository.GetAccuracy(AccuracyTypeEnum.Delta, shooter.ID, competitionId: competition.Id);

        public ObservableCollection<ShooterStatsOnStageOverview> GetShooterStatsOnStages()
        => ShooterRepository.GetShooterStatsOnStages(shooter.ID, competition.Id)
                            .Select(x => new ShooterStatsOnStageOverview(x.StageId, x.StageName, x.Points, x.Time, x.StagePoints))
                            .Convert();
    }
}
