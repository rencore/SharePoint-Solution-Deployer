namespace SPSD.Editor.Dialogs
{
    partial class EditVariable
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
            this.textBox_variable = new System.Windows.Forms.TextBox();
            this.button_Save = new System.Windows.Forms.Button();
            this.button_Cancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.variablesButton1 = new SPSD.Editor.Controls.VariablesButton();
            this.SuspendLayout();
            // 
            // textBox_variable
            // 
            this.textBox_variable.Location = new System.Drawing.Point(12, 43);
            this.textBox_variable.Multiline = true;
            this.textBox_variable.Name = "textBox_variable";
            this.textBox_variable.Size = new System.Drawing.Size(462, 67);
            this.textBox_variable.TabIndex = 1;
            // 
            // button_Save
            // 
            this.button_Save.Location = new System.Drawing.Point(408, 116);
            this.button_Save.Name = "button_Save";
            this.button_Save.Size = new System.Drawing.Size(66, 23);
            this.button_Save.TabIndex = 2;
            this.button_Save.Text = "&Save";
            this.button_Save.UseVisualStyleBackColor = true;
            this.button_Save.Click += new System.EventHandler(this.button_Save_Click);
            // 
            // button_Cancel
            // 
            this.button_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_Cancel.Location = new System.Drawing.Point(336, 116);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(66, 23);
            this.button_Cancel.TabIndex = 3;
            this.button_Cancel.Text = "&Cancel";
            this.button_Cancel.UseVisualStyleBackColor = true;
            this.button_Cancel.Click += new System.EventHandler(this.button_Cancel_Click);
            // 
            // label1
            // 
            this.label1.Cursor = System.Windows.Forms.Cursors.Default;
            this.label1.Location = new System.Drawing.Point(6, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(496, 26);
            this.label1.TabIndex = 6;
            this.label1.Text = "Create the variable value in the field below. You can use the $ button to insert " +
    "system and custom environment variables.";
            // 
            // variablesButton1
            // 
            this.variablesButton1.Location = new System.Drawing.Point(478, 43);
            this.variablesButton1.Name = "variablesButton1";
            this.variablesButton1.Size = new System.Drawing.Size(22, 23);
            this.variablesButton1.TabIndex = 8;
            this.variablesButton1.VariableMenuItemClick += new System.EventHandler(this.VariableMenuItem_Click);
            this.variablesButton1.Load += new System.EventHandler(this.variablesButton1_Load);
            // 
            // EditVariable
            // 
            this.AcceptButton = this.button_Save;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button_Cancel;
            this.ClientSize = new System.Drawing.Size(508, 146);
            this.Controls.Add(this.variablesButton1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button_Cancel);
            this.Controls.Add(this.button_Save);
            this.Controls.Add(this.textBox_variable);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EditVariable";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Edit Variable";
            this.Load += new System.EventHandler(this.EditVariable_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_variable;
        private System.Windows.Forms.Button button_Save;
        private System.Windows.Forms.Button button_Cancel;
        private System.Windows.Forms.Label label1;
        private Controls.VariablesButton variablesButton1;
    }
}