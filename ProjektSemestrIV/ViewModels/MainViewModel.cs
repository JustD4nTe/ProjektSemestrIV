using ProjektSemestrIV.Models;
using System.Windows.Input;
using System.Windows.Navigation;

namespace ProjektSemestrIV.ViewModels
{
    class MainViewModel : BaseViewModel 
    {
        private readonly NavigationService navigation;

        public ICommand UpdateFormView { get; }
        public ICommand NavigateBackward { get; }
        public ICommand NavigateForward { get; }

        public MainViewModel(NavigationService navigation)
        {
            this.navigation = navigation;

            UpdateFormView = new RelayCommand(ChooseGeneralView, null);
            NavigateBackward = new RelayCommand(arg=>this.navigation.GoBack(), arg=>this.navigation.CanGoBack);
            NavigateForward = new RelayCommand(arg => this.navigation.GoForward(), arg => this.navigation.CanGoForward);
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
