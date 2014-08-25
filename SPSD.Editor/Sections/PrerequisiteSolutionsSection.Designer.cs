namespace SPSD.Editor.Sections
{
    partial class PrerequisiteSolutionsSection
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
            this.mainPanel = new System.Windows.Forms.Panel();
            this.solutionsGrid = new SPSD.Editor.Controls.SolutionsGrid();
            this.label1 = new System.Windows.Forms.Label();
            this.mainPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainPanel
            // 
            this.mainPanel.Controls.Add(this.solutionsGrid);
            this.mainPanel.Controls.Add(this.label1);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(0, 0);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(739, 370);
            this.mainPanel.TabIndex = 1;
            this.mainPanel.Text = "Prerequisite Solutions";
            // 
            // solutionsGrid
            // 
            this.solutionsGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.solutionsGrid.Location = new System.Drawing.Point(0, 37);
            this.solutionsGrid.Name = "solutionsGrid";
            this.solutionsGrid.Size = new System.Drawing.Size(739, 333);
            this.solutionsGrid.SolutionForceEnabled = false;
            this.solutionsGrid.SolutionOverwriteEnabled = false;
            this.solutionsGrid.TabIndex = 21;
            this.solutionsGrid.Tag = "prerequisite solutions datagrid";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(739, 34);
            this.label1.TabIndex = 20;
            this.label1.Text = "The prerequisite section allows to define GAC, WebApplication or Sandboxed Soluti" +
    "ons which are required to be available on the farm in order to proceed with the " +
    "deployment.";
            // 
            // PrerequisiteSolutionsSection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.mainPanel);
            this.Name = "PrerequisiteSolutionsSection";
            this.Size = new System.Drawing.Size(739, 370);
            this.mainPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.Label label1;
        private Controls.SolutionsGrid solutionsGrid;

    }
}
