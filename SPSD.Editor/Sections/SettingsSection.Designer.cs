namespace SPSD.Editor.Sections
{
    partial class SettingsSection
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsSection));
            this.mainPanel = new System.Windows.Forms.Panel();
            this.comboBox_RunOnMultipleServersInFarm = new System.Windows.Forms.ComboBox();
            this.radioButton_WaitAfterDeployment = new System.Windows.Forms.RadioButton();
            this.radioButton_PauseAfterDeployment = new System.Windows.Forms.RadioButton();
            this.textBox_WaitAfterDeploy = new System.Windows.Forms.TextBox();
            this.textBox_DeployTimeout = new System.Windows.Forms.TextBox();
            this.textBox_DeployRetries = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label_DeployRetries = new System.Windows.Forms.Label();
            this.label_WaitAfterDeploy = new System.Windows.Forms.Label();
            this.label_DeployTimeout = new System.Windows.Forms.Label();
            this.label_ConfSettings = new System.Windows.Forms.Label();
            this.checkBox_RunOnMultipleServersInFarm = new System.Windows.Forms.CheckBox();
            this.checkBox_CreateULSLog = new System.Windows.Forms.CheckBox();
            this.checkBox_DisplayWizards = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.textBox_WaitAfterDeploy_val = new SPSD.Editor.Controls.ValidationIndicator();
            this.textBox_DeployTimeout_val = new SPSD.Editor.Controls.ValidationIndicator();
            this.textBox_DeployRetries_val = new SPSD.Editor.Controls.ValidationIndicator();
            this.mainPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainPanel
            // 
            this.mainPanel.Controls.Add(this.comboBox_RunOnMultipleServersInFarm);
            this.mainPanel.Controls.Add(this.radioButton_WaitAfterDeployment);
            this.mainPanel.Controls.Add(this.radioButton_PauseAfterDeployment);
            this.mainPanel.Controls.Add(this.textBox_WaitAfterDeploy_val);
            this.mainPanel.Controls.Add(this.textBox_DeployTimeout_val);
            this.mainPanel.Controls.Add(this.textBox_DeployRetries_val);
            this.mainPanel.Controls.Add(this.label17);
            this.mainPanel.Controls.Add(this.label18);
            this.mainPanel.Controls.Add(this.label15);
            this.mainPanel.Controls.Add(this.textBox_DeployRetries);
            this.mainPanel.Controls.Add(this.textBox_WaitAfterDeploy);
            this.mainPanel.Controls.Add(this.textBox_DeployTimeout);
            this.mainPanel.Controls.Add(this.label_DeployRetries);
            this.mainPanel.Controls.Add(this.label_WaitAfterDeploy);
            this.mainPanel.Controls.Add(this.label_DeployTimeout);
            this.mainPanel.Controls.Add(this.label_ConfSettings);
            this.mainPanel.Controls.Add(this.checkBox_RunOnMultipleServersInFarm);
            this.mainPanel.Controls.Add(this.checkBox_CreateULSLog);
            this.mainPanel.Controls.Add(this.checkBox_DisplayWizards);
            this.mainPanel.Controls.Add(this.label1);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(0, 0);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(578, 236);
            this.mainPanel.TabIndex = 1;
            this.mainPanel.Text = "Settings";
            // 
            // comboBox_RunOnMultipleServersInFarm
            // 
            this.comboBox_RunOnMultipleServersInFarm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_RunOnMultipleServersInFarm.FormattingEnabled = true;
            this.comboBox_RunOnMultipleServersInFarm.Items.AddRange(new object[] {
            "all",
            "WebFrontEnd",
            "Application"});
            this.comboBox_RunOnMultipleServersInFarm.Location = new System.Drawing.Point(217, 137);
            this.comboBox_RunOnMultipleServersInFarm.Name = "comboBox_RunOnMultipleServersInFarm";
            this.comboBox_RunOnMultipleServersInFarm.Size = new System.Drawing.Size(121, 21);
            this.comboBox_RunOnMultipleServersInFarm.TabIndex = 32;
            this.comboBox_RunOnMultipleServersInFarm.Tag = "all";
            this.toolTip.SetToolTip(this.comboBox_RunOnMultipleServersInFarm, "Specifies if prerequisite checks and actions should be run on multiple servers in" +
        " the farm or only the local server.");
            this.comboBox_RunOnMultipleServersInFarm.SelectedIndexChanged += new System.EventHandler(this.Control_Changed);
            // 
            // radioButton_WaitAfterDeployment
            // 
            this.radioButton_WaitAfterDeployment.AutoSize = true;
            this.radioButton_WaitAfterDeployment.Checked = true;
            this.radioButton_WaitAfterDeployment.Location = new System.Drawing.Point(131, 93);
            this.radioButton_WaitAfterDeployment.Name = "radioButton_WaitAfterDeployment";
            this.radioButton_WaitAfterDeployment.Size = new System.Drawing.Size(14, 13);
            this.radioButton_WaitAfterDeployment.TabIndex = 31;
            this.radioButton_WaitAfterDeployment.TabStop = true;
            this.radioButton_WaitAfterDeployment.UseVisualStyleBackColor = true;
            this.radioButton_WaitAfterDeployment.CheckedChanged += new System.EventHandler(this.RadioButton_WaitAfterDeployment_CheckedChanged);
            // 
            // radioButton_PauseAfterDeployment
            // 
            this.radioButton_PauseAfterDeployment.AutoSize = true;
            this.radioButton_PauseAfterDeployment.Location = new System.Drawing.Point(131, 115);
            this.radioButton_PauseAfterDeployment.Name = "radioButton_PauseAfterDeployment";
            this.radioButton_PauseAfterDeployment.Size = new System.Drawing.Size(55, 17);
            this.radioButton_PauseAfterDeployment.TabIndex = 30;
            this.radioButton_PauseAfterDeployment.Text = "Pause";
            this.toolTip.SetToolTip(this.radioButton_PauseAfterDeployment, "PowerShell window remains open after completion");
            this.radioButton_PauseAfterDeployment.UseVisualStyleBackColor = true;
            this.radioButton_PauseAfterDeployment.CheckedChanged += new System.EventHandler(this.RadioButton_WaitAfterDeployment_CheckedChanged);
            // 
            // textBox_WaitAfterDeploy
            // 
            this.textBox_WaitAfterDeploy.Location = new System.Drawing.Point(150, 89);
            this.textBox_WaitAfterDeploy.Name = "textBox_WaitAfterDeploy";
            this.textBox_WaitAfterDeploy.Size = new System.Drawing.Size(66, 20);
            this.textBox_WaitAfterDeploy.TabIndex = 22;
            this.textBox_WaitAfterDeploy.Tag = "10000";
            this.textBox_WaitAfterDeploy.Text = "10000";
            this.textBox_WaitAfterDeploy.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.toolTip.SetToolTip(this.textBox_WaitAfterDeploy, "Number of milliseconds to leave the deployment script window open after the deplo" +
        "yment or \"pause\" to remain it open indefinetly (default: 10000ms)");
            this.textBox_WaitAfterDeploy.WordWrap = false;
            this.textBox_WaitAfterDeploy.TextChanged += new System.EventHandler(this.Control_Changed);
            // 
            // textBox_DeployTimeout
            // 
            this.textBox_DeployTimeout.Location = new System.Drawing.Point(150, 64);
            this.textBox_DeployTimeout.Name = "textBox_DeployTimeout";
            this.textBox_DeployTimeout.Size = new System.Drawing.Size(66, 20);
            this.textBox_DeployTimeout.TabIndex = 23;
            this.textBox_DeployTimeout.Tag = "60000";
            this.textBox_DeployTimeout.Text = "60000";
            this.textBox_DeployTimeout.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.toolTip.SetToolTip(this.textBox_DeployTimeout, "Number of milliseconds to wait for processes, services (default: 60000ms)");
            this.textBox_DeployTimeout.WordWrap = false;
            this.textBox_DeployTimeout.TextChanged += new System.EventHandler(this.Control_Changed);
            // 
            // textBox_DeployRetries
            // 
            this.textBox_DeployRetries.Location = new System.Drawing.Point(150, 41);
            this.textBox_DeployRetries.Name = "textBox_DeployRetries";
            this.textBox_DeployRetries.Size = new System.Drawing.Size(66, 20);
            this.textBox_DeployRetries.TabIndex = 21;
            this.textBox_DeployRetries.Tag = "3";
            this.textBox_DeployRetries.Text = "3";
            this.textBox_DeployRetries.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.toolTip.SetToolTip(this.textBox_DeployRetries, "Number of retries if solution deployment fails (default: 3)");
            this.textBox_DeployRetries.WordWrap = false;
            this.textBox_DeployRetries.TextChanged += new System.EventHandler(this.Control_Changed);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(218, 44);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(31, 13);
            this.label17.TabIndex = 24;
            this.label17.Text = "times";
            this.toolTip.SetToolTip(this.label17, "Number of retries if solution deployment fails (default: 3)");
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(218, 92);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(20, 13);
            this.label18.TabIndex = 25;
            this.label18.Text = "ms";
            this.toolTip.SetToolTip(this.label18, "Number of milliseconds to leave the deployment script windows open after the depl" +
        "oyment or \"pause\" to remain it open indefinetly (default: 10000ms)");
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(218, 67);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(20, 13);
            this.label15.TabIndex = 26;
            this.label15.Text = "ms";
            this.toolTip.SetToolTip(this.label15, "Number of milliseconds to wait for processes, services (default: 60000ms)");
            // 
            // label_DeployRetries
            // 
            this.label_DeployRetries.Location = new System.Drawing.Point(1, 44);
            this.label_DeployRetries.Name = "label_DeployRetries";
            this.label_DeployRetries.Size = new System.Drawing.Size(143, 17);
            this.label_DeployRetries.TabIndex = 18;
            this.label_DeployRetries.Text = "Deployment Retries";
            this.toolTip.SetToolTip(this.label_DeployRetries, "Number of retries if solution deployment fails (default: 3)");
            // 
            // label_WaitAfterDeploy
            // 
            this.label_WaitAfterDeploy.Location = new System.Drawing.Point(1, 92);
            this.label_WaitAfterDeploy.Name = "label_WaitAfterDeploy";
            this.label_WaitAfterDeploy.Size = new System.Drawing.Size(143, 17);
            this.label_WaitAfterDeploy.TabIndex = 19;
            this.label_WaitAfterDeploy.Text = "Wait after deployment";
            this.toolTip.SetToolTip(this.label_WaitAfterDeploy, "Number of milliseconds to leave the deployment script window open after the deplo" +
        "yment or \"pause\" to remain it open indefinetly (default: 10000ms)");
            // 
            // label_DeployTimeout
            // 
            this.label_DeployTimeout.Location = new System.Drawing.Point(1, 67);
            this.label_DeployTimeout.Name = "label_DeployTimeout";
            this.label_DeployTimeout.Size = new System.Drawing.Size(143, 17);
            this.label_DeployTimeout.TabIndex = 20;
            this.label_DeployTimeout.Text = "Deployment Timeout";
            this.toolTip.SetToolTip(this.label_DeployTimeout, "Number of milliseconds to wait for processes, services (default: 60000ms)");
            // 
            // label_ConfSettings
            // 
            this.label_ConfSettings.Location = new System.Drawing.Point(0, 0);
            this.label_ConfSettings.Name = "label_ConfSettings";
            this.label_ConfSettings.Size = new System.Drawing.Size(578, 38);
            this.label_ConfSettings.TabIndex = 17;
            this.label_ConfSettings.Text = "The settings section defines general parameters for the deployment. Most deployme" +
    "nts should run fine with the default values. If neccesary the settings can be ad" +
    "justed later in the environment XML.";
            // 
            // checkBox_RunOnMultipleServersInFarm
            // 
            this.checkBox_RunOnMultipleServersInFarm.Checked = true;
            this.checkBox_RunOnMultipleServersInFarm.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_RunOnMultipleServersInFarm.Location = new System.Drawing.Point(4, 138);
            this.checkBox_RunOnMultipleServersInFarm.Name = "checkBox_RunOnMultipleServersInFarm";
            this.checkBox_RunOnMultipleServersInFarm.Size = new System.Drawing.Size(245, 20);
            this.checkBox_RunOnMultipleServersInFarm.TabIndex = 14;
            this.checkBox_RunOnMultipleServersInFarm.Tag = "all";
            this.checkBox_RunOnMultipleServersInFarm.Text = "Run prerequisite checks and actions on";
            this.toolTip.SetToolTip(this.checkBox_RunOnMultipleServersInFarm, "Specifies if prerequisite checks and actions should be run on multiple servers in" +
        " the farm or only the local server.");
            this.checkBox_RunOnMultipleServersInFarm.UseVisualStyleBackColor = true;
            this.checkBox_RunOnMultipleServersInFarm.CheckedChanged += new System.EventHandler(this.CheckBox_RunOnMultipleServersInFarm_CheckedChanged);
            // 
            // checkBox_CreateULSLog
            // 
            this.checkBox_CreateULSLog.Enabled = false;
            this.checkBox_CreateULSLog.Location = new System.Drawing.Point(4, 163);
            this.checkBox_CreateULSLog.Name = "checkBox_CreateULSLog";
            this.checkBox_CreateULSLog.Size = new System.Drawing.Size(294, 17);
            this.checkBox_CreateULSLog.TabIndex = 15;
            this.checkBox_CreateULSLog.Tag = "false";
            this.checkBox_CreateULSLog.Text = "Create a logfile in ULS format (not yet migrated)";
            this.toolTip.SetToolTip(this.checkBox_CreateULSLog, "Create a log file in ULS log format (not yet migrated from MSBuild to PowerShell)" +
        "");
            this.checkBox_CreateULSLog.UseVisualStyleBackColor = true;
            this.checkBox_CreateULSLog.CheckedChanged += new System.EventHandler(this.Control_Changed);
            // 
            // checkBox_DisplayWizards
            // 
            this.checkBox_DisplayWizards.Enabled = false;
            this.checkBox_DisplayWizards.Location = new System.Drawing.Point(4, 184);
            this.checkBox_DisplayWizards.Name = "checkBox_DisplayWizards";
            this.checkBox_DisplayWizards.Size = new System.Drawing.Size(294, 23);
            this.checkBox_DisplayWizards.TabIndex = 16;
            this.checkBox_DisplayWizards.Tag = "false";
            this.checkBox_DisplayWizards.Text = "Display wizards to collect parameters (not yet migrated)";
            this.toolTip.SetToolTip(this.checkBox_DisplayWizards, "Use wizards to specify variables (not yet migrated from MSBuild to PowerShell)");
            this.checkBox_DisplayWizards.UseVisualStyleBackColor = true;
            this.checkBox_DisplayWizards.CheckedChanged += new System.EventHandler(this.Control_Changed);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(339, 141);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 13);
            this.label1.TabIndex = 33;
            this.label1.Tag = "all";
            this.label1.Text = "servers in the farm";
            this.toolTip.SetToolTip(this.label1, "Specifies if prerequisite checks and actions should be run on multiple servers in" +
        " the farm or only the local server.");
            // 
            // textBox_WaitAfterDeploy_val
            // 
            this.textBox_WaitAfterDeploy_val.BackColor = System.Drawing.Color.White;
            this.textBox_WaitAfterDeploy_val.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("textBox_WaitAfterDeploy_val.BackgroundImage")));
            this.textBox_WaitAfterDeploy_val.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.textBox_WaitAfterDeploy_val.CannotBeEmpty = true;
            this.textBox_WaitAfterDeploy_val.CannotStartWithInteger = false;
            this.textBox_WaitAfterDeploy_val.ControlToValidate = this.textBox_WaitAfterDeploy;
            this.textBox_WaitAfterDeploy_val.DefaultValue = "10000";
            this.textBox_WaitAfterDeploy_val.InfoValue = null;
            this.textBox_WaitAfterDeploy_val.IsValid = true;
            this.textBox_WaitAfterDeploy_val.Location = new System.Drawing.Point(153, 92);
            this.textBox_WaitAfterDeploy_val.Margin = new System.Windows.Forms.Padding(0);
            this.textBox_WaitAfterDeploy_val.MustBeInt = false;
            this.textBox_WaitAfterDeploy_val.MustBePostitiveInt = true;
            this.textBox_WaitAfterDeploy_val.MustBeUnique = false;
            this.textBox_WaitAfterDeploy_val.Name = "textBox_WaitAfterDeploy_val";
            this.textBox_WaitAfterDeploy_val.NoSpecialChars = false;
            this.textBox_WaitAfterDeploy_val.NoWhiteSpaces = false;
            this.textBox_WaitAfterDeploy_val.SaveErrorFieldDesc = "\"wait after deployment\" field in the settings section";
            this.textBox_WaitAfterDeploy_val.Size = new System.Drawing.Size(14, 14);
            this.textBox_WaitAfterDeploy_val.TabIndex = 29;
            this.textBox_WaitAfterDeploy_val.Visible = false;
            // 
            // textBox_DeployTimeout_val
            // 
            this.textBox_DeployTimeout_val.BackColor = System.Drawing.Color.White;
            this.textBox_DeployTimeout_val.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("textBox_DeployTimeout_val.BackgroundImage")));
            this.textBox_DeployTimeout_val.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.textBox_DeployTimeout_val.CannotBeEmpty = true;
            this.textBox_DeployTimeout_val.CannotStartWithInteger = false;
            this.textBox_DeployTimeout_val.ControlToValidate = this.textBox_DeployTimeout;
            this.textBox_DeployTimeout_val.DefaultValue = "60000";
            this.textBox_DeployTimeout_val.InfoValue = null;
            this.textBox_DeployTimeout_val.IsValid = true;
            this.textBox_DeployTimeout_val.Location = new System.Drawing.Point(153, 66);
            this.textBox_DeployTimeout_val.Margin = new System.Windows.Forms.Padding(0);
            this.textBox_DeployTimeout_val.MustBeInt = false;
            this.textBox_DeployTimeout_val.MustBePostitiveInt = true;
            this.textBox_DeployTimeout_val.MustBeUnique = false;
            this.textBox_DeployTimeout_val.Name = "textBox_DeployTimeout_val";
            this.textBox_DeployTimeout_val.NoSpecialChars = false;
            this.textBox_DeployTimeout_val.NoWhiteSpaces = false;
            this.textBox_DeployTimeout_val.SaveErrorFieldDesc = "\"deployment timeout\" field in the settings section";
            this.textBox_DeployTimeout_val.Size = new System.Drawing.Size(14, 14);
            this.textBox_DeployTimeout_val.TabIndex = 28;
            this.textBox_DeployTimeout_val.Visible = false;
            // 
            // textBox_DeployRetries_val
            // 
            this.textBox_DeployRetries_val.BackColor = System.Drawing.Color.White;
            this.textBox_DeployRetries_val.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("textBox_DeployRetries_val.BackgroundImage")));
            this.textBox_DeployRetries_val.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.textBox_DeployRetries_val.CannotBeEmpty = true;
            this.textBox_DeployRetries_val.CannotStartWithInteger = false;
            this.textBox_DeployRetries_val.ControlToValidate = this.textBox_DeployRetries;
            this.textBox_DeployRetries_val.DefaultValue = "3";
            this.textBox_DeployRetries_val.InfoValue = null;
            this.textBox_DeployRetries_val.IsValid = true;
            this.textBox_DeployRetries_val.Location = new System.Drawing.Point(153, 44);
            this.textBox_DeployRetries_val.Margin = new System.Windows.Forms.Padding(0);
            this.textBox_DeployRetries_val.MustBeInt = false;
            this.textBox_DeployRetries_val.MustBePostitiveInt = true;
            this.textBox_DeployRetries_val.MustBeUnique = false;
            this.textBox_DeployRetries_val.Name = "textBox_DeployRetries_val";
            this.textBox_DeployRetries_val.NoSpecialChars = false;
            this.textBox_DeployRetries_val.NoWhiteSpaces = false;
            this.textBox_DeployRetries_val.SaveErrorFieldDesc = "\"deployment retries\" field in the settings section";
            this.textBox_DeployRetries_val.Size = new System.Drawing.Size(14, 14);
            this.textBox_DeployRetries_val.TabIndex = 27;
            this.textBox_DeployRetries_val.Visible = false;
            // 
            // SettingsSection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.mainPanel);
            this.Name = "SettingsSection";
            this.Size = new System.Drawing.Size(578, 236);
            this.mainPanel.ResumeLayout(false);
            this.mainPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox textBox_DeployRetries;
        private System.Windows.Forms.TextBox textBox_WaitAfterDeploy;
        private System.Windows.Forms.TextBox textBox_DeployTimeout;
        private System.Windows.Forms.Label label_DeployRetries;
        private System.Windows.Forms.Label label_WaitAfterDeploy;
        private System.Windows.Forms.Label label_DeployTimeout;
        private System.Windows.Forms.Label label_ConfSettings;
        private System.Windows.Forms.CheckBox checkBox_RunOnMultipleServersInFarm;
        private System.Windows.Forms.CheckBox checkBox_CreateULSLog;
        private System.Windows.Forms.CheckBox checkBox_DisplayWizards;
        private System.Windows.Forms.ToolTip toolTip;
        private Controls.ValidationIndicator textBox_WaitAfterDeploy_val;
        private Controls.ValidationIndicator textBox_DeployTimeout_val;
        private Controls.ValidationIndicator textBox_DeployRetries_val;
        private System.Windows.Forms.RadioButton radioButton_WaitAfterDeployment;
        private System.Windows.Forms.RadioButton radioButton_PauseAfterDeployment;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox_RunOnMultipleServersInFarm;

    }
}
