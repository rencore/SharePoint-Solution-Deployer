namespace SPSD.Editor.Actions
{
    partial class RecycleAppPools
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
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.radioButton_Action_AllAppPool = new System.Windows.Forms.RadioButton();
            this.radioButton_Action_SPAppPool = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.checkBox_Action_retract = new System.Windows.Forms.CheckBox();
            this.checkBox_Action_update = new System.Windows.Forms.CheckBox();
            this.checkBox_Action_deploy = new System.Windows.Forms.CheckBox();
            this.checkBox_Action = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // radioButton_Action_AllAppPool
            // 
            this.radioButton_Action_AllAppPool.Enabled = false;
            this.radioButton_Action_AllAppPool.Location = new System.Drawing.Point(185, 36);
            this.radioButton_Action_AllAppPool.Name = "radioButton_Action_AllAppPool";
            this.radioButton_Action_AllAppPool.Size = new System.Drawing.Size(145, 24);
            this.radioButton_Action_AllAppPool.TabIndex = 81;
            this.radioButton_Action_AllAppPool.Text = "All AppPools";
            this.toolTip.SetToolTip(this.radioButton_Action_AllAppPool, "Recycle all application pools, also the ones which don\'t belong to SharePoint");
            this.radioButton_Action_AllAppPool.UseVisualStyleBackColor = true;
            // 
            // radioButton_Action_SPAppPool
            // 
            this.radioButton_Action_SPAppPool.Checked = true;
            this.radioButton_Action_SPAppPool.Enabled = false;
            this.radioButton_Action_SPAppPool.Location = new System.Drawing.Point(185, 14);
            this.radioButton_Action_SPAppPool.Name = "radioButton_Action_SPAppPool";
            this.radioButton_Action_SPAppPool.Size = new System.Drawing.Size(145, 24);
            this.radioButton_Action_SPAppPool.TabIndex = 80;
            this.radioButton_Action_SPAppPool.TabStop = true;
            this.radioButton_Action_SPAppPool.Text = "SharePoint AppPools";
            this.toolTip.SetToolTip(this.radioButton_Action_SPAppPool, "Recycle only application pools which are used by SharePoint on the server");
            this.radioButton_Action_SPAppPool.UseVisualStyleBackColor = true;
            this.radioButton_Action_SPAppPool.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(490, 1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 77;
            this.label1.Text = "After update";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip.SetToolTip(this.label1, "Run the actions after an update of solutions");
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(576, 1);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 78;
            this.label2.Text = "After retract";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip.SetToolTip(this.label2, "Run the actions after the retraction of solutions");
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(406, 1);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 13);
            this.label4.TabIndex = 79;
            this.label4.Text = "After deploy";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip.SetToolTip(this.label4, "Run the actions after the deployment of solutions");
            // 
            // checkBox_Action_retract
            // 
            this.checkBox_Action_retract.AutoSize = true;
            this.checkBox_Action_retract.Checked = true;
            this.checkBox_Action_retract.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_Action_retract.Enabled = false;
            this.checkBox_Action_retract.Location = new System.Drawing.Point(605, 20);
            this.checkBox_Action_retract.Name = "checkBox_Action_retract";
            this.checkBox_Action_retract.Size = new System.Drawing.Size(15, 14);
            this.checkBox_Action_retract.TabIndex = 75;
            this.toolTip.SetToolTip(this.checkBox_Action_retract, "Run the actions after the retraction of solutions");
            this.checkBox_Action_retract.UseVisualStyleBackColor = true;
            this.checkBox_Action_retract.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            // 
            // checkBox_Action_update
            // 
            this.checkBox_Action_update.AutoSize = true;
            this.checkBox_Action_update.Checked = true;
            this.checkBox_Action_update.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_Action_update.Enabled = false;
            this.checkBox_Action_update.Location = new System.Drawing.Point(521, 20);
            this.checkBox_Action_update.Name = "checkBox_Action_update";
            this.checkBox_Action_update.Size = new System.Drawing.Size(15, 14);
            this.checkBox_Action_update.TabIndex = 76;
            this.toolTip.SetToolTip(this.checkBox_Action_update, "Run the actions after an update of solutions");
            this.checkBox_Action_update.UseVisualStyleBackColor = true;
            this.checkBox_Action_update.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            // 
            // checkBox_Action_deploy
            // 
            this.checkBox_Action_deploy.AutoSize = true;
            this.checkBox_Action_deploy.Checked = true;
            this.checkBox_Action_deploy.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_Action_deploy.Enabled = false;
            this.checkBox_Action_deploy.Location = new System.Drawing.Point(437, 20);
            this.checkBox_Action_deploy.Name = "checkBox_Action_deploy";
            this.checkBox_Action_deploy.Size = new System.Drawing.Size(15, 14);
            this.checkBox_Action_deploy.TabIndex = 74;
            this.toolTip.SetToolTip(this.checkBox_Action_deploy, "Run the actions after the deployment of solutions");
            this.checkBox_Action_deploy.UseVisualStyleBackColor = true;
            this.checkBox_Action_deploy.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            // 
            // checkBox_Action
            // 
            this.checkBox_Action.Location = new System.Drawing.Point(0, 18);
            this.checkBox_Action.Name = "checkBox_Action";
            this.checkBox_Action.Size = new System.Drawing.Size(180, 19);
            this.checkBox_Action.TabIndex = 73;
            this.checkBox_Action.Text = "Recycle Application Pools";
            this.toolTip.SetToolTip(this.checkBox_Action, "Recycles all IIS application pools on this/all servers in the farm\r\nCan be used a" +
        "lternatively to the ResetIIS action");
            this.checkBox_Action.CheckedChanged += new System.EventHandler(this.CheckBox_RecycleAppPool_CheckedChanged);
            // 
            // RecycleAppPools
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.radioButton_Action_AllAppPool);
            this.Controls.Add(this.radioButton_Action_SPAppPool);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.checkBox_Action_retract);
            this.Controls.Add(this.checkBox_Action_update);
            this.Controls.Add(this.checkBox_Action_deploy);
            this.Controls.Add(this.checkBox_Action);
            this.Name = "RecycleAppPools";
            this.Size = new System.Drawing.Size(661, 63);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.RadioButton radioButton_Action_AllAppPool;
        private System.Windows.Forms.RadioButton radioButton_Action_SPAppPool;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox checkBox_Action_retract;
        private System.Windows.Forms.CheckBox checkBox_Action_update;
        private System.Windows.Forms.CheckBox checkBox_Action_deploy;
        private System.Windows.Forms.CheckBox checkBox_Action;
    }
}
