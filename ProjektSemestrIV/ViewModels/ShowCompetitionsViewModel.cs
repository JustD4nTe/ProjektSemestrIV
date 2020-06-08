using ProjektSemestrIV.DAL.Entities;
using ProjektSemestrIV.Extensions;
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

        public ObservableCollection<Competition> Competitions { get; }

        public ShowCompetitionsViewModel()
        {
            model = new CompetitionModel();
            Competitions = model.GetAllCompetitionsFromDB().Convert();
        }
    }
}
