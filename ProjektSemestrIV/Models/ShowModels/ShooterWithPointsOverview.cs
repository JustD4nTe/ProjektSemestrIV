namespace ProjektSemestrIV.Models.ShowModels
{
    class ShooterWithPointsOverview
    {
        public string Name { get; }
        public string Surname { get; }
        public double Points { get; }

        public ShooterWithPointsOverview(string name, string surname, double points)
        {
            Name = name;
            Surname = surname;
            Points = points;
        }
    }
}
