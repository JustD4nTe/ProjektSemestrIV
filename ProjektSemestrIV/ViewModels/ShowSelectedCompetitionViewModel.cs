using ProjektSemestrIV.Events;
using ProjektSemestrIV.Extensions;
using ProjektSemestrIV.Models;
using ProjektSemestrIV.Models.ComplexModels;
using ProjektSemestrIV.Models.ShowModels;
using ProjektSemestrIV.ViewModels.BaseClass;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ProjektSemestrIV.ViewModels
{
    class ShowSelectedCompetitionViewModel : SwitchViewModel
    {
        private readonly ShowSelectedCompetitionModel model;

        public string DurationDate { get; private set; }
        public string Location { get; private set; }
        public uint ShootersCount { get; private set; }
        public string FastestShooter { get; private set; }
        public string Podium { get; private set; }
        public ObservableCollection<StageWithBestPlayerOverview> Stages { get; private set; }
        public ObservableCollection<ShooterWithPointsOverview> Shooters { get; private set; }

        public StageWithBestPlayerOverview SelectedStage { get; set; }

        public ICommand SwitchViewCommand { get; }

        public ShowSelectedCompetitionViewModel()
        {
            model = new ShowSelectedCompetitionModel();
            SwitchViewCommand = new RelayCommand(x => OnSwitchView(), x => SelectedStage != null);
        }

        public override IBaseViewModel GetViewModel(params uint[] id)
        {
            model.SetNewId(id[0]);

            DurationDate = model.GetDurationDate();
            Location = model.GetLocation();
            ShootersCount = model.GetShootersCount();
            FastestShooter = model.GetFastestShooter();
            Podium = model.GetShootersOnPodium();

            Stages = model.GetStageWithBestShooters().Convert();
            Shooters = model.GetShootersFromCompetition().Convert();

            return this;
        }

        private void OnSwitchView()
        => SwitchView(this, new SwitchViewEventArgs(
                                    ViewTypeEnum.ShowSelectedStage,
                                    SelectedStage.Id));
    }
}
