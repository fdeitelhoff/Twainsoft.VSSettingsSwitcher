using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Twainsoft.VSSettingsSwitcher.BLL.Contracts.Settings.Activate
{
    public interface ISettingsActivatedMessage
    {
        string FileName { get; }

        bool Success { get; }

        bool Found { get; }
    }
}
