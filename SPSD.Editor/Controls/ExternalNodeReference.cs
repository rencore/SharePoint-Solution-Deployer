#region

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using System.Xml.XPath;
using SPSD.Editor.Interfaces;
using SPSD.Editor.Properties;
using SPSD.Editor.Utilities;

#endregion

namespace SPSD.Editor.Controls
{
    public partial class ExternalNodeReference : UserControl, IFileHandler<IExternalizable>
    {
        private string _controlLocationName;

        public ExternalNodeReference()
        {
            InitializeComponent();
            SetDefault();
        }

        [Description(
            "The location were this control is displayed, to be used for error messages when a validation fails")]
        public string ControlLocationName
        {
            get { return _controlLocationName; }
            set
            {
                _controlLocationName = value;
                textBox_NodeId_val.SaveErrorFieldDesc = string.Format("node id field on the {0}", _controlLocationName);
                textBox_ExtNodeFile_val.SaveErrorFieldDesc = string.Format("external file field on the {0}",
                                                                           _controlLocationName);
                comboBox_ExtNodeId_val.SaveErrorFieldDesc = string.Format("external node id field on the {0}",
                                                                          _controlLocationName);
            }
        }

        [Category("External Node Reference")]
        public bool IsExternal
        {
            get { return !radioButton_IntNode.Checked; }
        }

        [Category("External Node Reference")]
        public string XPath { get; set; }

        public void SetDefault()
        {
            radioButton_IntNode.Checked = true;
            textBox_IntNodeId.Text = Resources.ExternalNodeReference_SetDefault_Default;
            textBox_ExtNodeFile.Text = string.Empty;
            comboBox_ExtNodeId.Items.Clear();
            comboBox_ExtNodeId.Text = string.Empty;
            textBox_NodeId_val.Validate();
            textBox_ExtNodeFile_val.Validate();
            comboBox_ExtNodeId_val.Validate();
        }

        void IFileHandler<IExternalizable>.LoadEnv(IExternalizable node)
        {
            if (node == null)
            {
                radioButton_NoNode.Checked = true;
                return;
            }
            bool isExternalNode = !string.IsNullOrEmpty(node.FilePath);
            radioButton_ExtNode.Checked = isExternalNode;
            radioButton_IntNode.Checked = !isExternalNode;
            if (isExternalNode)
            {
                textBox_ExtNodeFile.Text = node.FilePath;
                textBox_ExtNodeFile_val.Validate();
                LoadExternalNodeIds(node.FilePath);
                comboBox_ExtNodeId.Text = node.ID;
                comboBox_ExtNodeId_val.Validate();
            }

            textBox_IntNodeId.Text = node.ID;
            textBox_NodeId_val.Validate();
        }

        IExternalizable IFileHandler<IExternalizable>.SaveEnv(IExternalizable node)
        {
            if (radioButton_NoNode.Checked)
            {
                return null;
            }
            if (IsExternal)
            {
                node.FilePath = textBox_ExtNodeFile_val.GetValidatedValue();
                node.ID = comboBox_ExtNodeId_val.GetValidatedValue();
            }
            else
            {
                node.ID = textBox_NodeId_val.GetValidatedValue();
                node.FilePath = null;
            }
            return node;
        }


        private void Node_Changed(object sender, EventArgs e)
        {
            textBox_ExtNodeFile.Enabled = radioButton_ExtNode.Checked;
            button_ExtNodeBrowse.Enabled = radioButton_ExtNode.Checked;
            comboBox_ExtNodeId.Enabled = radioButton_ExtNode.Checked;
            textBox_IntNodeId.Enabled = radioButton_IntNode.Checked;
            EnvironmentFileHandler.MakeSingletonDirty();
            NodeReferenceHasChanged.Invoke(sender, e);
        }

        [Category("External Node Reference")]
        public event EventHandler NodeReferenceHasChanged;


        private void Changed(object sender, EventArgs e)
        {
            EnvironmentFileHandler.MakeSingletonDirty();
        }

        private void BrowseButton_Click(object sender, EventArgs e)
        {
            if (EnvironmentFileHandler.IsSingletonNew)
            {
                MessageBox.Show(
                    Resources.ExternalNodeReference_FileUnsavedWarningText,
                    Resources.ExternalNodeReference_FileUnsavedWarningTitle, MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }
            FileInfo fi = EnvironmentFileHandler.GetFile();
            if (fi != null)
            {
                openFileDialog.InitialDirectory = fi.DirectoryName;
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        FileInfo extFile = new FileInfo(openFileDialog.FileName);
                        if (extFile.FullName.Equals(fi.FullName, StringComparison.InvariantCultureIgnoreCase))
                        {
                            MessageBox.Show(
                                Resources.ExternalNodeReference_SelfReferenceMessage,
                                Resources.ExternalNodeReference_BrowseErrorTitle, MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                            return;
                        }
                        string relPath = fi.RelativePathTo(extFile);
                        //Model.SPSD file = EnvironmentFileHandler.LoadFile(openFileDialog.FileName);
                        //if (file != null)
                        //{
                        if (LoadExternalNodeIds(relPath))
                        {
                            textBox_ExtNodeFile.Text = relPath;
                            EnvironmentFileHandler.MakeSingletonDirty();
                        }
                        //}
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(
                            ex.Message,
                            Resources.ExternalNodeReference_BrowseErrorTitle, MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                        return;
                    }
                }
            }
        }

        private FileInfo GetExternalFile(string relFilePath)
        {
            if (string.IsNullOrEmpty(relFilePath))
            {
                MessageBox.Show(
                    string.Format("No external file is referenced in {0}.", _controlLocationName)
                    , Resources.ExternalNodeReference_ExternalFileNotFoundTitle, MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return null;
            }
            FileInfo fi = EnvironmentFileHandler.GetFile();
            if (fi != null)
            {
                FileInfo extFile = new FileInfo(Path.Combine(fi.DirectoryName, relFilePath));
                if (extFile.Exists)
                {
                    return extFile;
                }
                MessageBox.Show(
                    string.Format("The external file {1} referenced in {0} does not exist.", _controlLocationName,
                                  extFile.FullName)
                    , Resources.ExternalNodeReference_ExternalFileNotFoundTitle, MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }

            return null;
        }

        private bool LoadExternalNodeIds(string relPath)
        {
            if (string.IsNullOrEmpty(XPath))
            {
                throw new ArgumentException(string.Format("The XPath of the node in {0} is not specified.",
                                                          _controlLocationName));
            }

            FileInfo extFile = GetExternalFile(relPath);
            if (extFile == null)
            {
                return false;
            }


            XPathDocument doc = new XPathDocument(extFile.FullName);
            XPathNavigator nav = doc.CreateNavigator();
            Debug.Assert(nav.NameTable != null, "XPathNavigator.NameTable != null");
            XmlNamespaceManager ns = new XmlNamespaceManager(nav.NameTable);
            XPathNodeIterator iterator = nav.Select(XPath);
            comboBox_ExtNodeId.Items.Clear();
            while (iterator.MoveNext())
            {
                comboBox_ExtNodeId.Items.Add(iterator.Current.GetAttribute("ID", ns.DefaultNamespace));
            }
            if (comboBox_ExtNodeId.Items.Count == 0)
            {
                MessageBox.Show(
                    string.Format(
                        "The external file {1} referenced in the {0} does not contain any matching xml node the XPath '{2}'.",
                        _controlLocationName,
                        extFile.FullName, XPath)
                    , "No matching node found in file", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            return true;
        }
    }
}