namespace ProjektSemestrIV.Models.ShowModels
{
    class ShooterCompetitionOverview
    {
        public string Location { get;  }
        public string StartDate { get;  }
        public uint Position { get;  }
        public double Points { get; }

        public ShooterCompetitionOverview(string location, string startDate, uint position, double points)
        {
            Location = location;
            StartDate = startDate;
            Position = position;
            Points = points;
        }
    }
}