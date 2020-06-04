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
    class ShowCompetitionsViewModel : BaseViewModel
    {
        private CompetitionModel model;

        private ObservableCollection<Competition> competitions;
        public ObservableCollection<Competition> Competitions
        {
            get { return competitions; }
            private set { competitions = value; onPropertyChanged(nameof(Competitions)); }
        }

        public ShowCompetitionsViewModel()
        {
            model = new CompetitionModel();
            Competitions = new ObservableCollection<Competition>(model.GetAllCompetitionsFromDB());
        }
    }
}
