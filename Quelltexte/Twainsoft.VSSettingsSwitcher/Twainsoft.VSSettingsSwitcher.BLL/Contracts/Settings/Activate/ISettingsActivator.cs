using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Twainsoft.VSSettingsSwitcher.BLL.Options.VSSettings.Manage;
using Twainsoft.VSSettingsSwitcher.BLL.Options;
using Twainsoft.VSSettingsSwitcher.DAL.Options.VSSettings.Manage;

namespace Twainsoft.VSSettingsSwitcher.BLL.Contracts.Settings.Activate
{
    public interface ISettingsActivator
    {
        void In_ActivateSettings(ManageDataSet.ConfiguredSettingsRow settingsRow);

        event Action<ISettingsActivatedMessage> Out_SettingsActivatedMessage;

        void SetGeneralOptions(GeneralOptionsStore generalOptionsStore);

        void SetManageOptionsStore(ManageDataSet manageDataSet);
    }
}
