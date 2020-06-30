using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProjektSemestrIV.ViewModels {
    class ConnectionViewModel : BaseViewModel {
        public String ServerAddress { get; set; } = Properties.Settings.Default.ServerAddress;
        public String Port { get; set; } = Properties.Settings.Default.Port.ToString();
        public String Database { get; set; } = Properties.Settings.Default.Database;
        public String User { get; set; } = Properties.Settings.Default.User;
        public String Password { get; set; } = Properties.Settings.Default.Password;


        private ICommand confirmData = null;
        public ICommand ConfirmData {
            get {
                if(confirmData == null) {
                    confirmData = new RelayCommand(ExecuteConfirmData, CanExecuteConfirmData);
                }

                return confirmData;
            }
        }
        private Boolean CanExecuteConfirmData( object parameter )
            => uint.TryParse(Port, out _);
        public void ExecuteConfirmData( object parameter ) {
            Properties.Settings.Default.ServerAddress = ServerAddress;
            Properties.Settings.Default.Port = uint.Parse(Port);
            Properties.Settings.Default.Database = Database;
            Properties.Settings.Default.User = User;
            Properties.Settings.Default.Password = Password;
            Properties.Settings.Default.Save();
        }
    }
}
