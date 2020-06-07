using ProjektSemestrIV.Events;
using System;
using System.ComponentModel;

namespace ProjektSemestrIV.ViewModels
{
    class BaseViewModel : INotifyPropertyChanged
    {
        public EventHandler<SwitchViewEventArgs> SwitchView;

        public event PropertyChangedEventHandler PropertyChanged;

        protected void onPropertyChanged(params String[] namesOfProperties)
        {
            if (PropertyChanged != null)
            {
                foreach (String property in namesOfProperties)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(property));
                }
            }
        }
    }
}
