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

        public string Position { get; set; }

        public uint Points { get; set; }

        public ShooterCompetitionOverview(string location, string startDate, string position, uint points)
        {
            Location = location;
            StartDate = startDate;
            Position = position;
            Points = points;
        }
    }
}
