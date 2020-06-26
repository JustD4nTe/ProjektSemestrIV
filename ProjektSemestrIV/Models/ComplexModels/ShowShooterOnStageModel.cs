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
            shooter = ShooterRepository.GetShooterByIdFromDB(shooterId);
            stage = StageRepository.GetStageByIdFromDB(stageId);
        }

        public string GetShooterName() => shooter.Name;

        public string GetShooterSurname() => shooter.Surname;

        public string GetShooterOnStageGeneralAccuracy()
            => $"{ShooterRepository.GetAccuracy(AccuracyTypeEnum.General,shooter.ID, stage.ID):P2}";

        public string GetShooterOnStageAlphaAccuracy()
            => $"{ShooterRepository.GetAccuracy(AccuracyTypeEnum.Alpha, shooter.ID, stage.ID):P2}";

        public string GetShooterOnStageCharlieAccuracy()
            => $"{ShooterRepository.GetAccuracy(AccuracyTypeEnum.Charlie, shooter.ID, stage.ID):P2}";

        public string GetShooterOnStageDeltaAccuracy()
            => $"{ShooterRepository.GetAccuracy(AccuracyTypeEnum.Delta, shooter.ID, stage.ID):P2}";

        public string GetShooterGeneralAveragePosition()
            => $"{ShooterRepository.GetShooterGeneralAveragePositionFromDB(shooter.ID):N2}";

        public string GetShooterOnStageSumOfPoints()
            => $"{ShooterRepository.GetShooterOnStageSumOfPointsFromDB(shooter.ID, stage.ID):N3}";

        public string GetShooterOnStageTime()
            => $"{TimeSpan.FromSeconds(ShooterRepository.GetShooterOnStageTime(shooter.ID, stage.ID)):g}";

        public string GetShooterOnStageCompetition()
            => ShooterRepository.GetShooterOnStageCompetition(shooter.ID, stage.ID);

        public string GetShooterOnStageStageName()
            => stage.Name;

        public uint GetShooterOnStagePosition()
            => ShooterRepository.GetShooterOnStagePosition(shooter.ID, stage.ID);
    }
}