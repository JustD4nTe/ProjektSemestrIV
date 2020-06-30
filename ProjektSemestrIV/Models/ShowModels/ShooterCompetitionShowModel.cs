namespace ProjektSemestrIV.Models.ShowModels
{
    class ShooterCompetitionShowModel
    {
        public uint CompetitionId { get; }
        public string Location { get;  }
        public string StartDate { get;  }
        public uint Position { get;  }
        public double Points { get; }

        public ShooterCompetitionShowModel(uint competitionId, string location, string startDate, uint position, double points)
        {
            CompetitionId = competitionId;
            Location = location;
            StartDate = startDate;
            Position = position;
            Points = points;
        }
    }
}