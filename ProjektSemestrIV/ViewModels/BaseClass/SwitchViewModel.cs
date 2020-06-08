using ProjektSemestrIV.Events;
using System;

namespace ProjektSemestrIV.ViewModels.BaseClass
{
    abstract class SwitchViewModel : BaseViewModel, ISwitchViewModel
    {
        public EventHandler<SwitchViewEventArgs> SwitchView { get; set; }

        public abstract IBaseViewModel GetViewModel(params uint[] id);
    }
}
