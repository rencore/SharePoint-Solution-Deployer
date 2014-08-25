namespace SPSD.Editor.Sections
{
    partial class SolutionsSection
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
            this.solutionsGrid = new SPSD.Editor.Controls.SolutionsGrid();
            this.radioButton_specifiedSolutions = new System.Windows.Forms.RadioButton();
            this.radioButton_allSolutions = new System.Windows.Forms.RadioButton();
            this.checkBox_OverwriteExistingSolutions = new System.Windows.Forms.CheckBox();
            this.checkBox_ForceSolutionDeployment = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.mainPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainPanel
            // 
            this.mainPanel.Controls.Add(this.solutionsGrid);
            this.mainPanel.Controls.Add(this.radioButton_specifiedSolutions);
            this.mainPanel.Controls.Add(this.radioButton_allSolutions);
            this.mainPanel.Controls.Add(this.checkBox_OverwriteExistingSolutions);
            this.mainPanel.Controls.Add(this.checkBox_ForceSolutionDeployment);
            this.mainPanel.Controls.Add(this.label4);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mainPanel.Location = new System.Drawing.Point(0, 0);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(665, 347);
            this.mainPanel.TabIndex = 0;
            this.mainPanel.Text = "Solutions";
            // 
            // solutionsGrid
            // 
            this.solutionsGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.solutionsGrid.Location = new System.Drawing.Point(0, 114);
            this.solutionsGrid.Name = "solutionsGrid";
            this.solutionsGrid.Size = new System.Drawing.Size(665, 233);
            this.solutionsGrid.SolutionForceEnabled = true;
            this.solutionsGrid.SolutionOverwriteEnabled = true;
            this.solutionsGrid.TabIndex = 29;
            this.solutionsGrid.Tag = "deployed solutions datagrid";
            // 
            // radioButton_specifiedSolutions
            // 
            this.radioButton_specifiedSolutions.AutoSize = true;
            this.radioButton_specifiedSolutions.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton_specifiedSolutions.Location = new System.Drawing.Point(3, 91);
            this.radioButton_specifiedSolutions.Name = "radioButton_specifiedSolutions";
            this.radioButton_specifiedSolutions.Size = new System.Drawing.Size(635, 17);
            this.radioButton_specifiedSolutions.TabIndex = 28;
            this.radioButton_specifiedSolutions.Text = "Define solution deployment manually (all solution files need to be placed in the " +
    "/solutions folder of the SPSD deployment package)";
            this.radioButton_specifiedSolutions.UseVisualStyleBackColor = true;
            this.radioButton_specifiedSolutions.CheckedChanged += new System.EventHandler(this.RadioButton_SpecifiedSolutions_CheckedChanged);
            // 
            // radioButton_allSolutions
            // 
            this.radioButton_allSolutions.AutoSize = true;
            this.radioButton_allSolutions.Checked = true;
            this.radioButton_allSolutions.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton_allSolutions.Location = new System.Drawing.Point(3, 22);
            this.radioButton_allSolutions.Name = "radioButton_allSolutions";
            this.radioButton_allSolutions.Size = new System.Drawing.Size(623, 17);
            this.radioButton_allSolutions.TabIndex = 27;
            this.radioButton_allSolutions.TabStop = true;
            this.radioButton_allSolutions.Text = "Use all solutions in the /solutions folder of the SPSD deployment package (all so" +
    "lutions will be deployed to GAC/all content urls)\r\n";
            this.radioButton_allSolutions.UseVisualStyleBackColor = true;
            this.radioButton_allSolutions.CheckedChanged += new System.EventHandler(this.RadioButton_AllSolutions_CheckedChanged);
            // 
            // checkBox_OverwriteExistingSolutions
            // 
            this.checkBox_OverwriteExistingSolutions.AutoSize = true;
            this.checkBox_OverwriteExistingSolutions.Checked = true;
            this.checkBox_OverwriteExistingSolutions.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_OverwriteExistingSolutions.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox_OverwriteExistingSolutions.Location = new System.Drawing.Point(22, 68);
            this.checkBox_OverwriteExistingSolutions.Name = "checkBox_OverwriteExistingSolutions";
            this.checkBox_OverwriteExistingSolutions.Size = new System.Drawing.Size(205, 17);
            this.checkBox_OverwriteExistingSolutions.TabIndex = 26;
            this.checkBox_OverwriteExistingSolutions.Text = "Overwrite existing solutions in the farm";
            this.checkBox_OverwriteExistingSolutions.UseVisualStyleBackColor = true;
            this.checkBox_OverwriteExistingSolutions.CheckedChanged += new System.EventHandler(this.CheckBox_OverwriteExistingSolutions_CheckedChanged);
            // 
            // checkBox_ForceSolutionDeployment
            // 
            this.checkBox_ForceSolutionDeployment.AutoSize = true;
            this.checkBox_ForceSolutionDeployment.Checked = true;
            this.checkBox_ForceSolutionDeployment.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_ForceSolutionDeployment.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox_ForceSolutionDeployment.Location = new System.Drawing.Point(22, 45);
            this.checkBox_ForceSolutionDeployment.Name = "checkBox_ForceSolutionDeployment";
            this.checkBox_ForceSolutionDeployment.Size = new System.Drawing.Size(149, 17);
            this.checkBox_ForceSolutionDeployment.TabIndex = 25;
            this.checkBox_ForceSolutionDeployment.Text = "Force solution deployment";
            this.checkBox_ForceSolutionDeployment.UseVisualStyleBackColor = true;
            this.checkBox_ForceSolutionDeployment.CheckedChanged += new System.EventHandler(this.CheckBox_ForceSolutionDeployment_CheckedChanged);
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(0, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(665, 18);
            this.label4.TabIndex = 23;
            this.label4.Text = "The solutions section defines which WSP files should be deployed SPSD.";
            this.toolTip.SetToolTip(this.label4, "The solutions node specifies which solutions should be deployed, retracted or upd" +
        "ated by SPSD.");
            // 
            // SolutionsSection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.mainPanel);
            this.Name = "SolutionsSection";
            this.Size = new System.Drawing.Size(665, 347);
            this.mainPanel.ResumeLayout(false);
            this.mainPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox checkBox_OverwriteExistingSolutions;
        private System.Windows.Forms.CheckBox checkBox_ForceSolutionDeployment;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.RadioButton radioButton_allSolutions;
        private System.Windows.Forms.RadioButton radioButton_specifiedSolutions;
        private Controls.SolutionsGrid solutionsGrid;

    }
}
