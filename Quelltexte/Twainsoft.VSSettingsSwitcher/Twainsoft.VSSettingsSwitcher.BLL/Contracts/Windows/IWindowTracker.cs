using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Twainsoft.VSSettingsSwitcher.BLL.Options;

namespace Twainsoft.VSSettingsSwitcher.BLL.Contracts.Windows
{
    public interface IWindowTracker
    {
        GeneralOptionsStore GeneralOptionsStore { get; set; }

        void In_TrackWindows();
    }
}
