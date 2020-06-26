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
        private Competition competition;

        public ShowCompetitionModel(uint id)
        {
            competition = CompetitionRepository.GetCompetition(id);
        }

        // return dd.mm.yyyy-dd.mm.yyyy
        public string GetDurationDate()
        => competition.StartDate.Substring(0, 10) + "-" + competition.EndDate.Substring(0, 10);

        public string GetLocation()
        => competition.Location;

        public uint GetShootersCount()
        => CompetitionRepository.GetShootersCount(competition.Id);

        public string GetFastestShooter()
        {
            var shooter = CompetitionRepository.GetFastestShooter(competition.Id);
            return shooter.Name + " " + shooter.Surname + ": "
                                + TimeSpan.FromSeconds(shooter.TimeInSeconds)
                                          .ToString(@"hh\h\:mm\m\:ss\s\:fff\m\s");
        }

        public string GetPodium()
        {
            var playersOnPodium = CompetitionRepository.GetShootersWithPoints(competition.Id, true).ToList();

            var podium = $"I - {playersOnPodium[0].Name} {playersOnPodium[0].Surname}: {playersOnPodium[0].Points}\n"
                            + $"II - {playersOnPodium[1].Name} {playersOnPodium[1].Surname}: {playersOnPodium[1].Points}\n"
                            + $"III - {playersOnPodium[2].Name} {playersOnPodium[2].Surname}: {playersOnPodium[2].Points}";
            return podium;
        }

        public IEnumerable<ShooterWithPointsOverview> GetShootersWithPointsOnStage()
        => CompetitionRepository.GetShootersWithPoints(competition.Id)
                                .Select(x => new ShooterWithPointsOverview(x.Id, x.Name, x.Surname, x.Points));

        public IEnumerable<StageWithBestShooterOverview> GetStagesWithBestShooters()
        => CompetitionRepository.GetStagesWithBestShooter(competition.Id)
                                .Select(x => new StageWithBestShooterOverview(x.Id,
                                                                            x.StageName,
                                                                            x.ShooterName,
                                                                            x.ShooterSurname,
                                                                            x.ShooterPoints));
    }
}
