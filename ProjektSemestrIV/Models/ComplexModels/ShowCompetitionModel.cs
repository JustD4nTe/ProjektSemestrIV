using ProjektSemestrIV.DAL.Entities;
using ProjektSemestrIV.DAL.Repositories;
using ProjektSemestrIV.Models.ShowModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjektSemestrIV.Models.ComplexModels
{
    class ShowCompetitionModel
    {
        private readonly Competition competition;

        public ShowCompetitionModel(uint id)
        {
            competition = CompetitionRepository.GetCompetition(id);
        }

        /// <summary>
        /// Get Competition data containing: location, date of the start and optionally date of the end
        /// </summary>
        /// <returns>Formatted string</returns> 
        public string GetDurationDate()
        => $"{competition.Location} , {DateTime.Parse(competition.StartDate):g}{(DateTime.TryParse(competition.EndDate, out var result) ? $" - {result:g}" : "")}";


        public string GetLocation()
        => competition.Location;

        public uint GetShootersCount()
        => CompetitionRepository.GetShootersCount(competition.Id);

        public string GetFastestShooter()
        {
            var shooter = CompetitionRepository.GetFastestShooter(competition.Id);
            return $"{shooter.Name} {shooter.Surname} : {TimeSpan.FromSeconds(shooter.TimeInSeconds):g}";
        }

        public string GetPodium()
        {
            var playersOnPodium = CompetitionRepository.GetShootersWithPoints(competition.Id, true).ToList();

            var podium = $"I - {playersOnPodium[0].Name} {playersOnPodium[0].Surname}: {playersOnPodium[0].Points}\n"
                         + $"II - {playersOnPodium[1].Name} {playersOnPodium[1].Surname}: {playersOnPodium[1].Points}\n"
                         + $"III - {playersOnPodium[2].Name} {playersOnPodium[2].Surname}: {playersOnPodium[2].Points}";
            return podium;
        }

        public IEnumerable<ShooterWithPointsShowModel> GetShootersWithPointsOnStage()
        => CompetitionRepository.GetShootersWithPoints(competition.Id)
                                .Select(x => new ShooterWithPointsShowModel(x.Id, x.Name, x.Surname, x.Points));

        public IEnumerable<StageWithBestShooterShowModel> GetStagesWithBestShooters()
        => CompetitionRepository.GetStagesWithBestShooter(competition.Id)
                                .Select(x => new StageWithBestShooterShowModel(x.Id,
                                                                            x.StageName,
                                                                            x.ShooterName,
                                                                            x.ShooterSurname,
                                                                            x.ShooterPoints));
    }
}
