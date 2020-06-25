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
            SelectedIndex = -1;
            EditedItemIndex = -1;
        }

        private String name;
        public String Name {
            get { return name; }
            set {
                name = value;
                onPropertyChanged(nameof(Name));
            }
        }

        private String surname;
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

        public Shooter SelectedItem { get; set; }
        public Int32 SelectedIndex { get; set; }
        public Int32 EditedItemIndex { get; set; }


        private ICommand addShooter = null;
        public ICommand AddShooter {
            get {
                if(addShooter == null) {
                    addShooter = new RelayCommand(ExecuteAddShooter, CanExecuteAddShooter);
                }

                return addShooter;
            }
        }
        private Boolean CanExecuteAddShooter( object parameter ) {
            if(EditedItemIndex != -1) return false;
            if(Name == "" || Name == null) return false;
            if(Surname == "" || Surname == null) return false;
            return true;
        }
        private void ExecuteAddShooter( object parameter ) {
            Shooter newShooter = new Shooter(Name, Surname);
            shooterModel.AddShooterToDatabase(newShooter);

            Name = "";
            Surname = "";
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
        private Boolean CanExecuteConfirmShooterEdit( object parameter ) {
            if(EditedItemIndex == -1) return false;
            if(Name == "" || Name == null) return false;
            if(Surname == "" || Surname == null) return false;
            return true;
        }
        private void ExecuteConfirmShooterEdit( object parameter ) {
            Shooter newShooter = new Shooter(Name, Surname);
            UInt32 id = Shooters[SelectedIndex].ID;
            shooterModel.EditShooterInDatabase(newShooter, id);

            Name = "";
            Surname = "";
            EditedItemIndex = -1;
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
            => SelectedIndex != -1;
        private void ExecuteEditShooter( object parameter ) {
            Name = SelectedItem.Name;
            Surname = SelectedItem.Surname;
            EditedItemIndex = SelectedIndex;
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
            => (SelectedIndex != -1) && (SelectedIndex != EditedItemIndex);
        private void ExecuteDeleteShooter( object parameter ) {
            UInt32 id = SelectedItem.ID;
            shooterModel.DeleteShooterFromDatabase(id);
            Shooters = shooterModel.GetAllShooters();
        }
    }
}
