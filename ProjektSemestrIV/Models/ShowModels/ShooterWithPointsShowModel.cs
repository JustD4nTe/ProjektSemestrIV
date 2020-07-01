namespace ProjektSemestrIV.Models.ShowModels
{
    class ShooterWithPointsShowModel
    {
        public uint Id { get; }
        public string Name { get; }
        public string Surname { get; }
        public double Points { get; }

        public ShooterWithPointsShowModel(uint id, string name, string surname, double points)
        {
            Id = id;
            Name = name;
            Surname = surname;
            Points = points;
        }
    }
}
