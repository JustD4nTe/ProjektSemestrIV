using ProjektSemestrIV.DAL.Entities;
using ProjektSemestrIV.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektSemestrIV.ViewModels {
    class CompetitionsViewModel : BaseViewModel {
        public ObservableCollection<Competition> Competitions { get; set; } = null;

        public CompetitionsViewModel()
        {
            Competitions = new ObservableCollection<Competition>(CompetitionsRepository.GetAllCompetitions());
        }
    }
}
