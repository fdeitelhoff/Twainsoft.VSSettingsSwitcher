using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Twainsoft.VSSettingsSwitcher.BLL.Contracts.Settings.Export;

namespace Twainsoft.VSSettingsSwitcher.BLL.Components.Settings.Export
{
    public class SettingsExportedMessage : ISettingsExportedMessage
    {
        public string FileName { get; private set; }
        public bool Success { get; private set; }

        public SettingsExportedMessage(string fileName, bool success)
        {
            FileName = fileName;
            Success = success;
        }
    }
}
