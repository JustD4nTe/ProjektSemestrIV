using ProjektSemestrIV.DAL.Entities;
using ProjektSemestrIV.Events;
using ProjektSemestrIV.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ProjektSemestrIV.ViewModels
{
    class ShowShootersViewModel : BaseViewModel
    {
        private readonly ShooterModel model;

        public ObservableCollection<Shooter> Shooters { get; }
        public Shooter SelectedShooter { get; set; }
        public ICommand SwitchViewCommand { get; }

        public ShowShootersViewModel()
        {
            model = new ShooterModel();
            Shooters = model.GetAllShooters();

            SwitchViewCommand = new RelayCommand(x => OnSwitchView(), x => true);
        }

        private void OnSwitchView()
        => SwitchView(this, new SwitchViewEventArgs(
                                    ViewTypeEnum.ShowSelectedShooter,
                                    SelectedShooter.ID));
    }
}
