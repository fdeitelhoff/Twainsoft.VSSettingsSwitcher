using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Twainsoft.VSSettingsSwitcher.BLL.Options.Base;

namespace Twainsoft.VSSettingsSwitcher.BLL.Options.VSSettings.Create
{
    public class CreateOptionsStore : BaseOptionsStore
    {
        public CreateOptionsStore()
        {
            OptionsPage = new CreateOptionsPage();
        }
    }
}
