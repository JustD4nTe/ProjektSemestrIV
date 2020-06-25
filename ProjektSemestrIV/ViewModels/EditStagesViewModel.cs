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
            Stages = stageModel.GetAllStages();
            Competitions = competitionModel.GetAllCompetitionsFromDB().Convert();
            SelectedStageIndex = -1;
            EditedStageIndex = -1;
        }

        private UInt32 competitionID;
        public UInt32 CompetitionID {
            get { return competitionID; }
            set {
                competitionID = value;
                onPropertyChanged(nameof(CompetitionID));
            }
        }

        private String name;
        public String Name {
            get { return name; }
            set {
                name = value;
                onPropertyChanged(nameof(Name));
            }
        }

        private String rules;
        public String Rules {
            get { return rules; }
            set {
                rules = value;
                onPropertyChanged(nameof(Rules));
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

        private ObservableCollection<Competition> competition;
        public ObservableCollection<Competition> Competitions {
            get { return competition; }
            set {
                competition = value;
                onPropertyChanged(nameof(Competition));
            }
        }

        public Stage SelectedStage { get; set; }
        public Int32 SelectedStageIndex { get; set; }
        public Int32 EditedStageIndex { get; set; }

        private Competition selectedCompetition;
        public Competition SelectedCompetition {
            get { return selectedCompetition; }
            set {
                selectedCompetition = value;
                if(selectedCompetition != null) {
                    CompetitionID = value.Id;
                }
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
        private Boolean CanExecuteAddStage( object parameter ) {
            if(EditedStageIndex != -1) return false;
            if(CompetitionID == 0) return false;
            if(Name == "" || Name == null) return false;
            return true;
        }
        private void ExecuteAddStage( object parameter ) {
            Stage newStage = new Stage(CompetitionID, Name, Rules);
            stageModel.AddStageToDatabase(newStage);

            Name = "";
            Rules = "";
            Stages = stageModel.GetAllStages();
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
        private Boolean CanExecuteConfirmStageEdit( object parameter ) {
            if(EditedStageIndex == -1) return false;
            if(CompetitionID == 0) return false;
            if(Name == "" || Name == null) return false;
            return true;
        }
        private void ExecuteConfirmStageEdit( object parameter ) {
            Stage newStage = new Stage(CompetitionID, Name, Rules);
            UInt32 id = SelectedStage.ID;
            stageModel.EditStageInDatabase(newStage, id);

            Name = "";
            Rules = "";
            EditedStageIndex = -1;
            Stages = stageModel.GetAllStages();
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
            => SelectedStageIndex != -1;
        private void ExecuteEditStege( object parameter ) {
            Name = SelectedStage.Name;
            Rules = SelectedStage.Rules;
            EditedStageIndex = SelectedStageIndex;
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
            => (SelectedStageIndex != -1) && (SelectedStageIndex != EditedStageIndex);
        private void ExecuteDeleteStage( object parameter ) {
            UInt32 id = SelectedStage.ID;
            stageModel.DeleteStageFromDatabase(id);
            Stages = stageModel.GetAllStages();
        }
    }
}
