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
using Twainsoft.VSSettingsSwitcher.DAL.Options.VSSettings.Manage;
using Twainsoft.VSSettingsSwitcher.BLL.Components.Options.VSSettings.Manage;
using Twainsoft.VSSettingsSwitcher.Utility.Messages;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Shell;
using System.IO;
using System.Data;
using System.ComponentModel;

namespace Twainsoft.VSSettingsSwitcher.BLL.Options.VSSettings.Manage
{
    public partial class SettingsFilesOverview : UserControl
    {
        private ManageDataSet ManageDataSet { get; set; }
        public ManageDataSet ManageDataSetCopy { get; private set; }
        private ICollectionView CollectionView { get; set; }

        public SettingsFilesOverview()
        {
            InitializeComponent();
        }

        public void SetDataContext(ManageDataSet manageDataSet)
        {
            ManageDataSet = manageDataSet;
            ManageDataSetCopy = new ManageDataSet();

            settingsFilesOverview.ItemsSource = ManageDataSet.ConfiguredSettings.DefaultView;

            CollectionView = CollectionViewSource.GetDefaultView(settingsFilesOverview.ItemsSource);            

            CollectionView.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
        }

        private void addSettingsFile_Click(object sender, RoutedEventArgs e)
        {
            AddSettingsEntry addSettingsEntry = new AddSettingsEntry(ManageDataSet);
            addSettingsEntry.AddNewSettingsEntry += new AddSettingsEntry.AddNewSettingsEntryEventHandler(addSettingsEntry_AddNewSettingsEntry);
            addSettingsEntry.ShowDialog();
            addSettingsEntry.AddNewSettingsEntry -= addSettingsEntry_AddNewSettingsEntry;
        }

        void addSettingsEntry_AddNewSettingsEntry(object sender, AddNewSettingsEntryEventArgs e)
        {
            ManageDataSet.ConfiguredSettingsRow newRow = ManageDataSet.ConfiguredSettings.NewConfiguredSettingsRow();
            newRow.Name = e.Name;
            newRow.File = e.File;

            ManageDataSet.ConfiguredSettings.AddConfiguredSettingsRow(newRow);
        }

        private void settingsFilesOverview_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            deleteSelectedSettingsFiles.IsEnabled = settingsFilesOverview.SelectedItems.Count > 0;
        }

        private void deleteSelectedSettingsFiles_Click(object sender, RoutedEventArgs e)
        {
            VSMessageResult result = VSMessageBox.ShowQuestionMessageBox("Delete settings files too?",
                "Would you like to delete the associated settings files too?", 
                OLEMSGBUTTON.OLEMSGBUTTON_YESNOCANCEL, OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_SECOND);

            IVsProfileDataManager profileDataManager = Package.GetGlobalService(typeof(SVsProfileDataManager)) as IVsProfileDataManager;
            string settingsPath = null;
            string settingsFileExtension = ".vssettings";
            profileDataManager.GetDefaultSettingsLocation(out settingsPath);
            profileDataManager.GetSettingsFileExtension(out settingsFileExtension);

            if (result == VSMessageResult.IDYES || result == VSMessageResult.IDNO)
            {
                for (int i = settingsFilesOverview.SelectedItems.Count - 1; i >= 0; i--)
                {
                    DataRowView dataRow = settingsFilesOverview.SelectedItems[i] as DataRowView;
                    ManageDataSet.ConfiguredSettingsRow settingsRow = dataRow.Row as ManageDataSet.ConfiguredSettingsRow;

                    try
                    {
                        if (result == VSMessageResult.IDYES)
                        {
                            File.Delete(System.IO.Path.Combine(settingsPath, settingsRow.File) + settingsFileExtension);
                        }

                        settingsRow.Delete();
                    }
                    catch (IOException)
                    { }
                }

                if (result == VSMessageResult.IDYES)
                {
                    ManageDataSet.AcceptChanges();
                }
            }
        }

        internal void CopyData()
        {
            ManageDataSetCopy.ConfiguredSettings.Merge(ManageDataSet.ConfiguredSettings);
        }
    }
}
