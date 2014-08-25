#region

using System.Windows.Forms;
using SPSD.Editor.Interfaces;
using SPSD.Editor.Model;

#endregion

namespace SPSD.Editor.Sections
{
    public partial class SiteStructuresSection : UserControl, IFileHandler<SPSDSiteStructures>
    {
        public SiteStructuresSection()
        {
            InitializeComponent();
        }

        public void LoadEnv(SPSDSiteStructures node)
        {
        }

        public SPSDSiteStructures SaveEnv(SPSDSiteStructures node)
        {
            if (node == null)
            {
                node = new SPSDSiteStructures();
            }

            return node;
        }

        public void SetDefault()
        {
        }
    }
}