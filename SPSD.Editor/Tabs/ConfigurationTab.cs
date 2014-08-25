#region

using System;
using System.Diagnostics;
using System.Windows.Forms;
using SPSD.Editor.Interfaces;
using SPSD.Editor.Model;

#endregion

namespace SPSD.Editor
{
    public partial class ConfigurationTab : UserControl, IFileHandler<SPSDConfiguration>
    {
        public ConfigurationTab()
        {
            InitializeComponent();
        }

        void IFileHandler<SPSDConfiguration>.LoadEnv(SPSDConfiguration node)
        {
            if (node == null)
            {
                ((IFileHandler<IExternalizable>) externalNodeReference).LoadEnv(null);
                return;
//                node = new SPSDConfiguration {ID = "Default"};
            }
            ((IFileHandler<IExternalizable>) externalNodeReference).LoadEnv(node);
            if (!externalNodeReference.IsExternal)
            {
                ((IFileHandler<SPSDConfigurationRestrictions>) restrictionsSection).LoadEnv(node.Restrictions);
                ((IFileHandler<SPSDConfigurationActions>) actionsSection).LoadEnv(node.Actions);
                ((IFileHandler<SPSDConfigurationSettings>) settingsSection).LoadEnv(node.Settings);
            }
        }

        SPSDConfiguration IFileHandler<SPSDConfiguration>.SaveEnv(SPSDConfiguration node)
        {
            if (node == null)
            {
                node = new SPSDConfiguration();
            }
            if (!externalNodeReference.IsExternal)
            {
                node = ((IFileHandler<IExternalizable>) externalNodeReference).SaveEnv(node) as SPSDConfiguration;
                Debug.Assert(node != null, "node != null");
                node.Restrictions =
                    ((IFileHandler<SPSDConfigurationRestrictions>) restrictionsSection).SaveEnv(node.Restrictions);
                node.Actions = ((IFileHandler<SPSDConfigurationActions>) actionsSection).SaveEnv(node.Actions);
                node.Settings = ((IFileHandler<SPSDConfigurationSettings>) settingsSection).SaveEnv(node.Settings);
            }
            else
            {
                node =
                    ((IFileHandler<IExternalizable>) externalNodeReference).SaveEnv(new SPSDConfiguration()) as
                    SPSDConfiguration;
            }
            return node;
        }

        public void SetDefault()
        {
            ((IFileHandler<IExternalizable>) externalNodeReference).SetDefault();
            ((IFileHandler<SPSDConfigurationRestrictions>) restrictionsSection).SetDefault();
            ((IFileHandler<SPSDConfigurationActions>) actionsSection).SetDefault();
            ((IFileHandler<SPSDConfigurationSettings>) settingsSection).SetDefault();
        }


        private void ExternalNodeReference_IsExternalChanged(object sender, EventArgs e)
        {
            tabControl.Enabled = !externalNodeReference.IsExternal;
        }
    }
}