namespace SPSD.Editor.Actions
{
    partial class ResetIIS
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
            this.checkBox_Action_force = new System.Windows.Forms.CheckBox();
            this.checkBox_Action_retract = new System.Windows.Forms.CheckBox();
            this.checkBox_Action_update = new System.Windows.Forms.CheckBox();
            this.checkBox_Action_deploy = new System.Windows.Forms.CheckBox();
            this.checkBox_Action = new System.Windows.Forms.CheckBox();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
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
            this.checkBox_Action_force.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
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
            this.checkBox_Action_retract.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
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
            this.checkBox_Action_update.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
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
            this.checkBox_Action_deploy.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            // 
            // checkBox_Action
            // 
            this.checkBox_Action.Checked = true;
            this.checkBox_Action.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_Action.Location = new System.Drawing.Point(0, 3);
            this.checkBox_Action.Name = "checkBox_Action";
            this.checkBox_Action.Size = new System.Drawing.Size(213, 18);
            this.checkBox_Action.TabIndex = 76;
            this.checkBox_Action.Text = "Reset Internet Information Server (IIS)";
            this.toolTip.SetToolTip(this.checkBox_Action, "Perform IIS reset on this/all servers in the farm");
            this.checkBox_Action.CheckedChanged += new System.EventHandler(this.CheckBox_Action_CheckedChanged);
            // 
            // ResetIIS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.checkBox_Action_force);
            this.Controls.Add(this.checkBox_Action_retract);
            this.Controls.Add(this.checkBox_Action_update);
            this.Controls.Add(this.checkBox_Action_deploy);
            this.Controls.Add(this.checkBox_Action);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "ResetIIS";
            this.Size = new System.Drawing.Size(662, 25);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBox_Action_force;
        private System.Windows.Forms.CheckBox checkBox_Action_retract;
        private System.Windows.Forms.CheckBox checkBox_Action_update;
        private System.Windows.Forms.CheckBox checkBox_Action_deploy;
        private System.Windows.Forms.CheckBox checkBox_Action;
        private System.Windows.Forms.ToolTip toolTip;
    }
}
