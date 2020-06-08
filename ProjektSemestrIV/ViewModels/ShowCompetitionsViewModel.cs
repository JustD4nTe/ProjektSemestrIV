using ProjektSemestrIV.DAL.Entities;
using ProjektSemestrIV.Events;
using ProjektSemestrIV.Extensions;
using ProjektSemestrIV.Models;
using ProjektSemestrIV.ViewModels.BaseClass;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ProjektSemestrIV.ViewModels
{
    class ShowCompetitionsViewModel : SwitchViewModel
    {
        private readonly CompetitionModel model;

        public ObservableCollection<Competition> Competitions { get; private set; }
        public Competition SelectedCompetitionId { get; set; }

        public ICommand SwitchViewCommand { get; }

        public ShowCompetitionsViewModel()
        {
            model = new CompetitionModel();

            SwitchViewCommand = new RelayCommand(x => OnSwitchView(), x => true);
        }

        public override IBaseViewModel GetViewModel(params uint[] id)
        {
            Competitions = model.GetAllCompetitionsFromDB().Convert();

            return this;
        }

        private void OnSwitchView()
        => SwitchView(this, new SwitchViewEventArgs(
                                    ViewTypeEnum.ShowSelectedCompetition,
                                    SelectedCompetitionId.Id));
    }
}
