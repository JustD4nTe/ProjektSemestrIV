using ProjektSemestrIV.DAL.Entities;
using ProjektSemestrIV.Extensions;
using ProjektSemestrIV.Models;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ProjektSemestrIV.ViewModels {
    class EditCompetitionsViewModel : BaseViewModel {
        private CompetitionModel competitionModel;
        public EditCompetitionsViewModel() {
            competitionModel = new CompetitionModel();
            Competitions = competitionModel.GetAllCompetitions().Convert();
        }

        private String location = "";
        public String Location {
            get { return location; }
            set {
                location = value;
                onPropertyChanged(nameof(Location));
            }
        }

        private DateTime? startDate = null;
        public DateTime? StartDate {
            get { return startDate; }
            set {
                startDate = value;
                onPropertyChanged(nameof(StartDate));
            }
        }

        private DateTime? endDate = null;
        public DateTime? EndDate {
            get { return endDate; }
            set {
                endDate = value;
                onPropertyChanged(nameof(EndDate));
            }
        }

        private ObservableCollection<Competition> competitions;
        public ObservableCollection<Competition> Competitions {
            get { return competitions; }
            set {
                competitions = value;
                onPropertyChanged(nameof(Competitions));
            }
        }

        public Competition SelectedCompetition { get; set; } = null;
        public UInt32? EditedCompetitionId { get; set; } = null;


        private ICommand addCompetition = null;
        public ICommand AddCompetition {
            get {
                if(addCompetition == null) {
                    addCompetition = new RelayCommand(ExecuteAddCompetition, CanExecuteAddCompetition);
                }

                return addCompetition;
            }
        }
        private Boolean CanExecuteAddCompetition( object parameter )
            => !IsEditing() && InputIsValid();
        private void ExecuteAddCompetition( object parameter ) {
            Competition competition = new Competition(Location, StartDate.ToString(), EndDate.ToString());
            competitionModel.AddCompetition(competition);

            ClearInput();
            Competitions = competitionModel.GetAllCompetitions().Convert();
        }


        private ICommand confirmCompetitionEdit = null;
        public ICommand ConfirmCompetitionEdit {
            get {
                if(confirmCompetitionEdit == null) {
                    confirmCompetitionEdit = new RelayCommand(ExecuteConfirmCompetitionEdit, CanExecuteConfirmCompetitionEdit);
                }

                return confirmCompetitionEdit;
            }
        }
        private Boolean CanExecuteConfirmCompetitionEdit( object parameter ) 
            => IsEditing() && InputIsValid();
        private void ExecuteConfirmCompetitionEdit( object parameter ) {
            Competition newCompetition = new Competition(Location, StartDate.ToString(), EndDate.ToString());
            UInt32 id = SelectedCompetition.Id;
            competitionModel.EditCompetition(newCompetition, id);

            ClearInput();
            EditedCompetitionId = null;
            Competitions = competitionModel.GetAllCompetitions().Convert();
        }


        private ICommand editCompetition = null;
        public ICommand EditCompetition {
            get {
                if(editCompetition == null) {
                    editCompetition = new RelayCommand(ExecuteEditCompetition, CanExecuteEditCompetition);
                }

                return editCompetition;
            }
        }
        private Boolean CanExecuteEditCompetition( object parameter )
            => SelectedCompetition != null;
        private void ExecuteEditCompetition( object parameter ) {
            Location = SelectedCompetition.Location;
            StartDate = DateTime.Parse(SelectedCompetition.StartDate);
            EndDate = (SelectedCompetition.EndDate == "") ? (DateTime?)null : DateTime.Parse(SelectedCompetition.EndDate);
            EditedCompetitionId = SelectedCompetition.Id;
        }


        private ICommand deleteCompetition = null;
        public ICommand DeleteCompetition {
            get {
                if(deleteCompetition == null) {
                    deleteCompetition = new RelayCommand(ExecuteDeleteCompetition, CanExecuteDeleteCompetition);
                }

                return deleteCompetition;
            }
        }
        private Boolean CanExecuteDeleteCompetition( object parameter )
            => (SelectedCompetition != null) && (SelectedCompetition.Id != EditedCompetitionId);
        private void ExecuteDeleteCompetition( object parameter ) {
            UInt32 id = SelectedCompetition.Id;
            competitionModel.DeleteCompetition(id);
            Competitions = competitionModel.GetAllCompetitions().Convert();
        }


        private void ClearInput() {
            Location = "";
            StartDate = null;
            EndDate = null;
        }

        private bool IsEditing()
            => EditedCompetitionId != null;

        private bool InputIsValid() {
            if(Location.Length == 0) return false;
            if(Location.Length > 45) return false;
            if(StartDate == null) return false;
            return true;
        }
    }
}
