namespace SPSD.Editor
{
    partial class Editor
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Editor));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.newMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.saveMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveasMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.quitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.projectSiteMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.documentationMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tabControl = new System.Windows.Forms.TabControl();
            this.envTab = new System.Windows.Forms.TabPage();
            this.configTab = new System.Windows.Forms.TabPage();
            this.structTab = new System.Windows.Forms.TabPage();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.environmentTab = new SPSD.Editor.Tabs.EnvironmentTab();
            this.configurationTab = new SPSD.Editor.ConfigurationTab();
            this.siteStructuresTab = new SPSD.Editor.Tabs.SiteStructuresTab();
            this.menuStrip.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.envTab.SuspendLayout();
            this.configTab.SuspendLayout();
            this.structTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.BackgroundImage = global::SPSD.Editor.Properties.Resources.transpix;
            this.menuStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newMenuItem,
            this.helpMenuItem});
            this.menuStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(772, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip";
            // 
            // newMenuItem
            // 
            this.newMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openMenuItem,
            this.toolStripSeparator2,
            this.saveMenuItem,
            this.saveasMenuItem,
            this.toolStripSeparator1,
            this.quitMenuItem});
            this.newMenuItem.Name = "newMenuItem";
            this.newMenuItem.Size = new System.Drawing.Size(40, 20);
            this.newMenuItem.Text = "&FILE";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Image = global::SPSD.Editor.Properties.Resources.New;
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.newToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.newToolStripMenuItem.Text = "&New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.NewToolStripMenuItem_Click);
            // 
            // openMenuItem
            // 
            this.openMenuItem.Name = "openMenuItem";
            this.openMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openMenuItem.Size = new System.Drawing.Size(146, 22);
            this.openMenuItem.Text = "&Open";
            this.openMenuItem.Click += new System.EventHandler(this.OpenToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(143, 6);
            // 
            // saveMenuItem
            // 
            this.saveMenuItem.Image = global::SPSD.Editor.Properties.Resources.SaveFile;
            this.saveMenuItem.Name = "saveMenuItem";
            this.saveMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveMenuItem.Size = new System.Drawing.Size(146, 22);
            this.saveMenuItem.Text = "&Save";
            this.saveMenuItem.Click += new System.EventHandler(this.SaveToolStripMenuItem_Click);
            // 
            // saveasMenuItem
            // 
            this.saveasMenuItem.Name = "saveasMenuItem";
            this.saveasMenuItem.Size = new System.Drawing.Size(146, 22);
            this.saveasMenuItem.Text = "Save &as...";
            this.saveasMenuItem.Click += new System.EventHandler(this.SaveasToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(143, 6);
            // 
            // quitMenuItem
            // 
            this.quitMenuItem.Name = "quitMenuItem";
            this.quitMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.quitMenuItem.Size = new System.Drawing.Size(146, 22);
            this.quitMenuItem.Text = "&Quit";
            this.quitMenuItem.Click += new System.EventHandler(this.QuitToolStripMenuItem_Click);
            // 
            // helpMenuItem
            // 
            this.helpMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.projectSiteMenuItem,
            this.documentationMenuItem,
            this.toolStripSeparator3,
            this.aboutMenuItem});
            this.helpMenuItem.Name = "helpMenuItem";
            this.helpMenuItem.Size = new System.Drawing.Size(47, 20);
            this.helpMenuItem.Text = "&HELP";
            this.helpMenuItem.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // projectSiteMenuItem
            // 
            this.projectSiteMenuItem.Name = "projectSiteMenuItem";
            this.projectSiteMenuItem.Size = new System.Drawing.Size(170, 22);
            this.projectSiteMenuItem.Text = "&SPSD on CodePlex";
            this.projectSiteMenuItem.Click += new System.EventHandler(this.OpenProjectSite);
            // 
            // documentationMenuItem
            // 
            this.documentationMenuItem.Name = "documentationMenuItem";
            this.documentationMenuItem.Size = new System.Drawing.Size(170, 22);
            this.documentationMenuItem.Text = "&Documentation";
            this.documentationMenuItem.Click += new System.EventHandler(this.OpenDocumentationSite);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(167, 6);
            // 
            // aboutMenuItem
            // 
            this.aboutMenuItem.Name = "aboutMenuItem";
            this.aboutMenuItem.Size = new System.Drawing.Size(170, 22);
            this.aboutMenuItem.Text = "&About";
            this.aboutMenuItem.Click += new System.EventHandler(this.AboutToolStripMenuItem_Click);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Name = "contextMenuStrip1";
            this.contextMenuStrip.Size = new System.Drawing.Size(61, 4);
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.envTab);
            this.tabControl.Controls.Add(this.configTab);
            this.tabControl.Controls.Add(this.structTab);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 24);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(772, 579);
            this.tabControl.TabIndex = 2;
            // 
            // envTab
            // 
            this.envTab.Controls.Add(this.environmentTab);
            this.envTab.Location = new System.Drawing.Point(4, 22);
            this.envTab.Name = "envTab";
            this.envTab.Padding = new System.Windows.Forms.Padding(3);
            this.envTab.Size = new System.Drawing.Size(764, 553);
            this.envTab.TabIndex = 1;
            this.envTab.Text = "Environment";
            this.envTab.UseVisualStyleBackColor = true;
            // 
            // configTab
            // 
            this.configTab.AutoScroll = true;
            this.configTab.Controls.Add(this.configurationTab);
            this.configTab.Location = new System.Drawing.Point(4, 22);
            this.configTab.Name = "configTab";
            this.configTab.Padding = new System.Windows.Forms.Padding(3);
            this.configTab.Size = new System.Drawing.Size(764, 553);
            this.configTab.TabIndex = 0;
            this.configTab.Text = "Configuration";
            this.configTab.UseVisualStyleBackColor = true;
            // 
            // structTab
            // 
            this.structTab.Controls.Add(this.siteStructuresTab);
            this.structTab.Location = new System.Drawing.Point(4, 22);
            this.structTab.Name = "structTab";
            this.structTab.Size = new System.Drawing.Size(764, 553);
            this.structTab.TabIndex = 2;
            this.structTab.Text = "SiteStructures";
            this.structTab.UseVisualStyleBackColor = true;
            // 
            // openFileDialog
            // 
            this.openFileDialog.DefaultExt = "xml";
            this.openFileDialog.Filter = "SPSD files (XML)|*.xml";
            this.openFileDialog.ReadOnlyChecked = true;
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.DefaultExt = "xml";
            this.saveFileDialog.Filter = "SPSD files (XML)|*.xml";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.BackgroundImage = global::SPSD.Editor.Properties.Resources.SPSD_text_small;
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox2.Location = new System.Drawing.Point(594, 2);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(176, 42);
            this.pictureBox2.TabIndex = 4;
            this.pictureBox2.TabStop = false;
            this.toolTip.SetToolTip(this.pictureBox2, "Open project site on CodePlex");
            this.pictureBox2.Click += new System.EventHandler(this.OpenProjectSite);
            // 
            // environmentTab
            // 
            this.environmentTab.AutoScroll = true;
            this.environmentTab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.environmentTab.Location = new System.Drawing.Point(3, 3);
            this.environmentTab.MinimumSize = new System.Drawing.Size(740, 0);
            this.environmentTab.Name = "environmentTab";
            this.environmentTab.Size = new System.Drawing.Size(758, 547);
            this.environmentTab.TabIndex = 0;
            // 
            // configurationTab
            // 
            this.configurationTab.AutoScroll = true;
            this.configurationTab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.configurationTab.Location = new System.Drawing.Point(3, 3);
            this.configurationTab.MinimumSize = new System.Drawing.Size(760, 0);
            this.configurationTab.Name = "configurationTab";
            this.configurationTab.Size = new System.Drawing.Size(760, 547);
            this.configurationTab.TabIndex = 0;
            // 
            // siteStructuresTab
            // 
            this.siteStructuresTab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.siteStructuresTab.Location = new System.Drawing.Point(0, 0);
            this.siteStructuresTab.Name = "siteStructuresTab";
            this.siteStructuresTab.Size = new System.Drawing.Size(764, 553);
            this.siteStructuresTab.TabIndex = 1;
            // 
            // Editor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(772, 603);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.menuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.MinimumSize = new System.Drawing.Size(780, 500);
            this.Name = "Editor";
            this.Text = "SPSD Environment Editor";
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.envTab.ResumeLayout(false);
            this.configTab.ResumeLayout(false);
            this.structTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem newMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage envTab;
        private System.Windows.Forms.TabPage structTab;
        private System.Windows.Forms.TabPage configTab;
        private ConfigurationTab configurationTab;
        private Tabs.EnvironmentTab environmentTab;
        private Tabs.SiteStructuresTab siteStructuresTab;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem saveMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveasMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem quitMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.ToolStripMenuItem helpMenuItem;
        private System.Windows.Forms.ToolStripMenuItem projectSiteMenuItem;
        private System.Windows.Forms.ToolStripMenuItem documentationMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolTip toolTip;
    }
}

