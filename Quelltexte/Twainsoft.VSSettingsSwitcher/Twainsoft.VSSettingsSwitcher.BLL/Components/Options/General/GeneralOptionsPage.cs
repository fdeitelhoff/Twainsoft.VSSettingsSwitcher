using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Twainsoft.VSSettingsSwitcher.BLL.Options.Base;
using System.Runtime.InteropServices;

namespace Twainsoft.VSSettingsSwitcher.BLL.Options.General
{
    public partial class GeneralOptionsPage : BaseOptionsPage
    {
        private GeneralOptionsStore GeneralOptionsStore { get; set; }

        public GeneralOptionsPage(GeneralOptionsStore generalOptionsStore)
        {
            GeneralOptionsStore = generalOptionsStore;
            GeneralOptionsStore.LoadSettings += new BaseOptionsStore.LoadSettingsDelegate(GeneralOptionsStore_LoadSettings);
            GeneralOptionsStore.ApplySettings += new BaseOptionsStore.ApplySettingsDelegate(GeneralOptionsStore_ApplySettings);
            GeneralOptionsStore.DiscardSettings += new BaseOptionsStore.DiscardSettingsDelegate(GeneralOptionsStore_DiscardSettings);

            InitializeComponent();            
        }

        void GeneralOptionsStore_LoadSettings(object sender, EventArgs e)
        {
            LoadData();
        }

        void GeneralOptionsStore_DiscardSettings(object sender, EventArgs e)
        {
            LoadData();
        }

        void GeneralOptionsStore_ApplySettings(object sender, EventArgs e)
        {
            GeneralOptionsStore.AutoFormatOpeningDocuments = autoFormatOpeningDocuments.Checked;
            GeneralOptionsStore.AutoSaveOpeningDocuments = autoSaveDocuments.Checked;
            
            GeneralOptionsStore.AutoFormatDocumentsOnSettingsChanged = formatAllOpenDocuments.Checked;            
            GeneralOptionsStore.AutoSaveAllDocuments = saveAllOpenDocuments.Checked;
        }

        private void LoadData()
        {
            autoFormatOpeningDocuments.Checked = GeneralOptionsStore.AutoFormatOpeningDocuments;
            autoSaveDocuments.Checked = GeneralOptionsStore.AutoSaveOpeningDocuments;

            formatAllOpenDocuments.Checked = GeneralOptionsStore.AutoFormatDocumentsOnSettingsChanged;
            saveAllOpenDocuments.Checked = GeneralOptionsStore.AutoSaveAllDocuments;
        }

        private void autoFormatOpeningDocuments_CheckedChanged(object sender, EventArgs e)
        {
            autoSaveDocuments.Enabled = autoFormatOpeningDocuments.Checked;
        }

        private void formatAllOpenDocuments_CheckedChanged(object sender, EventArgs e)
        {
            saveAllOpenDocuments.Enabled = formatAllOpenDocuments.Checked;
        }
    }
}
