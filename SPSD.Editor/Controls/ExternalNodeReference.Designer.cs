namespace SPSD.Editor.Controls
{
    partial class ExternalNodeReference
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExternalNodeReference));
            this.button_ExtNodeBrowse = new System.Windows.Forms.Button();
            this.comboBox_ExtNodeId = new System.Windows.Forms.ComboBox();
            this.textBox_ExtNodeFile = new System.Windows.Forms.TextBox();
            this.radioButton_IntNode = new System.Windows.Forms.RadioButton();
            this.radioButton_ExtNode = new System.Windows.Forms.RadioButton();
            this.textBox_IntNodeId = new System.Windows.Forms.TextBox();
            this.extNodeIdLabel = new System.Windows.Forms.Label();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.radioButton_NoNode = new System.Windows.Forms.RadioButton();
            this.textBox_ExtNodeFile_val = new SPSD.Editor.Controls.ValidationIndicator();
            this.comboBox_ExtNodeId_val = new SPSD.Editor.Controls.ValidationIndicator();
            this.textBox_NodeId_val = new SPSD.Editor.Controls.ValidationIndicator();
            this.SuspendLayout();
            // 
            // button_ExtNodeBrowse
            // 
            this.button_ExtNodeBrowse.Enabled = false;
            this.button_ExtNodeBrowse.Location = new System.Drawing.Point(403, 24);
            this.button_ExtNodeBrowse.Name = "button_ExtNodeBrowse";
            this.button_ExtNodeBrowse.Size = new System.Drawing.Size(75, 21);
            this.button_ExtNodeBrowse.TabIndex = 0;
            this.button_ExtNodeBrowse.Text = "Browse";
            this.button_ExtNodeBrowse.UseVisualStyleBackColor = true;
            this.button_ExtNodeBrowse.Click += new System.EventHandler(this.BrowseButton_Click);
            // 
            // comboBox_ExtNodeId
            // 
            this.comboBox_ExtNodeId.Enabled = false;
            this.comboBox_ExtNodeId.FormattingEnabled = true;
            this.comboBox_ExtNodeId.Items.AddRange(new object[] {
            "<choose node>"});
            this.comboBox_ExtNodeId.Location = new System.Drawing.Point(529, 24);
            this.comboBox_ExtNodeId.Name = "comboBox_ExtNodeId";
            this.comboBox_ExtNodeId.Size = new System.Drawing.Size(163, 21);
            this.comboBox_ExtNodeId.TabIndex = 1;
            this.comboBox_ExtNodeId.SelectedIndexChanged += new System.EventHandler(this.Changed);
            // 
            // textBox_ExtNodeFile
            // 
            this.textBox_ExtNodeFile.BackColor = System.Drawing.SystemColors.Control;
            this.textBox_ExtNodeFile.Enabled = false;
            this.textBox_ExtNodeFile.Location = new System.Drawing.Point(137, 24);
            this.textBox_ExtNodeFile.Name = "textBox_ExtNodeFile";
            this.textBox_ExtNodeFile.ReadOnly = true;
            this.textBox_ExtNodeFile.Size = new System.Drawing.Size(260, 20);
            this.textBox_ExtNodeFile.TabIndex = 2;
            // 
            // radioButton_IntNode
            // 
            this.radioButton_IntNode.Checked = true;
            this.radioButton_IntNode.Location = new System.Drawing.Point(3, 49);
            this.radioButton_IntNode.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.radioButton_IntNode.Name = "radioButton_IntNode";
            this.radioButton_IntNode.Size = new System.Drawing.Size(132, 18);
            this.radioButton_IntNode.TabIndex = 9;
            this.radioButton_IntNode.TabStop = true;
            this.radioButton_IntNode.Text = "custom defined       ID";
            this.radioButton_IntNode.UseCompatibleTextRendering = true;
            this.radioButton_IntNode.UseVisualStyleBackColor = true;
            this.radioButton_IntNode.CheckedChanged += new System.EventHandler(this.Node_Changed);
            // 
            // radioButton_ExtNode
            // 
            this.radioButton_ExtNode.AutoSize = true;
            this.radioButton_ExtNode.Location = new System.Drawing.Point(3, 26);
            this.radioButton_ExtNode.Name = "radioButton_ExtNode";
            this.radioButton_ExtNode.Size = new System.Drawing.Size(97, 17);
            this.radioButton_ExtNode.TabIndex = 8;
            this.radioButton_ExtNode.Text = "reference in file";
            this.radioButton_ExtNode.UseVisualStyleBackColor = true;
            this.radioButton_ExtNode.CheckedChanged += new System.EventHandler(this.Node_Changed);
            // 
            // textBox_IntNodeId
            // 
            this.textBox_IntNodeId.Location = new System.Drawing.Point(137, 49);
            this.textBox_IntNodeId.Name = "textBox_IntNodeId";
            this.textBox_IntNodeId.Size = new System.Drawing.Size(260, 20);
            this.textBox_IntNodeId.TabIndex = 10;
            this.textBox_IntNodeId.TextChanged += new System.EventHandler(this.Changed);
            // 
            // extNodeIdLabel
            // 
            this.extNodeIdLabel.AutoSize = true;
            this.extNodeIdLabel.CausesValidation = false;
            this.extNodeIdLabel.Location = new System.Drawing.Point(483, 28);
            this.extNodeIdLabel.Name = "extNodeIdLabel";
            this.extNodeIdLabel.Size = new System.Drawing.Size(40, 13);
            this.extNodeIdLabel.TabIndex = 11;
            this.extNodeIdLabel.Text = "with ID";
            // 
            // openFileDialog
            // 
            this.openFileDialog.DefaultExt = "xml";
            this.openFileDialog.Filter = "SPSD files (XML)|*.xml";
            // 
            // radioButton_NoNode
            // 
            this.radioButton_NoNode.AutoSize = true;
            this.radioButton_NoNode.Location = new System.Drawing.Point(3, 3);
            this.radioButton_NoNode.Name = "radioButton_NoNode";
            this.radioButton_NoNode.Size = new System.Drawing.Size(93, 17);
            this.radioButton_NoNode.TabIndex = 16;
            this.radioButton_NoNode.Text = "not configured";
            this.radioButton_NoNode.UseVisualStyleBackColor = true;
            this.radioButton_NoNode.CheckedChanged += new System.EventHandler(this.Node_Changed);
            // 
            // textBox_ExtNodeFile_val
            // 
            this.textBox_ExtNodeFile_val.BackColor = System.Drawing.SystemColors.Control;
            this.textBox_ExtNodeFile_val.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("textBox_ExtNodeFile_val.BackgroundImage")));
            this.textBox_ExtNodeFile_val.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.textBox_ExtNodeFile_val.CannotBeEmpty = true;
            this.textBox_ExtNodeFile_val.CannotStartWithInteger = false;
            this.textBox_ExtNodeFile_val.ControlToValidate = this.textBox_ExtNodeFile;
            this.textBox_ExtNodeFile_val.DefaultValue = null;
            this.textBox_ExtNodeFile_val.InfoValue = null;
            this.textBox_ExtNodeFile_val.IsValid = false;
            this.textBox_ExtNodeFile_val.Location = new System.Drawing.Point(381, 27);
            this.textBox_ExtNodeFile_val.Margin = new System.Windows.Forms.Padding(0);
            this.textBox_ExtNodeFile_val.MustBeInt = false;
            this.textBox_ExtNodeFile_val.MustBePostitiveInt = false;
            this.textBox_ExtNodeFile_val.MustBeUnique = false;
            this.textBox_ExtNodeFile_val.Name = "textBox_ExtNodeFile_val";
            this.textBox_ExtNodeFile_val.NoSpecialChars = false;
            this.textBox_ExtNodeFile_val.NoWhiteSpaces = false;
            this.textBox_ExtNodeFile_val.SaveErrorFieldDesc = null;
            this.textBox_ExtNodeFile_val.Size = new System.Drawing.Size(14, 14);
            this.textBox_ExtNodeFile_val.TabIndex = 15;
            this.textBox_ExtNodeFile_val.Visible = false;
            // 
            // comboBox_ExtNodeId_val
            // 
            this.comboBox_ExtNodeId_val.BackColor = System.Drawing.Color.White;
            this.comboBox_ExtNodeId_val.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("comboBox_ExtNodeId_val.BackgroundImage")));
            this.comboBox_ExtNodeId_val.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.comboBox_ExtNodeId_val.CannotBeEmpty = true;
            this.comboBox_ExtNodeId_val.CannotStartWithInteger = false;
            this.comboBox_ExtNodeId_val.ControlToValidate = this.comboBox_ExtNodeId;
            this.comboBox_ExtNodeId_val.DefaultValue = null;
            this.comboBox_ExtNodeId_val.InfoValue = "<select id>";
            this.comboBox_ExtNodeId_val.IsValid = false;
            this.comboBox_ExtNodeId_val.Location = new System.Drawing.Point(658, 27);
            this.comboBox_ExtNodeId_val.Margin = new System.Windows.Forms.Padding(0);
            this.comboBox_ExtNodeId_val.MustBeInt = false;
            this.comboBox_ExtNodeId_val.MustBePostitiveInt = false;
            this.comboBox_ExtNodeId_val.MustBeUnique = false;
            this.comboBox_ExtNodeId_val.Name = "comboBox_ExtNodeId_val";
            this.comboBox_ExtNodeId_val.NoSpecialChars = false;
            this.comboBox_ExtNodeId_val.NoWhiteSpaces = false;
            this.comboBox_ExtNodeId_val.SaveErrorFieldDesc = null;
            this.comboBox_ExtNodeId_val.Size = new System.Drawing.Size(14, 14);
            this.comboBox_ExtNodeId_val.TabIndex = 14;
            this.comboBox_ExtNodeId_val.Visible = false;
            // 
            // textBox_NodeId_val
            // 
            this.textBox_NodeId_val.BackColor = System.Drawing.Color.White;
            this.textBox_NodeId_val.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("textBox_NodeId_val.BackgroundImage")));
            this.textBox_NodeId_val.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.textBox_NodeId_val.CannotBeEmpty = true;
            this.textBox_NodeId_val.CannotStartWithInteger = true;
            this.textBox_NodeId_val.ControlToValidate = this.textBox_IntNodeId;
            this.textBox_NodeId_val.DefaultValue = "Default";
            this.textBox_NodeId_val.InfoValue = null;
            this.textBox_NodeId_val.IsValid = false;
            this.textBox_NodeId_val.Location = new System.Drawing.Point(381, 52);
            this.textBox_NodeId_val.Margin = new System.Windows.Forms.Padding(0);
            this.textBox_NodeId_val.MustBeInt = false;
            this.textBox_NodeId_val.MustBePostitiveInt = false;
            this.textBox_NodeId_val.MustBeUnique = false;
            this.textBox_NodeId_val.Name = "textBox_NodeId_val";
            this.textBox_NodeId_val.NoSpecialChars = true;
            this.textBox_NodeId_val.NoWhiteSpaces = true;
            this.textBox_NodeId_val.SaveErrorFieldDesc = null;
            this.textBox_NodeId_val.Size = new System.Drawing.Size(14, 14);
            this.textBox_NodeId_val.TabIndex = 13;
            this.textBox_NodeId_val.Visible = false;
            // 
            // ExternalNodeReference
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.radioButton_NoNode);
            this.Controls.Add(this.textBox_ExtNodeFile_val);
            this.Controls.Add(this.comboBox_ExtNodeId_val);
            this.Controls.Add(this.textBox_NodeId_val);
            this.Controls.Add(this.textBox_IntNodeId);
            this.Controls.Add(this.extNodeIdLabel);
            this.Controls.Add(this.radioButton_IntNode);
            this.Controls.Add(this.radioButton_ExtNode);
            this.Controls.Add(this.textBox_ExtNodeFile);
            this.Controls.Add(this.comboBox_ExtNodeId);
            this.Controls.Add(this.button_ExtNodeBrowse);
            this.Name = "ExternalNodeReference";
            this.Size = new System.Drawing.Size(696, 74);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_ExtNodeBrowse;
        private System.Windows.Forms.ComboBox comboBox_ExtNodeId;
        private System.Windows.Forms.TextBox textBox_ExtNodeFile;
        private System.Windows.Forms.RadioButton radioButton_IntNode;
        private System.Windows.Forms.RadioButton radioButton_ExtNode;
        private System.Windows.Forms.TextBox textBox_IntNodeId;
        private System.Windows.Forms.Label extNodeIdLabel;
        private Controls.ValidationIndicator textBox_NodeId_val;
        private Controls.ValidationIndicator comboBox_ExtNodeId_val;
        private Controls.ValidationIndicator textBox_ExtNodeFile_val;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.RadioButton radioButton_NoNode;
    }
}
