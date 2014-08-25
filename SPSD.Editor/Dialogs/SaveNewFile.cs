#region

using System;
using System.Windows.Forms;

#endregion

namespace SPSD.Editor.Dialogs
{
    public partial class SaveNewFile : Form
    {
        private bool _isSave;

        public SaveNewFile()
        {
            InitializeComponent();
            radioMachine.Text = string.Format("{0}.xml", Environment.GetEnvironmentVariable("COMPUTERNAME"));
            radioUser.Text = string.Format("{0}.xml", Environment.GetEnvironmentVariable("USERNAME"));
            radioDefault.Checked = true;
            _isSave = false;
        }

        public string FileName
        {
            get
            {
                return radioDefault.Checked
                           ? radioDefault.Text
                           : radioMachine.Checked
                                 ? radioMachine.Text
                                 : radioUser.Checked
                                       ? radioUser.Text
                                       : "Environment.xml";
            }
        }

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Hide();
        }

        private void button_Save_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Hide();
        }


        private void label1_Click_1(object sender, EventArgs e)
        {
        }
    }
}