using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Twainsoft.VSSettingsSwitcher.BLL.Models.Settings;

namespace Twainsoft.VSSettingsSwitcher.BLL.Contracts.Settings.Category
{
    public interface ISettingCategoriesRetrievedMessage
    {
        SettingsCategoryViewModel SettingsCategoryViewModel { get; set; }
    }
}
