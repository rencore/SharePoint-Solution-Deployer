#region

using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using SPSD.Editor.Constants;
using SPSD.Editor.Dialogs;
using SPSD.Editor.Interfaces;
using SPSD.Editor.Model;
using SPSD.Editor.Properties;
using SPSD.Editor.Utilities;

#endregion

namespace SPSD.Editor
{
    public partial class Editor : Form, IFileHandler<Model.SPSD>
    {
        private readonly EnvironmentFileHandler _envFh;

        public Editor()
        {
            _envFh = new EnvironmentFileHandler(this);
            _envFh.FileOperationCompleted += EnvFh_FileOperationCompleted;
            InitializeComponent();
            string[] args = Environment.GetCommandLineArgs();
            try
            {
                if (args.Length > 1)
                {
                    FileInfo fi = new FileInfo(args[1]);
                    SetDefault();
                    _envFh.Load(fi.FullName);
                }
                else
                {
                    _envFh.Clean();
                    NewFile();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void LoadEnv(Model.SPSD node)
        {
            ((IFileHandler<SPSDConfiguration>) configurationTab).LoadEnv(node.Configuration);
            ((IFileHandler<SPSDEnvironment>) environmentTab).LoadEnv(node.Environment);
            ((IFileHandler<SPSDSiteStructures>) siteStructuresTab).LoadEnv(node.SiteStructures);
        }

        public Model.SPSD SaveEnv(Model.SPSD node)
        {
            node.Configuration = ((IFileHandler<SPSDConfiguration>) configurationTab).SaveEnv(node.Configuration);
            node.Environment = ((IFileHandler<SPSDEnvironment>) environmentTab).SaveEnv(node.Environment);
            node.SiteStructures = ((IFileHandler<SPSDSiteStructures>) siteStructuresTab).SaveEnv(node.SiteStructures);
            node.Version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            return node;
        }

        public void SetDefault()
        {
            ((IFileHandler<SPSDConfiguration>) configurationTab).SetDefault();
            ((IFileHandler<SPSDEnvironment>) environmentTab).SetDefault();
            ((IFileHandler<SPSDSiteStructures>) siteStructuresTab).SetDefault();
            _envFh.Clean();
        }

        private void EnvFh_FileOperationCompleted(object sender, EventArgs e)
        {
            saveMenuItem.Enabled = _envFh.IsDirty;
            if (_envFh.Filename != null)
            {
                FileInfo fi = new FileInfo(_envFh.Filepath);

                if (fi.Exists)
                {
                    openFileDialog.InitialDirectory = fi.DirectoryName;
                    saveFileDialog.InitialDirectory = fi.DirectoryName;
                    saveFileDialog.FileName = fi.Name;
                }
            }
            UpdateFormTitle();
        }


        private bool SaveAs()
        {
            try
            {
                if (_envFh.IsNew)
                {
                    SaveNewFile dl = new SaveNewFile();

                    if (dl.ShowDialog() == DialogResult.Cancel)
                    {
                        return false;
                    }
                    saveFileDialog.FileName = dl.FileName;
                }
                else
                {
                    saveFileDialog.FileName = _envFh.Filename;
                }
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    openFileDialog.FileName = saveFileDialog.FileName;
                    openFileDialog.InitialDirectory = saveFileDialog.InitialDirectory;
                    return _envFh.Save(saveFileDialog.FileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return false;
        }

        private void UpdateFormTitle()
        {
            string filename = _envFh.Filename;
            if (string.IsNullOrEmpty(filename))
            {
                filename = "unsaved";
            }
            Text = string.Format("{0} - {2}{1} {3}", Globals.APP_TITLE, filename, _envFh.IsDirty ? "*" : "",
                                 _envFh.IsReadonly ? "(readonly)" : "");
        }


        private void NewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewFile();
        }

        private void NewFile()
        {
            if (_envFh.Load(null))
            {
                SetDefault();
                EnvironmentFileHandler.MakeSingletonDirty();
            }
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    SetDefault();
                    _envFh.Load(openFileDialog.FileName);
                    saveFileDialog.FileName = openFileDialog.FileName;
                    saveFileDialog.InitialDirectory = openFileDialog.InitialDirectory;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_envFh.IsNew)
            {
                SaveAs();
            }
            else
            {
                _envFh.Save();
            }
        }

        private void SaveasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveAs();
        }

        private void QuitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_envFh.IsDirty)
            {
                if (
                    MessageBox.Show(
                        Resources.Editor_QuitToolStripMenuItem_Click_Save_changes_to_the_environment_file_before_exit_,
                        Resources.Editor_QuitToolStripMenuItem_Click_Save, MessageBoxButtons.YesNo) ==
                    DialogResult.Yes)
                {
                    if (_envFh.IsNew && !SaveAs())
                    {
                        return;
                    }
                    else if (!_envFh.Save())
                    {
                        return;
                    }
                }
            }
            Close();
        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About about = new About();
            about.ShowDialog();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            //Graphics g = e.Graphics;
            //Image image = Resources.SPSD_image_thumb;
            //g.DrawImage(image, Width - image.Width, 0);
            base.OnPaint(e);
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
        }

        private void OpenProjectSite(object sender, EventArgs e)
        {
            Url.Open("http://spsd.codeplex.com");
        }

        private void OpenDocumentationSite(object sender, EventArgs e)
        {
            Url.Open("http://spsd.codeplex.com/documentation");
        }
    }
}