using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Twainsoft.VSSettingsSwitcher.BLL.Contracts.Settings.Export;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Shell;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.IO;
using Twainsoft.VSSettingsSwitcher.BLL.Models.Settings;

namespace Twainsoft.VSSettingsSwitcher.BLL.Components.Settings.Export
{
    public class SettingsExporter : ISettingsExporter
    {
        public SettingsExporter()
        { }

        public void In_ExportSettings(string fileName, SettingsCategoryViewModel settingsCategoryViewModel)
        {
            IVsProfileDataManager profileDataManager = Package.GetGlobalService(typeof(SVsProfileDataManager)) as IVsProfileDataManager;

            IVsSettingsErrorInformation settingsErrorInfo;
            IVsProfileSettingsTree exportableSettings;
            
            profileDataManager.GetSettingsForExport(out exportableSettings);

            exportableSettings.SetEnabled(0, 1);
            //int c = 0;
            //string path = "";

            //exportableSettings.SetEnabled(0, 1);
            //exportableSettings.GetEnabledChildCount(out c);
            //exportableSettings.GetAlternatePath(out path);

            EnableSettingsCategories(exportableSettings, settingsCategoryViewModel.Children);

            int exportResult = profileDataManager.ExportSettings(fileName, exportableSettings, out settingsErrorInfo);

            Exception ex = Marshal.GetExceptionForHR(exportResult);
            int pnErrors = 0;

            bool success = false;
            if (ex != null)
            {
                settingsErrorInfo.GetErrorCount(out pnErrors);
            }
            else
            {
                success = true;
            }

            OnSettingsExported(new SettingsExportedMessage(fileName, success));
        }

        private void EnableSettingsCategories(IVsProfileSettingsTree exportableSettings, List<SettingsCategoryViewModel> list)
        {
            foreach (var item in list)
            {
                if (item.Children.Count > 0 && item.IsChecked.HasValue && item.IsChecked.Value)
                {
                    IVsProfileSettingsTree childTree;
                    exportableSettings.FindChildTree(item.GetFullRegisteredName(), out childTree);
                    if (childTree != null)
                    {
                        childTree.SetEnabled(1, 1);
                    }
                }
                else if (item.Children.Count == 0 && item.IsChecked.HasValue && item.IsChecked.Value)
                {
                    IVsProfileSettingsTree childTree;
                    exportableSettings.FindChildTree(item.GetFullRegisteredName(), out childTree);
                    if (childTree != null)
                    {
                        childTree.SetEnabled(1, 0);
                    }
                }
                else if (item.Children.Count > 0 && ((item.IsChecked.HasValue && item.IsChecked.Value) || item.IsChecked == null))
                {
                    EnableSettingsCategories(exportableSettings, item.Children);
                }
            }
        }

        public event Action<ISettingsExportedMessage> Out_SettingsExported;

        protected void OnSettingsExported(ISettingsExportedMessage settingsExportedMessage)
        {
            if (Out_SettingsExported != null)
            {
                Out_SettingsExported(settingsExportedMessage);
            }
        }
    }
}
