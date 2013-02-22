using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Twainsoft.VSSettingsSwitcher.BLL.Contracts.Settings.Save;
using Twainsoft.VSSettingsSwitcher.DAL.Options.VSSettings.Manage;
using Twainsoft.VSSettingsSwitcher.BLL.Exceptions.Settings.Save;

namespace Twainsoft.VSSettingsSwitcher.BLL.Components.Settings.Save
{
    public class SettingsEntrySaver : ISettingsEntrySaver
    {
        private ManageDataSet ManageDataSet { get; set; }

        public SettingsEntrySaver(ManageDataSet manageDataSet)
        {
            ManageDataSet = manageDataSet;
        }

        public void In_SaveSettingsEntry(string settingsEntryName, string file)
        {
            ManageDataSet.ConfiguredSettingsRow settingsRow = ManageDataSet.ConfiguredSettings.FindByName(settingsEntryName.Trim());

            bool valid = settingsRow == null;

            if (!valid)
            {
                throw new SettingsEntryUniqueException("The specified name is already present and have to be unique.");
            }

            ManageDataSet.ConfiguredSettingsRow newRow = ManageDataSet.ConfiguredSettings.NewConfiguredSettingsRow();
            newRow.Name = settingsEntryName;
            newRow.File = file;

            ManageDataSet.ConfiguredSettings.AddConfiguredSettingsRow(newRow);

            ManageDataSet.AcceptChanges();

            OnSettingsEntrySaved(new SettingsEntrySavedMessage(settingsEntryName, file));
        }

        protected void OnSettingsEntrySaved(ISettingsEntrySavedMessage settingsEntrySavedMessage)
        {
            if (Out_SettingsEntrySaved != null)
            {
                Out_SettingsEntrySaved(settingsEntrySavedMessage);
            }
        }

        public event Action<ISettingsEntrySavedMessage> Out_SettingsEntrySaved;
    }
}
