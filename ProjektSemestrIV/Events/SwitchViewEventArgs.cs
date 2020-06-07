using ProjektSemestrIV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektSemestrIV.Events
{
    class SwitchViewEventArgs : EventArgs
    {
        public ViewTypeEnum NextView { get; }
        public uint NextViewId { get; }

        public SwitchViewEventArgs(ViewTypeEnum nextView, uint nextViewId)
        {
            NextView = nextView;
            NextViewId = nextViewId;
        }
    }
}
