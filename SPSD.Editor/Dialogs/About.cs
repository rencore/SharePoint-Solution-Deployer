#region

using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;
using SPSD.Editor.Properties;

#endregion

namespace SPSD.Editor.Dialogs
{
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();
            spsd_disclaimer.Rtf = Resources.AboutText;
            Assembly assembly = Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            label_version.Text = "v" + fvi.FileVersion;
        }

        private void Button_Close_Click(object sender, EventArgs e)
        {
            Hide();
        }
    }
}