using SPSD.Editor.Controls;

namespace SPSD.Editor.Tabs
{
    partial class SiteStructuresTab
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
            this.externalNodeReference = new SPSD.Editor.Controls.ExternalNodeReference();
            this.siteStructuresSection = new SPSD.Editor.Sections.SiteStructuresSection();
            this.SuspendLayout();
            // 
            // externalNodeReference
            // 
            this.externalNodeReference.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.externalNodeReference.ControlLocationName = "site structures tab";
            this.externalNodeReference.Location = new System.Drawing.Point(3, 3);
            this.externalNodeReference.Name = "externalNodeReference";
            this.externalNodeReference.Size = new System.Drawing.Size(909, 72);
            this.externalNodeReference.TabIndex = 1;
            this.externalNodeReference.XPath = "SPSD/SiteStructures";
            this.externalNodeReference.NodeReferenceHasChanged += new System.EventHandler(this.ExternalNodeReference_IsExternalChanged);
            // 
            // siteStructuresSection
            // 
            this.siteStructuresSection.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.siteStructuresSection.Location = new System.Drawing.Point(0, 81);
            this.siteStructuresSection.Name = "siteStructuresSection";
            this.siteStructuresSection.Size = new System.Drawing.Size(909, 465);
            this.siteStructuresSection.TabIndex = 2;
            // 
            // SiteStructuresTab
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.siteStructuresSection);
            this.Controls.Add(this.externalNodeReference);
            this.Name = "SiteStructuresTab";
            this.Size = new System.Drawing.Size(909, 546);
            this.ResumeLayout(false);

        }

        #endregion

        private ExternalNodeReference externalNodeReference;
        private Sections.SiteStructuresSection siteStructuresSection;
    }
}
