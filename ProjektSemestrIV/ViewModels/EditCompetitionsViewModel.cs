using ProjektSemestrIV.DAL.Entities;
using ProjektSemestrIV.DAL.Repositories;
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
    class EditCompetitionsViewModel : BaseViewModel {
        private CompetitionModel competitionModel;
        public EditCompetitionsViewModel() {
            competitionModel = new CompetitionModel();
            Competitions = competitionModel.GetAllCompetitions().Convert();
            SelectedCompetitionIndex = -1;
            EditedCompetitionIndex = -1;
        }

        private String location;
        public String Location {
            get { return location; }
            set {
                location = value;
                onPropertyChanged(nameof(Location));
            }
        }

        private String startDate;
        public String StartDate {
            get { return startDate; }
            set {
                startDate = value;
                onPropertyChanged(nameof(StartDate));
            }
        }

        private String endDate;
        public String EndDate {
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

        public Competition SelectedCompetition { get; set; }
        public Int32 SelectedCompetitionIndex { get; set; }
        public Int32 EditedCompetitionIndex { get; set; }


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
            => EditedCompetitionIndex == -1;
        private void ExecuteAddCompetition( object parameter ) {
            Competition competition = new Competition(Location, StartDate, EndDate);
            competitionModel.AddCompetition(competition);

            Location = "";
            StartDate = "";
            EndDate = "";
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
            => EditedCompetitionIndex != -1;
        private void ExecuteConfirmCompetitionEdit( object parameter ) {
            Competition newCompetition = new Competition(Location, StartDate, EndDate);
            UInt32 id = SelectedCompetition.Id;
            competitionModel.EditCompetition(newCompetition, id);

            Location = "";
            StartDate = "";
            EndDate = "";
            EditedCompetitionIndex = -1;
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
            => SelectedCompetitionIndex != -1;
        private void ExecuteEditCompetition( object parameter ) {
            Location = SelectedCompetition.Location;
            StartDate = SelectedCompetition.StartDate;
            EndDate = SelectedCompetition.EndDate;
            EditedCompetitionIndex = SelectedCompetitionIndex;
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
            => (SelectedCompetitionIndex != -1) && (SelectedCompetitionIndex != EditedCompetitionIndex);
        private void ExecuteDeleteCompetition( object parameter ) {
            UInt32 id = SelectedCompetition.Id;
            competitionModel.DeleteCompetition(id);
            Competitions = competitionModel.GetAllCompetitions().Convert();
        }
    }
}
