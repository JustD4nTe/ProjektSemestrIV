using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ProjektSemestrIV.Commands;
using ProjektSemestrIV.DAL.Entities;
using ProjektSemestrIV.Models;

namespace ProjektSemestrIV.ViewModels {
    class EditShootersViewModel : BaseViewModel {
        public MainModel MainModel { get; private set; }
        public EditShootersViewModel() {
            MainModel = new MainModel();
            Shooters = MainModel.GetAllShooters();
            AddShooter = new AddShooterCommand(this);
            ConfirmShooterEdit = new ConfirmShooterEditCommand(this);
            EditShooter = new EditShooterCommand(this);
            DeleteShooter = new DeleteShooterCommand(this);
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
        public ICommand AddShooter { get; set; }
        public ICommand ConfirmShooterEdit { get; set; }
        public ICommand EditShooter { get; set; }
        public ICommand DeleteShooter { get; set; }
    }
}
