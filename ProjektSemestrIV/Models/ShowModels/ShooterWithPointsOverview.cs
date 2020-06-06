namespace ProjektSemestrIV.Models.ShowModels
{
    class ShooterWithPointsOverview
    {
        public string Name { get; set; }
        public string Surname{ get; set; }
        public double Points { get; set; }

        public ShooterWithPointsOverview(string name, string surname, double points)
        {
            Name = name;
            Surname = surname;
            Points = points;
        }
    }
}
