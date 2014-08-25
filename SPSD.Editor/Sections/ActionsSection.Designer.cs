namespace SPSD.Editor.Sections
{
    partial class ActionsSection
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ActionsSection));
            this.mainPanel = new System.Windows.Forms.Panel();
            this.ActionWarmUpUrls = new SPSD.Editor.Actions.WarmUpUrls();
            this.ActionRecycleAppPools = new SPSD.Editor.Actions.RecycleAppPools();
            this.ActionCustom = new SPSD.Editor.Actions.RestartService();
            this.ActionSPUserCodeV4 = new SPSD.Editor.Actions.RestartService();
            this.ActionSPAdminV4 = new SPSD.Editor.Actions.RestartService();
            this.ActionSPTimerV4 = new SPSD.Editor.Actions.RestartService();
            this.ActionResetIIS = new SPSD.Editor.Actions.ResetIIS();
            this.label6 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.ActionIISReset = new SPSD.Editor.Actions.ResetIIS();
            this.mainPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainPanel
            // 
            this.mainPanel.Controls.Add(this.ActionWarmUpUrls);
            this.mainPanel.Controls.Add(this.ActionRecycleAppPools);
            this.mainPanel.Controls.Add(this.ActionCustom);
            this.mainPanel.Controls.Add(this.ActionSPUserCodeV4);
            this.mainPanel.Controls.Add(this.ActionSPAdminV4);
            this.mainPanel.Controls.Add(this.ActionSPTimerV4);
            this.mainPanel.Controls.Add(this.ActionResetIIS);
            this.mainPanel.Controls.Add(this.label6);
            this.mainPanel.Controls.Add(this.label26);
            this.mainPanel.Controls.Add(this.label25);
            this.mainPanel.Controls.Add(this.label24);
            this.mainPanel.Controls.Add(this.label23);
            this.mainPanel.Controls.Add(this.label3);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(0, 0);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(826, 450);
            this.mainPanel.TabIndex = 3;
            this.mainPanel.Text = "Actions";
            // 
            // ActionWarmUpUrls
            // 
            this.ActionWarmUpUrls.Location = new System.Drawing.Point(3, 240);
            this.ActionWarmUpUrls.Name = "ActionWarmUpUrls";
            this.ActionWarmUpUrls.Size = new System.Drawing.Size(707, 172);
            this.ActionWarmUpUrls.TabIndex = 82;
            // 
            // ActionRecycleAppPools
            // 
            this.ActionRecycleAppPools.Location = new System.Drawing.Point(3, 184);
            this.ActionRecycleAppPools.Margin = new System.Windows.Forms.Padding(0);
            this.ActionRecycleAppPools.Name = "ActionRecycleAppPools";
            this.ActionRecycleAppPools.Size = new System.Drawing.Size(661, 55);
            this.ActionRecycleAppPools.TabIndex = 81;
            // 
            // ActionCustom
            // 
            this.ActionCustom.Location = new System.Drawing.Point(3, 156);
            this.ActionCustom.Margin = new System.Windows.Forms.Padding(0);
            this.ActionCustom.Name = "ActionCustom";
            this.ActionCustom.ServiceType = SPSD.Editor.Actions.RestartService.RestartServiceActionType.Custom;
            this.ActionCustom.Size = new System.Drawing.Size(662, 25);
            this.ActionCustom.TabIndex = 80;
            // 
            // ActionSPUserCodeV4
            // 
            this.ActionSPUserCodeV4.Location = new System.Drawing.Point(3, 131);
            this.ActionSPUserCodeV4.Margin = new System.Windows.Forms.Padding(0);
            this.ActionSPUserCodeV4.Name = "ActionSPUserCodeV4";
            this.ActionSPUserCodeV4.ServiceType = SPSD.Editor.Actions.RestartService.RestartServiceActionType.SPUserCodeV4;
            this.ActionSPUserCodeV4.Size = new System.Drawing.Size(662, 25);
            this.ActionSPUserCodeV4.TabIndex = 79;
            // 
            // ActionSPAdminV4
            // 
            this.ActionSPAdminV4.Location = new System.Drawing.Point(3, 106);
            this.ActionSPAdminV4.Margin = new System.Windows.Forms.Padding(0);
            this.ActionSPAdminV4.Name = "ActionSPAdminV4";
            this.ActionSPAdminV4.ServiceType = SPSD.Editor.Actions.RestartService.RestartServiceActionType.SPAdminV4;
            this.ActionSPAdminV4.Size = new System.Drawing.Size(662, 25);
            this.ActionSPAdminV4.TabIndex = 78;
            // 
            // ActionSPTimerV4
            // 
            this.ActionSPTimerV4.Location = new System.Drawing.Point(3, 81);
            this.ActionSPTimerV4.Margin = new System.Windows.Forms.Padding(0);
            this.ActionSPTimerV4.Name = "ActionSPTimerV4";
            this.ActionSPTimerV4.ServiceType = SPSD.Editor.Actions.RestartService.RestartServiceActionType.SPTimerV4;
            this.ActionSPTimerV4.Size = new System.Drawing.Size(662, 25);
            this.ActionSPTimerV4.TabIndex = 77;
            // 
            // ActionResetIIS
            // 
            this.ActionResetIIS.Location = new System.Drawing.Point(3, 56);
            this.ActionResetIIS.Margin = new System.Windows.Forms.Padding(0);
            this.ActionResetIIS.Name = "ActionResetIIS";
            this.ActionResetIIS.Size = new System.Drawing.Size(662, 25);
            this.ActionResetIIS.TabIndex = 76;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(207, 43);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 13);
            this.label6.TabIndex = 28;
            this.label6.Text = "Servicename";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.Location = new System.Drawing.Point(324, 43);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(79, 13);
            this.label26.TabIndex = 28;
            this.label26.Text = "Force Action";
            this.label26.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip.SetToolTip(this.label26, "Forces the action i.e. even if the service is not running before.");
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label25.Location = new System.Drawing.Point(493, 43);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(77, 13);
            this.label25.TabIndex = 29;
            this.label25.Text = "After update";
            this.label25.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip.SetToolTip(this.label25, "Run the actions after an update of solutions");
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.Location = new System.Drawing.Point(579, 43);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(75, 13);
            this.label24.TabIndex = 30;
            this.label24.Text = "After retract";
            this.label24.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip.SetToolTip(this.label24, "Run the actions after the retraction of solutions");
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.Location = new System.Drawing.Point(409, 43);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(75, 13);
            this.label23.TabIndex = 31;
            this.label23.Text = "After deploy";
            this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip.SetToolTip(this.label23, "Run the actions after the deployment of solutions");
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(826, 43);
            this.label3.TabIndex = 25;
            this.label3.Text = resources.GetString("label3.Text");
            // 
            // ActionIISReset
            // 
            this.ActionIISReset.Location = new System.Drawing.Point(3, 43);
            this.ActionIISReset.Margin = new System.Windows.Forms.Padding(0);
            this.ActionIISReset.Name = "ActionIISReset";
            this.ActionIISReset.Size = new System.Drawing.Size(662, 25);
            this.ActionIISReset.TabIndex = 76;
            // 
            // ActionsSection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.mainPanel);
            this.Name = "ActionsSection";
            this.Size = new System.Drawing.Size(826, 450);
            this.mainPanel.ResumeLayout(false);
            this.mainPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Label label6;
        private Actions.ResetIIS ActionResetIIS;
        private Actions.RestartService ActionCustom;
        private Actions.RestartService ActionSPUserCodeV4;
        private Actions.RestartService ActionSPAdminV4;
        private Actions.RestartService ActionSPTimerV4;
        private Actions.WarmUpUrls ActionWarmUpUrls;
        private Actions.RecycleAppPools ActionRecycleAppPools;
        private Actions.ResetIIS ActionIISReset;
    }
}
