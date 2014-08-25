#region

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SPSD.Editor.Dialogs;
using SPSD.Editor.Utilities;

#endregion

namespace SPSD.Editor.Controls
{
    public partial class SolutionsGrid : UserControl
    {
        private readonly EditUrls urlEditDialog = new EditUrls();

        public SolutionsGrid()
        {
            InitializeComponent();
            urlEditDialog.SaveUrls += SaveUrls_Click;
        }

        [Category("Solution Grid")]
        public bool SolutionOverwriteEnabled
        {
            get { return dataGrid_Solutions.Columns["SolutionOverwrite"].Visible; }
            set { dataGrid_Solutions.Columns["SolutionOverwrite"].Visible = value; }
        }

        [Category("Solution Grid")]
        public bool SolutionForceEnabled
        {
            get { return dataGrid_Solutions.Columns["SolutionForce"].Visible; }
            set { dataGrid_Solutions.Columns["SolutionForce"].Visible = value; }
        }

        private void dataGrid_Solutions_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dg = (DataGridView) sender;
            if (dg.Columns[e.ColumnIndex].Name == "CellMenu")
            {
                Point location = dg.PointToScreen(Point.Empty);
                location.Y += dg.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true).Top;
                location.X += dg.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true).Left;

                //bool inRow = dataGrid_Solutions.GetChildAtPoint(savedClickLocation) is DataGridViewRow;
                DataGridViewRow row = Clipboard.GetData("SolutionRow") as DataGridViewRow;
                pasteRowToolStripMenuItem.Enabled = row != null;
                editUrlsToolStripMenuItem.Enabled = e.RowIndex != -1 &&
                                                    !dg.Rows[e.RowIndex].Cells["SolutionUrls"].ReadOnly;
                copyRowToolStripMenuItem.Enabled = e.RowIndex != -1; //inRow;
                // if disabled because of "isNewRow" then leave it that way
                //deleteRowToolStripMenuItem.Enabled = deleteRowToolStripMenuItem.Enabled && true; //inRow;

                ShowContextMenu(dg, !dg.Rows[e.RowIndex].IsNewRow, location);
            }
        }

        private void ShowContextMenu(DataGridView dg, bool allowDelete, Point location)
        {
            deleteRowToolStripMenuItem.Enabled = allowDelete;
            contextMenu_GridView.Show(new Button(), location);
        }


        private void copyRowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = (DataGridViewRow) dataGrid_Solutions.CurrentRow.Clone();
            Clipboard.SetData("SolutionRow", row);
        }

        private void pasteRowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = Clipboard.GetData("SolutionRow") as DataGridViewRow;
            if (row != null)
            {
                dataGrid_Solutions.Rows.Add(row);
            }
        }

        private void deleteRowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGrid_Solutions.Rows.Remove(dataGrid_Solutions.CurrentRow);
            if (dataGrid_Solutions.Rows.Count == 0)
            {
                dataGrid_Solutions.Rows.Add();
            }
            EnvironmentFileHandler.MakeSingletonDirty();
        }


        private void editUrlsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            urlEditDialog.Urls = dataGrid_Solutions.CurrentRow.Cells["SolutionUrls"].Value as string;
            urlEditDialog.ShowDialog();
        }

        private void SaveUrls_Click(object sender, EventArgs e)
        {
            dataGrid_Solutions.CurrentRow.Cells["SolutionUrls"].Value = urlEditDialog.Urls;
            EnvironmentFileHandler.MakeSingletonDirty();
        }

        private void dataGrid_Solutions_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            SetAvailabilityOfUrlField(e.RowIndex);
        }

        private void SetAvailabilityOfUrlField(int rowIndex)
        {
            if (rowIndex < 0 || dataGrid_Solutions.Rows[rowIndex].IsNewRow || !dataGrid_Solutions.Enabled)
            {
                return;
            }
            DataGridViewRow row = dataGrid_Solutions.Rows[rowIndex];
            DataGridViewCell urlCell = row.Cells["SolutionUrls"];
            DataGridViewCell typeCell = row.Cells["SolutionType"];

            if (typeCell.Value != null
                && typeCell.Value.ToString().Equals("GAC"))
            {
                urlCell.SetCellEnabledState(false);
                urlCell.ToolTipText = "This field is only available for non-GAC solutions";
            }
            else
            {
                urlCell.SetCellEnabledState(true);
                urlCell.ToolTipText = "Semicolon separated URLs (can include variables)";
            }
        }

        internal void Clear()
        {
            dataGrid_Solutions.Rows.Clear();
        }

        internal void Add(Dictionary<string, object> row)
        {
            if (row == null)
            {
                return;
            }
            int index = dataGrid_Solutions.Rows.Add();
            DataGridViewCellCollection cells = dataGrid_Solutions.Rows[index].Cells;
            foreach (string key in row.Keys)
            {
                cells[key].Value = row[key];
            }
        }

        internal Dictionary<string, object>[] GetRows()
        {
            ArrayList list = new ArrayList();
            if (dataGrid_Solutions.Enabled)
            {
                foreach (DataGridViewRow dgRow in dataGrid_Solutions.Rows)
                {
                    if (dgRow.IsNewRow)
                    {
                        continue;
                    }
                    Dictionary<string, object> row = new Dictionary<string, object>();
                    DataGridViewCellCollection cells = dgRow.Cells;
                    if (!string.IsNullOrEmpty(cells["SolutionName"].ErrorText) ||
                        !string.IsNullOrEmpty(cells["SolutionType"].ErrorText) ||
                        !string.IsNullOrEmpty(cells["SolutionUrls"].ErrorText))
                    {
                        EnvironmentFileHandler.AddNotification(
                            string.Format("The {0} has still invalid or missing values.", Tag));
                    }

                    row.Add("SolutionName", cells["SolutionName"].Value);

                    if (dataGrid_Solutions.Columns["SolutionForce"].Visible)
                    {
                        row.Add("SolutionForce", cells["SolutionForce"].Value);
                    }
                    if (dataGrid_Solutions.Columns["SolutionOverwrite"].Visible)
                    {
                        row.Add("SolutionOverwrite", cells["SolutionOverwrite"].Value);
                    }
                    row.Add("SolutionUrls", cells["SolutionUrls"].Value != null ? cells["SolutionUrls"].Value.ToString().Trim() : string.Empty);
                    row.Add("SolutionType", cells["SolutionType"].Value);
                    list.Add(row);
                }
            }
            return list.Cast<Dictionary<string, object>>().ToArray<Dictionary<string, object>>();
        }

        private void dataGrid_Solutions_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            SetAvailabilityOfUrlField(e.RowIndex);
            EnvironmentFileHandler.MakeSingletonDirty();
        }

        private void dataGrid_Solutions_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void dataGrid_Solutions_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            DataGridViewRow row = dataGrid_Solutions.Rows[e.RowIndex];

            if (row.IsNewRow)
            {
                return;
            }
            DataGridViewCell cell = row.Cells[e.ColumnIndex];
            object solName = cell == row.Cells["SolutionName"] ? e.FormattedValue : row.Cells["SolutionName"].Value;
            object solType = cell == row.Cells["SolutionType"] ? e.FormattedValue : row.Cells["SolutionType"].Value;
            object solUrl = cell == row.Cells["SolutionUrls"] ? e.FormattedValue : row.Cells["SolutionUrls"].Value;

            // validate solution name
            if (solName == null || string.IsNullOrEmpty(solName.ToString()))
            {
                row.Cells["SolutionName"].ErrorText = "This is a required field.";
            }
            else if (!solName.ToString().ToLower().Contains(".wsp"))
            {
                row.Cells["SolutionName"].ErrorText = "The solution name must end with \".wsp\"";
            }
            else
            {
                row.Cells["SolutionName"].ErrorText = "";
            }

            // validate solution type
            if (solType == null || string.IsNullOrEmpty(solType.ToString()))
            {
                row.Cells["SolutionType"].ErrorText = "This is a required field.";
            }
            else
            {
                row.Cells["SolutionType"].ErrorText = "";
            }

            // validate solution url
            if (solType != null && !solType.ToString().Equals("GAC") &&
                (solUrl == null ||
                 string.IsNullOrEmpty(solUrl.ToString())))
            {
                row.Cells["SolutionUrls"].ErrorText = "You must specify at least one URL for this type of solution.";
            }
            else
            {
                row.Cells["SolutionUrls"].ErrorText = "";
            }
        }

        private void dataGrid_Solutions_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            Drawing.DataGridViewRenderValidationError(sender, e);
        }

        private void dataGrid_Solutions_EnabledChanged(object sender, EventArgs e)
        {
            dataGrid_Solutions.SetDataGridViewEnabledState();
            foreach (DataGridViewRow row in dataGrid_Solutions.Rows)
            {
                SetAvailabilityOfUrlField(row.Index);
            }
        }
    }
}