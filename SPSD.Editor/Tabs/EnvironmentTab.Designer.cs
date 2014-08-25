using SPSD.Editor.Controls;

namespace SPSD.Editor.Tabs
{
    partial class EnvironmentTab
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
            this.tabPage_Variables = new System.Windows.Forms.TabPage();
            this.variablesSection = new SPSD.Editor.Sections.VariablesSection();
            this.tabPage_Solutions = new System.Windows.Forms.TabPage();
            this.solutionsSection = new SPSD.Editor.Sections.SolutionsSection();
            this.tabPage_PreReqSolutions = new System.Windows.Forms.TabPage();
            this.prerequisiteSolutionsSection = new SPSD.Editor.Sections.PrerequisiteSolutionsSection();
            this.externalNodeReference = new SPSD.Editor.Controls.ExternalNodeReference();
            this.tabControl.SuspendLayout();
            this.tabPage_Variables.SuspendLayout();
            this.tabPage_Solutions.SuspendLayout();
            this.tabPage_PreReqSolutions.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.tabPage_Variables);
            this.tabControl.Controls.Add(this.tabPage_Solutions);
            this.tabControl.Controls.Add(this.tabPage_PreReqSolutions);
            this.tabControl.Location = new System.Drawing.Point(0, 78);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(707, 459);
            this.tabControl.TabIndex = 5;
            // 
            // tabPage_Variables
            // 
            this.tabPage_Variables.Controls.Add(this.variablesSection);
            this.tabPage_Variables.Location = new System.Drawing.Point(4, 22);
            this.tabPage_Variables.Name = "tabPage_Variables";
            this.tabPage_Variables.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_Variables.Size = new System.Drawing.Size(699, 433);
            this.tabPage_Variables.TabIndex = 0;
            this.tabPage_Variables.Text = "Variables";
            this.tabPage_Variables.UseVisualStyleBackColor = true;
            // 
            // variablesSection
            // 
            this.variablesSection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.variablesSection.Location = new System.Drawing.Point(3, 3);
            this.variablesSection.Name = "variablesSection";
            this.variablesSection.Size = new System.Drawing.Size(693, 427);
            this.variablesSection.TabIndex = 4;
            // 
            // tabPage_Solutions
            // 
            this.tabPage_Solutions.Controls.Add(this.solutionsSection);
            this.tabPage_Solutions.Location = new System.Drawing.Point(4, 22);
            this.tabPage_Solutions.Name = "tabPage_Solutions";
            this.tabPage_Solutions.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_Solutions.Size = new System.Drawing.Size(699, 433);
            this.tabPage_Solutions.TabIndex = 1;
            this.tabPage_Solutions.Text = "Solutions";
            this.tabPage_Solutions.UseVisualStyleBackColor = true;
            // 
            // solutionsSection
            // 
            this.solutionsSection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.solutionsSection.Location = new System.Drawing.Point(3, 3);
            this.solutionsSection.Name = "solutionsSection";
            this.solutionsSection.Size = new System.Drawing.Size(693, 427);
            this.solutionsSection.TabIndex = 5;
            // 
            // tabPage_PreReqSolutions
            // 
            this.tabPage_PreReqSolutions.Controls.Add(this.prerequisiteSolutionsSection);
            this.tabPage_PreReqSolutions.Location = new System.Drawing.Point(4, 22);
            this.tabPage_PreReqSolutions.Name = "tabPage_PreReqSolutions";
            this.tabPage_PreReqSolutions.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_PreReqSolutions.Size = new System.Drawing.Size(699, 433);
            this.tabPage_PreReqSolutions.TabIndex = 2;
            this.tabPage_PreReqSolutions.Text = "Prerequisite Solutions";
            this.tabPage_PreReqSolutions.UseVisualStyleBackColor = true;
            // 
            // prerequisiteSolutionsSection
            // 
            this.prerequisiteSolutionsSection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.prerequisiteSolutionsSection.Location = new System.Drawing.Point(3, 3);
            this.prerequisiteSolutionsSection.Name = "prerequisiteSolutionsSection";
            this.prerequisiteSolutionsSection.Size = new System.Drawing.Size(693, 427);
            this.prerequisiteSolutionsSection.TabIndex = 2;
            // 
            // externalNodeReference
            // 
            this.externalNodeReference.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.externalNodeReference.ControlLocationName = "environment tab";
            this.externalNodeReference.Location = new System.Drawing.Point(0, 0);
            this.externalNodeReference.Name = "externalNodeReference";
            this.externalNodeReference.Size = new System.Drawing.Size(707, 72);
            this.externalNodeReference.TabIndex = 0;
            this.externalNodeReference.XPath = "SPSD/Environment";
            this.externalNodeReference.NodeReferenceHasChanged += new System.EventHandler(this.ExternalNodeReference_IsExternalChanged);
            // 
            // EnvironmentTab
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.externalNodeReference);
            this.Name = "EnvironmentTab";
            this.Size = new System.Drawing.Size(707, 537);
            this.tabControl.ResumeLayout(false);
            this.tabPage_Variables.ResumeLayout(false);
            this.tabPage_Solutions.ResumeLayout(false);
            this.tabPage_PreReqSolutions.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ExternalNodeReference externalNodeReference;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPage_Variables;
        private Sections.VariablesSection variablesSection;
        private System.Windows.Forms.TabPage tabPage_Solutions;
        private Sections.SolutionsSection solutionsSection;
        private System.Windows.Forms.TabPage tabPage_PreReqSolutions;
        private Sections.PrerequisiteSolutionsSection prerequisiteSolutionsSection;
    }
}
