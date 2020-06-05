using ProjektSemestrIV.Models.ComplexModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektSemestrIV.ViewModels
{
    class ShowSelectedShooterViewModel
    {
        private ShowSelectedShooterModel model;

        public ShowSelectedShooterViewModel(UInt16 id)
        {
            model = new ShowSelectedShooterModel(id);

        }
    }
}
