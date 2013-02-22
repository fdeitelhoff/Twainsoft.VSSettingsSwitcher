using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Twainsoft.VSSettingsSwitcher.BLL.Options.Base;
using Twainsoft.VSSettingsSwitcher.DAL.Options.VSSettings.Manage;

namespace Twainsoft.VSSettingsSwitcher.BLL.Options.VSSettings.Manage
{
    public partial class ManageOptionsPage : BaseOptionsPage
    {
        private ManageOptionsStore ManageOptionsStore { get; set; }

        public ManageOptionsPage(ManageOptionsStore manageOptionsStore)
        {
            InitializeComponent();

            ManageOptionsStore = manageOptionsStore;

            manageDataSet = ManageOptionsStore.ManageDataSet;

            SettingsFilesOverview userControl = elementHost.Child as SettingsFilesOverview;

            userControl.SetDataContext(ManageOptionsStore.ManageDataSet);
        }
    }
}
