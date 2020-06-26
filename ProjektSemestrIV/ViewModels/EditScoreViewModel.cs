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
using ProjektSemestrIV.Models;

namespace ProjektSemestrIV.ViewModels {
    class EditScoreViewModel : BaseViewModel {
        private TargetModel targetModel;
        private RunModel runModel;
        public EditScoreViewModel() {
            targetModel = new TargetModel();
            runModel = new RunModel();
            EditedTargetIndex = -1;
            ShooterIdIsEnabled = true;
            StageIdIsEnabled = true;
        }

        private UInt32 target_id;
        public UInt32 Target_id {
            get { return target_id; }
            set {
                target_id = value;
                onPropertyChanged(nameof(Target_id));
            }
        }

        private UInt32 shooter_id;
        public UInt32 Shooter_id {
            get { return shooter_id; }
            set {
                shooter_id = value;
                onPropertyChanged(nameof(Shooter_id));
                Targets = targetModel.GetTargets(Shooter_id, Stage_id);
                if(runModel.GetRun(Shooter_id, Stage_id) != null) {
                    Time = runModel.GetRun(Shooter_id, Stage_id).RunTime;
                }
            }
        }

        private UInt32 stage_id;
        public UInt32 Stage_id {
            get { return stage_id; }
            set {
                stage_id = value;
                onPropertyChanged(nameof(Stage_id));
                Targets = targetModel.GetTargets(Shooter_id, Stage_id);
                if(runModel.GetRun(Shooter_id, Stage_id) != null) {
                    Time = runModel.GetRun(Shooter_id, Stage_id).RunTime;
                }
            }
        }

        private Byte alpha;
        public Byte Alpha {
            get { return alpha; }
            set {
                alpha = value;
                onPropertyChanged(nameof(Alpha));
            }
        }

        private Byte charlie;
        public Byte Charlie {
            get { return charlie; }
            set {
                charlie = value;
                onPropertyChanged(nameof(Charlie));
            }
        }

        private Byte delta;
        public Byte Delta {
            get { return delta; }
            set {
                delta = value;
                onPropertyChanged(nameof(Delta));
            }
        }

        private Byte miss;
        public Byte Miss {
            get { return miss; }
            set {
                miss = value;
                onPropertyChanged(nameof(Miss));
            }
        }

        private Byte noShoot;
        public Byte NoShoot {
            get { return noShoot; }
            set {
                noShoot = value;
                onPropertyChanged(nameof(NoShoot));
            }
        }

        private Byte procedure;
        public Byte Procedure {
            get { return procedure; }
            set {
                procedure = value;
                onPropertyChanged(nameof(Procedure));
            }
        }

        private Byte extra;
        public Byte Extra {
            get { return extra; }
            set {
                extra = value;
                onPropertyChanged(nameof(Extra));
            }
        }

        private String time;
        public String Time {
            get { return time; }
            set {
                time = value;
                onPropertyChanged(nameof(Time));
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

        public Target SelectedTarget { get; set; }
        public Int32 SelectedTargetIndex { get; set; }
        public Int32 EditedTargetIndex { get; set; }

        private Boolean shooterIdIsEnabled;
        public Boolean ShooterIdIsEnabled {
            get { return shooterIdIsEnabled; }
            set {
                shooterIdIsEnabled = value;
                onPropertyChanged(nameof(ShooterIdIsEnabled));
            }
        }

        private Boolean stageIdIsEnabled;
        public Boolean StageIdIsEnabled {
            get { return stageIdIsEnabled; }
            set {
                stageIdIsEnabled = value;
                onPropertyChanged(nameof(StageIdIsEnabled));
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
            => EditedTargetIndex == -1;
        private void ExecuteAddTarget( object parameter ) {
            Target newTarget = new Target(shooter_id, stage_id, alpha, charlie, delta, miss, noShoot, procedure, extra);
            targetModel.AddTarget(newTarget);

            Alpha = 0;
            Charlie = 0;
            Delta = 0;
            Miss = 0;
            NoShoot = 0;
            Procedure = 0;
            Extra = 0;
            Targets = targetModel.GetTargets(Shooter_id, Stage_id);
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
            => EditedTargetIndex != -1;
        private void ExecuteConfirmTargetEdit( object parameter ) {
            Target newTarget = new Target(shooter_id, stage_id, alpha, charlie, delta, miss, noShoot, procedure, extra);
            UInt32 id = SelectedTarget.ID;
            targetModel.EditTarget(newTarget, id);

            Alpha = 0;
            Charlie = 0;
            Delta = 0;
            Miss = 0;
            NoShoot = 0;
            Procedure = 0;
            Extra = 0;
            ShooterIdIsEnabled = true;
            StageIdIsEnabled = true;
            EditedTargetIndex = -1;
            Targets = targetModel.GetTargets(Shooter_id, Stage_id);
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
            => SelectedTargetIndex != -1;
        private void ExecuteEditTarget( object parameter ) {
            Alpha = SelectedTarget.Alpha;
            Charlie = SelectedTarget.Charlie;
            Delta = SelectedTarget.Delta;
            Miss = SelectedTarget.Miss;
            NoShoot = SelectedTarget.NoShoot;
            Procedure = SelectedTarget.Procedure;
            Extra = SelectedTarget.Extra;

            ShooterIdIsEnabled = false;
            StageIdIsEnabled = false;

            EditedTargetIndex = SelectedTargetIndex;
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
            => (SelectedTargetIndex != -1) && (SelectedTargetIndex != EditedTargetIndex);
        private void ExecuteDeleteTarget( object parameter ) {
            UInt32 id = SelectedTarget.ID;
            targetModel.DeleteTarget(id);
            Targets = targetModel.GetTargets(Shooter_id, Stage_id);
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
            => (Shooter_id > 0) && (Stage_id > 0);
        private void ExecuteSaveRun( object parameter ) {
            if(runModel.GetRun(Shooter_id,Stage_id) != null) {
                Run newRun = new Run(Time, Shooter_id, Stage_id);
                runModel.EditRun(newRun, Shooter_id, Stage_id);
            }
            else {
                Run newRun = new Run(Time, Shooter_id, Stage_id);
                runModel.AddRun(newRun);
            }
        }
    }
}
