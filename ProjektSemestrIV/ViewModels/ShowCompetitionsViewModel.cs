﻿using ProjektSemestrIV.DAL.Entities;
using ProjektSemestrIV.Extensions;
using ProjektSemestrIV.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Navigation;

namespace ProjektSemestrIV.ViewModels
{
    class ShowCompetitionsViewModel 
    {
        private readonly CompetitionModel model;
        private readonly NavigationService navigation;

        public ObservableCollection<Competition> Competitions { get; }
        public Competition SelectedCompetitionId { get; set; }

        public ICommand SwitchViewCommand { get; }


        public ShowCompetitionsViewModel(NavigationService navigation)
        {
            this.navigation = navigation;

            model = new CompetitionModel();

            Competitions = model.GetAllCompetitions().Convert();

            SwitchViewCommand = new RelayCommand(x => OnSwitchView(), x => true);
        }


        private void OnSwitchView()
        => navigation.Navigate(new ShowCompetitionViewModel(navigation, SelectedCompetitionId.Id));
    }
}
