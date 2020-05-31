using ProjektSemestrIV.ViewModels;
using System.Windows;

namespace ProjektSemestrIV {
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
            DataContext = new MainViewModel();
        }
    }
}
