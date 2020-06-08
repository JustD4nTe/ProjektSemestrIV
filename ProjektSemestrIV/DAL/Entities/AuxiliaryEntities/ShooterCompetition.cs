using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektSemestrIV.DAL.Entities.AuxiliaryEntities
{
    class ShooterCompetition
    {
        public string Location { get; set; }

        public string StartDate { get; set; }

        public uint Position { get; set; }

        public double Points { get; set; }

        public ShooterCompetition(string location, string startDate, uint position, double points)
        {
            Location = location;
            StartDate = startDate;
            Position = position;
            Points = points;
        }
    }
}
