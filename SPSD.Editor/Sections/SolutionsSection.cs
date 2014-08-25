#region

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using SPSD.Editor.Constants;
using SPSD.Editor.Interfaces;
using SPSD.Editor.Model;
using SPSD.Editor.Utilities;

#endregion

namespace SPSD.Editor.Sections
{
    public partial class SolutionsSection : UserControl, IFileHandler<SPSDEnvironmentSolutions>
    {
        public SolutionsSection()
        {
            InitializeComponent();
            solutionsGrid.Enabled = radioButton_specifiedSolutions.Checked;
        }


        public void LoadEnv(SPSDEnvironmentSolutions node)
        {
            if (node == null)
            {
                node = new SPSDEnvironmentSolutions
                    {
                        Force = false,
                        Overwrite = false,
                    };
            }
            checkBox_ForceSolutionDeployment.Checked = node.Force;
            checkBox_OverwriteExistingSolutions.Checked = node.Overwrite;

            radioButton_specifiedSolutions.Checked = node.Solution != null && node.Solution.Any();
            radioButton_allSolutions.Checked = !radioButton_specifiedSolutions.Checked;

            solutionsGrid.Clear();
            if (radioButton_specifiedSolutions.Checked)
            {
                Debug.Assert(node.Solution != null, "node.Solution != null");
                foreach (Solution solution in node.Solution)
                {
                    Dictionary<string, object> row = new Dictionary<string, object>();

                    row.Add("SolutionName", solution.Name);
                    row.Add("SolutionForce", solution.Force);
                    row.Add("SolutionOverwrite", solution.Overwrite);
                    if (solution.WebApplication != null)
                    {
                        row.Add("SolutionType", SolutionTypes.WebApplication);
                        string[] urls = (from webapp in solution.WebApplication
                                         where !string.IsNullOrEmpty(webapp.Url.Trim())
                                         select webapp.Url.Trim()).ToArray<string>();
                        row.Add("SolutionUrls", string.Join(";", urls));
                    }
                    else if (solution.SiteCollection != null)
                    {
                        row.Add("SolutionType", SolutionTypes.SiteCollection);
                        string[] urls = (from siteColl in solution.SiteCollection
                                         where !string.IsNullOrEmpty(siteColl.Url.Trim())
                                         select siteColl.Url.Trim()).ToArray<string>();
                        row.Add("SolutionUrls", string.Join(";", urls));
                    }
                    else
                    {
                        row.Add("SolutionType", SolutionTypes.GAC);
                        row.Add("SolutionUrls", "");
                    }
                    solutionsGrid.Add(row);
                }
            }
        }

        public SPSDEnvironmentSolutions SaveEnv(SPSDEnvironmentSolutions node)
        {
            if (node == null)
            {
                node = new SPSDEnvironmentSolutions();
            }
            if (radioButton_specifiedSolutions.Checked)
            {
                Dictionary<string, object>[] rows = solutionsGrid.GetRows();
                ArrayList list = new ArrayList();
                foreach (Dictionary<string, object> row in rows)
                {
                    if (row != null)
                    {
                        Solution solution = new Solution
                            {
                                Name = row["SolutionName"].ToString(),
                                Force = Convert.ToBoolean(row["SolutionForce"]),
                                ForceSpecified = true,
                                Overwrite = Convert.ToBoolean(row["SolutionOverwrite"]),
                                OverwriteSpecified = true
                            };
                        if (row["SolutionType"] != null && row["SolutionUrls"] != null)
                        {
                            string[] urls =
                                row["SolutionUrls"].ToString()
                                                   .Split(';')
                                                   .Where(url => !string.IsNullOrEmpty(url.Trim()))
                                                   .ToArray();
                            switch (row["SolutionType"].ToString())
                            {
                                case SolutionTypes.SiteCollection:
                                    solution.SiteCollection =
                                        urls.Select(u => new SiteCollection {Url = u.Trim()}).ToArray();
                                    break;
                                case SolutionTypes.WebApplication:
                                    solution.WebApplication =
                                        urls.Select(u => new WebApplication {Url = u.Trim()}).ToArray();
                                    break;
                                case SolutionTypes.GAC:
                                default:
                                    break;
                            }
                        }
                        list.Add(solution);
                    }
                }
                node.Solution = list.Cast<Solution>().ToArray<Solution>();
                node.ForceSpecified = false;
                node.OverwriteSpecified = false;
            }
            else
            {
                node.Force = checkBox_ForceSolutionDeployment.Checked;
                node.ForceSpecified = true;
                node.Overwrite = checkBox_OverwriteExistingSolutions.Checked;
                node.OverwriteSpecified = true;
            }
            return node;
        }

        public void SetDefault()
        {
            ((IFileHandler<SPSDEnvironmentSolutions>) this).LoadEnv(null);
        }

        private void RadioButton_AllSolutions_CheckedChanged(object sender, EventArgs e)
        {
            checkBox_ForceSolutionDeployment.Enabled = radioButton_allSolutions.Checked;
            checkBox_OverwriteExistingSolutions.Enabled = radioButton_allSolutions.Checked;
            EnvironmentFileHandler.MakeSingletonDirty();
        }

        private void RadioButton_SpecifiedSolutions_CheckedChanged(object sender, EventArgs e)
        {
            solutionsGrid.Enabled = radioButton_specifiedSolutions.Checked;
        }

        private void CheckBox_ForceSolutionDeployment_CheckedChanged(object sender, EventArgs e)
        {
            EnvironmentFileHandler.MakeSingletonDirty();
        }

        private void CheckBox_OverwriteExistingSolutions_CheckedChanged(object sender, EventArgs e)
        {
            EnvironmentFileHandler.MakeSingletonDirty();
        }
    }
}