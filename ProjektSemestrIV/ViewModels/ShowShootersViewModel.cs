using ProjektSemestrIV.DAL.Entities;
using ProjektSemestrIV.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektSemestrIV.ViewModels
{
    class ShowShootersViewModel : BaseViewModel
    {
        private ShooterModel model;

        private ObservableCollection<Shooter> shooters;
        public ObservableCollection<Shooter> Shooters
        {
            get { return shooters; }
            private set { shooters = value; onPropertyChanged(nameof(Shooters)); }
        }

        public ShowShootersViewModel()
        {
            model = new ShooterModel();
            Shooters = model.GetAllShooters();
        }
    }
}
