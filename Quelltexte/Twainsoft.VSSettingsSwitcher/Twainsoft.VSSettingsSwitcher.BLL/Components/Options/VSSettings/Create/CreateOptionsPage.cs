using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Twainsoft.VSSettingsSwitcher.BLL.Options.Base;
using Twainsoft.VSSettingsSwitcher.BLL.Components.Settings.Export;
using Twainsoft.VSSettingsSwitcher.BLL.Contracts.Settings.Export;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Shell;
using Twainsoft.VSSettingsSwitcher.Utility.Messages;
using System.IO;
using EnvDTE80;
using EnvDTE;

namespace Twainsoft.VSSettingsSwitcher.BLL.Options.VSSettings.Create
{
    public partial class CreateOptionsPage : BaseOptionsPage
    {
        private ISettingsExporter SettingsExporter { get; set; }

        public CreateOptionsPage()
        {
            InitializeComponent();

            SettingsExporter = new SettingsExporter();
            SettingsExporter.Out_SettingsExported += new Action<ISettingsExportedMessage>(SettingsExporter_Out_SettingsExported);

            IVsProfileSettingsTree sets;
            IVsProfileSettingsTree wl;
            IVsProfileDataManager profileDataManager = Package.GetGlobalService(typeof(SVsProfileDataManager)) as IVsProfileDataManager;
            profileDataManager.GetSettingsForExport(out sets);

            FillTree(sets);

            //DTE ApplicationObject = Package.GetGlobalService(typeof(SDTE)) as DTE;

            //try
            //{
            //    EnvDTE.Properties prop = ApplicationObject.Properties["VSSettingsSwitcher", "General"];

            //    //dynamic prop1 = ApplicationObject.Properties["VSSettingsSwitcher", "General"].Item("AutoFormatDocumentsOnSettingsChanged").Value;
            //    //EnvDTE.Properties prop = ApplicationObject.Properties["VSSettingsSwitcher", "General"];
            //    //int count = ApplicationObject.Properties["VSSettingsSwitcher", "General"].Count;

            //    foreach (Property item in ApplicationObject.Properties["VSSettingsSwitcher", "General"])
            //    {
            //        //Console.WriteLine(item.Name + " : " + item.Value);
            //        MessageBox.Show(item.Name + " : " + item.Value);
            //    }
            //}
            //catch (Exception exc)
            //{
            //    Console.WriteLine(exc);
            //}
        }

        void SettingsExporter_Out_SettingsExported(ISettingsExportedMessage settingsExportedMessage)
        {
            if (settingsExportedMessage.Success)
            {
                VSMessageBox.ShowInfoMessageBox("Successfully saved!",
                        "The file '" + settingsExportedMessage.FileName + "'was saved.");
            }
            else
            {
                VSMessageBox.ShowInfoMessageBox("Settings not saved!",
                    "The file '" + settingsExportedMessage.FileName + "'could not be saved.");
            }
        }

        private void saveSettings_Click(object sender, EventArgs e)
        {
            IVsProfileDataManager profileDataManager = Package.GetGlobalService(typeof(SVsProfileDataManager)) as IVsProfileDataManager;

            string settingsPath = null;
            string settingsFileExtension = null;
            profileDataManager.GetDefaultSettingsLocation(out settingsPath);
            profileDataManager.GetSettingsFileExtension(out settingsFileExtension);

            try
            {
                string fileName = this.fileName.Text.Trim();
                
                FileInfo fileInfo = new FileInfo(fileName);

                if (!String.IsNullOrEmpty(fileInfo.Extension))
                {
                    fileName = fileInfo.Name.Replace(fileInfo.Extension, "");
                }

                DirectoryInfo dir = new DirectoryInfo(Path.Combine(settingsPath, fileName) + settingsFileExtension);

                //SettingsExporter.In_ExportSettings(dir.FullName);
            }
            catch (NotSupportedException)
            {
                VSMessageBox.ShowInfoMessageBox("Incorrect file name!", 
                    "The file you have specified seems to be invalid. Check if it contains invalid characters!");
            }            
        }

        private void fileName_TextChanged(object sender, EventArgs e)
        {
            saveSettings.Enabled = fileName.Text.Trim().Length > 0;
        }

        internal void FillTree(IVsProfileSettingsTree sets)
        {
            TreeNode parent = new TreeNode("Root");
            this.selectedSettingsOptions.Nodes.Add(parent);

            this.AddNode(sets, parent);

            parent.ExpandAll();
        }

        private void AddNode(IVsProfileSettingsTree sets, TreeNode parent)
        {
            string displayName = "";
            string pbstrRegisteredName = "";

            sets.GetDisplayName(out displayName);
            sets.GetRegisteredName(out pbstrRegisteredName);

            int childCount = 0;
            sets.GetChildCount(out childCount);

            TreeNode newNode = new TreeNode(displayName + "(" + pbstrRegisteredName + ") (Children: " + childCount + ")");
            parent.Nodes.Add(newNode);

            for (int i = 0; i < childCount; i++)
            {
                IVsProfileSettingsTree childTree;
                sets.GetChild(i, out childTree);

                this.AddNode(childTree, newNode);
            }
        }
    }
}
