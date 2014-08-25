#region

using System;
using System.Diagnostics;
using System.Windows.Forms;
using SPSD.Editor.Interfaces;
using SPSD.Editor.Model;

#endregion

namespace SPSD.Editor.Tabs
{
    public partial class SiteStructuresTab : UserControl, IFileHandler<SPSDSiteStructures>
    {
        public SiteStructuresTab()
        {
            InitializeComponent();
        }

        void IFileHandler<SPSDSiteStructures>.LoadEnv(SPSDSiteStructures node)
        {
            if (node == null)
            {
                //node = new SPSDSiteStructures {ID = "Default"};
                ((IFileHandler<IExternalizable>) externalNodeReference).LoadEnv(null);
                return;
            }
            // needs to be reset even if externalized
            ((IFileHandler<IExternalizable>) externalNodeReference).LoadEnv(node);
        }

        SPSDSiteStructures IFileHandler<SPSDSiteStructures>.SaveEnv(SPSDSiteStructures node)
        {
            if (node == null)
            {
                node = new SPSDSiteStructures {ID = "Default"};
            }

            if (!externalNodeReference.IsExternal)
            {
                node = ((IFileHandler<IExternalizable>) externalNodeReference).SaveEnv(node) as SPSDSiteStructures;
                Debug.Assert(node != null, "node != null");
            }
            else
            {
                node =
                    ((IFileHandler<IExternalizable>) externalNodeReference).SaveEnv(new SPSDSiteStructures()) as
                    SPSDSiteStructures;
            }
            return node;
        }

        public void SetDefault()
        {
            ((IFileHandler<IExternalizable>) externalNodeReference).SetDefault();
        }

        private void ExternalNodeReference_IsExternalChanged(object sender, EventArgs e)
        {
            siteStructuresSection.Enabled = !externalNodeReference.IsExternal;
        }
    }
}