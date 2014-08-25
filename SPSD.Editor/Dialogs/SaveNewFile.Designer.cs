namespace SPSD.Editor.Dialogs
{
    partial class SaveNewFile
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
            this.button_Save = new System.Windows.Forms.Button();
            this.button_Cancel = new System.Windows.Forms.Button();
            this.radioDefault = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.radioMachine = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.radioUser = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.radioCustom = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button_Save
            // 
            this.button_Save.Location = new System.Drawing.Point(436, 206);
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
            this.button_Cancel.Location = new System.Drawing.Point(364, 206);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(66, 23);
            this.button_Cancel.TabIndex = 3;
            this.button_Cancel.Text = "&Cancel";
            this.button_Cancel.UseVisualStyleBackColor = true;
            this.button_Cancel.Click += new System.EventHandler(this.button_Cancel_Click);
            // 
            // radioDefault
            // 
            this.radioDefault.AutoSize = true;
            this.radioDefault.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioDefault.Location = new System.Drawing.Point(16, 32);
            this.radioDefault.Name = "radioDefault";
            this.radioDefault.Size = new System.Drawing.Size(88, 17);
            this.radioDefault.TabIndex = 4;
            this.radioDefault.TabStop = true;
            this.radioDefault.Text = "Default.xml";
            this.radioDefault.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(31, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(437, 33);
            this.label1.TabIndex = 5;
            this.label1.Text = "This file will be used automatically if SPSD finds no other suitable environment " +
    "XML in the environments folder.";
            this.label1.Click += new System.EventHandler(this.label1_Click_1);
            // 
            // radioMachine
            // 
            this.radioMachine.AutoSize = true;
            this.radioMachine.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioMachine.Location = new System.Drawing.Point(16, 84);
            this.radioMachine.Name = "radioMachine";
            this.radioMachine.Size = new System.Drawing.Size(94, 17);
            this.radioMachine.TabIndex = 4;
            this.radioMachine.TabStop = true;
            this.radioMachine.Text = "machine.xml";
            this.radioMachine.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(31, 103);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(437, 29);
            this.label2.TabIndex = 5;
            this.label2.Text = "This file will be used automatically if SPSD is run on a machine with this comput" +
    "ername.\r\n";
            // 
            // radioUser
            // 
            this.radioUser.AutoSize = true;
            this.radioUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioUser.Location = new System.Drawing.Point(16, 136);
            this.radioUser.Name = "radioUser";
            this.radioUser.Size = new System.Drawing.Size(71, 17);
            this.radioUser.TabIndex = 4;
            this.radioUser.TabStop = true;
            this.radioUser.Text = "user.xml";
            this.radioUser.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(31, 153);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(437, 33);
            this.label3.TabIndex = 5;
            this.label3.Text = "This file will be used automatically if SPSD is run on any machine with this user" +
    "name. It will be preferred if also a machine specific environment XML file exist" +
    "s.\r\n";
            // 
            // radioCustom
            // 
            this.radioCustom.AutoSize = true;
            this.radioCustom.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioCustom.Location = new System.Drawing.Point(16, 188);
            this.radioCustom.Name = "radioCustom";
            this.radioCustom.Size = new System.Drawing.Size(117, 17);
            this.radioCustom.TabIndex = 6;
            this.radioCustom.TabStop = true;
            this.radioCustom.Text = "customname.xml";
            this.radioCustom.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(13, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(483, 20);
            this.label4.TabIndex = 5;
            this.label4.Text = "Preselect the environment XML file name:";
            // 
            // SaveNewFile
            // 
            this.AcceptButton = this.button_Save;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button_Cancel;
            this.ClientSize = new System.Drawing.Size(508, 234);
            this.Controls.Add(this.radioCustom);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.radioUser);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.radioMachine);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.radioDefault);
            this.Controls.Add(this.button_Cancel);
            this.Controls.Add(this.button_Save);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SaveNewFile";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Save environment file as...";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_Save;
        private System.Windows.Forms.Button button_Cancel;
        private System.Windows.Forms.RadioButton radioDefault;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton radioMachine;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton radioUser;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton radioCustom;
        private System.Windows.Forms.Label label4;
    }
}