using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ProjektSemestrIV.Models.ShowModels
{
    class ShooterCompetitionOverview
    {
        public string Location { get; set; }

        public string StartDate { get; set; }

        public uint Position { get; set; }

        public double Points { get; set; }

        public ShooterCompetitionOverview(string location, string startDate, uint position, double points)
        {
            Location = location;
            StartDate = startDate;
            Position = position;
            Points = points;
        }
    }
}
