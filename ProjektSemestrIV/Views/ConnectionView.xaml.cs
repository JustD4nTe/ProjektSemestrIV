using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProjektSemestrIV.Views {
    /// <summary>
    /// Interaction logic for ConnectionView.xaml
    /// </summary>
    public partial class ConnectionView : UserControl {
        public ConnectionView() {
            InitializeComponent();
        }

        public static readonly DependencyProperty ServerAddressProperty = DependencyProperty.Register(
            "ServerAddress",
            typeof(String),
            typeof(ConnectionView),
            new FrameworkPropertyMetadata(null)
        );

        public String ServerAddress {
            get { return (String)GetValue(ServerAddressProperty); }
            set { SetValue(ServerAddressProperty, value); }
        }

        public static readonly DependencyProperty PortProperty = DependencyProperty.Register(
            "Port",
            typeof(UInt32),
            typeof(ConnectionView),
            new FrameworkPropertyMetadata(null)
        );

        public UInt32 Port {
            get { return (UInt32)GetValue(PortProperty); }
            set { SetValue(PortProperty, value); }
        }

        public static readonly DependencyProperty DatabaseProperty = DependencyProperty.Register(
            "Database",
            typeof(String),
            typeof(ConnectionView),
            new FrameworkPropertyMetadata(null)
        );

        public String Database {
            get { return (String)GetValue(DatabaseProperty); }
            set { SetValue(DatabaseProperty, value); }
        }

        public static readonly DependencyProperty UserProperty = DependencyProperty.Register(
            "User",
            typeof(String),
            typeof(ConnectionView),
            new FrameworkPropertyMetadata(null)
        );

        public String User {
            get { return (String)GetValue(UserProperty); }
            set { SetValue(UserProperty, value); }
        }

        public static readonly DependencyProperty PasswordProperty = DependencyProperty.Register(
            "Password",
            typeof(String),
            typeof(ConnectionView),
            new FrameworkPropertyMetadata(null)
        );

        public String Password {
            get { return (String)GetValue(PasswordProperty); }
            set { SetValue(PasswordProperty, value); }
        }

        public static readonly DependencyProperty OKProperty = DependencyProperty.Register(
            "OK",
            typeof(ICommand),
            typeof(ConnectionView),
            new FrameworkPropertyMetadata(null)
        );

        public ICommand OK {
            get { return (ICommand)GetValue(OKProperty); }
            set { SetValue(OKProperty, value); }
        }
    }
}
