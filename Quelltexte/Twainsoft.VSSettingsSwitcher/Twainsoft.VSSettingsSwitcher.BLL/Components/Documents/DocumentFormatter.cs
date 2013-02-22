using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Twainsoft.VSSettingsSwitcher.BLL.Contracts.Documents;
using EnvDTE80;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Shell;
using EnvDTE;

namespace Twainsoft.VSSettingsSwitcher.BLL.Components.Documents
{
    public class DocumentFormatter : IDocumentFormatter
    {
        private DTE2 ApplicationObject { get; set; }

        public DocumentFormatter()
        {
            ApplicationObject = Package.GetGlobalService(typeof(SDTE)) as DTE2;
        }

        public void In_FormatActiveDocument()
        {
            Document doc = ApplicationObject.ActiveDocument;

            TextDocument tex = doc.Object("TextDocument");
            EditPoint ep = tex.StartPoint.CreateEditPoint();
            ep.SmartFormat(tex.EndPoint);
        }

        public void In_FormatAllOpenDocuments()
        {
            for (int i = 1; i <= ApplicationObject.Documents.Count; i++)
            {
                Document doc = ApplicationObject.Documents.Item(i);

                TextDocument tex = doc.Object("TextDocument");
                EditPoint ep = tex.StartPoint.CreateEditPoint();
                ep.SmartFormat(tex.EndPoint);
            }
        }
    }
}
