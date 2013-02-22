using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Twainsoft.VSSettingsSwitcher.BLL.Contracts.Settings.Export
{
    public interface ISettingsExportedMessage
    {
        string FileName { get; }

        bool Success { get; }
    }
}
