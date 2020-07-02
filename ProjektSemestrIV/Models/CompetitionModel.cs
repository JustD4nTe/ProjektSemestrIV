using ProjektSemestrIV.DAL.Entities;
using ProjektSemestrIV.DAL.Repositories;
using ProjektSemestrIV.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ProjektSemestrIV.Models
{
    class CompetitionModel
    {
        public IEnumerable<Competition> GetAllCompetitions()
            => CompetitionRepository.GetAllCompetitions();

        public Boolean AddCompetition( Competition competition )
            => CompetitionRepository.AddCompetition(competition);

        public Boolean DeleteCompetition( UInt32 shooterID )
            => CompetitionRepository.DeleteCompetition(shooterID);

        public Boolean EditCompetition( Competition competition, UInt32 id )
            => CompetitionRepository.EditCompetition(competition, id);
    }
}
