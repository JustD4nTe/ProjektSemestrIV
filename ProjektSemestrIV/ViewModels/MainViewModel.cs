using ProjektSemestrIV.Commands;
using ProjektSemestrIV.Models;
using System;
using System.Windows.Input;

namespace ProjektSemestrIV.ViewModels {
    class MainViewModel : BaseViewModel {
		public MainModel MainModel { get; private set; } = new MainModel();

		private BaseViewModel selectedViewModel;
		public BaseViewModel SelectedViewModel {
			get { return selectedViewModel; }
			set {
				selectedViewModel = value;
				onPropertyChanged(nameof(SelectedViewModel));
			}
		}

		public MainViewModel() {
			UpdateFormView = new UpdateFormViewCommand(this);
		}

		public ICommand UpdateFormView { get; set; }
	}
}
