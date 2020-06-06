using ProjektSemestrIV.DAL.Entities;
using ProjektSemestrIV.DAL.Repositories;
using ProjektSemestrIV.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektSemestrIV.Models {
    class TargetModel {
        public ObservableCollection<Target> GetTargetsWhere(UInt32 shooter_id, UInt32 stage_id)
            => TargetRepository.GetTargetsWhere(shooter_id, stage_id).Convert();

        public Boolean AddTargetToDatabase( Target target )
            => TargetRepository.AddTargetToDatabase(target);

        public Boolean EditTargetInDatabase( Target target, UInt32 target_id )
            => TargetRepository.EditTargetInDatabase(target, target_id);

        public Boolean DeleteTargetFromDatabase( UInt32 id )
            => TargetRepository.DeleteTargetFromDatabase(id);
    }
}
