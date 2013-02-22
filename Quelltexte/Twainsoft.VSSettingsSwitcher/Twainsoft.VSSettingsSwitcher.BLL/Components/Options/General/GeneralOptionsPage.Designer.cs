namespace Twainsoft.VSSettingsSwitcher.BLL.Options.General
{
    partial class GeneralOptionsPage
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
            this.autoLoadAndFormatBox = new System.Windows.Forms.GroupBox();
            this.autoSaveDocuments = new System.Windows.Forms.CheckBox();
            this.autoFormatOpeningDocuments = new System.Windows.Forms.CheckBox();
            this.formatAllOpenDocuments = new System.Windows.Forms.CheckBox();
            this.saveAllOpenDocuments = new System.Windows.Forms.CheckBox();
            this.autoLoadAndFormatBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // autoLoadAndFormatBox
            // 
            this.autoLoadAndFormatBox.Controls.Add(this.saveAllOpenDocuments);
            this.autoLoadAndFormatBox.Controls.Add(this.formatAllOpenDocuments);
            this.autoLoadAndFormatBox.Controls.Add(this.autoSaveDocuments);
            this.autoLoadAndFormatBox.Controls.Add(this.autoFormatOpeningDocuments);
            this.autoLoadAndFormatBox.Location = new System.Drawing.Point(0, 0);
            this.autoLoadAndFormatBox.Name = "autoLoadAndFormatBox";
            this.autoLoadAndFormatBox.Size = new System.Drawing.Size(390, 111);
            this.autoLoadAndFormatBox.TabIndex = 0;
            this.autoLoadAndFormatBox.TabStop = false;
            this.autoLoadAndFormatBox.Text = "Auto load && format";
            // 
            // autoSaveDocuments
            // 
            this.autoSaveDocuments.AutoSize = true;
            this.autoSaveDocuments.Enabled = false;
            this.autoSaveDocuments.Location = new System.Drawing.Point(22, 43);
            this.autoSaveDocuments.Name = "autoSaveDocuments";
            this.autoSaveDocuments.Size = new System.Drawing.Size(271, 17);
            this.autoSaveDocuments.TabIndex = 2;
            this.autoSaveDocuments.Text = "Auto save these documents if changes were made?";
            this.autoSaveDocuments.UseVisualStyleBackColor = true;
            // 
            // autoFormatOpeningDocuments
            // 
            this.autoFormatOpeningDocuments.AutoSize = true;
            this.autoFormatOpeningDocuments.Location = new System.Drawing.Point(6, 20);
            this.autoFormatOpeningDocuments.Name = "autoFormatOpeningDocuments";
            this.autoFormatOpeningDocuments.Size = new System.Drawing.Size(318, 17);
            this.autoFormatOpeningDocuments.TabIndex = 0;
            this.autoFormatOpeningDocuments.Text = "Format newly opened documents with the default settings file?";
            this.autoFormatOpeningDocuments.UseVisualStyleBackColor = true;
            this.autoFormatOpeningDocuments.CheckedChanged += new System.EventHandler(this.autoFormatOpeningDocuments_CheckedChanged);
            // 
            // formatAllOpenDocuments
            // 
            this.formatAllOpenDocuments.AutoSize = true;
            this.formatAllOpenDocuments.Location = new System.Drawing.Point(6, 66);
            this.formatAllOpenDocuments.Name = "formatAllOpenDocuments";
            this.formatAllOpenDocuments.Size = new System.Drawing.Size(368, 17);
            this.formatAllOpenDocuments.TabIndex = 3;
            this.formatAllOpenDocuments.Text = "Format all current open documents when a new settings file was loaded?";
            this.formatAllOpenDocuments.UseVisualStyleBackColor = true;
            this.formatAllOpenDocuments.CheckedChanged += new System.EventHandler(this.formatAllOpenDocuments_CheckedChanged);
            // 
            // saveAllOpenDocuments
            // 
            this.saveAllOpenDocuments.AutoSize = true;
            this.saveAllOpenDocuments.Enabled = false;
            this.saveAllOpenDocuments.Location = new System.Drawing.Point(22, 89);
            this.saveAllOpenDocuments.Name = "saveAllOpenDocuments";
            this.saveAllOpenDocuments.Size = new System.Drawing.Size(271, 17);
            this.saveAllOpenDocuments.TabIndex = 4;
            this.saveAllOpenDocuments.Text = "Auto save these documents if changes were made?";
            this.saveAllOpenDocuments.UseVisualStyleBackColor = true;
            // 
            // GeneralOptionsPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.autoLoadAndFormatBox);
            this.Name = "GeneralOptionsPage";
            this.autoLoadAndFormatBox.ResumeLayout(false);
            this.autoLoadAndFormatBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox autoLoadAndFormatBox;
        private System.Windows.Forms.CheckBox autoSaveDocuments;
        private System.Windows.Forms.CheckBox autoFormatOpeningDocuments;
        private System.Windows.Forms.CheckBox saveAllOpenDocuments;
        private System.Windows.Forms.CheckBox formatAllOpenDocuments;

    }
}
