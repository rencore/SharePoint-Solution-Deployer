namespace SPSD.Editor.Actions
{
    partial class RestartService
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RestartService));
            this.textbox_ActionName = new System.Windows.Forms.TextBox();
            this.checkBox_Action_force = new System.Windows.Forms.CheckBox();
            this.checkBox_Action_retract = new System.Windows.Forms.CheckBox();
            this.checkBox_Action_update = new System.Windows.Forms.CheckBox();
            this.checkBox_Action_deploy = new System.Windows.Forms.CheckBox();
            this.checkBox_Action = new System.Windows.Forms.CheckBox();
            this.label_ActionName = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.textbox_ActionName_val = new SPSD.Editor.Controls.ValidationIndicator();
            this.SuspendLayout();
            // 
            // textbox_ActionName
            // 
            this.textbox_ActionName.Location = new System.Drawing.Point(204, 2);
            this.textbox_ActionName.Name = "textbox_ActionName";
            this.textbox_ActionName.Size = new System.Drawing.Size(113, 20);
            this.textbox_ActionName.TabIndex = 81;
            this.textbox_ActionName.TextChanged += new System.EventHandler(this.textbox_ActionName_TextChanged);
            // 
            // checkBox_Action_force
            // 
            this.checkBox_Action_force.AutoSize = true;
            this.checkBox_Action_force.Location = new System.Drawing.Point(352, 5);
            this.checkBox_Action_force.Name = "checkBox_Action_force";
            this.checkBox_Action_force.Size = new System.Drawing.Size(15, 14);
            this.checkBox_Action_force.TabIndex = 78;
            this.toolTip.SetToolTip(this.checkBox_Action_force, "Forces the action i.e. even if the service is not running before.");
            this.checkBox_Action_force.UseVisualStyleBackColor = true;
            this.checkBox_Action_force.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // checkBox_Action_retract
            // 
            this.checkBox_Action_retract.AutoSize = true;
            this.checkBox_Action_retract.Checked = true;
            this.checkBox_Action_retract.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_Action_retract.Location = new System.Drawing.Point(605, 5);
            this.checkBox_Action_retract.Name = "checkBox_Action_retract";
            this.checkBox_Action_retract.Size = new System.Drawing.Size(15, 14);
            this.checkBox_Action_retract.TabIndex = 79;
            this.toolTip.SetToolTip(this.checkBox_Action_retract, "Run the actions after the retraction of solutions");
            this.checkBox_Action_retract.UseVisualStyleBackColor = true;
            this.checkBox_Action_retract.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // checkBox_Action_update
            // 
            this.checkBox_Action_update.AutoSize = true;
            this.checkBox_Action_update.Checked = true;
            this.checkBox_Action_update.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_Action_update.Location = new System.Drawing.Point(521, 5);
            this.checkBox_Action_update.Name = "checkBox_Action_update";
            this.checkBox_Action_update.Size = new System.Drawing.Size(15, 14);
            this.checkBox_Action_update.TabIndex = 80;
            this.toolTip.SetToolTip(this.checkBox_Action_update, "Run the actions after an update of solutions");
            this.checkBox_Action_update.UseVisualStyleBackColor = true;
            this.checkBox_Action_update.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // checkBox_Action_deploy
            // 
            this.checkBox_Action_deploy.AutoSize = true;
            this.checkBox_Action_deploy.Checked = true;
            this.checkBox_Action_deploy.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_Action_deploy.Location = new System.Drawing.Point(437, 5);
            this.checkBox_Action_deploy.Name = "checkBox_Action_deploy";
            this.checkBox_Action_deploy.Size = new System.Drawing.Size(15, 14);
            this.checkBox_Action_deploy.TabIndex = 77;
            this.toolTip.SetToolTip(this.checkBox_Action_deploy, "Run the actions after the deployment of solutions");
            this.checkBox_Action_deploy.UseVisualStyleBackColor = true;
            this.checkBox_Action_deploy.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // checkBox_Action
            // 
            this.checkBox_Action.Checked = true;
            this.checkBox_Action.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_Action.Location = new System.Drawing.Point(0, 3);
            this.checkBox_Action.Name = "checkBox_Action";
            this.checkBox_Action.Size = new System.Drawing.Size(200, 18);
            this.checkBox_Action.TabIndex = 76;
            this.checkBox_Action.Text = "ActionTitle";
            this.checkBox_Action.CheckedChanged += new System.EventHandler(this.checkBox_Action_CheckedChanged);
            // 
            // label_ActionName
            // 
            this.label_ActionName.Location = new System.Drawing.Point(204, 5);
            this.label_ActionName.Name = "label_ActionName";
            this.label_ActionName.Size = new System.Drawing.Size(111, 14);
            this.label_ActionName.TabIndex = 83;
            this.label_ActionName.Text = "ActionName";
            // 
            // textbox_ActionName_val
            // 
            this.textbox_ActionName_val.BackColor = System.Drawing.Color.White;
            this.textbox_ActionName_val.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("textbox_ActionName_val.BackgroundImage")));
            this.textbox_ActionName_val.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.textbox_ActionName_val.CannotBeEmpty = true;
            this.textbox_ActionName_val.CannotStartWithInteger = false;
            this.textbox_ActionName_val.ControlToValidate = this.textbox_ActionName;
            this.textbox_ActionName_val.DefaultValue = null;
            this.textbox_ActionName_val.InfoValue = null;
            this.textbox_ActionName_val.IsValid = false;
            this.textbox_ActionName_val.Location = new System.Drawing.Point(301, 5);
            this.textbox_ActionName_val.Margin = new System.Windows.Forms.Padding(0);
            this.textbox_ActionName_val.MustBeInt = false;
            this.textbox_ActionName_val.MustBePostitiveInt = false;
            this.textbox_ActionName_val.MustBeUnique = false;
            this.textbox_ActionName_val.Name = "textbox_ActionName_val";
            this.textbox_ActionName_val.NoSpecialChars = false;
            this.textbox_ActionName_val.NoWhiteSpaces = false;
            this.textbox_ActionName_val.SaveErrorFieldDesc = "service name field for the custom restart service action";
            this.textbox_ActionName_val.Size = new System.Drawing.Size(14, 14);
            this.textbox_ActionName_val.TabIndex = 82;
            // 
            // RestartService
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label_ActionName);
            this.Controls.Add(this.textbox_ActionName_val);
            this.Controls.Add(this.textbox_ActionName);
            this.Controls.Add(this.checkBox_Action_force);
            this.Controls.Add(this.checkBox_Action_retract);
            this.Controls.Add(this.checkBox_Action_update);
            this.Controls.Add(this.checkBox_Action_deploy);
            this.Controls.Add(this.checkBox_Action);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "RestartService";
            this.Size = new System.Drawing.Size(662, 25);
            this.Load += new System.EventHandler(this.RestartService_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.ValidationIndicator textbox_ActionName_val;
        private System.Windows.Forms.TextBox textbox_ActionName;
        private System.Windows.Forms.CheckBox checkBox_Action_force;
        private System.Windows.Forms.CheckBox checkBox_Action_retract;
        private System.Windows.Forms.CheckBox checkBox_Action_update;
        private System.Windows.Forms.CheckBox checkBox_Action_deploy;
        private System.Windows.Forms.CheckBox checkBox_Action;
        private System.Windows.Forms.Label label_ActionName;
        private System.Windows.Forms.ToolTip toolTip;
    }
}
