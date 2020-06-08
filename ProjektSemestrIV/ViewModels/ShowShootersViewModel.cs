using ProjektSemestrIV.DAL.Entities;
using ProjektSemestrIV.Events;
using ProjektSemestrIV.Models;
using ProjektSemestrIV.ViewModels.BaseClass;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ProjektSemestrIV.ViewModels
{
    class ShowShootersViewModel : SwitchViewModel
    {
        private readonly ShooterModel model;

        public ObservableCollection<Shooter> Shooters { get; private set; }
        public Shooter SelectedShooter { get; set; }
        public ICommand SwitchViewCommand { get; }

        public ShowShootersViewModel()
        {
            model = new ShooterModel();            

            SwitchViewCommand = new RelayCommand(x => OnSwitchView(), x => true);
        }

        public override IBaseViewModel GetViewModel(params uint[] id)
        {
            Shooters = model.GetAllShooters();

            return this;
        }


        private void OnSwitchView()
        => SwitchView(this, new SwitchViewEventArgs(
                                    ViewTypeEnum.ShowSelectedShooter,
                                    SelectedShooter.ID));
    }
}
