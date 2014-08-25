#region

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Serialization;
using SPSD.Editor.Constants;
using SPSD.Editor.Interfaces;
using SPSD.Editor.Model;
using SPSD.Editor.Properties;
using SPSD.Editor.Utilities;

#endregion

namespace SPSD.Editor.Sections
{
    public partial class RestrictionsSection : UserControl, IFileHandler<SPSDConfigurationRestrictions>
    {
        public RestrictionsSection()
        {
            InitializeComponent();
            InitDropDowns();
        }

        public void LoadEnv(SPSDConfigurationRestrictions node)
        {
            InitDropDowns();
            if (node == null)
            {
                node = new SPSDConfigurationRestrictions
                    {
                        AllowCASPolicies = Convert.ToBoolean(checkBox_AllowCASPolicies.Tag),
                        AllowFullTrustBinDeployment = Convert.ToBoolean(checkBox_AllowTrustedBin.Tag),
                        AllowGACDeployment = Convert.ToBoolean(checkBox_AllowGACDeployment.Tag),
                        MinimalSharePointLicense = comboBox_SPLicense.Tag.ToString(),
                        MinimalSharePointVersion = comboBox_SPVersion.Tag.ToString(),
                    };
            }
            checkBox_AllowCASPolicies.Checked = node.AllowCASPolicies;
            checkBox_AllowTrustedBin.Checked = node.AllowFullTrustBinDeployment;
            checkBox_AllowGACDeployment.Checked = node.AllowGACDeployment;
            comboBox_SPLicense.SelectedItem =
                comboBox_SPLicense.Items.Cast<string>()
                                  .FirstOrDefault(i => i.Equals(node.MinimalSharePointLicense.ToString(),
                                                                StringComparison.CurrentCultureIgnoreCase));
            comboBox_SPVersion.SelectedItem =
                comboBox_SPVersion.Items.Cast<KeyValuePair<string, string>>().FirstOrDefault(k => k.Key.Equals(
                    node.MinimalSharePointVersion.ToString()));
        }

        public SPSDConfigurationRestrictions SaveEnv(
            SPSDConfigurationRestrictions node)
        {
            if (node == null)
            {
                node = new SPSDConfigurationRestrictions();
            }

            node.AllowCASPolicies = checkBox_AllowCASPolicies.Enabled &&
                                    Convert.ToBoolean(checkBox_AllowCASPolicies.Checked);
            node.AllowCASPoliciesSpecified = true;
            node.AllowFullTrustBinDeployment = checkBox_AllowTrustedBin.Enabled &&
                                               Convert.ToBoolean(checkBox_AllowTrustedBin.Checked);
            node.AllowFullTrustBinDeploymentSpecified = true;
            node.AllowGACDeployment = Convert.ToBoolean(checkBox_AllowGACDeployment.Checked);
            node.AllowGACDeploymentSpecified = true;
            node.MinimalSharePointLicense = comboBox_SPLicense.SelectedItem.ToString();
            node.MinimalSharePointVersion = ((KeyValuePair<string, string>) comboBox_SPVersion.SelectedItem).Key;
            return node;
        }

        public void SetDefault()
        {
            ((IFileHandler<SPSDConfigurationRestrictions>) this).LoadEnv(null);
        }

        private void InitDropDowns()
        {
            if (comboBox_SPLicense.Items.Count == 0 || comboBox_SPVersion.Items.Count == 0)
            {
                try
                {
                    FileInfo fi = new FileInfo(Path.Combine(Application.StartupPath, Globals.SHAREPOINT_CONFIG));
                    if (fi.Exists)
                    {
                        using (var fileStream = new FileStream(fi.FullName, FileMode.Open, FileAccess.Read))
                        {
                            var serializer = new XmlSerializer(typeof (Model.SPSD));
                            Model.SPSD config = serializer.Deserialize(fileStream) as Model.SPSD;
                            FillSPDropDowns(config);
                        }
                    }
                    else
                    {
                        using (var stringStream = new StringReader(Resources.SharePointVersions))
                        {
                            var serializer = new XmlSerializer(typeof (Model.SPSD));
                            Model.SPSD config = serializer.Deserialize(stringStream) as Model.SPSD;
                            FillSPDropDowns(config);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void FillSPDropDowns(Model.SPSD config)
        {
            if (config.SharePoint != null)
            {
                // fill licenses dropdown
                if (config.SharePoint.Licenses != null && config.SharePoint.Licenses.License.Count() > 0)
                {
                    string[] licenseTypes = (from lic in config.SharePoint.Licenses.License
                                             select lic.Type).Distinct<string>().ToArray<string>();

                    comboBox_SPLicense.Items.AddRange(licenseTypes);
                }
                else
                {
                    comboBox_SPLicense.Items.AddRange(new[] {"Foundation", "Standard", "Enterprise"});
                }
                comboBox_SPLicense.SelectedIndex = 0;


                if (config.SharePoint.Versions != null && config.SharePoint.Versions.Version.Count() > 0)
                {
                    // only show versions supported by SPSD,  >= 14
                    Dictionary<string, string> versions = config.SharePoint.Versions.Version.Where(
                        rel => new Version(rel.Number).Major >= 14)
                                                                .ToDictionary(rel => rel.Number,
                                                                              rel => string.Format("{0} - {1}",
                                                                                                   rel.Number,
                                                                                                   rel.Name),
                                                                              StringComparer.OrdinalIgnoreCase);


                    comboBox_SPVersion.DataSource = new BindingSource(versions, null);
                    comboBox_SPVersion.DisplayMember = "Value";
                    comboBox_SPVersion.ValueMember = "Key";
                }
                comboBox_SPLicense.SelectedIndex = 0;
            }
        }

        private void ComboBox_SPLicense_SelectedIndexChanged(object sender, EventArgs e)
        {
            EnvironmentFileHandler.MakeSingletonDirty();
        }

        private void ComboBox_SPVersion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox_SPVersion.Items.Count > 0 && comboBox_SPVersion.SelectedIndex != -1)
            {
                checkBox_AllowTrustedBin.Enabled =
                    new Version(((KeyValuePair<string, string>) comboBox_SPVersion.SelectedItem).Key).Major == 15;
                checkBox_AllowCASPolicies.Enabled =
                    new Version(((KeyValuePair<string, string>) comboBox_SPVersion.SelectedItem).Key).Major == 14;
            }
            EnvironmentFileHandler.MakeSingletonDirty();
        }

        private void CheckBox_AllowGacDeployment_CheckedChanged(object sender, EventArgs e)
        {
            EnvironmentFileHandler.MakeSingletonDirty();
        }

        private void CheckBox_AllowCasPolicies_CheckedChanged(object sender, EventArgs e)
        {
            EnvironmentFileHandler.MakeSingletonDirty();
        }

        private void CheckBox_AllowTrustedBin_CheckedChanged(object sender, EventArgs e)
        {
            EnvironmentFileHandler.MakeSingletonDirty();
        }
    }
}