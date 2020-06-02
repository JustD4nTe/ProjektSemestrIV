using ProjektSemestrIV.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProjektSemestrIV.Commands {
    class ConfirmDataCommand : ICommand {
        private ConnectionViewModel connectionViewModel;

        public ConfirmDataCommand( ConnectionViewModel connectionViewModel ) {
            this.connectionViewModel = connectionViewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute( object parameter ) {
            return true;
        }

        public void Execute( object parameter ) {
            Properties.Settings.Default.ServerAddress = connectionViewModel.ServerAddress;
            Properties.Settings.Default.Port = connectionViewModel.Port;
            Properties.Settings.Default.Database = connectionViewModel.Database;
            Properties.Settings.Default.User = connectionViewModel.User;
            Properties.Settings.Default.Password = connectionViewModel.Password;
            Properties.Settings.Default.Save();
        }
    }
}
