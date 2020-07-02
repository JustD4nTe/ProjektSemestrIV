using ProjektSemestrIV.DAL.Entities;
using ProjektSemestrIV.Extensions;
using ProjektSemestrIV.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProjektSemestrIV.ViewModels {
    class EditStagesViewModel : BaseViewModel {
        private StageModel stageModel;
        private CompetitionModel competitionModel;

        public EditStagesViewModel() {
            stageModel = new StageModel();
            competitionModel = new CompetitionModel();
            Competitions = competitionModel.GetAllCompetitions().Convert();
        }

        private UInt32 competitionID;
        public UInt32 CompetitionID {
            get { return competitionID; }
            set {
                competitionID = value;
                onPropertyChanged(nameof(CompetitionID));
            }
        }

        private String name = "";
        public String Name {
            get { return name; }
            set {
                name = value;
                onPropertyChanged(nameof(Name));
            }
        }

        private String rules = "";
        public String Rules {
            get { return rules; }
            set {
                rules = value;
                onPropertyChanged(nameof(Rules));
            }
        }

        private ObservableCollection<Competition> competition;
        public ObservableCollection<Competition> Competitions {
            get { return competition; }
            set {
                competition = value;
                onPropertyChanged(nameof(Competition));
            }
        }

        private Competition selectedCompetition;
        public Competition SelectedCompetition {
            get { return selectedCompetition; }
            set {
                selectedCompetition = value;
                if(selectedCompetition != null) {
                    CompetitionID = value.Id;
                    Stages = stageModel.GetCompetitionStages(value.Id).Convert();
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

        public Stage SelectedStage { get; set; } = null;
        public UInt32? EditedStageId { get; set; } = null;

        private Boolean isCompetitionsEnabled = true;
        public Boolean IsCompetitionsEnabled {
            get { return isCompetitionsEnabled; }
            set {
                isCompetitionsEnabled = value;
                onPropertyChanged(nameof(IsCompetitionsEnabled));
            }
        }


        private ICommand addStage = null;
        public ICommand AddStage {
            get {
                if(addStage == null) {
                    addStage = new RelayCommand(ExecuteAddStage, CanExecuteAddStage);
                }

                return addStage;
            }
        }
        private Boolean CanExecuteAddStage( object parameter )
            => !IsEditing() && InputIsValid();
        private void ExecuteAddStage( object parameter ) {
            Stage newStage = new Stage(CompetitionID, Name, Rules);
            stageModel.AddStage(newStage);

            ClearInput();
            Stages = stageModel.GetCompetitionStages(SelectedCompetition.Id).Convert();
        }


        private ICommand confirmStageEdit = null;
        public ICommand ConfirmStageEdit {
            get {
                if(confirmStageEdit == null) {
                    confirmStageEdit = new RelayCommand(ExecuteConfirmStageEdit, CanExecuteConfirmStageEdit);
                }

                return confirmStageEdit;
            }
        }
        private Boolean CanExecuteConfirmStageEdit( object parameter )
            => IsEditing() && InputIsValid();
        private void ExecuteConfirmStageEdit( object parameter ) {
            Stage newStage = new Stage(CompetitionID, Name, Rules);
            UInt32 id = SelectedStage.ID;
            stageModel.EditStage(newStage, id);

            ClearInput();
            EditedStageId = null;
            IsCompetitionsEnabled = true;
            Stages = stageModel.GetCompetitionStages(SelectedCompetition.Id).Convert();
        }


        private ICommand editStage = null;
        public ICommand EditStage {
            get {
                if(editStage == null) {
                    editStage = new RelayCommand(ExecuteEditStege, CanExecuteEditStage);
                }

                return editStage;
            }
        }
        private Boolean CanExecuteEditStage( object parameter )
            => SelectedStage!= null;
        private void ExecuteEditStege( object parameter ) {
            Name = SelectedStage.Name;
            Rules = SelectedStage.Rules;
            EditedStageId = SelectedStage.ID;
            IsCompetitionsEnabled = false;
        }


        private ICommand deleteStage = null;
        public ICommand DeleteStage {
            get {
                if(deleteStage == null) {
                    deleteStage = new RelayCommand(ExecuteDeleteStage, CanExecuteDeleteStage);
                }

                return deleteStage;
            }
        }
        private Boolean CanExecuteDeleteStage( object parameter )
            => (SelectedStage != null) && (SelectedStage.ID != EditedStageId);
        private void ExecuteDeleteStage( object parameter ) {
            UInt32 id = SelectedStage.ID;
            stageModel.DeleteStage(id);
            Stages = stageModel.GetCompetitionStages(SelectedCompetition.Id).Convert();
        }


        private void ClearInput() {
            Name = "";
            Rules = "";
        }

        private bool IsEditing()
            => EditedStageId != null;

        private bool InputIsValid() {
            if(Name.Length == 0) return false;
            if(Name.Length > 45) return false;
            if(Rules.Length > 1<<16) return false;
            if(CompetitionID == 0) return false;
            return true;
        }
    }
}
