#region

using System;
using System.Diagnostics;
using System.Windows.Forms;
using SPSD.Editor.Interfaces;
using SPSD.Editor.Model;

#endregion

namespace SPSD.Editor.Tabs
{
    public partial class EnvironmentTab : UserControl, IFileHandler<SPSDEnvironment>
    {
        public EnvironmentTab()
        {
            InitializeComponent();
        }

        void IFileHandler<SPSDEnvironment>.LoadEnv(SPSDEnvironment node)
        {
            if (node == null)
            {
                ((IFileHandler<IExternalizable>) externalNodeReference).LoadEnv(null);
                return;
                //node = new SPSDEnvironment {ID = "Default"};
            }
            ((IFileHandler<IExternalizable>) externalNodeReference).LoadEnv(node);
            if (!externalNodeReference.IsExternal)
            {
                ((IFileHandler<SPSDEnvironmentSolutions>) solutionsSection).LoadEnv(node.Solutions);
                ((IFileHandler<SPSDEnvironmentPreRequisiteSolutions>) prerequisiteSolutionsSection).LoadEnv(
                    node.PreRequisiteSolutions);
                ((IFileHandler<SPSDEnvironmentVariables>) variablesSection).LoadEnv(node.Variables);
            }
        }

        SPSDEnvironment IFileHandler<SPSDEnvironment>.SaveEnv(SPSDEnvironment node)
        {
            if (node == null)
            {
                node = new SPSDEnvironment();
            }
            if (!externalNodeReference.IsExternal)
            {
                node = ((IFileHandler<IExternalizable>) externalNodeReference).SaveEnv(node) as SPSDEnvironment;
                Debug.Assert(node != null, "node != null");
                node.Solutions = ((IFileHandler<SPSDEnvironmentSolutions>) solutionsSection).SaveEnv(node.Solutions);
                node.PreRequisiteSolutions =
                    ((IFileHandler<SPSDEnvironmentPreRequisiteSolutions>) prerequisiteSolutionsSection).SaveEnv(
                        node.PreRequisiteSolutions);
                node.Variables = ((IFileHandler<SPSDEnvironmentVariables>) variablesSection).SaveEnv(node.Variables);
            }
            else
            {
                node =
                    ((IFileHandler<IExternalizable>) externalNodeReference).SaveEnv(new SPSDEnvironment()) as
                    SPSDEnvironment;
            }

            return node;
        }

        public void SetDefault()
        {
            ((IFileHandler<SPSDEnvironmentSolutions>) solutionsSection).SetDefault();
            ((IFileHandler<SPSDEnvironmentPreRequisiteSolutions>) prerequisiteSolutionsSection).SetDefault();
            ((IFileHandler<SPSDEnvironmentVariables>) variablesSection).SetDefault();
            ((IFileHandler<IExternalizable>) externalNodeReference).SetDefault();
        }

        private void ExternalNodeReference_IsExternalChanged(object sender, EventArgs e)
        {
            tabControl.Enabled = !externalNodeReference.IsExternal;
        }
    }
}