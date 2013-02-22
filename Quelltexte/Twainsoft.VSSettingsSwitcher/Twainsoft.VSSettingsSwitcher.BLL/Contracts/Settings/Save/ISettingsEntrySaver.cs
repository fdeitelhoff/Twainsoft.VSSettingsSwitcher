using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Twainsoft.VSSettingsSwitcher.BLL.Contracts.Settings.Save
{
    public interface ISettingsEntrySaver
    {
        void In_SaveSettingsEntry(string name, string file);

        event Action<ISettingsEntrySavedMessage> Out_SettingsEntrySaved;
    }
}
