using ProjektSemestrIV.DAL.Entities;
using ProjektSemestrIV.DAL.Repositories;
using ProjektSemestrIV.Models.ShowModels;
using System.Collections.Generic;
using System.Linq;

namespace ProjektSemestrIV.Models.ComplexModels
{
    class ShowSelectedStageModel
    {
        private readonly Stage stage;
        private readonly Competition competition;

        public ShowSelectedStageModel(uint id)
        {
            stage = StageRepository.GetStageByIdFromDB(id);
            competition = CompetitionRepository.GetCompetitionFromDB(stage.Competition_ID);
        }

        public string GetCompetitionLocation() => competition.Location;

        public string GetStageName() => stage.Name;

        public string GetStageRules() => stage.Rules;

        public uint GetNumOfTargets() => StageRepository.GetNumOfTargetsOnStageFromDB(stage.ID);

        public string GetShooterWithPoints()
        {
            var shooter = ShooterRepository.GetShooterWithPointsByStageIdFromDB(stage.ID);
            return $"{shooter.Name} {shooter.Surname} : {shooter.Points}pkt";
        }

        public double GetAverageTime() => StageRepository.GetAverageTimeOnStageByIdFromDB(stage.ID);

        public IEnumerable<ShooterWithStagePointsAndCompetitionPointsOverview> GetShooters()
            => ShooterRepository.GetShootersWithStagePointsAndCompetitionPointsByIdFromDB(stage.ID)
                                .Select(x => new ShooterWithStagePointsAndCompetitionPointsOverview(x.Id,
                                                                                                    x.Position,
                                                                                                    x.Name,
                                                                                                    x.Surname,
                                                                                                    x.StagePoints,
                                                                                                    x.CompetitionPoints));
    }
}