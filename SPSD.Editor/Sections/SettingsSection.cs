#region

using System;
using System.Windows.Forms;
using SPSD.Editor.Interfaces;
using SPSD.Editor.Model;
using SPSD.Editor.Utilities;

#endregion

namespace SPSD.Editor.Sections
{
    public partial class SettingsSection : UserControl, IFileHandler<SPSDConfigurationSettings>
    {
        private const string Sectionname = "settings section";

        public SettingsSection()
        {
            InitializeComponent();
            comboBox_RunOnMultipleServersInFarm.SelectedIndex = 0;
        }

        public void LoadEnv(SPSDConfigurationSettings node)
        {
            if (node == null)
            {
                node = new SPSDConfigurationSettings
                    {
                        CreateULSLogfile = Convert.ToBoolean(checkBox_CreateULSLog.Tag),
                        DisplayWizards = Convert.ToBoolean(checkBox_CreateULSLog.Tag),
                        RunOnMultipleServersInFarm = checkBox_RunOnMultipleServersInFarm.Tag.ToString(),
                        DeploymentRetries = Convert.ToInt32(textBox_DeployRetries.Tag),
                        DeploymentTimeout = Convert.ToInt32(textBox_DeployTimeout.Tag),
                        WaitAfterDeployment = textBox_WaitAfterDeploy.Tag.ToString()
                    };
            }
            checkBox_CreateULSLog.Checked = node.CreateULSLogfile;
            checkBox_CreateULSLog.Checked = node.DisplayWizards;
            checkBox_RunOnMultipleServersInFarm.Checked =
                node.RunOnMultipleServersInFarm != null &&
                !node.RunOnMultipleServersInFarm.Equals("OnlyLocal", StringComparison.InvariantCultureIgnoreCase);
            if (checkBox_RunOnMultipleServersInFarm.Checked)
            {
                comboBox_RunOnMultipleServersInFarm.SelectedIndex =
                    comboBox_RunOnMultipleServersInFarm.Items.IndexOf(node.RunOnMultipleServersInFarm);
            }
            else
            {
                comboBox_RunOnMultipleServersInFarm.SelectedIndex = 0;
            }
            textBox_DeployRetries.Text = node.DeploymentRetries.ToString();
            textBox_DeployRetries_val.Validate();
            textBox_DeployTimeout.Text = node.DeploymentTimeout.ToString();
            textBox_DeployTimeout_val.Validate();
            string waitAfterDeploy = node.WaitAfterDeployment;
            if (node.WaitAfterDeployment.Equals("pause", StringComparison.InvariantCultureIgnoreCase))
            {
                textBox_WaitAfterDeploy.Text = textBox_WaitAfterDeploy.Tag.ToString();
                radioButton_PauseAfterDeployment.Checked = true;
            }
            else
            {
                textBox_WaitAfterDeploy.Text = waitAfterDeploy;
                textBox_WaitAfterDeploy_val.Validate();
            }
        }

        public SPSDConfigurationSettings SaveEnv(SPSDConfigurationSettings node)
        {
            if (node == null)
            {
                node = new SPSDConfigurationSettings();
            }
            node.CreateULSLogfile = checkBox_CreateULSLog.Checked;
            node.CreateULSLogfileSpecified = true;
            node.DisplayWizards = checkBox_CreateULSLog.Checked;
            node.DisplayWizardsSpecified = true;
            node.RunOnMultipleServersInFarm = checkBox_RunOnMultipleServersInFarm.Checked
                                                  ? comboBox_RunOnMultipleServersInFarm.SelectedItem.ToString()
                                                  : "OnlyLocal";
            node.DeploymentRetries = Convert.ToInt32(textBox_DeployRetries_val.GetValidatedValue());
            node.DeploymentRetriesSpecified = true;
            node.DeploymentTimeout = Convert.ToInt32(textBox_DeployTimeout_val.GetValidatedValue());
            node.DeploymentTimeoutSpecified = true;
            node.WaitAfterDeployment = radioButton_PauseAfterDeployment.Checked
                                           ? "pause"
                                           : textBox_WaitAfterDeploy_val.GetValidatedValue();
            return node;
        }

        public void SetDefault()
        {
            ((IFileHandler<SPSDConfigurationSettings>) this).LoadEnv(null);
        }

        private void RadioButton_WaitAfterDeployment_CheckedChanged(object sender, EventArgs e)
        {
            textBox_WaitAfterDeploy.Enabled = radioButton_WaitAfterDeployment.Checked;
            EnvironmentFileHandler.MakeSingletonDirty();
        }

        private void CheckBox_RunOnMultipleServersInFarm_CheckedChanged(object sender, EventArgs e)
        {
            comboBox_RunOnMultipleServersInFarm.Enabled = checkBox_RunOnMultipleServersInFarm.Checked;
            EnvironmentFileHandler.MakeSingletonDirty();
        }

        private void Control_Changed(object sender, EventArgs e)
        {
            EnvironmentFileHandler.MakeSingletonDirty();
        }
    }
}