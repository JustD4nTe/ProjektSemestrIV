using ProjektSemestrIV.DAL.Entities;
using ProjektSemestrIV.DAL.Repositories;
using System;

namespace ProjektSemestrIV.Models
{
    class ShowCompetitionModel
    {
        private Competition competition;

        public ShowCompetitionModel(uint id)
        {
            competition = CompetitionRepository.GetCompetitionFromDB(id);
        }

        // return dd.mm.yyyy-dd.mm.yyyy
        public string GetDurationDate()
        => competition.StartDate.Substring(0, 10) + "-" + competition.EndDate.Substring(0, 10);

        public string GetLocation()
        => competition.Location;

        public uint GetShootersCount()
        => CompetitionRepository.GetNumberOfShootersInCompetition(competition.Id);

        public string GetFastestShooter()
        {
            var shooter = CompetitionRepository.GetFastestShooterOfCompetition(competition.Id);
            return shooter.Name + " " + shooter.Surname + ": " 
                    + TimeSpan.FromSeconds(shooter.TimeInSeconds)
                              .ToString(@"hh\h\:mm\m\:ss\s\:fff\m\s");
        }
    }
}
