using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Twainsoft.VSSettingsSwitcher.BLL.Components.Options.VSSettings.Manage
{
    public class AddNewSettingsEntryEventArgs : EventArgs
    {
        public string Name { get; set; }
        public string File { get; set; }

        public AddNewSettingsEntryEventArgs()
        { }

        public AddNewSettingsEntryEventArgs(string name, string file)
        {
            Name = name;
            File = file;
        }
    }
}
