using ProjektSemestrIV.DAL.Entities;
using ProjektSemestrIV.Events;
using ProjektSemestrIV.Extensions;
using ProjektSemestrIV.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ProjektSemestrIV.ViewModels
{
    class ShowCompetitionsViewModel : BaseViewModel
    {
        private CompetitionModel model;

        public ObservableCollection<Competition> Competitions { get; }
        public Competition SelectedCompetitionId { get; set; }

        public ICommand SwitchViewCommand { get; }

        public ShowCompetitionsViewModel()
        {
            model = new CompetitionModel();
            Competitions = model.GetAllCompetitionsFromDB().Convert();

            SwitchViewCommand = new RelayCommand(x => OnSwitchView(), x => true);
        }

        private void OnSwitchView()
        => SwitchView(this, new SwitchViewEventArgs(
                                    ViewTypeEnum.ShowSelectedCompetition,
                                    SelectedCompetitionId.Id));
    }
}
