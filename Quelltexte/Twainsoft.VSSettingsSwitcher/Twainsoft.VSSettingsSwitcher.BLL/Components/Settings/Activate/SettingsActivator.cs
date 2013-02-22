using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Twainsoft.VSSettingsSwitcher.BLL.Contracts.Settings.Activate;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Shell;
using System.Runtime.InteropServices;
using Twainsoft.VSSettingsSwitcher.BLL.Options.VSSettings.Manage;
using Twainsoft.VSSettingsSwitcher.BLL.Options;
using Twainsoft.VSSettingsSwitcher.BLL.Contracts.Documents;
using Twainsoft.VSSettingsSwitcher.BLL.Components.Documents;
using Twainsoft.VSSettingsSwitcher.DAL.Options.VSSettings.Manage;

namespace Twainsoft.VSSettingsSwitcher.BLL.Components.Settings.Activate
{
    public class SettingsActivator : ISettingsActivator
    {
        private IDocumentFormatter DocumentFormatter { get; set; }
        private IDocumentSaver DocumentSaver { get; set; }

        private GeneralOptionsStore GeneralOptionsStore { get; set; }
        private ManageDataSet ManageDataSet { get; set; }

        public SettingsActivator()
        {
            DocumentFormatter = new DocumentFormatter();
            DocumentSaver = new DocumentSaver();
        }

        public void In_ActivateSettings(ManageDataSet.ConfiguredSettingsRow settingsRow)
        {
            // Fabian: TODO: Kann umgebaut werden! Die Schleife ist nicht mehr notwendig, wenn eine eigene Datei hinzugefügt wird!
            IVsProfileDataManager profileDataManager = (IVsProfileDataManager)Package.GetGlobalService(typeof(SVsProfileDataManager));

            IVsProfileSettingsTree settingsTree;
            IVsProfileSettingsFileInfo settingsFileInfo;
            IVsProfileSettingsFileCollection settingsFileCollection;

            profileDataManager.GetSettingsFiles((uint)__VSPROFILELOCATIONS.PFL_Other, out settingsFileCollection);

            settingsFileCollection.AddBrowseFile(settingsRow.File, out settingsFileInfo);

            bool success = false;

            settingsFileInfo.GetSettingsForImport(out settingsTree);

            settingsTree.SetEnabled(1, 1);
            //@"AutomationProperties\TextEditor\CSharp-Specific";
            IVsProfileSettingsTree childTree1;
            settingsTree.FindChildTree(@"VSSettingsSwitcher_CreateVSSetting", out childTree1);
            if (childTree1 != null)
            {
                childTree1.SetEnabled(0, 1);
            }

            IVsProfileSettingsTree childTree2;
            settingsTree.FindChildTree(@"VSSettingsSwitcher_General", out childTree2);
            if (childTree2 != null)
            {
                childTree2.SetEnabled(0, 1);
            }

            IVsProfileSettingsTree childTree3;
            settingsTree.FindChildTree(@"VSSettingsSwitcher_ManageVSSettings", out childTree3);
            if (childTree3 != null)
            {
                childTree3.SetEnabled(0, 1);
            }            
            
            IVsSettingsErrorInformation errorInformation;
            int returnCode = profileDataManager.ImportSettings(settingsTree, out errorInformation);

            Exception ex = Marshal.GetExceptionForHR(returnCode);
            int pnErrors = 0;

            if (ex != null)
            {
                errorInformation.GetErrorCount(out pnErrors);
            }
            else
            {
                success = true;
            }

            if (success)
            {
                foreach (ManageDataSet.ConfiguredSettingsRow currentSettingsRow in ManageDataSet.ConfiguredSettings.Rows)
                {
                    if (currentSettingsRow == settingsRow)
                    {
                        currentSettingsRow.IsActive = true;
                    }
                    else if (currentSettingsRow.IsActive)
                    {
                        currentSettingsRow.IsActive = false;
                    }
                }

                if (GeneralOptionsStore.AutoFormatDocumentsOnSettingsChanged)
                {
                    DocumentFormatter.In_FormatAllOpenDocuments();

                    if (GeneralOptionsStore.AutoSaveAllDocuments)
                    {
                        DocumentSaver.In_SaveAllOpenDocuments();
                    }
                }
            }

            OnSettingsActivatedMessage(new SettingsActivatedMessage(settingsRow.File, success, true));
        }

        public event Action<ISettingsActivatedMessage> Out_SettingsActivatedMessage;

        protected virtual void OnSettingsActivatedMessage(ISettingsActivatedMessage settingsActivatedMessage)
        {
            if (Out_SettingsActivatedMessage != null)
            {
                Out_SettingsActivatedMessage(settingsActivatedMessage);
            }
        }

        public void SetGeneralOptions(GeneralOptionsStore generalOptionsStore)
        {
            GeneralOptionsStore = generalOptionsStore;
        }

        public void SetManageOptionsStore(ManageDataSet manageDataSet)
        {
            ManageDataSet = manageDataSet;
        }
    }
}
