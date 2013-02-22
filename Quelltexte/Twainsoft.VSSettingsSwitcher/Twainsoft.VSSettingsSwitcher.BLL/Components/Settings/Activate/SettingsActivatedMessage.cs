using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Twainsoft.VSSettingsSwitcher.BLL.Contracts.Settings.Activate;

namespace Twainsoft.VSSettingsSwitcher.BLL.Components.Settings.Activate
{
    public class SettingsActivatedMessage : ISettingsActivatedMessage
    {
        public string FileName { get; private set; }
        public bool Success { get; private set; }
        public bool Found { get; private set; }

        public SettingsActivatedMessage(string fileName, bool success, bool found)
        {
            FileName = fileName;
            Success = success;
            Found = found;
        }
    }
}
