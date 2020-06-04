using System.Collections.ObjectModel;

namespace ProjektSemestrIV.ViewModels
{
    class ShowCompetitionViewModel : BaseViewModel
    {
        private string durationDate;
        public string DurationDate
        {
            get { return durationDate; }
            set { durationDate = value; onPropertyChanged(nameof(DurationDate)); }
        }

        private string location;
        public string Location
        {
            get { return location; }
            set { location = value; onPropertyChanged(nameof(Location)); }
        }

        private uint shootersCount;
        public uint ShootersCount
        {
            get { return shootersCount; }
            set { shootersCount = value; onPropertyChanged(nameof(ShootersCount)); }
        }

        private string fastestTime;
        public string FastestTime
        {
            get { return fastestTime; }
            set { fastestTime = value; onPropertyChanged(nameof(FastestTime)); }
        }

        private string podium;
        public string Podium
        {
            get { return podium; }
            set { podium = value; onPropertyChanged(nameof(Podium)); }
        }

        private ObservableCollection<(string Name, string BestPlayer)> stages;
        public ObservableCollection<(string Name, string BestPlayer)> Stages
        {
            get { return stages; }
            set { stages = value; onPropertyChanged(nameof(Stages)); }
        }

        private ObservableCollection<(string Name, string Surname, uint Point)> shooters;
        public ObservableCollection<(string Name, string Surname, uint Point)> Shooters
        {
            get { return shooters; }
            set { shooters = value; onPropertyChanged(nameof(Shooters)); }
        }
    }
}
