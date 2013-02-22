using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Twainsoft.VSSettingsSwitcher.BLL.Exceptions.Settings.Save
{
    public class SettingsEntryUniqueException : Exception
    {
        public SettingsEntryUniqueException()
            : base()
        { }

        public SettingsEntryUniqueException(string message)
            : base(message)
        { }
    }
}
