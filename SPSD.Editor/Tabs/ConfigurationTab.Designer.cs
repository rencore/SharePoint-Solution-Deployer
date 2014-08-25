using SPSD.Editor.Controls;

namespace SPSD.Editor
{
    partial class ConfigurationTab
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
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage_Settings = new System.Windows.Forms.TabPage();
            this.settingsSection = new SPSD.Editor.Sections.SettingsSection();
            this.tabPage_Restrictions = new System.Windows.Forms.TabPage();
            this.restrictionsSection = new SPSD.Editor.Sections.RestrictionsSection();
            this.tabPage_Actions = new System.Windows.Forms.TabPage();
            this.actionsSection = new SPSD.Editor.Sections.ActionsSection();
            this.externalNodeReference = new SPSD.Editor.Controls.ExternalNodeReference();
            this.siteStructuresTab1 = new SPSD.Editor.Tabs.SiteStructuresTab();
            this.tabControl.SuspendLayout();
            this.tabPage_Settings.SuspendLayout();
            this.tabPage_Restrictions.SuspendLayout();
            this.tabPage_Actions.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.tabPage_Settings);
            this.tabControl.Controls.Add(this.tabPage_Restrictions);
            this.tabControl.Controls.Add(this.tabPage_Actions);
            this.tabControl.Location = new System.Drawing.Point(0, 78);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(699, 404);
            this.tabControl.TabIndex = 14;
            // 
            // tabPage_Settings
            // 
            this.tabPage_Settings.AutoScroll = true;
            this.tabPage_Settings.Controls.Add(this.settingsSection);
            this.tabPage_Settings.Location = new System.Drawing.Point(4, 22);
            this.tabPage_Settings.Name = "tabPage_Settings";
            this.tabPage_Settings.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_Settings.Size = new System.Drawing.Size(691, 378);
            this.tabPage_Settings.TabIndex = 0;
            this.tabPage_Settings.Text = "Settings";
            this.tabPage_Settings.UseVisualStyleBackColor = true;
            // 
            // settingsSection
            // 
            this.settingsSection.AutoSize = true;
            this.settingsSection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.settingsSection.Location = new System.Drawing.Point(3, 3);
            this.settingsSection.Name = "settingsSection";
            this.settingsSection.Size = new System.Drawing.Size(685, 372);
            this.settingsSection.TabIndex = 11;
            // 
            // tabPage_Restrictions
            // 
            this.tabPage_Restrictions.AutoScroll = true;
            this.tabPage_Restrictions.Controls.Add(this.restrictionsSection);
            this.tabPage_Restrictions.Location = new System.Drawing.Point(4, 22);
            this.tabPage_Restrictions.Name = "tabPage_Restrictions";
            this.tabPage_Restrictions.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_Restrictions.Size = new System.Drawing.Size(691, 378);
            this.tabPage_Restrictions.TabIndex = 1;
            this.tabPage_Restrictions.Text = "Restrictions";
            this.tabPage_Restrictions.UseVisualStyleBackColor = true;
            // 
            // restrictionsSection
            // 
            this.restrictionsSection.AutoScroll = true;
            this.restrictionsSection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.restrictionsSection.Location = new System.Drawing.Point(3, 3);
            this.restrictionsSection.Name = "restrictionsSection";
            this.restrictionsSection.Size = new System.Drawing.Size(685, 372);
            this.restrictionsSection.TabIndex = 14;
            // 
            // tabPage_Actions
            // 
            this.tabPage_Actions.AutoScroll = true;
            this.tabPage_Actions.AutoScrollMinSize = new System.Drawing.Size(0, 410);
            this.tabPage_Actions.Controls.Add(this.actionsSection);
            this.tabPage_Actions.Location = new System.Drawing.Point(4, 22);
            this.tabPage_Actions.Name = "tabPage_Actions";
            this.tabPage_Actions.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_Actions.Size = new System.Drawing.Size(691, 378);
            this.tabPage_Actions.TabIndex = 2;
            this.tabPage_Actions.Text = "Actions";
            this.tabPage_Actions.UseVisualStyleBackColor = true;
            // 
            // actionsSection
            // 
            this.actionsSection.AutoScroll = true;
            this.actionsSection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.actionsSection.Location = new System.Drawing.Point(3, 3);
            this.actionsSection.MinimumSize = new System.Drawing.Size(670, 410);
            this.actionsSection.Name = "actionsSection";
            this.actionsSection.Size = new System.Drawing.Size(670, 410);
            this.actionsSection.TabIndex = 13;
            // 
            // externalNodeReference
            // 
            this.externalNodeReference.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.externalNodeReference.ControlLocationName = "configuration tab";
            this.externalNodeReference.Location = new System.Drawing.Point(0, 0);
            this.externalNodeReference.Name = "externalNodeReference";
            this.externalNodeReference.Size = new System.Drawing.Size(699, 72);
            this.externalNodeReference.TabIndex = 11;
            this.externalNodeReference.XPath = "SPSD/Configuration";
            this.externalNodeReference.NodeReferenceHasChanged += new System.EventHandler(this.ExternalNodeReference_IsExternalChanged);
            // 
            // siteStructuresTab1
            // 
            this.siteStructuresTab1.Location = new System.Drawing.Point(183, 146);
            this.siteStructuresTab1.Name = "siteStructuresTab1";
            this.siteStructuresTab1.Size = new System.Drawing.Size(150, 150);
            this.siteStructuresTab1.TabIndex = 9;
            // 
            // ConfigurationTab
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.externalNodeReference);
            this.Controls.Add(this.siteStructuresTab1);
            this.MinimumSize = new System.Drawing.Size(680, 440);
            this.Name = "ConfigurationTab";
            this.Size = new System.Drawing.Size(699, 482);
            this.tabControl.ResumeLayout(false);
            this.tabPage_Settings.ResumeLayout(false);
            this.tabPage_Settings.PerformLayout();
            this.tabPage_Restrictions.ResumeLayout(false);
            this.tabPage_Actions.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Tabs.SiteStructuresTab siteStructuresTab1;
        private ExternalNodeReference externalNodeReference;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPage_Settings;
        private System.Windows.Forms.TabPage tabPage_Restrictions;
        private Sections.SettingsSection settingsSection;
        private Sections.RestrictionsSection restrictionsSection;
        private System.Windows.Forms.TabPage tabPage_Actions;
        private Sections.ActionsSection actionsSection;
    }
}
