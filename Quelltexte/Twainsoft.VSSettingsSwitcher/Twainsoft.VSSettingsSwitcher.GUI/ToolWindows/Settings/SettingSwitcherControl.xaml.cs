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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Twainsoft.VSSettingsSwitcher.BLL.Contracts.Settings.Activate;
using Twainsoft.VSSettingsSwitcher.BLL.Components.Settings.Activate;
using System.Data;
using Twainsoft.VSSettingsSwitcher.DAL.Options.VSSettings.Manage;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Shell;
using Twainsoft.VSSettingsSwitcher.BLL.Contracts.StatusBar;
using Twainsoft.VSSettingsSwitcher.BLL.Components.StatusBar;
using Twainsoft.VSSettingsSwitcher.BLL.Options.VSSettings.Manage;
using Twainsoft.VSSettingsSwitcher.BLL.Components.Documents;
using Twainsoft.VSSettingsSwitcher.BLL.Options;
using Twainsoft.VSSettingsSwitcher.Utility.Messages;
using Twainsoft.VSSettingsSwitcher.GUI.Windows.Settings;
using System.ComponentModel;

namespace Twainsoft.VSSettingsSwitcher.GUI.ToolWindows.Settings
{
    public partial class SettingSwitcherControl : UserControl
    {
        private ManageOptionsStore ManageOptionsStore { get; set; }
        private ISettingsActivator SettingsActivator { get; set; }
        private IStatusBarUpdater StatusBarUpdater { get; set; }
        public DataView DataView { get; set; }
        public delegate void ManageSettingsEventHandler(object sender, EventArgs e);
        public event ManageSettingsEventHandler ManageSettings;

        public SettingSwitcherControl()
        {
            InitializeComponent();

            SettingsActivator = new SettingsActivator();
            SettingsActivator.Out_SettingsActivatedMessage += new Action<ISettingsActivatedMessage>(SettingsActivator_Out_SettingsActivatedMessage);

            StatusBarUpdater = new StatusBarUpdater();
        }

        void SettingsActivator_Out_SettingsActivatedMessage(ISettingsActivatedMessage settingsActivatedMessage)
        {
            if (settingsActivatedMessage.Success)
            {
                ManageOptionsStore.SaveData();

                StatusBarUpdater.In_UpdateStatusBar("The settings in the file '" + settingsActivatedMessage.FileName + "' were successfully loaded!");
            } 
            else if (!settingsActivatedMessage.Found)
            {
                StatusBarUpdater.In_UpdateStatusBar("The file '" + settingsActivatedMessage.FileName + "' could not be found!");

                VSMessageBox.ShowErrorMessageBox("Settings could not be loaded!",
                    "The file '" + settingsActivatedMessage.FileName + "' could not be found! Please check the settings!");
            }
        }

        public void SetDialogPage(ManageOptionsStore manageOptionsStore)
        {
            ManageOptionsStore = manageOptionsStore;
            SettingsActivator.SetManageOptionsStore(ManageOptionsStore.ManageDataSet);
            DataView = new System.Data.DataView(ManageOptionsStore.ManageDataSet.ConfiguredSettings);
            settingsFilesOverview.ItemsSource = DataView;
        }

        private void activateSelectedSettings_Click(object sender, RoutedEventArgs e)
        {
            ActivateSettingsFile();
        }

        private void settingsFilesOverview_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ActivateSettingsFile();
        }

        private void ActivateSettingsFile()
        {
            DataRowView dataRow = settingsFilesOverview.SelectedItem as DataRowView;
            ManageDataSet.ConfiguredSettingsRow settingsRow = dataRow.Row as ManageDataSet.ConfiguredSettingsRow;

            SettingsActivator.In_ActivateSettings(settingsRow);
        }

        public void SetGeneralOptions(GeneralOptionsStore generalOptionsStore)
        {
            SettingsActivator.SetGeneralOptions(generalOptionsStore);
        }

        private void settingsFilesOverview_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            activateSelectedSettings.IsEnabled = settingsFilesOverview.SelectedItems.Count == 1;
        }

        private void manageSettings_Click(object sender, RoutedEventArgs e)
        {
            ManageSettings(this, EventArgs.Empty);
        }

        private void createSetting_Click(object sender, RoutedEventArgs e)
        {
            CreateSettingsFileWindow createSettingsFileWindow = new CreateSettingsFileWindow(ManageOptionsStore.ManageDataSet);
            createSettingsFileWindow.Show();
        }

        private void search_Click(object sender, RoutedEventArgs e)
        {
            //ICollectionView collectionView = CollectionViewSource.GetDefaultView(settingsFilesOverview.ItemsSource);
            //collectionView.Filter = FilterSettingsFilesOverview;
            FilterSettingsFilesOverview();
        }

        //private bool FilterSettingsFilesOverview(object obj)
        //{
        //    Console.WriteLine();

        //    return true;
        //}

        private void searchTerm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                FilterSettingsFilesOverview();
            }
        }

        private void FilterSettingsFilesOverview()
        {
            DataView.RowFilter = "Name Like '%" + searchTerm.Text.Trim() + "%' or File Like '%" + searchTerm.Text.Trim() + "%'";
        }
    }
}
