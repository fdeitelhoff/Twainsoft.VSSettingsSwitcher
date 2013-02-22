namespace Twainsoft.VSSettingsSwitcher.BLL.Options.VSSettings.Create
{
    partial class CreateOptionsPage
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
            this.saveSettings = new System.Windows.Forms.Button();
            this.fileNameText = new System.Windows.Forms.Label();
            this.fileName = new System.Windows.Forms.TextBox();
            this.saveCurrentSettingsBox = new System.Windows.Forms.GroupBox();
            this.explanationText = new System.Windows.Forms.Label();
            this.selectedSettingsOptions = new System.Windows.Forms.TreeView();
            this.saveCurrentSettingsBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // saveSettings
            // 
            this.saveSettings.Enabled = false;
            this.saveSettings.Location = new System.Drawing.Point(6, 75);
            this.saveSettings.Name = "saveSettings";
            this.saveSettings.Size = new System.Drawing.Size(106, 23);
            this.saveSettings.TabIndex = 0;
            this.saveSettings.Text = "Save settings";
            this.saveSettings.UseVisualStyleBackColor = true;
            this.saveSettings.Click += new System.EventHandler(this.saveSettings_Click);
            // 
            // fileNameText
            // 
            this.fileNameText.AutoSize = true;
            this.fileNameText.Location = new System.Drawing.Point(6, 52);
            this.fileNameText.Name = "fileNameText";
            this.fileNameText.Size = new System.Drawing.Size(38, 13);
            this.fileNameText.TabIndex = 2;
            this.fileNameText.Text = "Name:";
            // 
            // fileName
            // 
            this.fileName.Location = new System.Drawing.Point(50, 49);
            this.fileName.Name = "fileName";
            this.fileName.Size = new System.Drawing.Size(337, 20);
            this.fileName.TabIndex = 3;
            this.fileName.TextChanged += new System.EventHandler(this.fileName_TextChanged);
            // 
            // saveCurrentSettingsBox
            // 
            this.saveCurrentSettingsBox.Controls.Add(this.explanationText);
            this.saveCurrentSettingsBox.Controls.Add(this.fileName);
            this.saveCurrentSettingsBox.Controls.Add(this.saveSettings);
            this.saveCurrentSettingsBox.Controls.Add(this.fileNameText);
            this.saveCurrentSettingsBox.Location = new System.Drawing.Point(0, 0);
            this.saveCurrentSettingsBox.Name = "saveCurrentSettingsBox";
            this.saveCurrentSettingsBox.Size = new System.Drawing.Size(393, 110);
            this.saveCurrentSettingsBox.TabIndex = 4;
            this.saveCurrentSettingsBox.TabStop = false;
            this.saveCurrentSettingsBox.Text = "Save current settings";
            // 
            // explanationText
            // 
            this.explanationText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.explanationText.Location = new System.Drawing.Point(6, 16);
            this.explanationText.Name = "explanationText";
            this.explanationText.Size = new System.Drawing.Size(387, 30);
            this.explanationText.TabIndex = 4;
            this.explanationText.Text = "The current loaded settings were save to the default settings path. You can find " +
    "this path under \"Environment -> Import and Export Settings\" in the options menu." +
    "";
            // 
            // selectedSettingsOptions
            // 
            this.selectedSettingsOptions.Location = new System.Drawing.Point(3, 116);
            this.selectedSettingsOptions.Name = "selectedSettingsOptions";
            this.selectedSettingsOptions.Size = new System.Drawing.Size(384, 167);
            this.selectedSettingsOptions.TabIndex = 5;
            // 
            // CreateOptionsPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.selectedSettingsOptions);
            this.Controls.Add(this.saveCurrentSettingsBox);
            this.Name = "CreateOptionsPage";
            this.saveCurrentSettingsBox.ResumeLayout(false);
            this.saveCurrentSettingsBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button saveSettings;
        private System.Windows.Forms.Label fileNameText;
        private System.Windows.Forms.TextBox fileName;
        private System.Windows.Forms.GroupBox saveCurrentSettingsBox;
        private System.Windows.Forms.Label explanationText;
        private System.Windows.Forms.TreeView selectedSettingsOptions;

    }
}
