using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.Shell;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Settings;
using Microsoft.VisualStudio.Shell.Settings;
using System.Diagnostics;
using Twainsoft.VSSettingsSwitcher.BLL.Options.Base;
using Twainsoft.VSSettingsSwitcher.BLL.Options.General;

namespace Twainsoft.VSSettingsSwitcher.BLL.Options
{
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [CLSCompliant(false), ComVisible(true)]
    public class GeneralOptionsStore : BaseOptionsStore
    {
        public bool AutoFormatOpeningDocuments { get; set; }
        public bool AutoSaveOpeningDocuments { get; set; }

        public bool AutoFormatDocumentsOnSettingsChanged { get; set; }
        public bool AutoSaveAllDocuments { get; set; }

        public GeneralOptionsStore()
        {
            OptionsPage = new GeneralOptionsPage(this);
        }
    }
}
