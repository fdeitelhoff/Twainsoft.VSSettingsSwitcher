using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Twainsoft.VSSettingsSwitcher.BLL.Contracts.Settings.Category
{
    public interface ISettingsCategoryRetriever
    {
        void In_RetrieveSettingCategories();

        event Action<ISettingCategoriesRetrievedMessage> Out_SettingCategoriesRetrieved;
    }
}
