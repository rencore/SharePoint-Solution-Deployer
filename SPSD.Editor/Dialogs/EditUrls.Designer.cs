namespace SPSD.Editor.Dialogs
{
    partial class EditUrls
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
            this.listBox_Urls = new System.Windows.Forms.ListBox();
            this.textBox_Url = new System.Windows.Forms.TextBox();
            this.button_Save = new System.Windows.Forms.Button();
            this.button_Cancel = new System.Windows.Forms.Button();
            this.button_Add = new System.Windows.Forms.Button();
            this.button_Remove = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.variablesButton1 = new SPSD.Editor.Controls.VariablesButton();
            this.button_Edit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listBox_Urls
            // 
            this.listBox_Urls.FormattingEnabled = true;
            this.listBox_Urls.Location = new System.Drawing.Point(31, 69);
            this.listBox_Urls.Name = "listBox_Urls";
            this.listBox_Urls.Size = new System.Drawing.Size(399, 95);
            this.listBox_Urls.TabIndex = 0;
            this.listBox_Urls.SelectedIndexChanged += new System.EventHandler(this.listBox_Urls_SelectedIndexChanged);
            this.listBox_Urls.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBox_Urls_MouseDoubleClick);
            // 
            // textBox_Url
            // 
            this.textBox_Url.Location = new System.Drawing.Point(31, 43);
            this.textBox_Url.Name = "textBox_Url";
            this.textBox_Url.Size = new System.Drawing.Size(375, 20);
            this.textBox_Url.TabIndex = 1;
            // 
            // button_Save
            // 
            this.button_Save.Location = new System.Drawing.Point(436, 170);
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
            this.button_Cancel.Location = new System.Drawing.Point(364, 170);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(66, 23);
            this.button_Cancel.TabIndex = 3;
            this.button_Cancel.Text = "&Cancel";
            this.button_Cancel.UseVisualStyleBackColor = true;
            this.button_Cancel.Click += new System.EventHandler(this.button_Cancel_Click);
            // 
            // button_Add
            // 
            this.button_Add.Location = new System.Drawing.Point(436, 43);
            this.button_Add.Name = "button_Add";
            this.button_Add.Size = new System.Drawing.Size(66, 23);
            this.button_Add.TabIndex = 4;
            this.button_Add.Text = "&Add";
            this.button_Add.UseVisualStyleBackColor = true;
            this.button_Add.Click += new System.EventHandler(this.button_Add_Click);
            // 
            // button_Remove
            // 
            this.button_Remove.Location = new System.Drawing.Point(436, 95);
            this.button_Remove.Name = "button_Remove";
            this.button_Remove.Size = new System.Drawing.Size(66, 23);
            this.button_Remove.TabIndex = 5;
            this.button_Remove.Text = "&Remove";
            this.button_Remove.UseVisualStyleBackColor = true;
            this.button_Remove.Click += new System.EventHandler(this.button_Remove_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(6, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(496, 26);
            this.label1.TabIndex = 6;
            this.label1.Text = "Enter a url in the text box and click \"Add\" to add it for the url field. You can " +
    "use the $ button to insert system and user environment variables (defined on the" +
    " environment tab).";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(20, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Url";
            // 
            // variablesButton1
            // 
            this.variablesButton1.Location = new System.Drawing.Point(408, 43);
            this.variablesButton1.Name = "variablesButton1";
            this.variablesButton1.Size = new System.Drawing.Size(22, 23);
            this.variablesButton1.TabIndex = 8;
            this.variablesButton1.VariableMenuItemClick += new System.EventHandler(this.VariableMenuItem_Click);
            // 
            // button_Edit
            // 
            this.button_Edit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.button_Edit.Enabled = false;
            this.button_Edit.Location = new System.Drawing.Point(436, 69);
            this.button_Edit.Name = "button_Edit";
            this.button_Edit.Size = new System.Drawing.Size(66, 23);
            this.button_Edit.TabIndex = 9;
            this.button_Edit.Text = "&Edit";
            this.button_Edit.UseVisualStyleBackColor = true;
            this.button_Edit.Click += new System.EventHandler(this.button_Edit_Click);
            // 
            // EditUrls
            // 
            this.AcceptButton = this.button_Save;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button_Cancel;
            this.ClientSize = new System.Drawing.Size(508, 196);
            this.Controls.Add(this.button_Edit);
            this.Controls.Add(this.variablesButton1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button_Remove);
            this.Controls.Add(this.button_Add);
            this.Controls.Add(this.button_Cancel);
            this.Controls.Add(this.button_Save);
            this.Controls.Add(this.textBox_Url);
            this.Controls.Add(this.listBox_Urls);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EditUrls";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Edit Urls";
            this.Load += new System.EventHandler(this.EditUrls_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBox_Urls;
        private System.Windows.Forms.TextBox textBox_Url;
        private System.Windows.Forms.Button button_Save;
        private System.Windows.Forms.Button button_Cancel;
        private System.Windows.Forms.Button button_Add;
        private System.Windows.Forms.Button button_Remove;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private Controls.VariablesButton variablesButton1;
        private System.Windows.Forms.Button button_Edit;
    }
}