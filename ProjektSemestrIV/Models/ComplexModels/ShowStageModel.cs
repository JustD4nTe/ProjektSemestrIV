using ProjektSemestrIV.DAL.Entities;
using ProjektSemestrIV.DAL.Repositories;
using ProjektSemestrIV.Models.ShowModels;
using System.Collections.Generic;
using System.Linq;

namespace ProjektSemestrIV.Models.ComplexModels
{
    class ShowStageModel
    {
        private readonly Stage stage;
        private readonly Competition competition;

        public ShowStageModel(uint id)
        {
            stage = StageRepository.GetStage(id);
            competition = CompetitionRepository.GetCompetition(stage.Competition_ID);
        }

        public string GetCompetitionLocation() => competition.Location;

        public string GetStageName() => stage.Name;

        public string GetStageRules() => stage.Rules;

        public uint GetTargetsCount() => StageRepository.GetTargetsCount(stage.ID);

        public string GetBestShooter()
        {
            var shooter = ShooterRepository.GetBestShooter(stage.ID);
            return $"{shooter.Name} {shooter.Surname} : {shooter.Points}pkt";
        }

        public double GetAvgTime() => StageRepository.GetAvgTime(stage.ID);

        public IEnumerable<ShooterWithStageAndCompetitionPointsOverview> GetShooters()
            => ShooterRepository.GetStageAndCompetitionPoints(stage.ID)
                                .Select(x => new ShooterWithStageAndCompetitionPointsOverview(x.Id,
                                                                                                x.Position,
                                                                                                x.Name,
                                                                                                x.Surname,
                                                                                                x.StagePoints,
                                                                                                x.CompetitionPoints));
    }
}