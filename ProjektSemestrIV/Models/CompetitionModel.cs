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
        public List<Competition> GetAllCompetitionsFromDB() => CompetitionsRepository.GetAllCompetitionsFromDB();
    }
}
