namespace SPSD.Editor.Dialogs
{
    partial class About
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.spsd_logo = new System.Windows.Forms.PictureBox();
            this.spsd_text = new System.Windows.Forms.PictureBox();
            this.spsd_disclaimer = new System.Windows.Forms.RichTextBox();
            this.button_close = new System.Windows.Forms.Button();
            this.label_version = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.spsd_logo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spsd_text)).BeginInit();
            this.SuspendLayout();
            // 
            // spsd_logo
            // 
            this.spsd_logo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.spsd_logo.BackColor = System.Drawing.Color.Transparent;
            this.spsd_logo.BackgroundImage = global::SPSD.Editor.Properties.Resources.SPSD;
            this.spsd_logo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.spsd_logo.Location = new System.Drawing.Point(394, -1);
            this.spsd_logo.Name = "spsd_logo";
            this.spsd_logo.Size = new System.Drawing.Size(200, 200);
            this.spsd_logo.TabIndex = 0;
            this.spsd_logo.TabStop = false;
            // 
            // spsd_text
            // 
            this.spsd_text.BackgroundImage = global::SPSD.Editor.Properties.Resources.SPSD_text;
            this.spsd_text.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.spsd_text.Location = new System.Drawing.Point(0, -1);
            this.spsd_text.Name = "spsd_text";
            this.spsd_text.Size = new System.Drawing.Size(282, 86);
            this.spsd_text.TabIndex = 1;
            this.spsd_text.TabStop = false;
            // 
            // spsd_disclaimer
            // 
            this.spsd_disclaimer.BackColor = System.Drawing.SystemColors.Control;
            this.spsd_disclaimer.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.spsd_disclaimer.Location = new System.Drawing.Point(12, 92);
            this.spsd_disclaimer.Name = "spsd_disclaimer";
            this.spsd_disclaimer.ReadOnly = true;
            this.spsd_disclaimer.Size = new System.Drawing.Size(319, 275);
            this.spsd_disclaimer.TabIndex = 2;
            this.spsd_disclaimer.Text = "\n\nRichtextBox";
            // 
            // button_close
            // 
            this.button_close.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button_close.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_close.Location = new System.Drawing.Point(249, 349);
            this.button_close.Name = "button_close";
            this.button_close.Size = new System.Drawing.Size(101, 23);
            this.button_close.TabIndex = 3;
            this.button_close.Text = "&Close";
            this.button_close.UseVisualStyleBackColor = true;
            this.button_close.Click += new System.EventHandler(this.Button_Close_Click);
            // 
            // label_version
            // 
            this.label_version.AutoSize = true;
            this.label_version.Location = new System.Drawing.Point(12, 354);
            this.label_version.Name = "label_version";
            this.label_version.Size = new System.Drawing.Size(68, 13);
            this.label_version.TabIndex = 4;
            this.label_version.Text = "VersionLabel";
            // 
            // About
            // 
            this.AcceptButton = this.button_close;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button_close;
            this.ClientSize = new System.Drawing.Size(593, 379);
            this.Controls.Add(this.label_version);
            this.Controls.Add(this.button_close);
            this.Controls.Add(this.spsd_disclaimer);
            this.Controls.Add(this.spsd_text);
            this.Controls.Add(this.spsd_logo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "About";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "About SPSD";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.spsd_logo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spsd_text)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox spsd_logo;
        private System.Windows.Forms.PictureBox spsd_text;
        private System.Windows.Forms.RichTextBox spsd_disclaimer;
        private System.Windows.Forms.Button button_close;
        private System.Windows.Forms.Label label_version;
    }
}