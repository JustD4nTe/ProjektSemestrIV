using ProjektSemestrIV.DAL.Entities;
using ProjektSemestrIV.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektSemestrIV.Models.ComplexModels
{
    class ShowSelectedStageModel
    {
        private Stage stage;

        public ShowSelectedStageModel(uint id)
        {
            stage = StageRepository.GetStageByIdFromDB(id);
        }
    }
}
