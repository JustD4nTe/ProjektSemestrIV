using ProjektSemestrIV.DAL.Entities;
using ProjektSemestrIV.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Navigation;

namespace ProjektSemestrIV.ViewModels
{
    class ShowShootersViewModel
    {
        private readonly ShooterModel model;
        private readonly NavigationService navigation;

        public ObservableCollection<Shooter> Shooters { get; }
        public Shooter SelectedShooter { get; set; }
        public ICommand SwitchViewCommand { get; }

        public ShowShootersViewModel(NavigationService navigation)
        {
            this.navigation = navigation;

            model = new ShooterModel();
            Shooters = model.GetAllShooters();

            SwitchViewCommand = new RelayCommand(x => OnSwitchView(), 
                                                 x => SelectedShooter != null);
        }

        private void OnSwitchView()
        => navigation.Navigate(new ShowSelectedShooterViewModel(navigation, SelectedShooter.ID));
    }
}
