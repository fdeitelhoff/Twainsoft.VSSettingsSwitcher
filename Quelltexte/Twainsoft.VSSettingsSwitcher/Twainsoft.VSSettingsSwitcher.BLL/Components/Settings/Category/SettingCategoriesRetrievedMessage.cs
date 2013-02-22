using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Twainsoft.VSSettingsSwitcher.BLL.Contracts.Settings.Category;
using Twainsoft.VSSettingsSwitcher.BLL.Models.Settings;

namespace Twainsoft.VSSettingsSwitcher.BLL.Components.Settings.Category
{
    public class SettingCategoriesRetrievedMessage : ISettingCategoriesRetrievedMessage
    {
        public SettingsCategoryViewModel SettingsCategoryViewModel { get; set; }

        public SettingCategoriesRetrievedMessage()
        { }

        public SettingCategoriesRetrievedMessage(SettingsCategoryViewModel settingsCategoryViewModel)
        {
            SettingsCategoryViewModel = settingsCategoryViewModel;
        }
    }
}
