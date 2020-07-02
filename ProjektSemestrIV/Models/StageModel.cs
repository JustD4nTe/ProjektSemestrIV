using ProjektSemestrIV.DAL.Entities;
using ProjektSemestrIV.DAL.Repositories;
using ProjektSemestrIV.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace ProjektSemestrIV.Models {
    class StageModel {
        public IEnumerable<Stage> GetAllStages()
            => StageRepository.GetAllStages();

        public Boolean AddStage( Stage stage )
            => StageRepository.AddStage(stage);
            
        public IEnumerable<Stage> GetCompetitionStages(uint competition_id)
            => StageRepository.GetCompetitionStages(competition_id);

        public Boolean EditStage( Stage stage, UInt32 id )
            => StageRepository.EditStage(stage, id);

        public Boolean DeleteStage( UInt32 id )
            => StageRepository.DeleteStage(id);
    }
}
