using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Twainsoft.VSSettingsSwitcher.BLL.Contracts.Settings.Category;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Shell;
using Twainsoft.VSSettingsSwitcher.BLL.Models.Settings;

namespace Twainsoft.VSSettingsSwitcher.BLL.Components.Settings.Category
{
    public class SettingsCategoryRetriever : ISettingsCategoryRetriever
    {
        public SettingsCategoryRetriever()
        { }

        public void In_RetrieveSettingCategories()
        {
            SettingsCategoryViewModel rootSettingsCategory = new SettingsCategoryViewModel("Exportable Settings Categories");

            IVsProfileSettingsTree settingCategories;
            IVsProfileDataManager profileDataManager = Package.GetGlobalService(typeof(SVsProfileDataManager)) as IVsProfileDataManager;
            profileDataManager.GetSettingsForExport(out settingCategories);

            GetSettingsCategories(settingCategories, @"Environment_Group", rootSettingsCategory);
            GetSettingsCategories(settingCategories, @"AutomationProperties", rootSettingsCategory);

            rootSettingsCategory.IsInitiallySelected = true;
            rootSettingsCategory.IsChecked = true;

            OnSettingCategoriesRetrieved(new SettingCategoriesRetrievedMessage(rootSettingsCategory));
        }

        private void GetSettingsCategories(IVsProfileSettingsTree vsProfileSettingsTree, string categoryPath, SettingsCategoryViewModel parent)
        {
            IVsProfileSettingsTree childTree;
            vsProfileSettingsTree.FindChildTree(categoryPath, out childTree);
            if (childTree != null)
            {
                CreateSettingsCategoryViewModel(childTree, parent);
            }
        }

        private void CreateSettingsCategoryViewModel(IVsProfileSettingsTree vsProfileSettingsTree, SettingsCategoryViewModel parent)
        {
            string displayName = "";
            string registeredName = "";

            vsProfileSettingsTree.GetDisplayName(out displayName);
            vsProfileSettingsTree.GetRegisteredName(out registeredName);

            int childCount = 0;
            vsProfileSettingsTree.GetChildCount(out childCount);

            SettingsCategoryViewModel newNode = new SettingsCategoryViewModel(displayName, registeredName);
            parent.Children.Add(newNode);
            newNode._parent = parent;

            for (int i = 0; i < childCount; i++)
            {
                IVsProfileSettingsTree childTree;
                vsProfileSettingsTree.GetChild(i, out childTree);

                CreateSettingsCategoryViewModel(childTree, newNode);
            }
        }

        protected void OnSettingCategoriesRetrieved(ISettingCategoriesRetrievedMessage settingCategoriesRetrievedMessage)
        {
            if (Out_SettingCategoriesRetrieved != null)
            {
                Out_SettingCategoriesRetrieved(settingCategoriesRetrievedMessage);
            }
        }

        public event Action<ISettingCategoriesRetrievedMessage> Out_SettingCategoriesRetrieved;
    }
}
