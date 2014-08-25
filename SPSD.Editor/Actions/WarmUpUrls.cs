#region

using System;
using System.Linq;
using System.Windows.Forms;
using SPSD.Editor.Dialogs;
using SPSD.Editor.Interfaces;
using SPSD.Editor.Model;
using SPSD.Editor.Utilities;

#endregion

namespace SPSD.Editor.Actions
{
    public partial class WarmUpUrls : UserControl, IFileHandler<SPSDConfigurationActionsWarmUpUrls>
    {
        private readonly EditUrls urlEditDialog = new EditUrls();

        public WarmUpUrls()
        {
            InitializeComponent();
            urlEditDialog.SaveUrls += SaveUrls_Click;
        }

        public void LoadEnv(SPSDConfigurationActionsWarmUpUrls node)
        {
            if (node != null)
            {
                checkBox_Action.Checked = true; // if node is present then it is also activated
                checkBox_Action_deploy.Checked = node.AfterDeploy;
                checkBox_Action_update.Checked = node.AfterUpdate;
                checkBox_Action_AllSites.Checked = node.AllSites;
                checkBox_Action_AllWebApps.Checked = node.AllWebApps;
                if (node.WarmUp != null && node.WarmUp.Count() > 0)
                {
                    textBox_Warmup_customUrls.Text = String.Join(";",
                                                                 (from wu in node.WarmUp
                                                                  where !string.IsNullOrEmpty(wu.Url.Trim())
                                                                  select wu.Url).ToArray());
                    checkBox_Action_CustomUrl.Checked = true;
                }
            }
            else if (!EnvironmentFileHandler.IsSingletonNew)
            {
                // use the default values set in the control
                // but disable the action as it was not present in the xml
                checkBox_Action.Checked = false;
            }
        }

        public SPSDConfigurationActionsWarmUpUrls SaveEnv(SPSDConfigurationActionsWarmUpUrls node)
        {
            if (!checkBox_Action.Checked)
            {
                return null;
            }
            if (node == null)
            {
                node = new SPSDConfigurationActionsWarmUpUrls();
            }

            // action not used for WarmupUrl
            node.AfterRetractSpecified = false;
            node.AfterDeploySpecified = true;
            node.AfterDeploy = checkBox_Action_deploy.Checked;
            node.AfterUpdateSpecified = true;
            node.AfterUpdate = checkBox_Action_update.Checked;
            node.AllSitesSpecified = true;
            node.AllSites = checkBox_Action_AllSites.Checked;
            node.AllWebAppsSpecified = true;
            node.AllWebApps = checkBox_Action_AllWebApps.Checked;
            if (checkBox_Action_CustomUrl.Checked && !string.IsNullOrEmpty(textBox_Warmup_customUrls.Text))
            {
                string[] urls = textBox_Warmup_customUrls.Text.Trim().Split(';');
                node.WarmUp = (from url in urls
                               where !string.IsNullOrEmpty(url.Trim())
                               select new SPSDConfigurationActionsWarmUpUrlsWarmUp {Url = url.Trim()}).ToArray();
                ;
            }
            return node;
        }

        public void SetDefault()
        {
            checkBox_Action.Checked = true;
            checkBox_Action_deploy.Checked = true;
            checkBox_Action_update.Checked = true;
            checkBox_Action_AllSites.Checked = false;
            checkBox_Action_AllWebApps.Checked = true;
            textBox_Warmup_customUrls.Text = "";
            checkBox_Action_CustomUrl.Checked = false;
        }

        private void SaveUrls_Click(object sender, EventArgs e)
        {
            textBox_Warmup_customUrls.Text = urlEditDialog.Urls;
            EnvironmentFileHandler.MakeSingletonDirty();
        }

        private void TextBox_Warmup_CustomUrls_EnabledChanged(object sender, EventArgs e)
        {
            button_Warmup_EditCustom.Enabled = textBox_Warmup_customUrls.Enabled;
            EnvironmentFileHandler.MakeSingletonDirty();
        }

        private void CheckBox_Warmup_CustomUrl_Changed(object sender, EventArgs e)
        {
            textBox_Warmup_customUrls.Enabled = checkBox_Action_CustomUrl.Enabled && checkBox_Action_CustomUrl.Checked;
            EnvironmentFileHandler.MakeSingletonDirty();
        }

        private void CheckBox_Warmup_CheckedChanged(object sender, EventArgs e)
        {
            bool enabled = (sender as CheckBox).Checked;
            checkBox_Action_AllSites.Enabled = enabled;
            checkBox_Action_AllWebApps.Enabled = enabled;
            checkBox_Action_CustomUrl.Enabled = enabled;
            checkBox_Action_deploy.Enabled = enabled;
            checkBox_Action_update.Enabled = enabled;
            EnvironmentFileHandler.MakeSingletonDirty();
        }


        private void CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            EnvironmentFileHandler.MakeSingletonDirty();
        }

        private void Button_Warmup_EditCustomUrls_Click(object sender, EventArgs e)
        {
            urlEditDialog.Urls = textBox_Warmup_customUrls.Text;
            urlEditDialog.ShowDialog();
        }

        private void TextBox_Warmup_CustomUrls_TextChanged(object sender, EventArgs e)
        {
            EnvironmentFileHandler.MakeSingletonDirty();
        }
    }
}