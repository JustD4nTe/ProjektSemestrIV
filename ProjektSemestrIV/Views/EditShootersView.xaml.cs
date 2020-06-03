using ProjektSemestrIV.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for AddShooterView.xaml
    /// </summary>
    public partial class EditShootersView : UserControl {
        public EditShootersView() {
            InitializeComponent();
        }

        public static readonly DependencyProperty ShooterNameProperty = DependencyProperty.Register(
            "ShooterName",
            typeof(String),
            typeof(EditShootersView),
            new FrameworkPropertyMetadata(null)
        );

        public String ShooterName {
            get { return (String)GetValue(ShooterNameProperty); }
            set { SetValue(ShooterNameProperty, value); }
        }

        public static readonly DependencyProperty ShooterSurnameProperty = DependencyProperty.Register(
            "ShooterSurname",
            typeof(String),
            typeof(EditShootersView),
            new FrameworkPropertyMetadata(null)
        );

        public String ShooterSurname {
            get { return (String)GetValue(ShooterSurnameProperty); }
            set { SetValue(ShooterSurnameProperty, value); }
        }

        public static readonly DependencyProperty AddProperty = DependencyProperty.Register(
            "Add",
            typeof(ICommand),
            typeof(EditShootersView),
            new FrameworkPropertyMetadata(null)
        );

        public ICommand Add {
            get { return (ICommand)GetValue(AddProperty); }
            set { SetValue(AddProperty, value); }
        }

        public static readonly DependencyProperty OKProperty = DependencyProperty.Register(
            "OK",
            typeof(ICommand),
            typeof(EditShootersView),
            new FrameworkPropertyMetadata(null)
        );

        public ICommand OK {
            get { return (ICommand)GetValue(OKProperty); }
            set { SetValue(OKProperty, value); }
        }

        public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register(
            "ItemsSource",
            typeof(ObservableCollection<Shooter>),
            typeof(EditShootersView),
            new FrameworkPropertyMetadata(null)
        );

        public ObservableCollection<Shooter> ItemsSource {
            get { return (ObservableCollection<Shooter>)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register(
            "SelectedItem",
            typeof(Shooter),
            typeof(EditShootersView),
            new FrameworkPropertyMetadata(null)
        );

        public Shooter SelectedItem {
            get { return (Shooter)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public static readonly DependencyProperty SelectedIndexProperty = DependencyProperty.Register(
            "SelectedIndex",
            typeof(Int32),
            typeof(EditShootersView),
            new FrameworkPropertyMetadata(null)
        );

        public Int32 SelectedIndex {
            get { return (Int32)GetValue(SelectedIndexProperty); }
            set { SetValue(SelectedIndexProperty, value); }
        }

        public static readonly DependencyProperty EditProperty = DependencyProperty.Register(
            "Edit",
            typeof(ICommand),
            typeof(EditShootersView),
            new FrameworkPropertyMetadata(null)
        );

        public ICommand Edit {
            get { return (ICommand)GetValue(EditProperty); }
            set { SetValue(EditProperty, value); }
        }

        public static readonly DependencyProperty DeleteProperty = DependencyProperty.Register(
            "Delete",
            typeof(ICommand),
            typeof(EditShootersView),
            new FrameworkPropertyMetadata(null)
        );

        public ICommand Delete {
            get { return (ICommand)GetValue(DeleteProperty); }
            set { SetValue(DeleteProperty, value); }
        }
    }
}
