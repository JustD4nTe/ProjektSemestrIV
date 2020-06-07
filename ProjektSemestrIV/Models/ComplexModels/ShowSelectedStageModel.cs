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
        private Competition competition;

        public ShowSelectedStageModel(uint id)
        {
            stage = StageRepository.GetStageByIdFromDB(id);
            competition = CompetitionRepository.GetCompetitionFromDB(stage.Competition_ID);
        }

        public string GetCompetitionLocation() => competition.Location;

        public string GetStageName() => stage.Name;

        public string GetStageRules() => stage.Rules;

        public uint GetNumOfTargets() => StageRepository.GetNumOfTargetsOnStageFromDB(stage.ID);



    }
}
