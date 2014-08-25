#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SPSD.Editor.Actions;
using SPSD.Editor.Interfaces;
using SPSD.Editor.Model;

#endregion

namespace SPSD.Editor.Sections
{
    public partial class ActionsSection : UserControl, IFileHandler<SPSDConfigurationActions>
    {
        public ActionsSection()
        {
            InitializeComponent();
        }

        public void LoadEnv(SPSDConfigurationActions node)
        {
            if (node == null)
            {
                node = new SPSDConfigurationActions();
            }
            InheritActionTargetsFromParentNode(node, node.RecycleAppPools);
            InheritActionTargetsFromParentNode(node, node.ResetIIS);
            InheritActionTargetsFromParentNode(node, node.WarmUpUrls);

            ((IFileHandler<SPSDConfigurationActionsRecycleAppPools>) ActionRecycleAppPools).LoadEnv(
                node.RecycleAppPools);
            ((IFileHandler<SPSDConfigurationActionsResetIIS>) ActionResetIIS).LoadEnv(node.ResetIIS);
            ((IFileHandler<SPSDConfigurationActionsWarmUpUrls>) ActionWarmUpUrls).LoadEnv(node.WarmUpUrls);


            if (node.RestartService != null)
            {
                bool customServiceIsSet = false;
                foreach (SPSDConfigurationActionsRestartService service in node.RestartService)
                {
                    if (service == null || string.IsNullOrEmpty(service.Name))
                    {
                        continue;
                    }
                    InheritActionTargetsFromParentNode(node, service);

                    switch (RestartService.GetServiceActionType(service.Name))
                    {
                        case RestartService.RestartServiceActionType.SPAdminV4:
                            ((IFileHandler<SPSDConfigurationActionsRestartService>) ActionSPAdminV4).LoadEnv(service);
                            break;
                        case RestartService.RestartServiceActionType.SPTimerV4:
                            ((IFileHandler<SPSDConfigurationActionsRestartService>) ActionSPTimerV4).LoadEnv(service);
                            break;
                        case RestartService.RestartServiceActionType.SPUserCodeV4:
                            ((IFileHandler<SPSDConfigurationActionsRestartService>) ActionSPUserCodeV4).LoadEnv(
                                service);
                            break;
                        default:
                            if (!customServiceIsSet)
                            {
                                ((IFileHandler<SPSDConfigurationActionsRestartService>) ActionCustom).LoadEnv(
                                    service);
                                customServiceIsSet = true;
                            }
                            else
                            {
                                MessageBox.Show(
                                    string.Format(
                                        "Your environment file contains more than one custom RestartService action which is unsupported by the editor. You will not be able to configure the action of the service \"{0}\" but your current settings will be retained even after modifiying this file.",
                                        service.Name), "Restart service action cannot be loaded", MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                            }
                            break;
                    }
                }
            }
        }

        public SPSDConfigurationActions SaveEnv(SPSDConfigurationActions node)
        {
            if (node == null)
            {
                node = new SPSDConfigurationActions();
            }
            node.RecycleAppPools =
                ((IFileHandler<SPSDConfigurationActionsRecycleAppPools>) ActionRecycleAppPools).SaveEnv(null);
            node.ResetIIS = ((IFileHandler<SPSDConfigurationActionsResetIIS>) ActionResetIIS).SaveEnv(null);
            node.WarmUpUrls = ((IFileHandler<SPSDConfigurationActionsWarmUpUrls>) ActionWarmUpUrls).SaveEnv(null);

            // cannot just overwrite as there might be some custom services which may not have been shown in the editor 
            // and need to be preserved
            List<SPSDConfigurationActionsRestartService> services = null;
            if (node.RestartService != null)
            {
                // get all valid custom services which were not shown in the editor
                services = node.RestartService.Where(s =>
                                                     !string.IsNullOrEmpty(s.Name) &&
                                                     RestartService.GetServiceActionType(s.Name) ==
                                                     RestartService.RestartServiceActionType.Custom &&
                                                     !s.Name.Equals(ActionCustom.ServiceName,
                                                                    StringComparison.InvariantCultureIgnoreCase))
                               .ToList();
            }
            else
            {
                services = new List<SPSDConfigurationActionsRestartService>();
            }
            services.Add(((IFileHandler<SPSDConfigurationActionsRestartService>) ActionSPAdminV4).SaveEnv(null));
            services.Add(((IFileHandler<SPSDConfigurationActionsRestartService>) ActionSPTimerV4).SaveEnv(null));
            services.Add(((IFileHandler<SPSDConfigurationActionsRestartService>) ActionSPUserCodeV4).SaveEnv(null));
            services.Add(((IFileHandler<SPSDConfigurationActionsRestartService>) ActionCustom).SaveEnv(null));

            // purge also null services
            node.RestartService = services.Where(s => s != null).ToArray();
            return node;
        }

        private static void InheritActionTargetsFromParentNode(SPSDConfigurationActions node, IAction action)
        {
            if (action == null)
            {
                return;
            }
            if (node.AfterDeploySpecified && !action.AfterDeploySpecified)
            {
                action.AfterDeploySpecified = true;
                action.AfterDeploy = node.AfterDeploy;
            }
            if (node.AfterRetractSpecified && !action.AfterRetractSpecified)
            {
                action.AfterRetractSpecified = true;
                action.AfterRetract = node.AfterRetract;
            }
            if (node.AfterUpdateSpecified && !action.AfterUpdateSpecified)
            {
                action.AfterUpdateSpecified = true;
                action.AfterUpdate = node.AfterUpdate;
            }
        }

        public void SetDefault()
        {
            ((IFileHandler<SPSDConfigurationActionsRecycleAppPools>)ActionRecycleAppPools).SetDefault();
            ((IFileHandler<SPSDConfigurationActionsResetIIS>)ActionResetIIS).SetDefault();
            ((IFileHandler<SPSDConfigurationActionsWarmUpUrls>)ActionWarmUpUrls).SetDefault();
            ((IFileHandler<SPSDConfigurationActionsRestartService>) ActionSPAdminV4).SetDefault();
            ((IFileHandler<SPSDConfigurationActionsRestartService>) ActionSPTimerV4).SetDefault();
            ((IFileHandler<SPSDConfigurationActionsRestartService>) ActionSPUserCodeV4).SetDefault();
            ((IFileHandler<SPSDConfigurationActionsRestartService>) ActionCustom).SetDefault();
        }
    }
}