using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Twainsoft.VSSettingsSwitcher.BLL.Contracts.Documents
{
    public interface IDocumentFormatter
    {
        void In_FormatActiveDocument();

        void In_FormatAllOpenDocuments();
    }
}
