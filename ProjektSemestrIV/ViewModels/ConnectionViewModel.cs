using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ProjektSemestrIV.Commands;

namespace ProjektSemestrIV.ViewModels {
    class ConnectionViewModel : BaseViewModel {
        public ConnectionViewModel() {
            ConfirmData = new ConfirmDataCommand(this);
        }
        public String ServerAddress { get; set; } = Properties.Settings.Default.ServerAddress;
        public UInt32 Port { get; set; } = Properties.Settings.Default.Port;
        public String Database { get; set; } = Properties.Settings.Default.Database;
        public String User { get; set; } = Properties.Settings.Default.User;
        public String Password { get; set; } = Properties.Settings.Default.Password;
        public ICommand ConfirmData { get; set; }
    }
}
