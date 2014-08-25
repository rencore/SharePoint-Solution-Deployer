namespace SPSD.Editor.Sections
{
    partial class VariablesSection
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VariablesSection));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.dataGrid_Variables = new System.Windows.Forms.DataGridView();
            this.VariableName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VariableValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DeleteEntry = new System.Windows.Forms.DataGridViewButtonColumn();
            this.contextMenu_GridView = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editVariableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.copyRowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteRowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteRowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid_Variables)).BeginInit();
            this.contextMenu_GridView.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainPanel
            // 
            this.mainPanel.Controls.Add(this.label4);
            this.mainPanel.Controls.Add(this.dataGrid_Variables);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(0, 0);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(954, 498);
            this.mainPanel.TabIndex = 0;
            this.mainPanel.Text = "Variables";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(0, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(954, 62);
            this.label4.TabIndex = 25;
            this.label4.Text = resources.GetString("label4.Text");
            // 
            // dataGrid_Variables
            // 
            this.dataGrid_Variables.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGrid_Variables.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGrid_Variables.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGrid_Variables.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGrid_Variables.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dataGrid_Variables.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGrid_Variables.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.VariableName,
            this.VariableValue,
            this.DeleteEntry});
            this.dataGrid_Variables.ContextMenuStrip = this.contextMenu_GridView;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGrid_Variables.DefaultCellStyle = dataGridViewCellStyle4;
            this.dataGrid_Variables.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGrid_Variables.Location = new System.Drawing.Point(0, 65);
            this.dataGrid_Variables.Name = "dataGrid_Variables";
            this.dataGrid_Variables.RowHeadersVisible = false;
            this.dataGrid_Variables.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGrid_Variables.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGrid_Variables.Size = new System.Drawing.Size(954, 433);
            this.dataGrid_Variables.TabIndex = 24;
            this.dataGrid_Variables.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGrid_Variables_CellContentClick);
            this.dataGrid_Variables.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.DataGrid_Variables_CellPainting);
            this.dataGrid_Variables.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.DataGrid_Variables_CellValidating);
            this.dataGrid_Variables.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGrid_Variables_CellValueChanged);
            this.dataGrid_Variables.EnabledChanged += new System.EventHandler(this.DataGrid_Variables_EnabledChanged);
            // 
            // VariableName
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            this.VariableName.DefaultCellStyle = dataGridViewCellStyle1;
            this.VariableName.FillWeight = 50F;
            this.VariableName.HeaderText = "Variable Name";
            this.VariableName.Name = "VariableName";
            this.VariableName.ToolTipText = "A unique name in the variables sections.";
            // 
            // VariableValue
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.VariableValue.DefaultCellStyle = dataGridViewCellStyle2;
            this.VariableValue.HeaderText = "Variable Value";
            this.VariableValue.Name = "VariableValue";
            this.VariableValue.ToolTipText = "Can use other system and custom variables (avoid reference circles!)";
            // 
            // DeleteEntry
            // 
            this.DeleteEntry.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            this.DeleteEntry.DefaultCellStyle = dataGridViewCellStyle3;
            this.DeleteEntry.FillWeight = 1F;
            this.DeleteEntry.HeaderText = "";
            this.DeleteEntry.MinimumWidth = 22;
            this.DeleteEntry.Name = "DeleteEntry";
            this.DeleteEntry.ReadOnly = true;
            this.DeleteEntry.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.DeleteEntry.Text = "...";
            this.DeleteEntry.UseColumnTextForButtonValue = true;
            this.DeleteEntry.Width = 22;
            // 
            // contextMenu_GridView
            // 
            this.contextMenu_GridView.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editVariableToolStripMenuItem,
            this.toolStripSeparator8,
            this.copyRowToolStripMenuItem,
            this.pasteRowToolStripMenuItem,
            this.deleteRowToolStripMenuItem});
            this.contextMenu_GridView.Name = "contextMenu_GridView";
            this.contextMenu_GridView.Size = new System.Drawing.Size(160, 98);
            // 
            // editVariableToolStripMenuItem
            // 
            this.editVariableToolStripMenuItem.Name = "editVariableToolStripMenuItem";
            this.editVariableToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.editVariableToolStripMenuItem.Text = "Edit Variable...";
            this.editVariableToolStripMenuItem.Click += new System.EventHandler(this.EditVariableToolStripMenuItem_Click);
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
            this.copyRowToolStripMenuItem.Click += new System.EventHandler(this.CopyRowToolStripMenuItem_Click);
            // 
            // pasteRowToolStripMenuItem
            // 
            this.pasteRowToolStripMenuItem.Name = "pasteRowToolStripMenuItem";
            this.pasteRowToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.pasteRowToolStripMenuItem.Text = "Paste as new row";
            this.pasteRowToolStripMenuItem.Visible = false;
            this.pasteRowToolStripMenuItem.Click += new System.EventHandler(this.PasteRowToolStripMenuItem_Click);
            // 
            // deleteRowToolStripMenuItem
            // 
            this.deleteRowToolStripMenuItem.Name = "deleteRowToolStripMenuItem";
            this.deleteRowToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.deleteRowToolStripMenuItem.Text = "Delete row";
            this.deleteRowToolStripMenuItem.Click += new System.EventHandler(this.DeleteRowToolStripMenuItem_Click);
            // 
            // VariablesSection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.mainPanel);
            this.Name = "VariablesSection";
            this.Size = new System.Drawing.Size(954, 498);
            this.mainPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid_Variables)).EndInit();
            this.contextMenu_GridView.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.DataGridView dataGrid_Variables;
        private System.Windows.Forms.ContextMenuStrip contextMenu_GridView;
        private System.Windows.Forms.ToolStripMenuItem editVariableToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripMenuItem copyRowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteRowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteRowToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn VariableName;
        private System.Windows.Forms.DataGridViewTextBoxColumn VariableValue;
        private System.Windows.Forms.DataGridViewButtonColumn DeleteEntry;
        private System.Windows.Forms.Label label4;

    }
}
