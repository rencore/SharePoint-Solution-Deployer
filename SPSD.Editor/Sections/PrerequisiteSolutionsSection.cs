#region

using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SPSD.Editor.Constants;
using SPSD.Editor.Interfaces;
using SPSD.Editor.Model;

#endregion

namespace SPSD.Editor.Sections
{
    public partial class PrerequisiteSolutionsSection : UserControl,
                                                        IFileHandler<SPSDEnvironmentPreRequisiteSolutions>
    {
        public PrerequisiteSolutionsSection()
        {
            InitializeComponent();
        }


        public void LoadEnv(SPSDEnvironmentPreRequisiteSolutions node)
        {
            if (node == null)
            {
                node = new SPSDEnvironmentPreRequisiteSolutions();
            }
            solutionsGrid.Clear();

            if (node.Solution != null && node.Solution.Any())
            {
                foreach (Solution solution in node.Solution)
                {
                    Dictionary<string, object> row = new Dictionary<string, object>();

                    row.Add("SolutionName", solution.Name);
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

        public SPSDEnvironmentPreRequisiteSolutions SaveEnv(
            SPSDEnvironmentPreRequisiteSolutions node)
        {
            if (node == null)
            {
                node = new SPSDEnvironmentPreRequisiteSolutions();
            }

            Dictionary<string, object>[] rows = solutionsGrid.GetRows();
            List<Solution> list = new List<Solution>();
            foreach (Dictionary<string, object> row in rows)
            {
                if (row != null)
                {
                    Solution solution = new Solution
                        {
                            Name = row["SolutionName"].ToString(),
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
            node.Solution = list.ToArray<Solution>();
            return node;
        }

        public void SetDefault()
        {
            solutionsGrid.Clear();
        }
    }
}