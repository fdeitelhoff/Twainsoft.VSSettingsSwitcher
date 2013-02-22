using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Twainsoft.VSSettingsSwitcher.BLL.Contracts.Documents;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;

namespace Twainsoft.VSSettingsSwitcher.BLL.Components.Documents
{
    public class DocumentSaver : IDocumentSaver
    {
        private DTE2 ApplicationObject { get; set; }

        public DocumentSaver()
        {
            ApplicationObject = Package.GetGlobalService(typeof(SDTE)) as DTE2;
        }

        public void In_SaveActiveDocument()
        {
            if (!ApplicationObject.ActiveDocument.Saved)
            {
                ApplicationObject.ActiveDocument.Save();
            }
        }

        public void In_SaveAllOpenDocuments()
        {
            for (int i = 1; i <= ApplicationObject.Documents.Count; i++)
            {
                if (!ApplicationObject.Documents.Item(i).Saved)
                {
                    ApplicationObject.Documents.Item(i).Save();
                }
            }
        }
    }
}
