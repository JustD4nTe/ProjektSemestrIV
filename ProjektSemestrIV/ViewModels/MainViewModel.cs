using ProjektSemestrIV.Models;
using System.Windows.Input;
using System.Windows.Navigation;

namespace ProjektSemestrIV.ViewModels
{
    class MainViewModel : BaseViewModel 
    {
        private ICommand updateFormView = null;
        public ICommand UpdateFormView
        {
            get
            {
                if (updateFormView == null)
                {
                    updateFormView = new RelayCommand(ChooseGeneralView, null);
                }

                return updateFormView;
            }
        }

        private readonly NavigationService navigation;

        public MainViewModel(NavigationService _navigation)
        {
            navigation = _navigation;
        }

        public void ChooseGeneralView(object parameter)
        {
            switch ((ViewTypeEnum)parameter)
            {
                case ViewTypeEnum.Configuration:
                    navigation.Navigate(new ConnectionViewModel());
                    break;
                case ViewTypeEnum.EditCompetitions:
                    navigation.Navigate(new EditCompetitionsViewModel());
                    break;
                case ViewTypeEnum.EditShooters:
                    navigation.Navigate(new EditShootersViewModel());
                    break;
                case ViewTypeEnum.EditStages:
                    navigation.Navigate(new EditStagesViewModel());
                    break;
                case ViewTypeEnum.EditScore:
                    navigation.Navigate(new EditScoreViewModel());
                    break;
                case ViewTypeEnum.ShowCompetitions:
                    navigation.Navigate( new ShowCompetitionsViewModel(navigation));
                    break;
                case ViewTypeEnum.ShowShooters:
                    navigation.Navigate(new ShowShootersViewModel(navigation));
                    break;
                default:
                    break;
            }
        }
    }
}
