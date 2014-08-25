#region

using System;
using System.Windows.Forms;
using SPSD.Editor.Interfaces;
using SPSD.Editor.Model;
using SPSD.Editor.Utilities;

#endregion

namespace SPSD.Editor.Actions
{
    public partial class RecycleAppPools : UserControl, IFileHandler<SPSDConfigurationActionsRecycleAppPools>
    {
        public RecycleAppPools()
        {
            InitializeComponent();
        }

        public void LoadEnv(SPSDConfigurationActionsRecycleAppPools node)
        {
            if (node != null)
            {
                checkBox_Action.Checked = true; // if node is present then it is also activated
                checkBox_Action_deploy.Checked = node.AfterDeploy;
                checkBox_Action_retract.Checked = node.AfterRetract;
                checkBox_Action_update.Checked = node.AfterUpdate;
                radioButton_Action_AllAppPool.Checked = node.All;
            }
            else if (!EnvironmentFileHandler.IsSingletonNew)
            {
                // use the default values set in the control
                // but disable the action as it was not present in the xml
                checkBox_Action.Checked = false;
            }
        }

        public SPSDConfigurationActionsRecycleAppPools SaveEnv(SPSDConfigurationActionsRecycleAppPools node)
        {
            if (!checkBox_Action.Checked)
            {
                return null;
            }
            if (node == null)
            {
                node = new SPSDConfigurationActionsRecycleAppPools();
            }
            node.AfterDeploy = checkBox_Action_deploy.Checked;
            node.AfterDeploySpecified = true;
            node.AfterRetract = checkBox_Action_retract.Checked;
            node.AfterRetractSpecified = true;
            node.AfterUpdate = checkBox_Action_update.Checked;
            node.AfterUpdateSpecified = true;
            node.All = radioButton_Action_AllAppPool.Checked;
            node.AllSpecified = true;
            return node;
        }

        public void SetDefault()
        {
            checkBox_Action.Checked = false;
            checkBox_Action_deploy.Checked = true;
            checkBox_Action_retract.Checked = true;
            checkBox_Action_update.Checked = true;
            radioButton_Action_AllAppPool.Checked = true;
        }

        private void CheckBox_RecycleAppPool_CheckedChanged(object sender, EventArgs e)
        {
            bool enabled = ((CheckBox) sender).Checked;
            checkBox_Action_deploy.Enabled = enabled;
            checkBox_Action_retract.Enabled = enabled;
            checkBox_Action_update.Enabled = enabled;
            radioButton_Action_AllAppPool.Enabled = enabled;
            radioButton_Action_SPAppPool.Enabled = enabled;
            EnvironmentFileHandler.MakeSingletonDirty();
        }

        private void CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            EnvironmentFileHandler.MakeSingletonDirty();
        }
    }
}