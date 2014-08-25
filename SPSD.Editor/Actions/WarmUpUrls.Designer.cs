namespace SPSD.Editor.Actions
{
    partial class WarmUpUrls
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WarmUpUrls));
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.button_Warmup_EditCustom = new System.Windows.Forms.Button();
            this.checkBox_Action_CustomUrl = new System.Windows.Forms.CheckBox();
            this.checkBox_Action_AllSites = new System.Windows.Forms.CheckBox();
            this.checkBox_Action_AllWebApps = new System.Windows.Forms.CheckBox();
            this.checkBox_Action_update = new System.Windows.Forms.CheckBox();
            this.checkBox_Action_deploy = new System.Windows.Forms.CheckBox();
            this.checkBox_Action = new System.Windows.Forms.CheckBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.textBox_Warmup_customUrls = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // button_Warmup_EditCustom
            // 
            this.button_Warmup_EditCustom.Enabled = false;
            this.button_Warmup_EditCustom.Location = new System.Drawing.Point(608, 80);
            this.button_Warmup_EditCustom.Name = "button_Warmup_EditCustom";
            this.button_Warmup_EditCustom.Size = new System.Drawing.Size(38, 24);
            this.button_Warmup_EditCustom.TabIndex = 78;
            this.button_Warmup_EditCustom.Text = "Edit";
            this.toolTip.SetToolTip(this.button_Warmup_EditCustom, "Edit custom Url");
            this.button_Warmup_EditCustom.UseVisualStyleBackColor = true;
            this.button_Warmup_EditCustom.Click += new System.EventHandler(this.Button_Warmup_EditCustomUrls_Click);
            // 
            // checkBox_Action_CustomUrl
            // 
            this.checkBox_Action_CustomUrl.AutoSize = true;
            this.checkBox_Action_CustomUrl.Location = new System.Drawing.Point(185, 60);
            this.checkBox_Action_CustomUrl.Name = "checkBox_Action_CustomUrl";
            this.checkBox_Action_CustomUrl.Size = new System.Drawing.Size(210, 17);
            this.checkBox_Action_CustomUrl.TabIndex = 73;
            this.checkBox_Action_CustomUrl.Text = "Custom Urls (seperated with semicolon)";
            this.toolTip.SetToolTip(this.checkBox_Action_CustomUrl, "Warms up custom Urls");
            this.checkBox_Action_CustomUrl.UseVisualStyleBackColor = true;
            this.checkBox_Action_CustomUrl.CheckedChanged += new System.EventHandler(this.CheckBox_Warmup_CustomUrl_Changed);
            this.checkBox_Action_CustomUrl.EnabledChanged += new System.EventHandler(this.CheckBox_Warmup_CustomUrl_Changed);
            // 
            // checkBox_Action_AllSites
            // 
            this.checkBox_Action_AllSites.AutoSize = true;
            this.checkBox_Action_AllSites.Location = new System.Drawing.Point(185, 40);
            this.checkBox_Action_AllSites.Name = "checkBox_Action_AllSites";
            this.checkBox_Action_AllSites.Size = new System.Drawing.Size(109, 17);
            this.checkBox_Action_AllSites.TabIndex = 74;
            this.checkBox_Action_AllSites.Text = "All SiteCollections";
            this.toolTip.SetToolTip(this.checkBox_Action_AllSites, "Warms up all SharePoint SiteCollections. \r\nOnly recommended in development enviro" +
        "nments.");
            this.checkBox_Action_AllSites.UseVisualStyleBackColor = true;
            this.checkBox_Action_AllSites.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            // 
            // checkBox_Action_AllWebApps
            // 
            this.checkBox_Action_AllWebApps.AutoSize = true;
            this.checkBox_Action_AllWebApps.Checked = true;
            this.checkBox_Action_AllWebApps.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_Action_AllWebApps.Location = new System.Drawing.Point(185, 20);
            this.checkBox_Action_AllWebApps.Name = "checkBox_Action_AllWebApps";
            this.checkBox_Action_AllWebApps.Size = new System.Drawing.Size(120, 17);
            this.checkBox_Action_AllWebApps.TabIndex = 75;
            this.checkBox_Action_AllWebApps.Text = "All WebApplications";
            this.toolTip.SetToolTip(this.checkBox_Action_AllWebApps, "Warms up all SharePoint WebApplications");
            this.checkBox_Action_AllWebApps.UseVisualStyleBackColor = true;
            this.checkBox_Action_AllWebApps.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            // 
            // checkBox_Action_update
            // 
            this.checkBox_Action_update.AutoSize = true;
            this.checkBox_Action_update.Checked = true;
            this.checkBox_Action_update.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_Action_update.Location = new System.Drawing.Point(521, 21);
            this.checkBox_Action_update.Name = "checkBox_Action_update";
            this.checkBox_Action_update.Size = new System.Drawing.Size(15, 14);
            this.checkBox_Action_update.TabIndex = 76;
            this.toolTip.SetToolTip(this.checkBox_Action_update, "Run the actions after an update of solutions");
            this.checkBox_Action_update.UseVisualStyleBackColor = true;
            this.checkBox_Action_update.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            // 
            // checkBox_Action_deploy
            // 
            this.checkBox_Action_deploy.AutoSize = true;
            this.checkBox_Action_deploy.Checked = true;
            this.checkBox_Action_deploy.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_Action_deploy.Location = new System.Drawing.Point(437, 21);
            this.checkBox_Action_deploy.Name = "checkBox_Action_deploy";
            this.checkBox_Action_deploy.Size = new System.Drawing.Size(15, 14);
            this.checkBox_Action_deploy.TabIndex = 72;
            this.toolTip.SetToolTip(this.checkBox_Action_deploy, "Run the actions after the deployment of solutions");
            this.checkBox_Action_deploy.UseVisualStyleBackColor = true;
            this.checkBox_Action_deploy.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            // 
            // checkBox_Action
            // 
            this.checkBox_Action.Checked = true;
            this.checkBox_Action.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_Action.Location = new System.Drawing.Point(0, 20);
            this.checkBox_Action.Name = "checkBox_Action";
            this.checkBox_Action.Size = new System.Drawing.Size(180, 17);
            this.checkBox_Action.TabIndex = 71;
            this.checkBox_Action.Text = "Warmup URLs";
            this.toolTip.SetToolTip(this.checkBox_Action, resources.GetString("checkBox_Action.ToolTip"));
            this.checkBox_Action.CheckedChanged += new System.EventHandler(this.CheckBox_Warmup_CheckedChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(490, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(77, 13);
            this.label10.TabIndex = 80;
            this.label10.Text = "After update";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(406, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(75, 13);
            this.label9.TabIndex = 81;
            this.label9.Text = "After deploy";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox_Warmup_customUrls
            // 
            this.textBox_Warmup_customUrls.Enabled = false;
            this.textBox_Warmup_customUrls.Location = new System.Drawing.Point(204, 80);
            this.textBox_Warmup_customUrls.Multiline = true;
            this.textBox_Warmup_customUrls.Name = "textBox_Warmup_customUrls";
            this.textBox_Warmup_customUrls.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox_Warmup_customUrls.Size = new System.Drawing.Size(398, 87);
            this.textBox_Warmup_customUrls.TabIndex = 77;
            this.textBox_Warmup_customUrls.EnabledChanged += new System.EventHandler(this.TextBox_Warmup_CustomUrls_EnabledChanged);
            this.textBox_Warmup_customUrls.TextChanged += new System.EventHandler(this.TextBox_Warmup_CustomUrls_TextChanged);
            // 
            // WarmUpUrls
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.button_Warmup_EditCustom);
            this.Controls.Add(this.textBox_Warmup_customUrls);
            this.Controls.Add(this.checkBox_Action_CustomUrl);
            this.Controls.Add(this.checkBox_Action_AllSites);
            this.Controls.Add(this.checkBox_Action_AllWebApps);
            this.Controls.Add(this.checkBox_Action_update);
            this.Controls.Add(this.checkBox_Action_deploy);
            this.Controls.Add(this.checkBox_Action);
            this.Name = "WarmUpUrls";
            this.Size = new System.Drawing.Size(651, 172);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button button_Warmup_EditCustom;
        private System.Windows.Forms.TextBox textBox_Warmup_customUrls;
        private System.Windows.Forms.CheckBox checkBox_Action_CustomUrl;
        private System.Windows.Forms.CheckBox checkBox_Action_AllSites;
        private System.Windows.Forms.CheckBox checkBox_Action_AllWebApps;
        private System.Windows.Forms.CheckBox checkBox_Action_update;
        private System.Windows.Forms.CheckBox checkBox_Action_deploy;
        private System.Windows.Forms.CheckBox checkBox_Action;
    }
}
