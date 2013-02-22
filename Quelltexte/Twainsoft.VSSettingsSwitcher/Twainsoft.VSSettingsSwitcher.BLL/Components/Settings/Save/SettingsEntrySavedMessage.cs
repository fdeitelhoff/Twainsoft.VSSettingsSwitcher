using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Twainsoft.VSSettingsSwitcher.BLL.Contracts.Settings.Save;

namespace Twainsoft.VSSettingsSwitcher.BLL.Components.Settings.Save
{
    public class SettingsEntrySavedMessage : ISettingsEntrySavedMessage
    {
        public string Name { get; set; }
        public string File { get; set; }

        public SettingsEntrySavedMessage()
        { }

        public SettingsEntrySavedMessage(string name, string file)
        {
            Name = name;
            File = file;
        }
    }
}
