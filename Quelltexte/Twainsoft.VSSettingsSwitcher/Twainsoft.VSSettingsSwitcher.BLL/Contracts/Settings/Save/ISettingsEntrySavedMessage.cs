using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Twainsoft.VSSettingsSwitcher.BLL.Contracts.Settings.Save
{
    public interface ISettingsEntrySavedMessage
    {
        string Name { get; set; }
        string File { get; set; }
    }
}
