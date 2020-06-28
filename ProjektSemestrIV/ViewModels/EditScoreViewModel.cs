using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO.Packaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ProjektSemestrIV.DAL.Entities;
using ProjektSemestrIV.Extensions;
using ProjektSemestrIV.Models;

namespace ProjektSemestrIV.ViewModels {
    class EditScoreViewModel : BaseViewModel {
        private TargetModel targetModel;
        private RunModel runModel;
        private StageModel stageModel;
        private CompetitionModel competitionModel;
        private ShooterModel shooterModel;

        public EditScoreViewModel() {
            targetModel = new TargetModel();
            runModel = new RunModel();
            stageModel = new StageModel();
            competitionModel = new CompetitionModel();
            shooterModel = new ShooterModel();

            Competitions = competitionModel.GetAllCompetitionsFromDB().Convert();
            Shooters = shooterModel.GetAllShooters();
        }


        private ObservableCollection<Competition> competition;
        public ObservableCollection<Competition> Competitions {
            get { return competition; }
            set {
                competition = value;
                onPropertyChanged(nameof(Competitions));
            }
        }

        private Competition selectedCompetition = null;
        public Competition SelectedCompetition {
            get { return selectedCompetition; }
            set {
                selectedCompetition = value;
                if(value != null) {
                    Stages = stageModel.GetCompetitionStages(value.Id);
                }
                SelectedStage = null;
                onPropertyChanged(nameof(SelectedCompetition));
            }
        }


        private ObservableCollection<Stage> stages;
        public ObservableCollection<Stage> Stages {
            get { return stages; }
            set {
                stages = value;
                onPropertyChanged(nameof(Stages));
            }
        }

        private Stage selectedStage = null;
        public Stage SelectedStage {
            get { return selectedStage; }
            set {
                selectedStage = value;
                Stage_id = (value != null) ? value.ID : 0;
                onPropertyChanged(nameof(SelectedStage));
            }
        }


        private ObservableCollection<Target> targets;
        public ObservableCollection<Target> Targets {
            get { return targets; }
            set {
                targets = value;
                onPropertyChanged(nameof(Targets));
            }
        }

        public Target SelectedTarget { get; set; } = null;
        public UInt32? EditedTargetId { get; set; } = null;


        private ObservableCollection<Shooter> shooters;
        public ObservableCollection<Shooter> Shooters {
            get { return shooters; }
            set {
                shooters = value;
                onPropertyChanged(nameof(Shooters));
            }
        }

        private Shooter selectedShooter = null;
        public Shooter SelectedShooter {
            get { return selectedShooter; }
            set {
                selectedShooter = value;
                Shooter_id = (value != null) ? value.ID : 0;
                onPropertyChanged(nameof(SelectedShooter));
            }
        }


        private UInt32 shooter_id = 0;
        public UInt32 Shooter_id {
            get { return shooter_id; }
            set {
                shooter_id = value;
                onPropertyChanged(nameof(Shooter_id));
                Targets = targetModel.GetTargetsWhere(Shooter_id, Stage_id);
                if(runModel.GetRunWhere(Shooter_id, Stage_id) != null) {
                    Time = runModel.GetRunWhere(Shooter_id, Stage_id).RunTime;
                }
            }
        }

        private UInt32 stage_id = 0;
        public UInt32 Stage_id {
            get { return stage_id; }
            set {
                stage_id = value;
                onPropertyChanged(nameof(Stage_id));
                Targets = targetModel.GetTargetsWhere(Shooter_id, Stage_id);
                if(runModel.GetRunWhere(Shooter_id, Stage_id) != null) {
                    Time = runModel.GetRunWhere(Shooter_id, Stage_id).RunTime;
                }
            }
        }

        private Boolean isDataGridEnabled = true;
        private Boolean IsDataGridEnabled {
            get { return isDataGridEnabled; }
            set {
                isDataGridEnabled = value;
                onPropertyChanged(nameof(IsDataGridEnabled));
            }
        }


        private String alpha = "";
        public String Alpha {
            get { return alpha; }
            set {
                alpha = value;
                onPropertyChanged(nameof(Alpha));
            }
        }

        private String charlie = "";
        public String Charlie {
            get { return charlie; }
            set {
                charlie = value;
                onPropertyChanged(nameof(Charlie));
            }
        }

        private String delta = "";
        public String Delta {
            get { return delta; }
            set {
                delta = value;
                onPropertyChanged(nameof(Delta));
            }
        }

        private String miss = "";
        public String Miss {
            get { return miss; }
            set {
                miss = value;
                onPropertyChanged(nameof(Miss));
            }
        }

        private String noShoot = "";
        public String NoShoot {
            get { return noShoot; }
            set {
                noShoot = value;
                onPropertyChanged(nameof(NoShoot));
            }
        }

        private String procedure = "";
        public String Procedure {
            get { return procedure; }
            set {
                procedure = value;
                onPropertyChanged(nameof(Procedure));
            }
        }

        private String extra = "";
        public String Extra {
            get { return extra; }
            set {
                extra = value;
                onPropertyChanged(nameof(Extra));
            }
        }

        private String time;
        public String Time {
            get {
                if(time != null) {
                    return time.Substring(0, 2) + time.Substring(3, 2) + time.Substring(6, 2) + time.Substring(9, 3);
                }
                return null;
            }
            set {
                time = value.Replace(',', '.');
                onPropertyChanged(nameof(Time));
            }
        }


        private ICommand addTarget = null;
        public ICommand AddTarget {
            get {
                if(addTarget == null) {
                    addTarget = new RelayCommand(ExecuteAddTarget, CanExecuteAddTarget);
                }

                return addTarget;
            }
        }
        private Boolean CanExecuteAddTarget( object parameter )
            => !IsEditing() && IsInputValid();
        private void ExecuteAddTarget( object parameter ) {
            Target newTarget = new Target(shooter_id, stage_id, Byte.Parse(alpha), Byte.Parse(charlie), Byte.Parse(delta), Byte.Parse(miss), Byte.Parse(noShoot), Byte.Parse(procedure), Byte.Parse(extra));
            targetModel.AddTargetToDatabase(newTarget);

            ClearInput();
            Targets = targetModel.GetTargetsWhere(Shooter_id, Stage_id);
        }


        private ICommand confirmTargetEdit = null;
        public ICommand ConfirmTargetEdit {
            get {
                if(confirmTargetEdit == null) {
                    confirmTargetEdit = new RelayCommand(ExecuteConfirmTargetEdit, CanExecuteConfirmTargetEdit);
                }

                return confirmTargetEdit;
            }
        }
        private Boolean CanExecuteConfirmTargetEdit( object parameter ) 
            => IsEditing() && IsInputValid();
        private void ExecuteConfirmTargetEdit( object parameter ) {
            Target newTarget = new Target(shooter_id, stage_id, Byte.Parse(alpha), Byte.Parse(charlie), Byte.Parse(delta), Byte.Parse(miss), Byte.Parse(noShoot), Byte.Parse(procedure), Byte.Parse(extra));
            UInt32 id = SelectedTarget.ID;
            targetModel.EditTargetInDatabase(newTarget, id);

            ClearInput();
            isDataGridEnabled = true;
            EditedTargetId = null;
            Targets = targetModel.GetTargetsWhere(Shooter_id, Stage_id);
        }


        private ICommand editTarget = null;
        public ICommand EditTarget {
            get {
                if(editTarget == null) {
                    editTarget = new RelayCommand(ExecuteEditTarget, CanExecuteEditTarget);
                }

                return editTarget;
            }
        }
        private Boolean CanExecuteEditTarget( object parameter )
            => SelectedTarget != null;
        private void ExecuteEditTarget( object parameter ) {
            Alpha = SelectedTarget.Alpha.ToString();
            Charlie = SelectedTarget.Charlie.ToString();
            Delta = SelectedTarget.Delta.ToString();
            Miss = SelectedTarget.Miss.ToString();
            NoShoot = SelectedTarget.NoShoot.ToString();
            Procedure = SelectedTarget.Procedure.ToString();
            Extra = SelectedTarget.Extra.ToString();

            isDataGridEnabled = false;
            EditedTargetId = SelectedTarget.ID;
        }


        private ICommand deleteTarget = null;
        public ICommand DeleteTarget {
            get {
                if(deleteTarget == null) {
                    deleteTarget = new RelayCommand(ExecuteDeleteTarget, CanExecuteDeleteTarget);
                }

                return deleteTarget;
            }
        }
        private Boolean CanExecuteDeleteTarget( object parameter )
            => (SelectedTarget != null) && (SelectedTarget.ID != EditedTargetId);
        private void ExecuteDeleteTarget( object parameter ) {
            UInt32 id = SelectedTarget.ID;
            targetModel.DeleteTargetFromDatabase(id);
            Targets = targetModel.GetTargetsWhere(Shooter_id, Stage_id);
        }


        private ICommand saveRun = null;
        public ICommand SaveRun {
            get {
                if(saveRun == null) {
                    saveRun = new RelayCommand(ExecuteSaveRun, CanExecuteSaveRun);
                }

                return saveRun;
            }
        }
        private Boolean CanExecuteSaveRun( object parameter )
            => (Stage_id != 0) && (Shooter_id != 0) && TimeSpan.TryParse(time, out _);
        private void ExecuteSaveRun( object parameter ) {
            if(runModel.GetRunWhere(Shooter_id, Stage_id) != null) {
                Run newRun = new Run(time, Shooter_id, Stage_id);
                runModel.EditRunInDatabase(newRun, Shooter_id, Stage_id);
            }
            else {
                Run newRun = new Run(time, Shooter_id, Stage_id);
                runModel.AddRunToDatabase(newRun);
            }
        }


        private void ClearInput() {
            Alpha = "";
            Charlie = "";
            Delta = "";
            Miss = "";
            NoShoot = "";
            Procedure = "";
            Extra = "";
        }

        private bool IsEditing()
            => EditedTargetId != null;

        private bool IsInputValid() {
            if(!Byte.TryParse(Alpha, out _)) return false;
            if(!Byte.TryParse(Charlie, out _)) return false;
            if(!Byte.TryParse(Delta, out _)) return false;
            if(!Byte.TryParse(Miss, out _)) return false;
            if(!Byte.TryParse(NoShoot, out _)) return false;
            if(!Byte.TryParse(Procedure, out _)) return false;
            if(!Byte.TryParse(Extra, out _)) return false;
            return true;
        }
    }
}
