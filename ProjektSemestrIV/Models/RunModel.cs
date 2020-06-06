using ProjektSemestrIV.DAL.Entities;
using ProjektSemestrIV.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektSemestrIV.Models {
    class RunModel {
        public Boolean AddRunToDatabase( Run run )
            => RunRepository.AddRunToDatabase(run);

        public Boolean DeleteRunFromDatabase( UInt32 runID )
            => RunRepository.DeleteRunFromDatabase(runID);

        public Boolean EditRunInDatabase( Run run, UInt32 shooter_id, UInt32 stage_id )
            => RunRepository.EditRunInDatabase(run, shooter_id, stage_id);

        public Run GetRunWhere( UInt32 shooter_id, UInt32 stage_id ) {
            Run run = RunRepository.GetRunWhere(shooter_id, stage_id);
            return run;
        }
    }
}
