namespace Twainsoft.VSSettingsSwitcher.BLL.Options.VSSettings.Manage
{
    partial class ManageOptionsPage
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.manageDataSet = new Twainsoft.VSSettingsSwitcher.DAL.Options.VSSettings.Manage.ManageDataSet();
            this.elementHost = new System.Windows.Forms.Integration.ElementHost();
            this.settingsFilesOverview = new Twainsoft.VSSettingsSwitcher.BLL.Options.VSSettings.Manage.SettingsFilesOverview();
            ((System.ComponentModel.ISupportInitialize)(this.manageDataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // manageDataSet
            // 
            this.manageDataSet.DataSetName = "ManageDataSet";
            this.manageDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // elementHost
            // 
            this.elementHost.Dock = System.Windows.Forms.DockStyle.Fill;
            this.elementHost.Location = new System.Drawing.Point(0, 0);
            this.elementHost.Name = "elementHost";
            this.elementHost.Size = new System.Drawing.Size(393, 286);
            this.elementHost.TabIndex = 0;
            this.elementHost.Child = this.settingsFilesOverview;
            // 
            // ManageOptionsPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.elementHost);
            this.Name = "ManageOptionsPage";
            ((System.ComponentModel.ISupportInitialize)(this.manageDataSet)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DAL.Options.VSSettings.Manage.ManageDataSet manageDataSet;
        private System.Windows.Forms.Integration.ElementHost elementHost;
        private SettingsFilesOverview settingsFilesOverview;
    }
}
