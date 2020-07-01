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
        public ObservableCollection<Target> GetTargets(UInt32 shooter_id, UInt32 stage_id)
            => TargetRepository.GetTargets(shooter_id, stage_id).Convert();

        public Boolean AddTarget( Target target )
            => TargetRepository.AddTarget(target);

        public Boolean EditTarget( Target target, UInt32 target_id )
            => TargetRepository.EditTarget(target, target_id);

        public Boolean DeleteTarget( UInt32 id )
            => TargetRepository.DeleteTarget(id);
    }
}
