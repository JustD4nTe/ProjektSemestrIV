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
            shooter = ShooterRepository.GetShooterByIdFromDB(shooterId);
            stage = StageRepository.GetStageByIdFromDB(stageId);
        }

        public string GetShooterName() => shooter.Name;

        public string GetShooterSurname() => shooter.Surname;

        public string GetShooterOnStageGeneralAccuracy()
            => String.Format("{0:P2}", ShooterRepository.GetShooterOnStageGeneralAccuracyFromDB(shooter.ID, stage.ID));

        public string GetShooterOnStageAlphaAccuracy()
            => String.Format("{0:P2}", ShooterRepository.GetShooterOnStageAlphaAccuracyFromDB(shooter.ID, stage.ID));

        public string GetShooterOnStageCharlieAccuracy()
            => String.Format("{0:P2}", ShooterRepository.GetShooterOnStageCharlieAccuracyFromDB(shooter.ID, stage.ID));

        public string GetShooterOnStageDeltaAccuracy()
            => String.Format("{0:P2}", ShooterRepository.GetShooterOnStageDeltaAccuracyFromDB(shooter.ID, stage.ID));

        public string GetShooterGeneralAveragePosition()
            => String.Format("{0:N2}", ShooterRepository.GetShooterGeneralAveragePositionFromDB(shooter.ID));

        public string GetShooterOnStageSumOfPoints()
            => String.Format("{0:N3}", ShooterRepository.GetShooterOnStageSumOfPointsFromDB(shooter.ID, stage.ID));

        public string GetShooterOnStageTime()
            => TimeSpan.FromSeconds(ShooterRepository.GetShooterOnStageTime(shooter.ID, stage.ID))
                       .ToString(@"hh\h\:mm\m\:ss\s\:fff\m\s");

        public string getShooterOnStageCompetition()
            => ShooterRepository.getShooterOnStageCompetition(shooter.ID, stage.ID);

        public string getShooterOnStageStageName()
            => stage.Name;

        public uint GetShooterOnStagePosition()
            => ShooterRepository.GetShooterOnStagePosition(shooter.ID, stage.ID);
    }
}