using ProjektSemestrIV.DAL.Entities;
using ProjektSemestrIV.DAL.Repositories;
using ProjektSemestrIV.Models.ShowModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjektSemestrIV.Models.ComplexModels {
    class ShowShooterOnStageModel {
        private Shooter shooter;
        private Stage stage;


        public ShowShooterOnStageModel(uint shooterId, uint stageId)
        {
            shooter = ShooterRepository.GetShooter(shooterId);
            stage = StageRepository.GetStage(stageId);
        }

        public string GetShooterName() => shooter.Name;

        public string GetShooterSurname() => shooter.Surname;

        public string GetGeneralAccuracy()
            => String.Format("{0:P2}", ShooterRepository.GetAccuracy(AccuracyTypeEnum.General,shooter.ID, stage.ID));

        public string GetAlphaAccuracy()
            => String.Format("{0:P2}", ShooterRepository.GetAccuracy(AccuracyTypeEnum.Alpha, shooter.ID, stage.ID));

        public string GetCharlieAccuracy()
            => String.Format("{0:P2}", ShooterRepository.GetAccuracy(AccuracyTypeEnum.Charlie, shooter.ID, stage.ID));

        public string GetDeltaAccuracy()
            => String.Format("{0:P2}", ShooterRepository.GetAccuracy(AccuracyTypeEnum.Delta, shooter.ID, stage.ID));

        public string GetAvgPosition()
            => String.Format("{0:N2}", ShooterRepository.GetGeneralAvgPosition(shooter.ID));

        public string GetStagePoints()
            => String.Format("{0:N3}", ShooterRepository.GetStagePoints(shooter.ID, stage.ID));

        public string GetStageTime()
            => TimeSpan.FromSeconds(ShooterRepository.GetStageTime(shooter.ID, stage.ID))
                       .ToString(@"hh\h\:mm\m\:ss\s\:fff\m\s");

        public string GetShooterOnStageCompetition()
            => ShooterRepository.GetShooterOnStageCompetition(shooter.ID, stage.ID);

        public string GetStageName()
            => stage.Name;

        public uint GetPositionOnStage()
            => ShooterRepository.GetPositionOnStage(shooter.ID, stage.ID);
    }
}