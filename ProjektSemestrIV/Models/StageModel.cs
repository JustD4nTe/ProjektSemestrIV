using ProjektSemestrIV.DAL.Entities;
using ProjektSemestrIV.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace ProjektSemestrIV.Models {
    class StageModel {
        public ObservableCollection<Stage> GetAllStages() {
            List<Stage> stages = StageRepository.GetAllStages();
            return new ObservableCollection<Stage>(stages);
        }

        public Boolean AddStageToDatabase( Stage stage )
            => StageRepository.AddStageToDatabase(stage);

        public Boolean EditStageInDatabase( Stage stage, UInt32 id )
            => StageRepository.EditStageInDatabase(stage, id);

        public Boolean DeleteStageFromDatabase( UInt32 id )
            => StageRepository.DeleteStageFromDatabase(id);
    }
}
