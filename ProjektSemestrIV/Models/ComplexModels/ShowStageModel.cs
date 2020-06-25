using ProjektSemestrIV.DAL.Entities;
using ProjektSemestrIV.DAL.Repositories;
using ProjektSemestrIV.Models.ShowModels;
using System.Collections.Generic;
using System.Linq;

namespace ProjektSemestrIV.Models.ComplexModels
{
    class ShowStageModel
    {
        private Stage stage;
        private Competition competition;

        public ShowStageModel(uint id)
        {
            stage = StageRepository.GetStageById(id);
            competition = CompetitionRepository.GetCompetition(stage.Competition_ID);
        }

        public string GetCompetitionLocation() => competition.Location;

        public string GetStageName() => stage.Name;

        public string GetStageRules() => stage.Rules;

        public uint GetNumOfTargets() => StageRepository.GetNumOfTargetsOnStage(stage.ID);

        public string GetShooterWithPoints()
        {
            var shooter = ShooterRepository.GetShooterWithPointsByStageId(stage.ID);
            return $"{shooter.Name} {shooter.Surname} : {shooter.Points}pkt";
        }

        public double GetAverageTime() => StageRepository.GetAverageTimeOnStageById(stage.ID);

        public IEnumerable<ShooterWithStageAndCompetitionPointsOverview> GetShooters()
            => ShooterRepository.GetShootersWithStagePointsAndCompetitionPointsById(stage.ID)
                                .Select(x => new ShooterWithStageAndCompetitionPointsOverview(x.Id,
                                                                                                x.Position,
                                                                                                x.Name,
                                                                                                x.Surname,
                                                                                                x.StagePoints,
                                                                                                x.CompetitionPoints));
    }
}