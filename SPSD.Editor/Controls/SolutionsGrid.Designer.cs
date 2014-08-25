namespace SPSD.Editor.Controls
{
    partial class SolutionsGrid
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SolutionsGrid));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGrid_Solutions = new System.Windows.Forms.DataGridView();
            this.contextMenu_GridView = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editUrlsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.copyRowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteRowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteRowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.dataGridViewImageColumn2 = new System.Windows.Forms.DataGridViewImageColumn();
            this.CellMenu = new System.Windows.Forms.DataGridViewButtonColumn();
            this.SolutionUrls = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SolutionOverwrite = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.SolutionForce = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.SolutionType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.SolutionName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid_Solutions)).BeginInit();
            this.contextMenu_GridView.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGrid_Solutions
            // 
            this.dataGrid_Solutions.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGrid_Solutions.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGrid_Solutions.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGrid_Solutions.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dataGrid_Solutions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGrid_Solutions.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SolutionName,
            this.SolutionType,
            this.SolutionForce,
            this.SolutionOverwrite,
            this.SolutionUrls,
            this.CellMenu});
            this.dataGrid_Solutions.ContextMenuStrip = this.contextMenu_GridView;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGrid_Solutions.DefaultCellStyle = dataGridViewCellStyle5;
            this.dataGrid_Solutions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGrid_Solutions.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGrid_Solutions.Location = new System.Drawing.Point(0, 0);
            this.dataGrid_Solutions.Name = "dataGrid_Solutions";
            this.dataGrid_Solutions.RowHeadersVisible = false;
            this.dataGrid_Solutions.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGrid_Solutions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGrid_Solutions.Size = new System.Drawing.Size(639, 508);
            this.dataGrid_Solutions.TabIndex = 23;
            this.dataGrid_Solutions.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGrid_Solutions_CellClick);
            this.dataGrid_Solutions.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGrid_Solutions_CellContentClick);
            this.dataGrid_Solutions.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dataGrid_Solutions_CellPainting);
            this.dataGrid_Solutions.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dataGrid_Solutions_CellValidating);
            this.dataGrid_Solutions.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGrid_Solutions_CellValueChanged);
            this.dataGrid_Solutions.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.dataGrid_Solutions_RowPrePaint);
            this.dataGrid_Solutions.EnabledChanged += new System.EventHandler(this.dataGrid_Solutions_EnabledChanged);
            // 
            // contextMenu_GridView
            // 
            this.contextMenu_GridView.BackColor = System.Drawing.SystemColors.Control;
            this.contextMenu_GridView.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editUrlsToolStripMenuItem,
            this.toolStripSeparator8,
            this.copyRowToolStripMenuItem,
            this.pasteRowToolStripMenuItem,
            this.deleteRowToolStripMenuItem});
            this.contextMenu_GridView.Name = "contextMenu_GridView";
            this.contextMenu_GridView.Size = new System.Drawing.Size(160, 98);
            // 
            // editUrlsToolStripMenuItem
            // 
            this.editUrlsToolStripMenuItem.Name = "editUrlsToolStripMenuItem";
            this.editUrlsToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.editUrlsToolStripMenuItem.Text = "Edit Urls...";
            this.editUrlsToolStripMenuItem.Click += new System.EventHandler(this.editUrlsToolStripMenuItem_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(156, 6);
            // 
            // copyRowToolStripMenuItem
            // 
            this.copyRowToolStripMenuItem.Name = "copyRowToolStripMenuItem";
            this.copyRowToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.copyRowToolStripMenuItem.Text = "Copy row";
            this.copyRowToolStripMenuItem.Visible = false;
            this.copyRowToolStripMenuItem.Click += new System.EventHandler(this.copyRowToolStripMenuItem_Click);
            // 
            // pasteRowToolStripMenuItem
            // 
            this.pasteRowToolStripMenuItem.Name = "pasteRowToolStripMenuItem";
            this.pasteRowToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.pasteRowToolStripMenuItem.Text = "Paste as new row";
            this.pasteRowToolStripMenuItem.Visible = false;
            this.pasteRowToolStripMenuItem.Click += new System.EventHandler(this.pasteRowToolStripMenuItem_Click);
            // 
            // deleteRowToolStripMenuItem
            // 
            this.deleteRowToolStripMenuItem.Name = "deleteRowToolStripMenuItem";
            this.deleteRowToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.deleteRowToolStripMenuItem.Text = "Delete row";
            this.deleteRowToolStripMenuItem.Click += new System.EventHandler(this.deleteRowToolStripMenuItem_Click);
            // 
            // dataGridViewImageColumn1
            // 
            this.dataGridViewImageColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            dataGridViewCellStyle6.NullValue = ((object)(resources.GetObject("dataGridViewCellStyle6.NullValue")));
            this.dataGridViewImageColumn1.DefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridViewImageColumn1.FillWeight = 1F;
            this.dataGridViewImageColumn1.HeaderText = "";
            this.dataGridViewImageColumn1.Image = global::SPSD.Editor.Properties.Resources.delete_bitmap;
            this.dataGridViewImageColumn1.Name = "dataGridViewImageColumn1";
            this.dataGridViewImageColumn1.Width = 30;
            // 
            // dataGridViewImageColumn2
            // 
            this.dataGridViewImageColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            dataGridViewCellStyle7.NullValue = ((object)(resources.GetObject("dataGridViewCellStyle7.NullValue")));
            this.dataGridViewImageColumn2.DefaultCellStyle = dataGridViewCellStyle7;
            this.dataGridViewImageColumn2.FillWeight = 1F;
            this.dataGridViewImageColumn2.HeaderText = "";
            this.dataGridViewImageColumn2.Image = ((System.Drawing.Image)(resources.GetObject("dataGridViewImageColumn2.Image")));
            this.dataGridViewImageColumn2.Name = "dataGridViewImageColumn2";
            this.dataGridViewImageColumn2.ReadOnly = true;
            this.dataGridViewImageColumn2.Width = 30;
            // 
            // CellMenu
            // 
            this.CellMenu.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            this.CellMenu.DefaultCellStyle = dataGridViewCellStyle4;
            this.CellMenu.FillWeight = 1F;
            this.CellMenu.HeaderText = "";
            this.CellMenu.MinimumWidth = 22;
            this.CellMenu.Name = "CellMenu";
            this.CellMenu.ReadOnly = true;
            this.CellMenu.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.CellMenu.Text = "...";
            this.CellMenu.UseColumnTextForButtonValue = true;
            this.CellMenu.Width = 22;
            // 
            // SolutionUrls
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.SolutionUrls.DefaultCellStyle = dataGridViewCellStyle3;
            this.SolutionUrls.HeaderText = "Urls";
            this.SolutionUrls.Name = "SolutionUrls";
            this.SolutionUrls.ToolTipText = "Semicolon separated URLs (can include variables)";
            // 
            // SolutionOverwrite
            // 
            this.SolutionOverwrite.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.SolutionOverwrite.HeaderText = "Overwrite";
            this.SolutionOverwrite.MinimumWidth = 55;
            this.SolutionOverwrite.Name = "SolutionOverwrite";
            this.SolutionOverwrite.ToolTipText = "Set to overwrite solutions if they already exist, solution will be retracted befo" +
    "re deployment";
            this.SolutionOverwrite.Width = 55;
            // 
            // SolutionForce
            // 
            this.SolutionForce.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.SolutionForce.Frozen = true;
            this.SolutionForce.HeaderText = "Force";
            this.SolutionForce.MinimumWidth = 40;
            this.SolutionForce.Name = "SolutionForce";
            this.SolutionForce.ToolTipText = "Set to force the deploy/retract/update command";
            this.SolutionForce.Width = 40;
            // 
            // SolutionType
            // 
            this.SolutionType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            this.SolutionType.DefaultCellStyle = dataGridViewCellStyle2;
            this.SolutionType.Frozen = true;
            this.SolutionType.HeaderText = "Type";
            this.SolutionType.Items.AddRange(new object[] {
            "GAC",
            "WebApplication",
            "SiteCollection"});
            this.SolutionType.MinimumWidth = 100;
            this.SolutionType.Name = "SolutionType";
            // 
            // SolutionName
            // 
            this.SolutionName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            this.SolutionName.DefaultCellStyle = dataGridViewCellStyle1;
            this.SolutionName.FillWeight = 50F;
            this.SolutionName.Frozen = true;
            this.SolutionName.HeaderText = "Solution Name";
            this.SolutionName.MinimumWidth = 150;
            this.SolutionName.Name = "SolutionName";
            this.SolutionName.Width = 150;
            // 
            // SolutionsGrid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dataGrid_Solutions);
            this.Name = "SolutionsGrid";
            this.Size = new System.Drawing.Size(639, 508);
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid_Solutions)).EndInit();
            this.contextMenu_GridView.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGrid_Solutions;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn1;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn2;
        private System.Windows.Forms.ContextMenuStrip contextMenu_GridView;
        private System.Windows.Forms.ToolStripMenuItem editUrlsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripMenuItem copyRowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteRowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteRowToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn SolutionName;
        private System.Windows.Forms.DataGridViewComboBoxColumn SolutionType;
        private System.Windows.Forms.DataGridViewCheckBoxColumn SolutionForce;
        private System.Windows.Forms.DataGridViewCheckBoxColumn SolutionOverwrite;
        private System.Windows.Forms.DataGridViewTextBoxColumn SolutionUrls;
        private System.Windows.Forms.DataGridViewButtonColumn CellMenu;
    }
}
