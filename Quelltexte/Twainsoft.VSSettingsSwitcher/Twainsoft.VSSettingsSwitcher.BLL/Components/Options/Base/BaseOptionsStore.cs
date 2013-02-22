using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.Shell;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.ComponentModel;

namespace Twainsoft.VSSettingsSwitcher.BLL.Options.Base
{
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [CLSCompliant(false), ComVisible(true)]
    //[Guid("b16c8b55-1eb6-4b1d-9061-3c172e93e186")]
    public class BaseOptionsStore : DialogPage
    {
        protected BaseOptionsPage OptionsPage { get; set; }
        protected bool IsFirstView { get; set; }
        protected bool IsSaved { get; set; }

        public BaseOptionsStore()
        {
            IsFirstView = true;
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        protected override IWin32Window Window
        {
            get
            {
                return OptionsPage;
            }
        }

        public delegate void ApplySettingsDelegate(object sender, EventArgs e);
        public event ApplySettingsDelegate ApplySettings;

        public delegate void DiscardSettingsDelegate(object sender, EventArgs e);
        public event DiscardSettingsDelegate DiscardSettings;

        public delegate void LoadSettingsDelegate(object sender, EventArgs e);
        public event LoadSettingsDelegate LoadSettings;

        protected override void OnApply(PageApplyEventArgs e)
        {
            OnApplySettings(EventArgs.Empty);

            base.OnApply(e);
        }

        protected virtual void OnApplySettings(EventArgs eventArgs)
        {
            if (ApplySettings != null)
            {
                ApplySettings(this, eventArgs);
            }

            IsSaved = true;
        }

        protected override void OnActivate(CancelEventArgs e)
        {
            base.OnActivate(e);

            if (IsFirstView)
            {
                OnLoadSettings(EventArgs.Empty);

                IsFirstView = false;
            }
        }

        protected virtual void OnDiscardSettings(EventArgs eventArgs)
        {
            if (DiscardSettings != null)
            {
                DiscardSettings(this, eventArgs);

                IsFirstView = true;
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            OnDiscardSettings(EventArgs.Empty);
        }

        protected virtual void OnLoadSettings(EventArgs e)
        {
            if (LoadSettings != null)
            {
                LoadSettings(this, e);
            }
        }
    }
}
