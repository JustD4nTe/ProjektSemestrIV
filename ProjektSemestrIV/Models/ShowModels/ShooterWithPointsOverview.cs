namespace ProjektSemestrIV.Models.ShowModels
{
    class ShooterWithPointsOverview
    {
        public uint Id { get; }
        public string Name { get; }
        public string Surname { get; }
        public double Points { get; }

        public ShooterWithPointsOverview(uint id, string name, string surname, double points)
        {
            Id = id;
            Name = name;
            Surname = surname;
            Points = points;
        }
    }
}
