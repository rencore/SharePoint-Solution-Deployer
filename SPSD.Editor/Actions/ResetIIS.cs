#region

using System;
using System.Windows.Forms;
using SPSD.Editor.Interfaces;
using SPSD.Editor.Model;
using SPSD.Editor.Utilities;

#endregion

namespace SPSD.Editor.Actions

{
    public partial class ResetIIS : UserControl, IFileHandler<SPSDConfigurationActionsResetIIS>
    {
        public ResetIIS()
        {
            InitializeComponent();
        }


        public void LoadEnv(SPSDConfigurationActionsResetIIS node)
        {
            if (node != null)
            {
                checkBox_Action.Checked = true; // if node is present then it is also activated
                checkBox_Action_deploy.Checked = node.AfterDeploy;
                checkBox_Action_retract.Checked = node.AfterRetract;
                checkBox_Action_update.Checked = node.AfterUpdate;
                checkBox_Action_force.Checked = node.Force;
            }
            else if (!EnvironmentFileHandler.IsSingletonNew)
            {
                // use the default values set in the control
                // but disable the action as it was not present in the xml
                checkBox_Action.Checked = false;
            }
        }

        public SPSDConfigurationActionsResetIIS SaveEnv(SPSDConfigurationActionsResetIIS node)
        {
            if (!checkBox_Action.Checked)
            {
                return null;
            }
            if (node == null)
            {
                node = new SPSDConfigurationActionsResetIIS();
            }
            node.AfterDeploy = checkBox_Action_deploy.Checked;
            node.AfterDeploySpecified = true;
            node.AfterRetract = checkBox_Action_retract.Checked;
            node.AfterRetractSpecified = true;
            node.AfterUpdate = checkBox_Action_update.Checked;
            node.AfterUpdateSpecified = true;
            node.Force = checkBox_Action_force.Checked;
            node.ForceSpecified = true;
            return node;
        }

        public void SetDefault()
        {
            checkBox_Action.Checked = true; 
            checkBox_Action_deploy.Checked = true;
            checkBox_Action_retract.Checked = true;
            checkBox_Action_update.Checked = true;
            checkBox_Action_force.Checked = false;
        }

        private void CheckBox_Action_CheckedChanged(object sender, EventArgs e)
        {
            bool enabled = ((CheckBox) sender).Checked;
            checkBox_Action_deploy.Enabled = enabled;
            checkBox_Action_force.Enabled = enabled;
            checkBox_Action_retract.Enabled = enabled;
            checkBox_Action_update.Enabled = enabled;
            EnvironmentFileHandler.MakeSingletonDirty();
        }


        private void CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            EnvironmentFileHandler.MakeSingletonDirty();
        }
    }
}