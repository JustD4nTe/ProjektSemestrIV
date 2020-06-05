namespace ProjektSemestrIV.DisplayModels
{
    class ShowCompetitionShooterModel
    {
        public string Name { get; set; }
        public string Surname{ get; set; }
        public double Points { get; set; }

        public ShowCompetitionShooterModel(string name, string surname, double points)
        {
            Name = name;
            Surname = surname;
            Points = points;
        }
    }
}
