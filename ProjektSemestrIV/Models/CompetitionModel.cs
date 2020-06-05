using ProjektSemestrIV.DAL.Entities;
using ProjektSemestrIV.DAL.Repositories;
using System.Collections.Generic;

namespace ProjektSemestrIV.Models
{
    class CompetitionModel
    {
        public List<Competition> GetAllCompetitionsFromDB() => CompetitionRepository.GetAllCompetitionsFromDB();
    }
}
