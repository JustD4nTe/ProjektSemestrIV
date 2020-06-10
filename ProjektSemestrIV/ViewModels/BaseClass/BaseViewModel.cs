using System;
using System.ComponentModel;

namespace ProjektSemestrIV.ViewModels
{
    abstract class BaseViewModel : INotifyPropertyChanged
    {
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
