using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ProjektSemestrIV.DAL.Entities;
using ProjektSemestrIV.Models;

namespace ProjektSemestrIV.ViewModels {
    class EditShootersViewModel : BaseViewModel {
        private ShooterModel shooterModel;
        public EditShootersViewModel() {
            shooterModel = new ShooterModel();
            Shooters = shooterModel.GetAllShooters();
        }

        private String name = "";
        public String Name {
            get { return name; }
            set {
                name = value;
                onPropertyChanged(nameof(Name));
            }
        }

        private String surname = "";
        public String Surname {
            get { return surname; }
            set {
                surname = value;
                onPropertyChanged(nameof(Surname));
            }
        }

        private ObservableCollection<Shooter> shooters;
        public ObservableCollection<Shooter> Shooters {
            get { return shooters; }
            set {
                shooters = value;
                onPropertyChanged(nameof(Shooters));
            }
        }

        public Shooter SelectedShooter { get; set; } = null;
        public UInt32? EditedShooterId { get; set; } = null;


        private ICommand addShooter = null;
        public ICommand AddShooter {
            get {
                if(addShooter == null) {
                    addShooter = new RelayCommand(ExecuteAddShooter, CanExecuteAddShooter);
                }

                return addShooter;
            }
        }
        private Boolean CanExecuteAddShooter( object parameter )
            => !IsEditing() && InputIsValid();
        private void ExecuteAddShooter( object parameter ) {
            Shooter newShooter = new Shooter(Name, Surname);
            shooterModel.AddShooterToDatabase(newShooter);

            ClearInput();
            Shooters = shooterModel.GetAllShooters();
        }


        private ICommand confirmShooterEdit = null;
        public ICommand ConfirmShooterEdit {
            get {
                if(confirmShooterEdit == null) {
                    confirmShooterEdit = new RelayCommand(ExecuteConfirmShooterEdit, CanExecuteConfirmShooterEdit);
                }

                return confirmShooterEdit;
            }
        }
        private Boolean CanExecuteConfirmShooterEdit( object parameter )
            => IsEditing() && InputIsValid();
        private void ExecuteConfirmShooterEdit( object parameter ) {
            Shooter newShooter = new Shooter(Name, Surname);
            UInt32 id = SelectedShooter.ID;
            shooterModel.EditShooterInDatabase(newShooter, id);

            ClearInput();
            EditedShooterId = null;
            Shooters = shooterModel.GetAllShooters();
        }


        private ICommand editShooter = null;
        public ICommand EditShooter {
            get {
                if(editShooter == null) {
                    editShooter = new RelayCommand(ExecuteEditShooter, CanExecuteEditShooter);
                }

                return editShooter;
            }
        }
        private Boolean CanExecuteEditShooter( object parameter )
            => SelectedShooter != null;
        private void ExecuteEditShooter( object parameter ) {
            Name = SelectedShooter.Name;
            Surname = SelectedShooter.Surname;
            EditedShooterId = SelectedShooter.ID;
        }


        private ICommand deleteShooter = null;
        public ICommand DeleteShooter {
            get {
                if(deleteShooter == null) {
                    deleteShooter = new RelayCommand(ExecuteDeleteShooter, CanExecuteDeleteShooter);
                }

                return deleteShooter;
            }
        }
        private Boolean CanExecuteDeleteShooter( object parameter )
            => (SelectedShooter != null) && (SelectedShooter.ID != EditedShooterId);
        private void ExecuteDeleteShooter( object parameter ) {
            UInt32 id = SelectedShooter.ID;
            shooterModel.DeleteShooterFromDatabase(id);
            Shooters = shooterModel.GetAllShooters();
        }


        private void ClearInput() {
            Name = "";
            Surname = "";
        }

        private bool IsEditing()
            => EditedShooterId != null;

        private bool InputIsValid() {
            if(Name.Length == 0 || Surname.Length == 0) return false;
            if(Name.Length > 45 || Surname.Length > 45) return false;
            return true;
        }
    }
}
