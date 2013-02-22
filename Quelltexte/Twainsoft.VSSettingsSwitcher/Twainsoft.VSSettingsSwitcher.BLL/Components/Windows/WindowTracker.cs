using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Twainsoft.VSSettingsSwitcher.BLL.Contracts.Windows;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using EnvDTE;
using Twainsoft.VSSettingsSwitcher.BLL.Contracts.Documents;
using Twainsoft.VSSettingsSwitcher.BLL.Components.Documents;
using Twainsoft.VSSettingsSwitcher.BLL.Options;

namespace Twainsoft.VSSettingsSwitcher.BLL.Components.Windows
{
    public class WindowTracker : IWindowTracker
    {
        private IDocumentFormatter DocumentFormatter { get; set; }
        private IDocumentSaver DocumentSaver { get; set; }

        private DocumentEvents DocumentEvents { get; set; }
        private WindowEvents WindowEvents { get; set; }
        private DTE2 ApplicationObject { get; set; }
        private Window LastOpendedWindow { get; set; }

        public GeneralOptionsStore GeneralOptionsStore { get; set; }

        public WindowTracker()
        {
            DocumentFormatter = new DocumentFormatter();
            DocumentSaver = new DocumentSaver();
        }

        public void In_TrackWindows()
        {                        
            ApplicationObject = Package.GetGlobalService(typeof(SDTE)) as DTE2;
            DocumentEvents = ApplicationObject.Events.DocumentEvents;
            WindowEvents = ApplicationObject.Events.WindowEvents;

            DocumentEvents.DocumentOpened += new EnvDTE._dispDocumentEvents_DocumentOpenedEventHandler(DocumentEvents_DocumentOpened);
            WindowEvents.WindowActivated += new _dispWindowEvents_WindowActivatedEventHandler(WindowEvents_WindowActivated);
        }

        void WindowEvents_WindowActivated(Window gotFocus, Window lostFocus)
        {
            if (GeneralOptionsStore.AutoFormatOpeningDocuments && gotFocus == LastOpendedWindow)
            {
                DocumentFormatter.In_FormatActiveDocument();

                LastOpendedWindow = null;

                if (GeneralOptionsStore.AutoSaveOpeningDocuments)
                {
                    DocumentSaver.In_SaveActiveDocument();
                }
            }
        }

        void DocumentEvents_DocumentOpened(Document document)
        {
            LastOpendedWindow = document.ActiveWindow;
        }
    }
}
