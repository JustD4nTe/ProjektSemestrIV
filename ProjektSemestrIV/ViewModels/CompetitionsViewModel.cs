using ProjektSemestrIV.DAL.Entities;
using ProjektSemestrIV.DAL.Repositories;
using ProjektSemestrIV.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektSemestrIV.ViewModels {
    class CompetitionsViewModel : BaseViewModel {

        private CompetitionModel model;    

        public ObservableCollection<Competition> Competitions { get; private set; }

        public CompetitionsViewModel()
        {
            model = new CompetitionModel();

            Competitions = new ObservableCollection<Competition>(model.GetAllCompetitionsFromDB());
        }
    }
}
