namespace SPSD.Editor.Sections
{
    partial class RestrictionsSection
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
            this.mainPanel = new System.Windows.Forms.Panel();
            this.checkBox_AllowTrustedBin = new System.Windows.Forms.CheckBox();
            this.comboBox_SPVersion = new System.Windows.Forms.ComboBox();
            this.label_SPVersion = new System.Windows.Forms.Label();
            this.label_SPLicense = new System.Windows.Forms.Label();
            this.comboBox_SPLicense = new System.Windows.Forms.ComboBox();
            this.label16 = new System.Windows.Forms.Label();
            this.checkBox_AllowCASPolicies = new System.Windows.Forms.CheckBox();
            this.checkBox_AllowGACDeployment = new System.Windows.Forms.CheckBox();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.mainPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainPanel
            // 
            this.mainPanel.Controls.Add(this.checkBox_AllowTrustedBin);
            this.mainPanel.Controls.Add(this.comboBox_SPVersion);
            this.mainPanel.Controls.Add(this.label_SPVersion);
            this.mainPanel.Controls.Add(this.label_SPLicense);
            this.mainPanel.Controls.Add(this.comboBox_SPLicense);
            this.mainPanel.Controls.Add(this.label16);
            this.mainPanel.Controls.Add(this.checkBox_AllowCASPolicies);
            this.mainPanel.Controls.Add(this.checkBox_AllowGACDeployment);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mainPanel.Location = new System.Drawing.Point(0, 0);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(741, 158);
            this.mainPanel.TabIndex = 2;
            this.mainPanel.Text = "Restrictions";
            // 
            // checkBox_AllowTrustedBin
            // 
            this.checkBox_AllowTrustedBin.AutoSize = true;
            this.checkBox_AllowTrustedBin.Checked = true;
            this.checkBox_AllowTrustedBin.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_AllowTrustedBin.Enabled = false;
            this.checkBox_AllowTrustedBin.Location = new System.Drawing.Point(5, 137);
            this.checkBox_AllowTrustedBin.Name = "checkBox_AllowTrustedBin";
            this.checkBox_AllowTrustedBin.Size = new System.Drawing.Size(241, 17);
            this.checkBox_AllowTrustedBin.TabIndex = 28;
            this.checkBox_AllowTrustedBin.Tag = "true";
            this.checkBox_AllowTrustedBin.Text = "Allow FullTrust BIN deployment (only SP2013)";
            this.toolTip.SetToolTip(this.checkBox_AllowTrustedBin, "Allow the deployment of binaries with full trust");
            this.checkBox_AllowTrustedBin.UseVisualStyleBackColor = true;
            this.checkBox_AllowTrustedBin.CheckedChanged += new System.EventHandler(this.CheckBox_AllowTrustedBin_CheckedChanged);
            // 
            // comboBox_SPVersion
            // 
            this.comboBox_SPVersion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_SPVersion.FormattingEnabled = true;
            this.comboBox_SPVersion.Location = new System.Drawing.Point(150, 39);
            this.comboBox_SPVersion.Name = "comboBox_SPVersion";
            this.comboBox_SPVersion.Size = new System.Drawing.Size(346, 21);
            this.comboBox_SPVersion.TabIndex = 24;
            this.comboBox_SPVersion.Tag = "14.0.0.0";
            this.toolTip.SetToolTip(this.comboBox_SPVersion, "Minimal version number of the SharePoint installation required to deploy the solu" +
        "tions\r\n");
            this.comboBox_SPVersion.SelectedIndexChanged += new System.EventHandler(this.ComboBox_SPVersion_SelectedIndexChanged);
            // 
            // label_SPVersion
            // 
            this.label_SPVersion.AutoSize = true;
            this.label_SPVersion.Location = new System.Drawing.Point(2, 42);
            this.label_SPVersion.Name = "label_SPVersion";
            this.label_SPVersion.Size = new System.Drawing.Size(135, 13);
            this.label_SPVersion.TabIndex = 25;
            this.label_SPVersion.Text = "Minimal SharePoint Version";
            this.toolTip.SetToolTip(this.label_SPVersion, "Minimal version number of the SharePoint installation required to deploy this sol" +
        "ution");
            // 
            // label_SPLicense
            // 
            this.label_SPLicense.AutoSize = true;
            this.label_SPLicense.Location = new System.Drawing.Point(2, 62);
            this.label_SPLicense.Name = "label_SPLicense";
            this.label_SPLicense.Size = new System.Drawing.Size(137, 13);
            this.label_SPLicense.TabIndex = 27;
            this.label_SPLicense.Tag = "Minimal SharePoint license to deploy this solution";
            this.label_SPLicense.Text = "Minimal SharePoint License";
            // 
            // comboBox_SPLicense
            // 
            this.comboBox_SPLicense.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_SPLicense.FormattingEnabled = true;
            this.comboBox_SPLicense.Location = new System.Drawing.Point(150, 62);
            this.comboBox_SPLicense.Name = "comboBox_SPLicense";
            this.comboBox_SPLicense.Size = new System.Drawing.Size(346, 21);
            this.comboBox_SPLicense.TabIndex = 26;
            this.comboBox_SPLicense.Tag = "Foundation";
            this.toolTip.SetToolTip(this.comboBox_SPLicense, "Minimal SharePoint license to deploy the solutions");
            this.comboBox_SPLicense.SelectedIndexChanged += new System.EventHandler(this.ComboBox_SPLicense_SelectedIndexChanged);
            // 
            // label16
            // 
            this.label16.Location = new System.Drawing.Point(0, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(741, 36);
            this.label16.TabIndex = 21;
            this.label16.Text = "The restrictions section defines which requirements the farm must fulfill in orde" +
    "r to proceed with the deployment and which types of solutions can be deployed.\r\n" +
    "";
            // 
            // checkBox_AllowCASPolicies
            // 
            this.checkBox_AllowCASPolicies.AutoSize = true;
            this.checkBox_AllowCASPolicies.Checked = true;
            this.checkBox_AllowCASPolicies.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_AllowCASPolicies.Location = new System.Drawing.Point(5, 114);
            this.checkBox_AllowCASPolicies.Name = "checkBox_AllowCASPolicies";
            this.checkBox_AllowCASPolicies.Size = new System.Drawing.Size(113, 17);
            this.checkBox_AllowCASPolicies.TabIndex = 23;
            this.checkBox_AllowCASPolicies.Tag = "true";
            this.checkBox_AllowCASPolicies.Text = "Allow CAS policies";
            this.toolTip.SetToolTip(this.checkBox_AllowCASPolicies, "Allow the deployment of binaries with code access security (CAS) policies");
            this.checkBox_AllowCASPolicies.UseVisualStyleBackColor = true;
            this.checkBox_AllowCASPolicies.CheckedChanged += new System.EventHandler(this.CheckBox_AllowCasPolicies_CheckedChanged);
            // 
            // checkBox_AllowGACDeployment
            // 
            this.checkBox_AllowGACDeployment.AutoSize = true;
            this.checkBox_AllowGACDeployment.Checked = true;
            this.checkBox_AllowGACDeployment.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_AllowGACDeployment.Location = new System.Drawing.Point(5, 91);
            this.checkBox_AllowGACDeployment.Name = "checkBox_AllowGACDeployment";
            this.checkBox_AllowGACDeployment.Size = new System.Drawing.Size(133, 17);
            this.checkBox_AllowGACDeployment.TabIndex = 22;
            this.checkBox_AllowGACDeployment.Tag = "true";
            this.checkBox_AllowGACDeployment.Text = "Allow GAC deployment";
            this.toolTip.SetToolTip(this.checkBox_AllowGACDeployment, "Allow deployment of solution binaries to the global assembly cache");
            this.checkBox_AllowGACDeployment.UseVisualStyleBackColor = true;
            this.checkBox_AllowGACDeployment.CheckedChanged += new System.EventHandler(this.CheckBox_AllowGacDeployment_CheckedChanged);
            // 
            // RestrictionsSection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.mainPanel);
            this.Name = "RestrictionsSection";
            this.Size = new System.Drawing.Size(741, 158);
            this.mainPanel.ResumeLayout(false);
            this.mainPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.CheckBox checkBox_AllowTrustedBin;
        private System.Windows.Forms.ComboBox comboBox_SPVersion;
        private System.Windows.Forms.Label label_SPVersion;
        private System.Windows.Forms.Label label_SPLicense;
        private System.Windows.Forms.ComboBox comboBox_SPLicense;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.CheckBox checkBox_AllowCASPolicies;
        private System.Windows.Forms.CheckBox checkBox_AllowGACDeployment;
        private System.Windows.Forms.ToolTip toolTip;
    }
}
