#region

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using SPSD.Editor.Interfaces;
using SPSD.Editor.Model;
using SPSD.Editor.Utilities;

#endregion

namespace SPSD.Editor.Actions
{
    public partial class RestartService : UserControl, IFileHandler<SPSDConfigurationActionsRestartService>
    {
        // 0 == default
        public enum RestartServiceActionType
        {
            Custom = 0,
            SPTimerV4 = 1,
            SPAdminV4 = 2,
            SPUserCodeV4 = 3
        };

        private static readonly Dictionary<RestartServiceActionType, string> Titles = new Dictionary
            <RestartServiceActionType, string>
            {
                {RestartServiceActionType.SPTimerV4, "Restart SharePoint Timer service"},
                {RestartServiceActionType.SPAdminV4, "Restart SharePoint Admin service"},
                {RestartServiceActionType.SPUserCodeV4, "Restart SharePoint UserCode service"},
                {RestartServiceActionType.Custom, "Restart a custom service"}
            };

        private static readonly Dictionary<RestartServiceActionType, string> ToolTips = new Dictionary
            <RestartServiceActionType, string>
            {
                {
                    RestartServiceActionType.SPTimerV4,
                    "Restart SPTimerV4 service on this/all servers with the Application role in the farm"
                },
                {RestartServiceActionType.SPAdminV4, "Restart SPAdminV4 service on this/all servers in the farm"},
                {RestartServiceActionType.SPUserCodeV4, "Restart SPUserCodeV4 service on this/all servers in the farm"},
                {RestartServiceActionType.Custom, "Restart a custom service on this/all servers in the farm"}
            };

        private static readonly Dictionary<RestartServiceActionType, string> Names = new Dictionary
            <RestartServiceActionType, string>
            {
                {RestartServiceActionType.SPTimerV4, "SPTimerV4"},
                {RestartServiceActionType.SPAdminV4, "SPAdminV4"},
                {RestartServiceActionType.SPUserCodeV4, "SPUserCodeV4"},
                {RestartServiceActionType.Custom, ""}
            };


        public RestartService()
        {
            InitializeComponent();
            SetActionStrings();
            if (ServiceType == RestartServiceActionType.Custom)
            {
                checkBox_Action.Checked = false;
            }
        }

        public string ServiceName
        {
            get
            {
                if (ServiceType == RestartServiceActionType.Custom)
                {
                    return textbox_ActionName.Text;
                }
                else return Names[ServiceType];
            }
        }

        [Category("RestartService Action")]
        public RestartServiceActionType ServiceType { get; set; }

        public void LoadEnv(SPSDConfigurationActionsRestartService node)
        {
            if (node != null)
            {
                if (ServiceType == RestartServiceActionType.Custom)
                {
                    textbox_ActionName.Text = node.Name;
                    textbox_ActionName_val.Validate();
                }
                else textbox_ActionName.Text = Names[ServiceType];
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

        SPSDConfigurationActionsRestartService IFileHandler<SPSDConfigurationActionsRestartService>.SaveEnv(
            SPSDConfigurationActionsRestartService node)
        {
            if (!checkBox_Action.Checked)
            {
                return null;
            }
            if (node == null)
            {
                node = new SPSDConfigurationActionsRestartService();
            }
            if (ServiceType == RestartServiceActionType.Custom)
            {
                node.Name = textbox_ActionName_val.GetValidatedValue();
            }
            else
            {
                node.Name = Names[ServiceType];
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
            if (ServiceType == RestartServiceActionType.Custom)
            {
                textbox_ActionName.Text = "";
                checkBox_Action.Checked = false;
            }
        }

        public static RestartServiceActionType GetServiceActionType(string name)
        {
            return Names.FirstOrDefault(n => n.Value.Equals(name, StringComparison.InvariantCultureIgnoreCase)).Key;
        }

        private void SetActionStrings()
        {
            textbox_ActionName.Visible = ServiceType == RestartServiceActionType.Custom;
            label_ActionName.Visible = !textbox_ActionName.Visible;

            checkBox_Action.Text = Titles[ServiceType];
            toolTip.SetToolTip(checkBox_Action, ToolTips[ServiceType]);
            label_ActionName.Text = Names[ServiceType];
            toolTip.SetToolTip(label_ActionName, ToolTips[ServiceType]);
        }

        private void checkBox_Action_CheckedChanged(object sender, EventArgs e)
        {
            bool enabled = (sender as CheckBox).Checked;
            checkBox_Action_deploy.Enabled = enabled;
            checkBox_Action_force.Enabled = enabled;
            checkBox_Action_retract.Enabled = enabled;
            checkBox_Action_update.Enabled = enabled;
            textbox_ActionName.Enabled = enabled;
            textbox_ActionName_val.Validate();
            EnvironmentFileHandler.MakeSingletonDirty();
        }

        private void textbox_ActionName_TextChanged(object sender, EventArgs e)
        {
            EnvironmentFileHandler.MakeSingletonDirty();
        }

        private void checkBox_CheckedChanged(object sender, EventArgs e)
        {
            EnvironmentFileHandler.MakeSingletonDirty();
        }

        private void RestartService_Load(object sender, EventArgs e)
        {
            SetActionStrings();
        }
    }
}