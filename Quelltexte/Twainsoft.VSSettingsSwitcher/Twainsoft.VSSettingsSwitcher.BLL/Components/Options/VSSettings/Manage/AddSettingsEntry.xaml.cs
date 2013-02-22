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
using Microsoft.Win32;
using System.ComponentModel;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Shell;
using System.IO;
using Twainsoft.VSSettingsSwitcher.Utility.Messages;
using Twainsoft.VSSettingsSwitcher.DAL.Options.VSSettings.Manage;

namespace Twainsoft.VSSettingsSwitcher.BLL.Components.Options.VSSettings.Manage
{
    public partial class AddSettingsEntry : Window
    {
        private OpenFileDialog OpenFileDialog { get; set; }
        private string DefaultSettingsLocation { get; set; }
        private ManageDataSet ManageDataSet { get; set; }

        public delegate void AddNewSettingsEntryEventHandler(object sender, AddNewSettingsEntryEventArgs e);
        public event AddNewSettingsEntryEventHandler AddNewSettingsEntry;

        public AddSettingsEntry()
        {
            InitializeComponent();

            IVsProfileDataManager profileDataManager = Package.GetGlobalService(typeof(SVsProfileDataManager)) as IVsProfileDataManager;

            string settingsPath = null;
            string settingsFileExtension = ".vssettings";
            profileDataManager.GetDefaultSettingsLocation(out settingsPath);
            profileDataManager.GetSettingsFileExtension(out settingsFileExtension);

            DefaultSettingsLocation = settingsPath;

            OpenFileDialog = new OpenFileDialog();
            OpenFileDialog.Multiselect = false;
            OpenFileDialog.DefaultExt = settingsFileExtension;
            OpenFileDialog.AddExtension = true;
            OpenFileDialog.InitialDirectory = settingsPath;
            OpenFileDialog.CheckFileExists = true;
            OpenFileDialog.CheckPathExists = true;
            OpenFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(openFileDialog_FileOk);
        }

        public AddSettingsEntry(ManageDataSet manageDataSet)
            : this()
        {
            ManageDataSet = manageDataSet;
        }

        private void selectFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog.ShowDialog(this);
        }

        void openFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            fileName.Text = OpenFileDialog.FileName;
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void buttonOK_Click(object sender, RoutedEventArgs e)
        {
            bool valid = !String.IsNullOrEmpty(name.Text.Trim());

            if (!valid)
            {
                VSMessageBox.ShowErrorMessageBox("Invalid name!",
                    "The specified name is not valid.");

                return;
            }

            ManageDataSet.ConfiguredSettingsRow settingsRow = ManageDataSet.ConfiguredSettings.FindByName(name.Text.Trim());

            valid = settingsRow == null;

            if (!valid)
            {
                VSMessageBox.ShowErrorMessageBox("Invalid name!",
                    "The specified name is already present and have to be unique. Please try another name.");

                return;
            }

            DirectoryInfo directoryInfo = null;
            string destinationFile = null;
            try
            {
                directoryInfo = new DirectoryInfo(fileName.Text.Trim());

                FileInfo fileInfo = new FileInfo(directoryInfo.FullName);
                destinationFile = directoryInfo.FullName;

                if (copyFileToSettingsDir.IsChecked.HasValue 
                    && copyFileToSettingsDir.IsChecked.Value
                    && !directoryInfo.FullName.ToLower().Contains(DefaultSettingsLocation.ToLower()))
                {
                    destinationFile = System.IO.Path.Combine(DefaultSettingsLocation, fileInfo.Name);

                    if (File.Exists(destinationFile))
                    {
                        VSMessageResult result = VSMessageBox.ShowQuestionMessageBox("File already exist!",
                            "The file already exist in the default settings folder. Would you like to overwrite it?",
                            OLEMSGBUTTON.OLEMSGBUTTON_YESNO, OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_SECOND);

                        if (result == VSMessageResult.IDYES)
                        {
                            File.Delete(destinationFile);
                            File.Copy(directoryInfo.FullName, destinationFile);
                        }
                        else
                        {
                            valid = false;
                        }
                    }
                    else
                    {
                        File.Copy(directoryInfo.FullName, destinationFile);
                    }
                }
            }
            catch (ArgumentException)
            {
                valid = false;

                VSMessageBox.ShowErrorMessageBox("Invalid directory!",
                    "The specified directory '" + fileName.Text.Trim() + "' is not valid.");
            }
            catch (NotSupportedException)
            {
                valid = false;

                VSMessageBox.ShowErrorMessageBox("Invalid directory!", 
                    "The specified directory '" + fileName.Text.Trim() + "' is not valid.");
            }

            if (valid)
            {
                OnAddSettingsEntry(new AddNewSettingsEntryEventArgs(name.Text.Trim(), destinationFile));

                Close();
            }            
        }

        protected virtual void OnAddSettingsEntry(AddNewSettingsEntryEventArgs e)
        {
            if (AddNewSettingsEntry != null)
            {
                AddNewSettingsEntry(this, e);
            }
        }
    }
}
