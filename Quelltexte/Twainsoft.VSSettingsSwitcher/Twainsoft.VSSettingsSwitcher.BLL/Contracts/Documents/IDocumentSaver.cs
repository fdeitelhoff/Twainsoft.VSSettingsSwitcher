using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Twainsoft.VSSettingsSwitcher.BLL.Contracts.Documents
{
    public interface IDocumentSaver
    {
        void In_SaveActiveDocument();

        void In_SaveAllOpenDocuments();
    }
}
