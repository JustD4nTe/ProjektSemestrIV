using ProjektSemestrIV.Events;
using System;

namespace ProjektSemestrIV.ViewModels.BaseClass
{
    interface ISwitchViewModel
    {
        EventHandler<SwitchViewEventArgs> SwitchView { get; set; }
        IBaseViewModel GetViewModel(params uint[] id);
    }
}
