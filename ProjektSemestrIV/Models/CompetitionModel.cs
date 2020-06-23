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
        public ObservableCollection<Competition> GetAllCompetitionsFromDB()
            => CompetitionRepository.GetAllCompetitionsFromDB().Convert();

        public Boolean AddCompetitionToDatabase( Competition competition )
            => CompetitionRepository.AddCompetitionToDatabase(competition);

        public Boolean DeleteCompetitionFromDatabase( UInt32 shooterID )
            => CompetitionRepository.DeleteCompetitionFromDatabase(shooterID);

        public Boolean EditCompetitionInDatabase( Competition competition, UInt32 id )
            => CompetitionRepository.EditCompetitionInDatabase(competition, id);
    }
}
