using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Shell;
using Twainsoft.VSSettingsSwitcher.GUI.Properties;

namespace Twainsoft.VSSettingsSwitcher.GUI.ToolWindows.Settings
{
    [Guid("3ef26d8c-7d7d-4e79-835c-2ada89334dbe")]
    public class SettingSwitcherToolWindow : ToolWindowPane
    {
        public SettingSwitcherToolWindow()
            : base(null)
        {
            this.Caption = "Available settings";

            base.Content = new SettingSwitcherControl();
        }
    }
}
