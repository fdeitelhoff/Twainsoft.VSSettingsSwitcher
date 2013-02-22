using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Twainsoft.VSSettingsSwitcher.BLL.Contracts.StatusBar;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Shell;

namespace Twainsoft.VSSettingsSwitcher.BLL.Components.StatusBar
{
    public class StatusBarUpdater : IStatusBarUpdater
    {
        public StatusBarUpdater()
        { }

        public void In_UpdateStatusBar(string text)
        {
            IVsStatusbar statusBar = (IVsStatusbar)Package.GetGlobalService(typeof(SVsStatusbar));

            int frozen = 0;

            statusBar.IsFrozen(out frozen);

            if (frozen == 0)
            {
                statusBar.SetText(text);
            }
        }
    }
}
