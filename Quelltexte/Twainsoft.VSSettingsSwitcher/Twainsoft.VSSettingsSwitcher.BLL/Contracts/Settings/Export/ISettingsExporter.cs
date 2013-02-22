using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Twainsoft.VSSettingsSwitcher.BLL.Models.Settings;

namespace Twainsoft.VSSettingsSwitcher.BLL.Contracts.Settings.Export
{
    public interface ISettingsExporter
    {
        void In_ExportSettings(string fileName, SettingsCategoryViewModel settingsCategoryViewModel);

        event Action<ISettingsExportedMessage> Out_SettingsExported;
    }
}
