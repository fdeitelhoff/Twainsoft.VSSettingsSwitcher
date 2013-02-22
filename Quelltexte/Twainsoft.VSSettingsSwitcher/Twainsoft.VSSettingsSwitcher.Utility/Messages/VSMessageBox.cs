using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Shell;
using System.Globalization;

namespace Twainsoft.VSSettingsSwitcher.Utility.Messages
{
    public enum VSMessageResult
    {
        IDABORT,
        IDCANCEL = 2,
        IDIGNORE,
        IDNO = 7,
        IDOK,
        IDRETRY,
        IDYES = 6
    }

    public static class VSMessageBox
    {
        public static VSMessageResult ShowMessageBox(string title, string message, 
            OLEMSGBUTTON buttons, OLEMSGDEFBUTTON defaultButton, OLEMSGICON icon)
        {
            IVsUIShell uiShell = Package.GetGlobalService(typeof(SVsUIShell)) as IVsUIShell;
            Guid clsid = Guid.Empty;
            
            int result;
            uiShell.ShowMessageBox(
                0,
                ref clsid,
                title,
                message,
                string.Empty,
                0,
                buttons,
                defaultButton,
                icon,
                0,        // false
                out result);

            return (VSMessageResult)result;
        }

        public static VSMessageResult ShowInfoMessageBox(string title, string message)
        {
            return ShowMessageBox(title, message, OLEMSGBUTTON.OLEMSGBUTTON_OK, OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST, OLEMSGICON.OLEMSGICON_INFO);
        }

        public static VSMessageResult ShowErrorMessageBox(string title, string message)
        {
            return ShowMessageBox(title, message, OLEMSGBUTTON.OLEMSGBUTTON_OK, OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST, OLEMSGICON.OLEMSGICON_CRITICAL);
        }

        public static VSMessageResult ShowQuestionMessageBox(string title, string message, OLEMSGBUTTON buttons, OLEMSGDEFBUTTON defaultButton)
        {
            return ShowMessageBox(title, message, buttons, defaultButton, OLEMSGICON.OLEMSGICON_QUERY);
        }
    }
}
