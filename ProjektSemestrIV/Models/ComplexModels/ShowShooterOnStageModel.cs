using ProjektSemestrIV.DAL.Entities;
using ProjektSemestrIV.DAL.Repositories;
using ProjektSemestrIV.Models.ShowModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjektSemestrIV.Models.ComplexModels {
    class ShowShooterOnStageModel
    {
        private readonly Shooter shooter;
        private readonly Stage stage;

        public ShowShooterOnStageModel(uint shooterId, uint stageId)
        {
            shooter = ShooterRepository.GetShooter(shooterId);
            stage = StageRepository.GetStage(stageId);
        }

        public string GetShooterName() => shooter.Name;

        public string GetShooterSurname() => shooter.Surname;

        public string GetGeneralAccuracy()
            => $"{ShooterRepository.GetAccuracy(AccuracyTypeEnum.General,shooter.ID, stage.ID):P2}";

        public string GetAlphaAccuracy()
            => $"{ShooterRepository.GetAccuracy(AccuracyTypeEnum.Alpha, shooter.ID, stage.ID):P2}";

        public string GetCharlieAccuracy()
            => $"{ShooterRepository.GetAccuracy(AccuracyTypeEnum.Charlie, shooter.ID, stage.ID):P2}";

        public string GetDeltaAccuracy()
            => $"{ShooterRepository.GetAccuracy(AccuracyTypeEnum.Delta, shooter.ID, stage.ID):P2}";

        public string GetAvgPosition()
            => $"{ShooterRepository.GetGeneralAvgPosition(shooter.ID):N2}";

        public string GetStagePoints()
            => $"{ShooterRepository.GetStagePoints(shooter.ID, stage.ID):N3}";

        public string GetStageTime()
            => $"{TimeSpan.FromSeconds(ShooterRepository.GetStageTime(shooter.ID, stage.ID)):g}";

        public string GetCompetitionName()
            => StageRepository.GetCompetitionName(stage.ID);

        public string GetStageName()
            => stage.Name;

        public uint GetPositionOnStage()
            => ShooterRepository.GetPositionOnStage(shooter.ID, stage.ID);
    }
}