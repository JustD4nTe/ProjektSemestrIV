using ProjektSemestrIV.DAL.Entities;
using ProjektSemestrIV.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektSemestrIV.Models
{
    class CompetitionModel
    {
        public List<Competition> GetAllCompetitionsFromDB()
            => CompetitionsRepository.GetAllCompetitionsFromDB();

        public Boolean AddCompetitionToDatabase( Competition competition )
            => CompetitionsRepository.AddCompetitionToDatabase(competition);

        public Boolean DeleteCompetitionFromDatabase( UInt32 shooterID )
            => CompetitionsRepository.DeleteCompetitionFromDatabase(shooterID);

        public Boolean EditCompetitionInDatabase( Competition competition, UInt32 id )
            => CompetitionsRepository.EditCompetitionInDatabase(competition, id);
    }
}
