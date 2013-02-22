using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Twainsoft.VSSettingsSwitcher.BLL.Models.Settings;
using Twainsoft.VSSettingsSwitcher.BLL.Contracts.Settings.Category;
using Twainsoft.VSSettingsSwitcher.BLL.Components.Settings.Category;
using Twainsoft.VSSettingsSwitcher.BLL.Contracts.Settings.Export;
using Twainsoft.VSSettingsSwitcher.BLL.Components.Settings.Export;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Shell;
using Microsoft.Win32;
using System.ComponentModel;
using Twainsoft.VSSettingsSwitcher.BLL.Contracts.Settings.Save;
using Twainsoft.VSSettingsSwitcher.BLL.Components.Settings.Save;
using Twainsoft.VSSettingsSwitcher.DAL.Options.VSSettings.Manage;
using Twainsoft.VSSettingsSwitcher.Utility.Messages;
using System.IO;

namespace Twainsoft.VSSettingsSwitcher.GUI.Windows.Settings
{
    public partial class CreateSettingsFileWindow : Window
    {
        private ISettingsExporter SettingsExporter { get; set; }
        private ISettingsCategoryRetriever SettingsCategoryRetriever { get; set; }
        private ISettingsEntrySaver SettingsEntrySaver { get; set; }

        private SettingsCategoryViewModel SettingsCategoryViewModel { get; set; }
        private ManageDataSet ManageDataSet { get; set; }

        private SaveFileDialog SaveFileDialog { get; set; }
        private string DefaultSettingsLocation { get; set; }

        public CreateSettingsFileWindow()
        {
            InitializeComponent();

            SettingsExporter = new SettingsExporter();
            SettingsExporter.Out_SettingsExported += new Action<ISettingsExportedMessage>(SettingsExporter_Out_SettingsExported);

            SettingsCategoryRetriever = new SettingsCategoryRetriever();
            SettingsCategoryRetriever.Out_SettingCategoriesRetrieved += new Action<ISettingCategoriesRetrievedMessage>(SettingsCategoryRetriever_Out_SettingCategoriesRetrieved);
        }

        public CreateSettingsFileWindow(ManageDataSet manageDataSet)
            : this()
        {
            SettingsEntrySaver = new SettingsEntrySaver(manageDataSet);
            SettingsEntrySaver.Out_SettingsEntrySaved += new Action<ISettingsEntrySavedMessage>(SettingsEntrySaver_Out_SettingsEntrySaved);

            SettingsCategoryRetriever.In_RetrieveSettingCategories();
        }

        void SettingsEntrySaver_Out_SettingsEntrySaved(ISettingsEntrySavedMessage settingsEntrySavedMessage)
        {
            VSMessageBox.ShowInfoMessageBox("Successfully saved",
                "The file '" + System.IO.Path.GetFileName(settingsEntrySavedMessage.File) 
                + "' was saved successfully and a Settings-Switcher entry named '" + settingsEntrySavedMessage.Name + "' was created.");
        }

        void SettingsExporter_Out_SettingsExported(ISettingsExportedMessage settingsExportedMessage)
        {
            if (settingsExportedMessage.Success)
            {
                if (isSettingsEntryCreated.IsChecked.HasValue && isSettingsEntryCreated.IsChecked.Value)
                {
                    SettingsEntrySaver.In_SaveSettingsEntry(settingsEntryName.Text.Trim(), settingsExportedMessage.FileName);
                }
                else
                {
                    VSMessageBox.ShowInfoMessageBox("Successfully saved",
                        "The file '" + System.IO.Path.GetFileName(settingsExportedMessage.FileName) + "' was saved successfully.");
                }
            }
            else
            {
                VSMessageBox.ShowInfoMessageBox("Error while saving",
                    "The file '" + System.IO.Path.GetFileName(settingsExportedMessage.FileName) + "' could not saved!");
            }
        }

        void SettingsCategoryRetriever_Out_SettingCategoriesRetrieved(ISettingCategoriesRetrievedMessage settingCategoriesRetrievedMessage)
        {
            exportableSettingsCategories.ItemsSource = new List<SettingsCategoryViewModel> { settingCategoriesRetrievedMessage.SettingsCategoryViewModel };

            SettingsCategoryViewModel = settingCategoriesRetrievedMessage.SettingsCategoryViewModel;
        }

        private void exportSettings_Click(object sender, RoutedEventArgs e)
        {
            IVsProfileDataManager profileDataManager = Package.GetGlobalService(typeof(SVsProfileDataManager)) as IVsProfileDataManager;

            string settingsPath = null;
            string settingsFileExtension = ".vssettings";
            profileDataManager.GetDefaultSettingsLocation(out settingsPath);
            profileDataManager.GetSettingsFileExtension(out settingsFileExtension);

            DefaultSettingsLocation = settingsPath;

            SaveFileDialog = new SaveFileDialog();
            SaveFileDialog.DefaultExt = settingsFileExtension;
            SaveFileDialog.AddExtension = true;
            SaveFileDialog.InitialDirectory = settingsPath;
            SaveFileDialog.CheckPathExists = true;
            SaveFileDialog.Filter = String.Format("Settings Files  (*{0})|*{0}", settingsFileExtension);

            if (SaveFileDialog.ShowDialog().Value)
            {
                SettingsExporter.In_ExportSettings(SaveFileDialog.FileName, SettingsCategoryViewModel);
            }
        }

        private void isSettingsEntryCreated_Click(object sender, RoutedEventArgs e)
        {
            checkUIStatus();
        }

        private void settingsEntryName_TextChanged(object sender, TextChangedEventArgs e)
        {
            checkUIStatus();
        }

        private void checkUIStatus()
        {
            exportSettings.IsEnabled = !isSettingsEntryCreated.IsChecked.Value || (isSettingsEntryCreated.IsChecked.Value && !String.IsNullOrWhiteSpace(settingsEntryName.Text));
            settingsEntryName.IsEnabled = isSettingsEntryCreated.IsChecked.Value;
        }
    }
}
